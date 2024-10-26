using System;
using System.Collections.Generic;
using System.Threading;
using System.Media;
using System.Windows.Media;
using TeamWork.Objects;
using System.Linq;

namespace TeamWork.Field
{
    public class Engine
    {
        public static Random Rnd = new Random();
        public static Player Player = new Player();
        public bool DrawMenu = false;
        public const int StartingDifficulty = 25;
        public Thread MusicThread; // Luồng nhạc nền
        public Thread EffectsThread; // Luồng hiệu ứng âm thanh

        public const int WindowWidth = 115; //Hằng số Chiều Rộng Cửa Sổ để truy cập từ mọi nơi
        public const int WindowHeight = 32; //Hằng số Chiều Cao Cửa Sổ để truy cập từ mọi nơi


        /// <summary>
        /// Hàm khởi tạo khởi động động cơ ngay lập tức
        /// </summary>
        public Engine()
        {
            this.Start();
        }

        /// <summary>
        /// Phương thức chơi game, chứa vòng lặp chính của trò chơi và tất cả các cuộc gọi cập nhật
        /// </summary>
        public void Start()
        {

            LoadGameLogoMusic();
            Menu.StartLogo();
            LoadMenuMusic();
            Menu.StartMenu();
            EffectsThread = new Thread(SoundEffects);
            EffectsThread.Start();
            Menu.EntryStoryLine(); // Vẽ ra câu chuyện ngắn
            Printing.EnterName(); // vẽ nơi nhập tên
            TakeName(); // Nhận tên người chơi
            MusicThread = new Thread(LoadMusic);
           MusicThread.Start();
            Thread.Sleep(1000); // Dừng kịch tính
            while (true) //Vòng lặp chính của trò chơi
            {
                Console.Clear();
                Player.Print(); // Hiển thị người chơi ở vị trí bắt đầu
                Menu.Table(); // In bảng UI
                Menu.UIDescription(); // Xuất mô tả giao diện người dùng

                while (Player.Lifes > 0) // Vòng lặp chính của trò chơi kết thúc khi người chơi hết mạng
                {
                    //lắng nghe nhấn từ bàn phím
                    if (Console.KeyAvailable) // Kiểm tra xem bộ đệm bàn phím có phím nhấn nào không
                    {
                        this.TakeInput(Console.ReadKey(true)); // Truyền phím đó đến phương thức Xử Lý Nhập Liệu
                        while (Console.KeyAvailable) // Xóa bỏ phần còn lại của các phím đã được nhấn
                        {
                            Console.ReadKey(true);
                        }
                    }

                    UpdateAndRender(); // Cập nhật tất cả các đối tượng và vẽ lại mọi thứ
                    Thread.Sleep(80); //Tốc độ trò chơi không đổi
                }
                LoadDeathMusic();
                Console.Clear();
                //Thêm điểm số mới 
                Menu.SetHighscore();
                Printing.GameOver();
                ResetGame();
            }
        }

        public static bool BossActive = false;
        public static Boss boss = new Boss(0); // Đối tượng boss tĩnh, có thể được tái tạo nếu cần

        /// <summary>
        /// Phương thức chính gọi tất cả các tính toán và lệnh vẽ khác
        /// </summary>
        private void UpdateAndRender()
        {
            if (Player.Level % 3 == 2 && BossActive == false) // When to spawn a boss        
            {
                BossActive = true;

                if (boss.BossLife <= 0)
                {
                    boss = new Boss(0);
                }
            }

            ProjectileMoveAndPrint(); // Di chuyển và hiển thị đạn (thiên thạch, đạn của kẻ địch)
            ProjectileCollisionCheck(); // Kiểm tra va chạm
            ShipCollisionBoss(boss.Point.X, boss.Point.Y);
            if (BossActive) //Nếu boss đang hoạt động, gọi phương thức AI của nó
            {
                DrawAndMoveMeteor();
                boss.BossAI();
                foreach (var bullets in _bullets) // Kiểm tra xem con boss bị đánh trúng chưa
                {
                    if (boss.BossHit(bullets.Point))
                    {
                        bullets.ClearObject(); // Xóa đạn
                        bullets.Point.X += 200; // Di chuyển nó ra khỏi màn hình để xóa
                    }
                }
            }
            else
            {
                DrawAndMoveMeteor();
                GenerateMeteorit(); //Sinh ra thiên thạch
            }
        }
        /// <summary>
        /// Đặt lại tất cả các giá trị khởi đầu về mặc định và xóa tất cả các bộ sưu tập đối tượng
        /// </summary>
        private void ResetGame()
        {
            Player.Level = 1;
            Player.Score = 0;
            Player.Lifes = 10;
            Player.Point = Player.PlayerPoint;
            BossActive = false;
            boss = new Boss(0);
            _bullets.Clear();
            _objectProjectiles.Clear();
            _meteorits.Clear();
            MusicThread = new Thread(LoadMusic);
            MusicThread.Start();
        }

        /// <summary>
        /// Xử lý di chuyển của người chơi
        /// </summary>
        /// <param name="keyPressed"></param>
        private void TakeInput(ConsoleKeyInfo keyPressed)
        {
            switch (keyPressed.Key)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    Player.MoveUp();
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    Player.MoveDown();
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    Player.MoveLeft();
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    Player.MoveRight();
                    break;
                // Tạo một đối tượng đạn mới
                case ConsoleKey.Spacebar:
                    // Thêm một GameObject vào danh sách đạn với vị trí bắt đầu là mũi máy bay của người chơi với loại đạn
                    _bullets.Add(new GameObject(new Point2D(Player.Point.X + 3, Player.Point.Y - 2), 0));
                    _playEffect = true; // Phát âm thanh bắn của người chơi
                    break;
            }
        }

        #region Projectiles

        public static List<GameObject> _objectProjectiles = new List<GameObject>(); // Stores all projectiles
        private List<GameObject> _bullets = new List<GameObject>(); // Stores all bullets fired
        private void ProjectileMoveAndPrint()
        {
            List<GameObject> newProjectiles = new List<GameObject>(); //Stores the new coordinates of the projectiles
            List<GameObject> newBullets = new List<GameObject>(); //Stores the new coordinates of the bullets
            for (int i = 0; i < _objectProjectiles.Count; i++) // Cycle through all projectiles
            {
                if ( _objectProjectiles[i].Point.Y <= 30)
                {
                    _objectProjectiles[i].ClearObject();
                }
                if (_objectProjectiles[i].Point.Y - _objectProjectiles[i].Speed  - 2 >= 24)
                {
                    // Nếu Đạn vượt quá kích thước màn hình, không thêm nó vào danh sách Đạn mới
                }
                else
                { 

                _objectProjectiles[i].Point.Y += _objectProjectiles[i].Speed + 2; // Đạn đi xuống
                _objectProjectiles[i].PrintObject(); //In ra đạn
                    newProjectiles.Add((_objectProjectiles[i])); // Thêm nó vào danh sách mới
                }
            }
            _objectProjectiles = newProjectiles; // Ghi đè vị trí của các đạn cũ bằng các vị trí mới


            for (int i = 0; i < _bullets.Count; i++) // Duyệt qua tất cả các viên đạn
            {
                if (_bullets[i].Point.Y > 0) // Kiểm tra xem viên đạn có nằm ngoài màn hình trước khi xóa nó
                {
                    _bullets[i].ClearObject();
                }
                // Xóa đạn tại vị trí hiện tại của nó
                if (_bullets[i].Point.Y + _bullets[i].Speed + 1 <= 3)
                {
                    // Nếu Đạn vượt quá kích thước màn hình, không thêm nó vào danh sách Đạn mới
                }
                else
                {
                    _bullets[i].Point.Y -= _bullets[i].Speed + 1; //Di chuyển viên đạn sang phải # ô
                    _bullets[i].PrintObject(); // in viên đạn ở vị trí mới của nó 
                    newBullets.Add((_bullets[i])); // hêm viên đạn đã di chuyển vào danh sách đạn mới
                }
            }
            _bullets = newBullets; // Ghi đè danh sách đạn toàn cục bằng danh sách đạn mới
        }

        #endregion

        #region Object Generator

        /// <summary>
        /// Tạo ra các đối tượng thiên thạch
        /// </summary>
        private List<GameObject> _meteorits = new List<GameObject>();
        private int _counter = 0; // Chỉ là một bộ đếm
        public static int Chance = StartingDifficulty; // Biến cơ hội 1 trên # vòng lặp sinh ra một thiên thạch
        private void GenerateMeteorit()
        {
            if (_counter % Chance == 0)
            {
                // Khi đến lúc sinh ra kẻ địch, ngẫu nhiên loại của nó
                _meteorits.Add(new GameObject(Rnd.Next(1,8)));
                _counter++;
            }
            else 
            {
                _counter++;
            }
        }


        /// <summary>
        /// In ra và di chuyển thiên thạch
        /// </summary>
        private void DrawAndMoveMeteor()
        {
            List<GameObject> newMeteorits = new List<GameObject>();
            if (_counter % 1 == 0) // Có thể được sử dụng để làm cho thiên thạch di chuyển chậm hơn
            {
                for (int i = 0; i < _meteorits.Count; i++) // Duyệt qua tất cả các thiên thạch
                {
                    _meteorits[i].ClearObject(); // Xóa thiên thạch

                    if (_meteorits[i].Point.Y - _meteorits[i].Speed >= 28 || _meteorits[i].Point.X - _meteorits[i].Speed <= 1  )
                    {
                        // Nếu thiên thạch vượt quá kích thước màn hình, không thêm nó vào danh sách thiên thạch mới
                    }

                    else
                    {
                        // Xử lý va chạm
                        if (BulletCollision(_meteorits[i]) || ShipCollision(_meteorits[i])) // Kiểm tra va chạm giữa đạn và tàu
                        {
                            // Nếu có va chạm với đạn của người chơi hoặc tàu của người chơi
                            if (--_meteorits[i].life == 0) // Kiểm tra xem mạng sống của thiên thạch đã giảm xuống 0 chưa
                            {
                                _meteorits[i].ClearObject(); // Xóa thiên thạch
                                _playMeteorEffect = true; // Phát hiệu ứng nổ thiên thạch
                                _meteorits[i].GotHit = true; // Đánh dấu thiên thạch đã bị bắn trúng
                            }

                            newMeteorits.Add((_meteorits[i])); // Thêm thiên thạch vào danh sách thiên thạch mới

                        }
                        else // Nếu không có va chạm
                        {
                            _meteorits[i].MoveObject(_meteorits[i].Down); // di chuyển thiên thạch
                            if (!_meteorits[i].toBeDeleted) // // Kiểm tra xem thiên thạch có nên bị xóa
                            {
                                _meteorits[i].PrintObject(); // // In ra thiên thạch ở vị trí mới của nó
                                newMeteorits.Add((_meteorits[i])); // Thêm nó vào danh sách mới
                            }
                        }
                    }
                }
                _meteorits = newMeteorits; // Ghi đè danh sách thiên thạch toàn cục bằng danh sách thiên thạch mới

            }
        }
        #endregion

        #region Collision Handling Methods
        /// <summary>
        /// Kiểm tra va chạm đạn
        /// </summary>
        /// <param name="obj">Đối tượng Thiên thạch</param>
        /// <returns>Nếu có đạn nào đó bắn trúng thiên thạch</returns>
        private bool BulletCollision(GameObject obj)
        {
            for (int i = 0; i < _bullets.Count; i++)
            {
                if (obj.Collided(_bullets[i].Point))
                {
                    Printing.ClearAtPosition(_bullets[i].Point);
                    _bullets.RemoveAt(i);

                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Xử lý va chạm tàu
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Nếu tàu bị thiên thạch đâm</returns>
        private static bool ShipCollision(GameObject obj)
        {
            Point2D point = Player.Point;
            if (obj.Collided(point.X + 5, point.Y) || obj.Collided(point.X + 4, point.Y - 1) ||
                obj.Collided(point.X + 6, point.Y + 1) || obj.Collided(point.X + 7, point.Y + 2) ||
                obj.Collided(point.X + 3, point.Y) || obj.Collided(point.X + 2, point.Y + 1) ||
                obj.Collided(point.X, point.Y + 2) || obj.Collided(point.X + 4, point.Y + 2))
            {
                Player.DecreaseLifes();

                Menu.Table();
                Menu.UIDescription();
                return true;
            }
            return false;
        }
        private static bool ShipCollisionBoss(int x, int y)
        {
            Point2D point = Player.Point;


            if ((point.X >= x && point.X <= x + 40) && (point.Y <= y + 2 && point.Y >= y - 11))
            {
                Player.DecreaseLifes();

                Menu.Table();
                Menu.UIDescription();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Kiểm tra va chạm đạn
        /// </summary>
        private static void ProjectileCollisionCheck()
        {
            // Lấy tất cả các viên đạn va chạm và lấy chỉ số và loại của nó
            var hits =
                _objectProjectiles.Select((x, i) => new { Value = x, Index = i })
                    .Where(x => Player.ShipCollided(x.Value.Point)).ToList(); // Gọi phương thức ToList để truy vấn được thực thi ngay lập tức

            foreach (var hit in hits) // Duyệt qua từng viên đạn va chạm
            {
                hit.Value.ClearObject(); // Xóa đối tượng
                _objectProjectiles.RemoveAt(hit.Index); // Xóa đối tượng khỏi danh sách chính dựa trên chỉ số
                Player.Lifes--; // Giảm mạng sống của người chơi (phương thức này hơi chậm và người chơi bị đạn bắn trúng hai lần, nhưng không sao :D)
                Menu.Table();
                Menu.UIDescription();

            }
        }
        #endregion

        #region Music
        private static bool _playMeteorEffect; // Kích hoạt hiệu ứng âm thanh khi thiên thạch phát nổ
        private static bool _playEffect; // Kích hoạt hiệu ứng âm thanh khi người chơi bắn
        public static bool PlayBossHit; // Kích hoạt hiệu ứng âm thanh khi đánh trúng boss

        /// <summary>
        /// Tải nhạc nền
        /// </summary>
        private static void LoadGameLogoMusic()
        {
            var sound = new SoundPlayer { SoundLocation = "Resources/Logo.wav" };
            sound.Play();
        }

        private static void LoadMenuMusic()
        {
            var sound = new SoundPlayer { SoundLocation = "Resources/GameMenu.wav" };
            sound.PlayLooping();
        }

        private static void LoadMusic()
        {
            var sound = new SoundPlayer { SoundLocation = "Resources/GamePlay.wav" };
            sound.PlayLooping();
        }

        private static void LoadDeathMusic()
        {
            var sound = new SoundPlayer { SoundLocation = "Resources/Die.wav" };
            sound.Play();
        }

        /// <summary>
        /// Phát âm thanh dựa trên các kích hoạt
        /// </summary>
        private void SoundEffects()
        {
            MediaPlayer soundFx = new MediaPlayer();
            MediaPlayer soundFx2 = new MediaPlayer();
            MediaPlayer soundFx3 = new MediaPlayer();

            while (true)
            {
                if (_playMeteorEffect) // Nếu được kích hoạt, phát hiệu ứng nổ thiên thạch
                {
                    soundFx.Open(new Uri("Resources/meteor.wav", UriKind.Relative));
                    soundFx.Volume = 60;
                    soundFx.Play();
                    _playMeteorEffect = false;
                }
                if (_playEffect) // Nếu được kích hoạt, phát hiệu ứng bắn laser
                {
                    soundFx2.Open(new Uri("Resources/laser.wav", UriKind.Relative));
                    soundFx2.Volume = 100;
                    soundFx2.Play();
                    _playEffect = false;
                }
                if (PlayBossHit) // Nếu được kích hoạt, phát hiệu ứng đánh trúng boss
                {
                    soundFx3.Open(new Uri("Resources/Meow.wav", UriKind.Relative));
                    soundFx3.Volume = 100;
                    soundFx3.Play();
                    PlayBossHit = false;
                }
                Thread.Sleep(80);
            }
        }


        #endregion
        /// <summary>
        /// Nhập tên người chơi
        /// </summary>
        private static void TakeName()
        {
            Console.WriteLine();
            Console.Write("\n\t\t\t\t Name:");
            string name = Console.ReadLine();
            if (String.IsNullOrEmpty(name) || name.Length >= 10)
            {
                // In thông báo và
                // Đặt lại console nếu tên là khoảng trắng
                Console.WriteLine("\t\t\t    Please enter your name!");
                Thread.Sleep(2000);
                Console.Clear();
                Printing.EnterName();
                TakeName();
            }
            // Đặt tên chính xác
            else
            {
                Player.setName(name);
                Console.Clear();
            }
        }

    }
}