using System;
using System.IO;
using System.Threading;
using System.Windows.Media;

namespace TeamWork.Field
{
    class Menu
    {
        public static bool menuActive = true;
        public static bool validInput = true;
        public static MediaPlayer mediaPlayer = new MediaPlayer();

        /// <summary>
        /// Màn hình load menu chính
        /// </summary>
        public static void StartLogo()
        {
            Printing.WelcomeScreen();
            Thread.Sleep(2000);
        }
        public static void StartMenu()
        {      
            while (menuActive)
            {
                if (validInput)
                {
                    Console.Clear();
                    Printing.StartMenu();
                    validInput = false;
                }

                if (UserChoice(Console.ReadKey(true)))
                {
                    validInput = true;
                }
                else
                {
                    validInput = false;
                }
            }
        }

        // Các nút giao diện menu

        public static bool UserChoice(ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.C:
                    Console.Clear();
                    mediaPlayer.Stop();
                    menuActive = false;
                    return true;
                case ConsoleKey.B:
                    Console.Clear();
                    Printing.HighScore();
                    return true;
                case ConsoleKey.D4:
                    Console.Clear();
                    Printing.Credits();
                    return true;
                case ConsoleKey.T:
                    Environment.Exit(0);
                    return false;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Câu chuyện giới thiệu
        /// </summary>
        public static void EntryStoryLine()
        {
            Printing.LoadStory();
            Thread.Sleep(2500);
            Console.Clear();
            Printing.LoadContent();
            Console.Clear();
        }

        /// <summary>
        /// In viền trên và dưới của UI
        /// </summary>
        public static void Table()
        {
            int uiWidth = Math.Min(Console.WindowWidth, 115);
            int nameBoard = 14 + Engine.Player.Name.Length;

            // Vẽ viền trên
            for (int i = 0; i < uiWidth; i++)
            {
                Printing.DrawAt(new Point2D(i, 0), '\u2588', ConsoleColor.DarkRed); 

            }

            // Vẽ viền dưới
            for (int i = 0; i < uiWidth; i++)
            {
                Printing.DrawAt(new Point2D(i, 30), '\u2588', ConsoleColor.DarkRed);
            }
        }

        /// <summary>
        /// Vẽ UI 
        /// </summary>
        public static void UIDescription()
        {
            string level = string.Format(" Level: {0} ", Engine.Player.Level).PadLeft(2, '0');
            string score = string.Format("               Score: {0}               ", Engine.Player.Score).PadLeft(3, '0');
            string playerName = string.Format(" Player: {0} ", Engine.Player.Name);

            Printing.DrawAt(new Point2D(5, 0), playerName, ConsoleColor.Cyan) ;
            Printing.DrawAt(new Point2D(100, 0), level, ConsoleColor.Cyan);
            Printing.DrawAt(new Point2D(5, 30), " Lifes: ", ConsoleColor.Cyan); ;
            Printing.DrawHLineAt(12, 30, Engine.Player.Lifes, '\u2665', ConsoleColor.DarkRed);
            Printing.ClearAtPosition(12 + Engine.Player.Lifes, 30);
            Printing.DrawAt(new Point2D(41, 0), score, ConsoleColor.Cyan); ;
        }

        #region Phương thức điểm cao và điểm số
        // Kiểm tra xem oldHighScore và CurrentHighScore có khác nhau không và thiết lập giá trị cao hơn là HighScore mới
        // Cũng thêm tất cả các điểm vào file Scores.txt
        public static void SetHighscore()
        {
            // Đọc điểm số và tên từ các file
            string[] Scores = File.ReadAllLines("Resources/Scores.txt");
            string[] Names = File.ReadAllLines("Resources/Name.txt");

            // Chuyển đổi điểm số sang mảng số nguyên để sắp xếp
            int[] intScores = Array.ConvertAll(Scores, int.Parse);

            bool isInRank = false;

            // Kiểm tra nếu điểm của người chơi cao hơn điểm thấp nhất
            if (Engine.Player.Score > intScores[intScores.Length - 1])
            {
                // Thay thế điểm số thấp nhất bằng điểm mới và tên của người chơi
                intScores[intScores.Length - 1] = Engine.Player.Score;
                Names[Names.Length - 1] = Engine.Player.Name;
                isInRank = true;
            }

            if (isInRank)
            {
                // Sắp xếp điểm số theo thứ tự giảm dần
                Array.Sort(intScores);
                Array.Reverse(intScores);

                // Tìm vị trí xếp hạng của điểm mới
                int playerRank = Array.IndexOf(intScores, Engine.Player.Score);

                // Điều chỉnh tên theo thứ hạng mới
                string tempName = Names[Names.Length - 1];
                for (int i = Names.Length - 1; i > playerRank; i--)
                {
                    Names[i] = Names[i - 1];
                }
                Names[playerRank] = tempName;

                // Ghi lại điểm số và tên đã cập nhật vào các file
                File.WriteAllLines("Resources/Scores.txt", Array.ConvertAll(intScores, x => x.ToString()));
                File.WriteAllLines("Resources/Name.txt", Names);
            }
        }

        /// <summary>
        /// In điểm cao trong màn hình điểm số menu chính
        /// </summary>
        public static void PrintHighscore()
        {
            string[] Scores = File.ReadAllLines("Resources/Scores.txt");
            string[] Names = File.ReadAllLines("Resources/Name.txt");
            int y = 12;

            for (int i = 0; i < 10; i++)
            {

                Printing.DrawAt(new Point2D(44, y), Names[i], ConsoleColor.Yellow);
                Printing.DrawAt(new Point2D(70, y), Scores[i], ConsoleColor.Yellow);
                y += 1;

            }
        }
        #endregion
    }
}
