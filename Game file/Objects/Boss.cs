using System;
using System.Collections.Generic;
using System.Windows.Media;
using TeamWork.Field;

namespace TeamWork.Objects
{
    public class Boss : Entity
    {
        public enum BossType
        {
            PurpleStorm,
        }

        public bool Movealbe = true; // Gắn thẻ để làm cho boss không di chuyển được
        public int BossLife; // Điểm sinh mệnh của boss
        private BossType bossType;

        /// <tóm tắt>
        /// Tạo một đối tượng boss từ một loại đã cho
        /// </summary>
        /// <tham số name="type">Type</param>
        public Boss(int type)
        {
            bossType = (BossType)type;
            this.Point = new Point2D(30, 14);
            switch (bossType)
            {
                case BossType.PurpleStorm:
                    BossLife = 100;
                    break;
            }
        }
        /// <summary>
        /// Vẽ nhân vật boss
        /// </summary>
        private void BossPrint()
        {

            Printing.DrawAt(this.Point.X, this.Point.Y - 10, @"                      ████", ConsoleColor.DarkMagenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 9, @"███████         ███  █    █      ███████", ConsoleColor.DarkMagenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 8, @"  █████        █   █ ████        █████  ", ConsoleColor.DarkMagenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 7, @"   ████████     ███████ █     ████████  ", ConsoleColor.Magenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 6, @"    ██████████  █ ███████ ██████████    ", ConsoleColor.Magenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 5, @"                ████████████            ", ConsoleColor.Magenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 4, @"           █          ██          █     ", ConsoleColor.Magenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 3, @"       █████          ██          █████ ", ConsoleColor.Magenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 2, @"       ██████  ███ ██████████ ███ ██████", ConsoleColor.Magenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 1, @"             █████ █ █████ ██ █████     ", ConsoleColor.Magenta);
            Printing.DrawAt(this.Point.X, this.Point.Y, @"       █████████   ████  ████   █████████", ConsoleColor.Magenta);
            Printing.DrawAt(this.Point.X, this.Point.Y + 1, @"      ████████  ██████  ██████  ████████ ", ConsoleColor.DarkMagenta);
            Printing.DrawAt(this.Point.X, this.Point.Y + 2, @"      ████          █    █          ████ ", ConsoleColor.DarkMagenta);
        }
        /// <summary>
        /// Xóa nhân vật boss 
        /// </summary>
        private void BossClear()
        {
            Printing.DrawAt(this.Point.X, this.Point.Y - 10, @"                          ", ConsoleColor.DarkMagenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 9, @"                                        ", ConsoleColor.DarkMagenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 8, @"                                        ", ConsoleColor.DarkMagenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 7, @"                                        ", ConsoleColor.Magenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 6, @"                                        ", ConsoleColor.Magenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 5, @"                                        ", ConsoleColor.Magenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 4, @"                                        ", ConsoleColor.Magenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 3, @"                                        ", ConsoleColor.Magenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 2, @"                                        ", ConsoleColor.Magenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 1, @"                                        ", ConsoleColor.Magenta);
            Printing.DrawAt(this.Point.X, this.Point.Y, @"                                         ", ConsoleColor.Magenta);
            Printing.DrawAt(this.Point.X, this.Point.Y + 1, @"                                         ", ConsoleColor.DarkMagenta);
            Printing.DrawAt(this.Point.X, this.Point.Y + 2, @"                                         ", ConsoleColor.DarkMagenta);
        }

        private List<BossObject> bossGameObjects = new List<BossObject>(); // Các đối tượng được sinh ra bởi boss
        private int _counter = 1; // Bộ đếm
        private int chance = 30; // Cơ hội để sinh ra một đối tượng 1 trong # lần
        private bool _entryAnimationPlayed = false; // Thẻ để cho biết liệu hoạt ảnh bắt đầu đã được phát hay chưa

        /// <summary>
        /// AI của Boss, tất cả chuyển động và việc sinh ra các đối tượng boss được tính toán ở đây
        /// </summary>
        public void BossAI()
        {
            if (!_entryAnimationPlayed) // Kiểm tra xem hiệu ứng đã được phát chưa
            {

                BossEntryAnimation(); // Phát nó
                _entryAnimationPlayed = true; // Và kích hoạt thẻ entryAnimation
            }
            if (this.BossLife <= 0) // Nếu boss hết mạng
            {
                Engine.BossActive = false; // Kích hoạt biến boolean boss đang hoạt động trong lớp Engine
                                           // Xóa tất cả các đối tượng boss đã sinh ra khỏi màn hình
                foreach (var bossGameObject in bossGameObjects)
                {
                    bossGameObject.ClearObjectCheckColision();
                }
                MediaPlayer death = new MediaPlayer();
                death.Open(new Uri("Resources/cat.wav", UriKind.Relative));
                death.Play();
                BossDeathAnimation(); // Phát "hoạt ảnh" boss chết
                Engine.Player.IncreasePoints(90); // Tăng điểm của người chơi lên 90
                Menu.UIDescription(); // vẽ lại nội dung UI
                return;
            }

            // Nếu đã đến lúc sinh ra một đối tượng mới
            if (_counter % chance == 0)
            {
                // Lấy một loại ngẫu nhiên và truyền nó vào switch
                int type = Engine.Rnd.Next(0, 4);
                switch (type)
                {
                    // Tạo 10 tên lửa 
                    case 0:
                        for (int i = 0; i < 3; i++)
                        {
                            bossGameObjects.Add(new BossObject(new Point2D(this.Point.X + 23 + Engine.Rnd.Next(-5, 5), this.Point.Y + 3), type));
                        }
                        break;
                    // Tạo đạn
                    case 1:
                        bossGameObjects.Add(new BossObject(new Point2D(this.Point.X + 23, this.Point.Y + 3), type));
                        break;
                    // Tạo Laser
                    case 2:
                        bossGameObjects.Add(new BossObject(new Point2D(this.Point.X + 23, this.Point.Y + 3), type));
                        break;
                    // Tạo bom
                    case 3:
                        bossGameObjects.Add(new BossObject(new Point2D(this.Point.X + 23, this.Point.Y + 3), type));
                        break;
                }
            }
            _counter++;
            // Số ngẫu nhiên để quyết định xem boss có nên di chuyển hay không
            int move = Engine.Rnd.Next(0, 100);
            if (move > 20 && move < 30 && Movealbe && this.Point.X + 1 <= Engine.WindowWidth - 40)
            {
                BossClear();
                this.Point.X += 5;
            }
            if (move > 80 && move < 90 && Movealbe && this.Point.X - 1 >= 10)
            {
                BossClear();
                this.Point.X -= 5;
            }
            // In con boss
            BossPrint();
            BossObjectsMoveAndDraw();

        }
        /// <summary>
        /// Di chuyển và vẽ các đối tượng boss
        /// </summary>
        private void BossObjectsMoveAndDraw()
        {
            List<BossObject> newObjects = new List<BossObject>(); //  Danh sách các đối tượng đã được di chuyển
            foreach (var bossGameObject in bossGameObjects) // Duyệt qua tất cả các đối tượng hiện tại
            {
                bossGameObject.ClearObjectCheckColision(); //  Xóa khỏi màn hình và kiểm tra va chạm với người chơi

                // Nếu mạng sống của đối tượng là 0 hoặc nhỏ hơn, hoặc nó nằm ngoài màn hình
                if (bossGameObject.GetLifeOnScreen() <= 0 ||
                    (bossGameObject.Point.X < 5 || bossGameObject.Point.X > Engine.WindowWidth - 5 || bossGameObject.Point.Y < 3 || bossGameObject.Point.Y >= Engine.WindowHeight - 3))
                {
                    // Không thêm nó vào danh sách với các đối tượng đã di chuyển
                }
                else
                {
                    // Di chuyển đối tượng
                    bossGameObject.MoveObject();
                    // In ra đối tượng ở vị trí mới của nó
                    bossGameObject.PrintObject();
                    // Thêm nó vào danh sách với các đối tượng đã di chuyển
                    newObjects.Add(bossGameObject);
                }
            }
            bossGameObjects = newObjects; //Ghi đè các đối tượng cũ bằng các đối tượng đã di chuyển
        }

        /// <summary>
        /// Kiểm tra va chạm với boss
        /// </summary>
        /// <tham số name="point">Point2D to check with</param>
        /// <kết quả>If the boss is hit</returns>
        public bool BossHit(Point2D point)
        {
            if (((point.X == this.Point.X + 13 || point.X == this.Point.X + 14) && point.Y == this.Point.Y - 12) ||
                ((point.X >= this.Point.X + 12 && point.X <= this.Point.X + 20) && point.Y == this.Point.Y - 11) ||
                ((point.X >= this.Point.X + 12 && point.X <= this.Point.X + 20) && point.Y == this.Point.Y - 10) ||
                ((point.X >= this.Point.X + 12 && point.X <= this.Point.X + 20) && point.Y == this.Point.Y - 9) ||
                ((point.X >= this.Point.X + 11 && point.X <= this.Point.X + 20) && point.Y == this.Point.Y - 8) ||
                ((point.X >= this.Point.X + 11 && point.X <= this.Point.X + 20) && point.Y == this.Point.Y - 7) ||
                ((point.X >= this.Point.X + 11 && point.X <= this.Point.X + 20) && point.Y == this.Point.Y - 6) ||
                ((point.X >= this.Point.X + 11 && point.X <= this.Point.X + 20) && point.Y == this.Point.Y - 5) ||
                ((point.X >= this.Point.X + 11 && point.X <= this.Point.X + 20) && point.Y == this.Point.Y - 4) ||
                ((point.X >= this.Point.X + 11 && point.X <= this.Point.X + 20) && point.Y == this.Point.Y - 3) ||
                ((point.X >= this.Point.X + 13 && point.X <= this.Point.X + 20) && point.Y == this.Point.Y - 2) ||
                ((point.X >= this.Point.X + 0 && point.X <= this.Point.X + 30) && point.Y == this.Point.Y - 1) ||
                ((point.X >= this.Point.X + 0 && point.X <= this.Point.X + 30) && point.Y == this.Point.Y - 0) ||
                ((point.X >= this.Point.X + 0 && point.X <= this.Point.X + 30) && point.Y == this.Point.Y + 1) ||
                ((point.X >= this.Point.X + 13 && point.X <= this.Point.X + 20) && point.Y == this.Point.Y + 2) ||
                ((point.X >= this.Point.X + 14 && point.X <= this.Point.X + 20) && point.Y == this.Point.Y + 3) ||
                ((point.X >= this.Point.X + 9 && point.X <= this.Point.X + 20) && point.Y == this.Point.Y + 4) ||
                ((point.X >= this.Point.X + 7 && point.X <= this.Point.X + 20) && point.Y == this.Point.Y + 5) ||
                ((point.X >= this.Point.X + 7 && point.X <= this.Point.X + 20) && point.Y == this.Point.Y + 6))
            {
                this.BossLife--; // Giảm sinh mệnh của boss
                Engine.PlayBossHit = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Hiệu ứng Boss chết
        /// </summary>
        private void BossDeathAnimation()
        {
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y - 10, @"                           ", 5, false);
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y - 9, @"                                        ", 5, false);
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y - 8, @"                                        ", 5, false);
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y - 7, @"                                        ", 5, false);
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y - 6, @"                                        ", 5, false);
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y - 5, @"                                        ", 5, false);
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y - 4, @"                                        ", 5, false);
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y - 3, @"                                        ", 5, false);
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y, @"                                         ", 5, false);
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y + 1, @"                                         ", 5, false);
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y + 2, @"                                         ", 5, false);

            Printing.DrawAt(this.Point.X, this.Point.Y - 10, @"                          ", ConsoleColor.DarkMagenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 9, @"                                        ", ConsoleColor.DarkMagenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 8, @"                                        ", ConsoleColor.DarkMagenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 7, @"                                        ", ConsoleColor.Magenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 6, @"                                        ", ConsoleColor.Magenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 5, @"                                        ", ConsoleColor.Magenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 4, @"                                        ", ConsoleColor.Magenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 3, @"                                        ", ConsoleColor.Magenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 2, @"                                        ", ConsoleColor.Magenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 1, @"                                        ", ConsoleColor.Magenta);
            Printing.DrawAt(this.Point.X, this.Point.Y, @"                                         ", ConsoleColor.Magenta);
            Printing.DrawAt(this.Point.X, this.Point.Y + 1, @"                                         ", ConsoleColor.DarkMagenta);
            Printing.DrawAt(this.Point.X, this.Point.Y + 2, @"                                         ", ConsoleColor.DarkMagenta);

        }

        /// <summary>
        /// Hiệu ứng Boss nhập cuộc
        /// </summary>
        private void BossEntryAnimation()
        {
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y - 10, @"                      ████", 1, false, ConsoleColor.DarkMagenta);
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y - 9, @"███████         ███  █    █      ███████", 1, false, ConsoleColor.DarkMagenta);
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y - 8, @"  █████        █   █ ████        █████  ", 1, false, ConsoleColor.DarkMagenta);
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y - 7, @"   ████████     ███████ █     ████████  ", 1, false, ConsoleColor.Magenta);
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y - 6, @"    ██████████  █ ███████ ██████████    ", 1, false, ConsoleColor.Magenta);
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y - 5, @"                ████████████            ", 1, false, ConsoleColor.Magenta);
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y - 4, @"           █          ██          █     ", 1, false, ConsoleColor.Magenta);
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y - 3, @"       █████          ██          █████ ", 1, false, ConsoleColor.Magenta);
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y - 2, @"       ██████  ███ ██████████ ███ ██████", 1, false, ConsoleColor.Magenta);
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y - 1, @"             █████ █ █████ ██ █████     ", 1, false, ConsoleColor.Magenta);
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y, @"       █████████   ████  ████   █████████", 1, false, ConsoleColor.Magenta);
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y + 1, @"      ████████  ██████  ██████  ████████ ", 1, false, ConsoleColor.DarkMagenta);
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y + 2, @"      ████          █    █          ████ ", 1, false, ConsoleColor.DarkMagenta);
        }
    }
}

