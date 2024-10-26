using System;
using System.Runtime.ConstrainedExecution;
using System.Threading;

namespace TeamWork.Field
{
    public static class Printing
    {
        #region Printing Methods

        /// <summary>
        /// Vẽ một đối tượng tại tọa độ X và Y cho trước
        /// </summary>
        /// <param name="x">Số cột</param>
        /// <param name="y">Số hàng</param>
        /// <param name="obj">Đối tượng cần in</param>
        public static void DrawAt(int x, int y, object obj)
        {
            try
            {
                Console.SetCursorPosition(x, y);
                Console.Write(obj.ToString());
            }
            catch { return; }
        }

        /// <summary>
        /// Vẽ một đối tượng tại tọa độ Point2D
        /// </summary>
        /// <param name="point">Point2D để in tại đó</param>
        /// <param name="obj">Đối tượng cần in</param>
        public static void DrawAt(Point2D point, object obj)
        {
            try
            {
                Console.SetCursorPosition(point.X, point.Y);
                Console.Write(obj.ToString());
            }
            catch { return; }
        }

        /// <summary>
        /// Vẽ một đối tượng tại tọa độ X và Y cho trước với màu
        /// </summary>
        /// <param name="x">Số cột</param>
        /// <param name="y">Số hàng</param>
        /// <param name="obj">Đối tượng cần in</param>
        /// <param name="clr">Màu để in</param>
        public static void DrawAt(int x, int y, object obj, ConsoleColor clr)
        {
            try
            {
                Console.ForegroundColor = clr;
                DrawAt(x, y, obj);
                Console.ResetColor();
            }
            catch { return; }
        }

        /// <summary>
        /// Vẽ một đối tượng tại tọa độ Point2D với màu
        /// </summary>
        /// <param name="point">Point2D để in tại đó</param>
        /// <param name="obj">Đối tượng cần in</param>
        /// <param name="clr">Màu để in</param>
        public static void DrawAt(Point2D point, object obj, ConsoleColor clr)
        {
            try
            {
                Console.ForegroundColor = clr;
                DrawAt(point, obj);
                Console.ResetColor();
            }
            catch { return; }
        }

        public static void DrawAtBG(int x, int y, object obj, ConsoleColor bclr)
        {
            try
            {
                Console.BackgroundColor = bclr;
                DrawAt(x, y, obj);
                Console.ResetColor();
            }
            catch { return; }
        }
        public static void DrawAtBGPlus(Point2D point, object obj, ConsoleColor clr, ConsoleColor bclr) //Chữ và hình có màu khác nhau
        {
            try
            {
                Console.ForegroundColor = clr;
                DrawAt(point, obj);
                Console.BackgroundColor = bclr;
                DrawAt(point, obj);
                Console.ResetColor();
            }
            catch { return; }
        }

        /// <summary>
        /// Vẽ một đường thẳng đứng với độ dài cho trước bắt đầu từ X và Y
        /// </summary>
        /// <param name="x">Số cột</param>
        /// <param name="y">Số hàng</param>
        /// <param name="lenght">Độ dài của đường</param>
        /// <param name="obj">Đối tượng cần in</param>
        /// <param name="clr">Màu để in</param>
        public static void DrawVLineAt(int x, int y, int lenght, object obj, ConsoleColor clr = ConsoleColor.White)
        {
            try
            {
                for (int i = 0; i < lenght; i++)
                {
                    DrawAt(x, y, obj, clr);
                    y++;
                }
            }
            catch { return; }
        }

        /// <summary>
        /// Vẽ một đường thẳng đứng với độ dài cho trước và dừng lại giữa các ký tự
        /// </summary>
        /// <param name="x">Số cột</param>
        /// <param name="y">Số hàng</param>
        /// <param name="lenght">Độ dài của đường</param>
        /// <param name="obj">Đối tượng cần in</param>
        /// <param name="sleep">Thời gian dừng giữa các ký tự (ms)</param>
        /// <param name="reverse">True nếu muốn vẽ từ phải qua trái</param>
        /// <param name="clr">Màu để in</param>
        public static void DrawVLineAt(int x, int y, int lenght, object obj, int sleep, bool reverse, ConsoleColor clr = ConsoleColor.White)
        {
            try
            {
                for (int i = 0; i < lenght; i++)
                {
                    DrawAt(x, y, obj, clr);
                    if (reverse)
                    {
                        y--;
                    }
                    else
                    {
                        y++;
                    }
                    Thread.Sleep(sleep);
                }
            }
            catch { return; }
        }

        /// <summary>
        /// Vẽ một đường thẳng đứng với độ dài cho trước bắt đầu từ Point2D
        /// </summary>
        /// <param name="point">Point2D để in tại đó</param>
        /// <param name="lenght">Độ dài của đường</param>
        /// <param name="obj">Đối tượng cần in</param>
        /// <param name="clr">Màu để in</param>
        public static void DrawVLineAt(Point2D point, int lenght, object obj, ConsoleColor clr = ConsoleColor.White)
        {
            try
            {
                DrawVLineAt(point.X, point.Y, lenght, obj, clr);
            }
            catch { return; }
        }

        /// <summary>
        /// Vẽ một đường ngang với độ dài cho trước bắt đầu từ X và Y
        /// </summary>
        /// <param name="x">Số cột</param>
        /// <param name="y">Số hàng</param>
        /// <param name="lenght">Độ dài của đường</param>
        /// <param name="obj">Đối tượng cần in</param>
        /// <param name="clr">Màu để in</param>
        public static void DrawHLineAt(int x, int y, int lenght, object obj, ConsoleColor clr = ConsoleColor.White)
        {
            try
            {
                for (int i = 0; i < lenght; i++)
                {
                    DrawAt(x, y, obj, clr);
                    x++;
                }
            }
            catch { return; }
        }

        /// <summary>
        /// Vẽ một đường ngang với độ dài cho trước và dừng lại giữa các ký tự
        /// </summary>
        /// <param name="x">Số cột</param>
        /// <param name="y">Số hàng</param>
        /// <param name="lenght">Độ dài của đường</param>
        /// <param name="obj">Đối tượng cần in</param>
        /// <param name="sleep">Thời gian dừng giữa các ký tự (ms)</param>
        /// <param name="reverse">Nếu muốn in từ phải qua trái</param>
        /// <param name="clr"></param>
        public static void DrawHLineAt(int x, int y, int lenght, object obj, int sleep, bool reverse, ConsoleColor clr = ConsoleColor.White)
        {
            try
            {
                for (int i = 0; i < lenght; i++)
                {
                    DrawAt(x, y, obj, clr);
                    if (reverse)
                    {
                        x--;
                    }
                    else
                    {
                        x++;
                    }
                    Thread.Sleep(sleep);
                }
            }
            catch { return; }
        }

        /// <summary>
        /// Vẽ một đường ngang với độ dài cho trước bắt đầu từ Point2D
        /// </summary>
        /// <param name="point">Point2D để in tại đó</param>
        /// <param name="lenght">Độ dài của đường</param>
        /// <param name="obj">Đối tượng cần in</param>
        /// <param name="clr">Màu để in</param>
        public static void DrawHLineAt(Point2D point, int lenght, object obj, ConsoleColor clr = ConsoleColor.White)
        {
            try
            {
                DrawHLineAt(point.X, point.Y, lenght, obj, clr);
            }
            catch { return; }
        }

        /// <summary>
        /// Vẽ chuỗi ký tự từng ký tự một với thời gian dừng giữa các ký tự
        /// </summary>
        /// <param name="x">Số cột</param>
        /// <param name="y">Số hàng</param>
        /// <param name="str">Chuỗi cần in</param>
        /// <param name="sleep">Thời gian dừng giữa các ký tự (ms)</param>
        /// <param name="reverse">Nếu muốn in từ phải qua trái</param>
        /// <param name="clr"></param>
        public static void DrawStringCharByChar(int x, int y, string str, int sleep, bool reverse,
            ConsoleColor clr = ConsoleColor.White)
        {
            try
            {
                if (reverse)
                {
                    x = x + str.Length - 1;
                    for (int i = str.Length - 1; i >= 0; i--)
                    {
                        DrawAt(x, y, str[i], clr);
                        x--;
                        Thread.Sleep(sleep);
                    }
                }
                else
                {
                    for (int i = 0; i < str.Length; i++)
                    {
                        DrawAt(x, y, str[i], clr);
                        x++;
                        Thread.Sleep(sleep);
                    }
                }
            }
            catch { return; }
        }

        #endregion

        #region Grаphics
        /// <summary>
        /// Vẽ màn hình bảng điểm 
        /// </summary>
        public static void HighScore()
        {
            int x = 42;
            DrawHLineAt(0, 0, 115, '\u2588', 1, false, ConsoleColor.Yellow);
            DrawVLineAt(114, 0, 30, '\u2588', 1, false, ConsoleColor.Yellow);
            DrawHLineAt(114, 29, 115, '\u2588', 1, true, ConsoleColor.Yellow);
            DrawVLineAt(0, 29, 30, '\u2588', 1, true, ConsoleColor.Yellow);

            DrawAt(1, 2, @"                      ♦          ♦                                 ♦               ♦                   ♦      ♦ ", ConsoleColor.Cyan);
            DrawAt(1, 3, @"      .           +                 ,             *                       ,             *                       ", ConsoleColor.Cyan);
            DrawAt(1, 4, @"   .                             .     .                ,             *                                     .   ", ConsoleColor.Cyan);
            DrawAt(1, 5, @"     ,              *                     .                ,             *                            '        *", ConsoleColor.Cyan);
            DrawAt(1, 6, @"                                .                                                             +                 ", ConsoleColor.Cyan);
            DrawAt(1, 7, @"                                                +                       ,             *                         ", ConsoleColor.Cyan);
            DrawAt(1, 8, @"                                                              .          .                ,             *       ", ConsoleColor.Cyan);
            DrawAt(1, 9, @"             *                                                                                     *            ", ConsoleColor.Cyan);
            DrawAt(1, 10, @"                           '                                                                                    ", ConsoleColor.Cyan);
            DrawAt(1, 11, @"   .                               +                                                                            ", ConsoleColor.Cyan);
            DrawAt(1, 12, @"                  *         .                    .                              *           +                   ", ConsoleColor.Cyan);
            DrawAt(1, 13, @"      .                                                             .                              *            ", ConsoleColor.Cyan);
            DrawAt(1, 14, @"              ,                             .                ,                                                  ", ConsoleColor.Cyan);
            DrawAt(1, 15, @"                                                       .                ,                       +               ", ConsoleColor.Cyan);
            DrawAt(1, 16, @"                 *                              .                ,             *                                ", ConsoleColor.Cyan);
            DrawAt(1, 17, @"     .                                               *                                                          ", ConsoleColor.Cyan);
            DrawAt(1, 18, @"                                                *                .                                              ", ConsoleColor.Cyan);
            DrawAt(1, 19, @".                    *                                                                      *                   ", ConsoleColor.Cyan);
            DrawAt(1, 20, @".                            *                                     + .                              *           ", ConsoleColor.Cyan);
            DrawAt(1, 21, @"                                                *            .                                                  ", ConsoleColor.Cyan);
            DrawAt(1, 22, @"                                                                         .                                      ", ConsoleColor.Cyan);
            DrawAt(1, 23, @"         +                                 *                 .                ,             *                   ", ConsoleColor.Cyan);
            DrawAt(1, 24, @"                                                                        .                ,          *           ", ConsoleColor.Cyan);
            DrawAt(1, 25, @".                    *                                                                                          ", ConsoleColor.Cyan);
            DrawAt(1, 26, @".                            *                                     +                  ,                         ", ConsoleColor.Cyan);
            DrawAt(1, 27, @"                                                *                                                    *          ", ConsoleColor.Cyan);
            DrawAt(1, 28, @"                                                        , .                ,             *                      ", ConsoleColor.Cyan);

            DrawStringCharByChar(x, 5, "██████╗  █████╗ ███╗   ██╗██╗  ██", 1, false, ConsoleColor.White);
            DrawStringCharByChar(x, 6, "██╔══██╗██╔══██╗████╗  ██║██║ ██╔╝", 1, true, ConsoleColor.White);
            DrawStringCharByChar(x, 7, "██████╔╝███████║██╔██╗ ██║█████╔╝ ", 1, false, ConsoleColor.White);
            DrawStringCharByChar(x, 8, "██╔══██╗██╔══██║██║╚██╗██║██╔═██╗ ", 1, true, ConsoleColor.White);
            DrawStringCharByChar(x, 9, "██║  ██║██║  ██║██║ ╚████║██║  ██╗", 1, false, ConsoleColor.White);
            DrawStringCharByChar(x, 10, "╚═╝  ╚═╝╚═╝  ╚═╝╚═╝  ╚═══╝╚═╝  ╚═╝", 1, true, ConsoleColor.White);
            Thread.Sleep(550);
            DrawAt(49, 27, @"[T]rở về Menu", ConsoleColor.Yellow);
            Menu.PrintHighscore();
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.T)
                {
                    Menu.validInput = true;
                    break;
                }
            }

        }
        /// <summary>
        /// Vẽ màn hình mở game 
        /// </summary>
        public static void WelcomeScreen()
        {
            DrawHLineAt(45, 4, 60, '\u2588', 1, false, ConsoleColor.DarkRed);
            DrawHLineAt(70, 25, 60, '\u2588', 1, true, ConsoleColor.DarkRed);

            DrawAt(35, 12, "██╗  ██╗███████╗██╗  ██╗███████╗███████╗██████╗", ConsoleColor.DarkRed);
            DrawAt(35, 12, "██╗  ██╗███████╗██╗  ██╗███████╗███████╗██████╗", ConsoleColor.DarkRed);
            DrawAt(35, 13, "██║  ██║██╔════╝██║  ██║██╔════╝██╔════╝██╔══██╗", ConsoleColor.DarkRed);
            DrawAt(35, 14, "███████║███████╗███████║█████╗  █████╗  ██████╔╝", ConsoleColor.DarkRed);
            DrawAt(35, 15, "╚════██║╚════██║██╔══██║██╔══╝  ██╔══╝  ██╔═══╝ ", ConsoleColor.DarkRed);
            DrawAt(35, 16, "     ██║███████║██║  ██║███████╗███████╗██║", ConsoleColor.DarkRed);
            DrawAt(35, 17, "     ╚═╝╚══════╝╚═╝  ╚═╝╚══════╝╚══════╝╚═╝     ", ConsoleColor.DarkRed);
        }
        /// <summary>
        /// Vẽ màn hình menu 
        /// </summary>
        public static void StartMenu()
        {
            DrawHLineAt(0, 0, 115, '\u2588', 1, false, ConsoleColor.Red);
            DrawVLineAt(114, 0, 30, '\u2588', 1, false, ConsoleColor.Red);
            DrawHLineAt(114, 29, 115, '\u2588', 1, true, ConsoleColor.Red);
            DrawVLineAt(0, 29, 30, '\u2588', 1, true, ConsoleColor.Red);
            // Vẽ nền 
            DrawAt(1, 1, @"                                         ♦                              ♦         ♦                              ", ConsoleColor.White);
            DrawAt(1, 2, @"  ♦          ♦        ♦        ♦                        ♦       ♦                                                ", ConsoleColor.White);
            DrawAt(1, 3, @"                            ███████╗████████╗███████╗██╗     ██╗      █████╗  ██████╗                            ", ConsoleColor.White);
            DrawAt(1, 4, @"                            ██╔════╝╚══██╔══╝██╔════╝██║     ██║     ██╔══██╗ ██╔══██╗       ♦                   ", ConsoleColor.White);
            DrawAt(1, 5, @"        ♦     ♦          ♦  ███████╗   ██║   █████╗  ██║     ██║     ███████║ ██████╔╝                           ", ConsoleColor.White);
            DrawAt(1, 6, @"                            ╚════██║   ██║   ██╔══╝  ██║     ██║     ██╔══██║ ██╔══██╗                           ", ConsoleColor.White);
            DrawAt(1, 7, @"    ♦          ♦            ███████║   ██║   ███████╗███████╗███████╗██║  ██║ ██║  ██║       ♦     ♦             ", ConsoleColor.White);
            DrawAt(1, 8, @"                            ╚══════╝   ╚═╝   ╚══════╝╚══════╝╚══════╝╚═╝  ╚═╝╚═╝  ╚═╝                           ", ConsoleColor.White);
            DrawAt(1, 9, @"                     ♦                                                               ♦                          ", ConsoleColor.White);
            DrawAt(1, 10, @"                                        ♦   ██╗    ██╗ █████╗ ██████╗       ♦                         ♦         ", ConsoleColor.White);
            DrawAt(1, 11, @"                                            ██║    ██║██╔══██╗██╔══██╗                                       ♦  ", ConsoleColor.White);
            DrawAt(1, 12, @"                          ♦       ♦         ██║ █╗ ██║███████║██████╔╝                                          ", ConsoleColor.White);
            DrawAt(1, 13, @"    ♦                                       ██║███╗██║██╔══██║██╔══██╗          ♦                               ", ConsoleColor.White);
            DrawAt(1, 14, @"                   ♦        ♦               ╚███╔███╔╝██║  ██║██║  ██║                                         ♦", ConsoleColor.White);
            DrawAt(1, 15, @"                                    ♦        ╚══╝╚══╝ ╚═╝  ╚═╝╚═╝  ╚═╝                                          ", ConsoleColor.White);
            DrawAt(1, 16, @"          ♦        ♦                                                        ♦                     ♦    ♦        ", ConsoleColor.White);
            DrawAt(1, 17, @"                      ♦          ♦                                 ♦               ♦                   ♦      ♦ ", ConsoleColor.White);

            DrawAt(88, 10, @"     ████", ConsoleColor.Red);
            DrawAt(88, 11, @"   ██    ██", ConsoleColor.Red);
            DrawAt(88, 12, @"  █        █", ConsoleColor.Red);
            DrawAt(88, 13, @"  █        █", ConsoleColor.Red);
            DrawAt(88, 14, @"██████████████", ConsoleColor.Magenta);
            DrawAt(88, 15, @"   ██    ██ ", ConsoleColor.DarkMagenta);
            DrawAt(88, 16, @"     ████", ConsoleColor.DarkMagenta);

            DrawAt(102, 1, @"  ██████████", ConsoleColor.Magenta);
            DrawAt(102, 2, @" ███████████", ConsoleColor.Magenta);
            DrawAt(102, 3, @"████████████", ConsoleColor.Red);
            DrawAt(102, 4, @"████████████", ConsoleColor.Red);
            DrawAt(102, 5, @" ███████████", ConsoleColor.Red);
            DrawAt(102, 6, @"  ██████████", ConsoleColor.Red);
            DrawAt(102, 7, @"   █████████", ConsoleColor.Yellow);
            DrawAt(102, 8, @"     ███████", ConsoleColor.Yellow);

            DrawAt(1, 23, @"████                 ♦          ♦        ♦                                      ♦                         ♦    ██", ConsoleColor.Cyan);
            DrawAt(1, 24, @"██████                                               ♦      ♦                                                ████", ConsoleColor.Cyan);
            DrawAt(1, 25, @"███████          ♦                     ♦      ♦         ♦                                            ♦      █████", ConsoleColor.Cyan);
            DrawAt(1, 26, @"████████                                                                                                   ██████", ConsoleColor.Magenta);
            DrawAt(1, 27, @"█████████                                                                                                 ███████", ConsoleColor.DarkMagenta);
            DrawAt(1, 28, @"█████████████████████████████████████████████████████████████████████████████████████████████████████████████████", ConsoleColor.DarkMagenta);

            DrawAt(25, 21, @" ██", ConsoleColor.Red);
            DrawAt(25, 22, @"████", ConsoleColor.DarkMagenta);
            DrawAt(25, 23, @"████", ConsoleColor.Magenta);
            DrawAt(25, 24, @"█  █", ConsoleColor.Magenta);
            DrawAt(25, 25, @"█  █", ConsoleColor.Magenta);
            DrawAt(25, 26, @"████", ConsoleColor.Magenta);
            DrawAt(25, 27, @"████", ConsoleColor.Red);

            DrawAt(1, 23, @"████", ConsoleColor.DarkMagenta);
            DrawAt(1, 24, @"██████", ConsoleColor.Magenta);
            DrawAt(1, 25, @"███████", ConsoleColor.Magenta);
     
            DrawAt(92, 24, @"  █", ConsoleColor.Yellow);
            DrawAt(92, 25, @" ███", ConsoleColor.Red);
            DrawAt(92, 26, @"█████", ConsoleColor.Red);
            DrawAt(92, 27, @" ███", ConsoleColor.DarkMagenta);

            DrawAt(109, 23, @"   ██", ConsoleColor.DarkMagenta);
            DrawAt(109, 24, @" ████", ConsoleColor.Magenta);
            DrawAt(109, 25, @"█████", ConsoleColor.Magenta);

            DrawAt(11, 9, "  █ █  ", ConsoleColor.Yellow);
            DrawAt(11, 10, "███████", ConsoleColor.Red);
            DrawAt(11, 11, "█ ███ █", ConsoleColor.Red);
            DrawAt(11, 12, "███████", ConsoleColor.Magenta);
            DrawAt(11, 13, " █ █ █", ConsoleColor.DarkMagenta);

            DrawStringCharByChar(29, 3, @"███████╗████████╗███████╗██╗     ██╗      █████╗  ██████╗ ",1, false,  ConsoleColor.DarkRed);
            DrawStringCharByChar(29, 4, @"██╔════╝╚══██╔══╝██╔════╝██║     ██║     ██╔══██╗ ██╔══██╗", 1, false, ConsoleColor.DarkRed);
            DrawStringCharByChar(29, 5, @"███████╗   ██║   █████╗  ██║     ██║     ███████║ ██████╔╝", 1, false, ConsoleColor.DarkRed);
            DrawStringCharByChar(29, 6, @"╚════██║   ██║   ██╔══╝  ██║     ██║     ██╔══██║ ██╔══██╗", 1, false, ConsoleColor.Red);
            DrawStringCharByChar(29, 7, @"███████║   ██║   ███████╗███████╗███████╗██║  ██║ ██║  ██║", 1, false, ConsoleColor.Red);
            DrawStringCharByChar(29, 8, @"╚══════╝   ╚═╝   ╚══════╝╚══════╝╚══════╝╚═╝  ╚═╝ ╚═╝  ╚═╝", 1, false, ConsoleColor.DarkRed);
            DrawStringCharByChar(45, 10, @"██╗    ██╗ █████╗ ██████╗ ", 1, false, ConsoleColor.DarkRed);
            DrawStringCharByChar(45, 11, @"██║    ██║██╔══██╗██╔══██╗", 1, false, ConsoleColor.Red);
            DrawStringCharByChar(45, 12, @"██║ █╗ ██║███████║██████╔ ", 1, false, ConsoleColor.Red);
            DrawStringCharByChar(45, 13, @"██║███╗██║██╔══██║██╔══██╗", 1, false, ConsoleColor.Red);
            DrawStringCharByChar(45, 14, @"╚███╔███╔╝██║  ██║██║  ██║", 1, false, ConsoleColor.DarkRed);
            DrawStringCharByChar(45, 15, @" ╚══╝╚══╝ ╚═╝  ╚═╝╚═╝  ╚═╝", 1, false, ConsoleColor.DarkRed);
            Thread.Sleep(600);

            // Thêm menu 
            DrawStringCharByChar(53, 18, @"[C]hơi", 1, false, ConsoleColor.White);
            DrawStringCharByChar(53, 19, @"[B]ảng điểm", 1, true, ConsoleColor.White);
            DrawStringCharByChar(53, 20, @"[4]Sheep", 1, false, ConsoleColor.White);
            DrawStringCharByChar(53, 21, @"[Thoát]", 1, true, ConsoleColor.White);

           
        }
        /// <summary>
        /// Màn hình credits
        /// </summary>
        public static void Credits()
        {
            DrawHLineAt(0, 0, 115, '\u2588', 3, false, ConsoleColor.Red);
            DrawVLineAt(114, 0, 30, '\u2588', 3, false, ConsoleColor.Red);
            DrawHLineAt(114, 29, 115, '\u2588', 3, true, ConsoleColor.Red);
            DrawVLineAt(0, 29, 30, '\u2588', 3, true, ConsoleColor.Red);
            DrawAt(90, 1, "♦  ♦     ♦    ♦     ♦", ConsoleColor.Cyan);
            DrawAt(19, 2, "♦       ♦         ♦           ♦                   ♦", ConsoleColor.Cyan);
            DrawAt(43, 2, "♦               ♦                       ♦", ConsoleColor.Cyan);
            DrawAt(9, 3, "♦          ♦          ♦       ♦             ♦", ConsoleColor.Cyan);
            DrawAt(25, 5, "                                                               ♦ ", ConsoleColor.Cyan);
            DrawAt(25, 6, "                                                                        ♦", ConsoleColor.Cyan);
            DrawAt(25, 7, "                                                                    ♦", ConsoleColor.Cyan);
            DrawAt(25, 8, "                                                                       ♦", ConsoleColor.Cyan);
            DrawAt(25, 9, "                                                                            ♦", ConsoleColor.Cyan);
            DrawAt(25, 10, "                                                                     ♦", ConsoleColor.Cyan);
            DrawAt(35, 12, "                ", ConsoleColor.Green);
            DrawAt(35, 13, "           ", ConsoleColor.Gray);
            DrawAt(20, 14, "♦", ConsoleColor.Cyan); // Trang trí thêm dấu ♦
            DrawAt(35, 14, "              ", ConsoleColor.Gray);
            DrawAt(55, 14, "                             ♦", ConsoleColor.Cyan); // Trang trí thêm dấu ♦
            DrawAt(35, 15, "           ", ConsoleColor.Gray);
            DrawAt(35, 16, "              ", ConsoleColor.Gray);

            // Tùy chọn điều hướng
            DrawAt(10, 20, "                   ", ConsoleColor.Yellow);
            DrawAt(55, 20, "                    ", ConsoleColor.Yellow);
            DrawAt(55, 21, "                ", ConsoleColor.Yellow);

            // Trang trí thêm các biểu tượng ♦ ở cuối màn hình
            DrawAt(10, 22, "♦                  ♦", ConsoleColor.Cyan);
            DrawAt(29, 22, "♦               ♦", ConsoleColor.Cyan);
            DrawAt(85, 22, "♦            ♦", ConsoleColor.Cyan);
            DrawAt(70, 23, "♦         ♦", ConsoleColor.Cyan);
            DrawAt(45, 24, "♦              ♦", ConsoleColor.Cyan);
            DrawAt(65, 12, "              ♦              ♦", ConsoleColor.Cyan);
            DrawAt(65, 13, "                      ♦              ♦", ConsoleColor.Cyan);



            Thread.Sleep(650);

            DrawAt(25, 5, "       ██████╗ ██████╗ ███████╗██████╗ ██╗████████╗███████╗", ConsoleColor.White);
            DrawAt(25, 6, "       ██╔════╝██╔══██╗██╔════╝██╔══██╗██║╚══██╔══╝██╔════╝", ConsoleColor.White);
            DrawAt(25, 7, "       ██║     ██████╔╝█████╗  ██║  ██║██║   ██║   ███████╗", ConsoleColor.White);
            DrawAt(25, 8, "       ██║     ██╔══██╗██╔══╝  ██║  ██║██║   ██║   ╚════██║", ConsoleColor.White);
            DrawAt(25, 9, "       ╚██████╗██║  ██║███████╗██████╔╝██║   ██║   ███████║", ConsoleColor.White);
            DrawAt(25, 10, "        ╚═════╝╚═╝  ╚═╝╚══════╝╚═════╝ ╚═╝   ╚═╝   ╚══════╝", ConsoleColor.White);
            Thread.Sleep(1500);
            DrawAt(42, 12, @"    Được đem đến bởi 4Sheep", ConsoleColor.Red);
            Thread.Sleep(650);

            DrawAt(42, 20, @"      [T]rở về Menu", ConsoleColor.Red);

            string[,] ThanhVienNhom = new string[4, 2]
            {
        {"Lê Quốc Bảo","31231021075" },
        {"Đỗ Việt Anh", "31231021860" },
        {"Tăng Thoại Hào","31231027959" },
        {"Trần Minh Tuấn","31231020865" }
            };
            int x = 40;
            int y = 14;
            int z = 14;

            for (int i = 0; i < 4; i++)
            {

                DrawAt(x, y, ThanhVienNhom[i, 0], ConsoleColor.Gray);
                y += 1;
            }

            for (int i = 0; i < 4; i++)
            {

                DrawAt(x + 20, z, ThanhVienNhom[i, 1], ConsoleColor.Gray);
                z += 1;
            }


            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.T)
                {
                    Menu.validInput = true;
                    break;
                }

            }
        }
        /// <summary>
        /// Vẽ màn hình game kết thúc 
        /// </summary>
        public static void GameOver()
        {
            DrawHLineAt(0, 0, 115, '\u2588', 1, false, ConsoleColor.Red);
            DrawVLineAt(114, 0, 30, '\u2588', 1, false, ConsoleColor.Red);
            DrawHLineAt(114, 29, 115, '\u2588', 1, true, ConsoleColor.Red);
            DrawVLineAt(0, 29, 30, '\u2588', 1, true, ConsoleColor.Red);
            DrawAt(3, 1, "   ♦      ♦                   ♦       ♦               ♦       ♦                   ♦       ♦                    ", ConsoleColor.Cyan);
            DrawAt(1, 2, "                  ♦                                  █████                            ♦                         ", ConsoleColor.Cyan);
            DrawAt(1, 3, "                                                  ███████████            ♦      ♦                               ", ConsoleColor.Cyan);
            DrawAt(1, 4, "                       ♦        ♦                █████████████                          ♦                       ", ConsoleColor.Cyan);
            DrawAt(1, 5, "███████████     ♦                                █████████████       ♦                                          ", ConsoleColor.Cyan);
            DrawAt(1, 6, "          █                                      █████████████          ♦      ♦                       ██████████", ConsoleColor.Cyan);
            DrawAt(1, 7, "          █               ♦       ♦               ███████████    ♦                                     █        ", ConsoleColor.Cyan);
            DrawAt(1, 8, "█████████ █                                          █████                                             █        ", ConsoleColor.Cyan);
            DrawAt(1, 9, "        █ ██                                                                                          ██   ██████", ConsoleColor.Cyan);
            DrawAt(1, 10, "████    █  █                                                                                          █  ███    ", ConsoleColor.Cyan);
            DrawAt(1, 11, "   █    █  ██                                                                                       ███  █      ", ConsoleColor.Cyan);
            DrawAt(1, 12, "   █    █   █      ██████╗  █████╗  ███╗   ███╗███████╗    ██████╗ ██╗   ██╗███████╗██████╗        ███  ███     ", ConsoleColor.Cyan);
            DrawAt(1, 13, "   ███  █   █      ██╔════╝ ██╔══██╗████╗ ████║██╔════╝   ██╔═══██╗██║   ██║██╔════╝██╔══██╗       █    █       ", ConsoleColor.Cyan);
            DrawAt(1, 14, "     █  ██  █      ██║  ███╗███████║██╔████╔██║█████╗     ██║   ██║██║   ██║█████╗  ██████╔        █    ███     ", ConsoleColor.Cyan);
            DrawAt(1, 15, "     █   █  ██     ██║   ██║██╔══██║██║╚██╔╝██║██╔══╝     ██║   ██║╚██╗ ██╔╝██╔══╝  ██╔══██╗     ███   ██  ██████", ConsoleColor.Cyan);
            DrawAt(1, 16, "     █    █  █     ╚██████╔╝██║  ██║██║ ╚═╝ ██║███████╗   ╚██████╔╝ ╚████╔╝ ███████╗██║  ██║     █    ██   █    ", ConsoleColor.Cyan);
            DrawAt(1, 17, "     █    █  ██     ╚═════╝ ╚═╝  ╚═╝╚═╝     ╚═╝╚══════╝    ╚═════╝   ╚═══╝  ╚══════╝╚═╝  ╚═╝  ████  ███  ███    ", ConsoleColor.Cyan);
            DrawAt(1, 18, "      ██  █   █                                                                               █   ██   ████     ", ConsoleColor.Cyan);
            DrawAt(1, 19, "       █  ██   █                                                                            ██    █    █        ", ConsoleColor.Cyan);
            DrawAt(1, 20, "       █   █   █                                                                            █     █    █        ", ConsoleColor.Cyan);
            DrawAt(1, 21, "       ███ ██  ██                                                                       █████   ██    ██        ", ConsoleColor.Cyan);
            DrawAt(1, 22, "         █  █   █                                                                     ██       █      █         ", ConsoleColor.Cyan);
            DrawAt(1, 23, "         █  █   █                                                                    ██       ██   ████         ", ConsoleColor.Cyan);
            DrawAt(1, 24, "         █  █   █                                                                    █     ███     █            ", ConsoleColor.Cyan);
            DrawAt(1, 25, "         ██ ██  █                                                                    █     █      ██            ", ConsoleColor.Cyan);
            DrawAt(1, 26, "          █  █  █                                                                    █   ██       █             ", ConsoleColor.Cyan);
            DrawAt(1, 27, "          █  █  █                                                                    █   █      ███             ", ConsoleColor.Cyan);
            DrawAt(1, 28, "          █  █  █                                                                    █   █      █               ", ConsoleColor.Cyan);

            DrawAt(40, 19, "Được đem đến bởi nhóm: 4Sheep", ConsoleColor.White);
            DrawAt(50, 2, "    █████    ", ConsoleColor.DarkYellow);
            DrawAt(50, 3, " ███████████ ", ConsoleColor.DarkYellow);
            DrawAt(50, 4, "█████████████", ConsoleColor.Yellow);
            DrawAt(50, 5, "█████████████", ConsoleColor.Yellow);
            DrawAt(50, 6, "█████████████", ConsoleColor.Yellow);
            DrawAt(50, 7, " ███████████ ", ConsoleColor.DarkYellow);
            DrawAt(50, 8, "    █████    ", ConsoleColor.DarkYellow);


            DrawStringCharByChar(20, 12, @"██████╗  █████╗  ███╗   ███╗███████╗    ██████╗ ██╗   ██╗███████╗██████╗ ", 2, false, ConsoleColor.DarkYellow);
            DrawStringCharByChar(20, 13, @"██╔════╝ ██╔══██╗████╗ ████║██╔════╝   ██╔═══██╗██║   ██║██╔════╝██╔══██╗", 2, true, ConsoleColor.DarkYellow);
            DrawStringCharByChar(20, 14, @"██║  ███╗███████║██╔████╔██║█████╗     ██║   ██║██║   ██║█████╗  ██████╔", 2, false, ConsoleColor.Yellow);
            DrawStringCharByChar(20, 15, @"██║   ██║██╔══██║██║╚██╔╝██║██╔══╝     ██║   ██║╚██╗ ██╔╝██╔══╝  ██╔══██╗", 2, true, ConsoleColor.Yellow);
            DrawStringCharByChar(20, 16, @"╚██████╔╝██║  ██║██║ ╚═╝ ██║███████╗   ╚██████╔╝ ╚████╔╝ ███████╗██║  ██║", 3, false, ConsoleColor.Yellow);
            DrawStringCharByChar(20, 17, @" ╚═════╝ ╚═╝  ╚═╝╚═╝     ╚═╝╚══════╝    ╚═════╝   ╚═══╝  ╚══════╝╚═╝  ╚═╝", 3, true, ConsoleColor.DarkYellow);
            Thread.Sleep(600);
            DrawAt(45, 21, @"[C]hơi lại", ConsoleColor.Green);
            DrawAt(45, 22, @"[T]hoát game", ConsoleColor.Red);
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.C)
                {
                    break;
                }
                else if (key.Key == ConsoleKey.T)
                {
                    Environment.Exit(0);
                }
            }

        }
        /// <summary>
        /// Vẽ màn hình nhập tên 
        /// </summary>
        public static void EnterName()
        {
            DrawHLineAt(0, 0, 115, '\u2588', 1, false, ConsoleColor.Magenta);
            DrawVLineAt(114, 0, 30, '\u2588', 1, false, ConsoleColor.Magenta);
            DrawHLineAt(114, 29, 115, '\u2588', 1, true, ConsoleColor.Magenta);
            DrawVLineAt(0, 29, 30, '\u2588', 1, true, ConsoleColor.Magenta);
            DrawAt(17, 1, "♦                                       ♦               ♦     ♦                          ♦", ConsoleColor.Cyan);
            DrawAt(4, 2, "♦                                                                                                            ♦", ConsoleColor.Cyan);
            DrawAt(12, 3, "♦                                                                                       ", ConsoleColor.Cyan);
            DrawAt(13, 4, "                                                                                         ♦", ConsoleColor.Cyan);
            DrawAt(3, 5, "♦                                                                                     ", ConsoleColor.Cyan);
            DrawAt(13, 6, "                                                                                            ♦", ConsoleColor.Cyan);
            DrawAt(9, 7, "♦                                                                                      ", ConsoleColor.Cyan);
            DrawAt(13, 8, "                                                                                    ", ConsoleColor.Cyan);
            DrawAt(19, 9, "♦                                                                                   ", ConsoleColor.Cyan);
            DrawAt(36, 10, "♦        ♦                                                               ♦", ConsoleColor.Cyan);
            DrawAt(4, 11, "♦    ♦     ♦          ♦   ♦                                ♦    ♦       ♦     ♦                          ♦", ConsoleColor.Cyan);
            DrawAt(83, 12, "♦        ♦", ConsoleColor.Cyan);
            DrawAt(40, 13, "                 ♦                            ♦           ♦", ConsoleColor.Cyan);
            DrawAt(10, 14, "♦                ♦           ♦", ConsoleColor.Cyan);
            DrawAt(3, 15, "♦        ♦                               ♦                                  ♦       ♦", ConsoleColor.Cyan);
            DrawAt(1, 16, "                     ♦    █                        ♦", ConsoleColor.Cyan);
            DrawAt(1, 17, "         ♦               ███                                                         ███        █████         ♦♦", ConsoleColor.Cyan);
            DrawAt(1, 18, "                        █████                                   █████████           ████        █████", ConsoleColor.Cyan);
            DrawAt(1, 19, "                        █ █ █                                   █████████          █████       ███████", ConsoleColor.Cyan);
            DrawAt(1, 20, "          ♦             █████                                   █ █ █ █ █          █ █ █       █ █ █ █", ConsoleColor.Cyan);
            DrawAt(1, 21, "                        █ █ █        ██████████████████         █████████          █████       ███████", ConsoleColor.Cyan);
            DrawAt(1, 22, "                        █████        ██████████████████         █ █ █ █ █          █ █ █       █ █ █ █", ConsoleColor.Cyan);
            DrawAt(1, 23, "████                    █ █ █        ██ ██ █    █ ██ ██         █████████          █████       ███████       ████", ConsoleColor.Cyan);
            DrawAt(1, 24, "██████          █       █████        ██ ██ █ ████ ██ ██         █ █ █ █ █          █ █ █       █ █ █ █     ██████", ConsoleColor.Cyan);
            DrawAt(1, 25, "███████        ███      █ █ █        ██ ██ █    █    ██         █████████          █████       ███████    ███████", ConsoleColor.Cyan);
            DrawAt(1, 26, "████████      █████     █████        ██ ██ █ ████ ██ ██         █ █ █ █ █          █ █ █       █ █ █ █   ████████", ConsoleColor.Cyan);
            DrawAt(1, 27, "████████      █████     █████        ██    █    █ ██ ██         █████████          █████       ███████  █████████", ConsoleColor.Cyan);
            DrawAt(1, 28, "██████████████████████████████████████████████████████████████████████████████████████████████████████████████████", ConsoleColor.Black);

            DrawAt(1, 23, "████", ConsoleColor.DarkMagenta);
            DrawAt(1, 24, "██████", ConsoleColor.Magenta);
            DrawAt(1, 25, "███████", ConsoleColor.Magenta);
            DrawAt(1, 26, "████████", ConsoleColor.Magenta);
            DrawAt(1, 27, "████████", ConsoleColor.DarkMagenta);
            DrawAt(1, 28, "█████████", ConsoleColor.DarkMagenta);

            DrawAt(14, 24, "   █    ", ConsoleColor.DarkMagenta);
            DrawAt(14, 25, "  ███   ", ConsoleColor.Magenta);
            DrawAt(14, 26, " █████  ", ConsoleColor.Magenta);
            DrawAt(14, 27, " █████  ", ConsoleColor.Magenta);
            DrawAt(14, 28, "███████ ", ConsoleColor.DarkMagenta);

            DrawAt(24, 16, "   █   ", ConsoleColor.DarkMagenta);
            DrawAt(24, 17, "  ███  ", ConsoleColor.DarkMagenta);
            DrawAt(24, 18, " █████ ", ConsoleColor.DarkMagenta);
            DrawAt(24, 19, " █ █ █ ", ConsoleColor.Magenta);
            DrawAt(24, 20, " █████ ", ConsoleColor.Magenta);
            DrawAt(24, 21, " █ █ █ ", ConsoleColor.Magenta);
            DrawAt(24, 22, " █████ ", ConsoleColor.Magenta);
            DrawAt(24, 23, " █ █ █ ", ConsoleColor.Magenta);
            DrawAt(24, 24, " █████ ", ConsoleColor.Magenta);
            DrawAt(24, 25, " █ █ █ ", ConsoleColor.Magenta);
            DrawAt(24, 26, " █████ ", ConsoleColor.Magenta);
            DrawAt(24, 27, " █████ ", ConsoleColor.DarkMagenta);
            DrawAt(24, 28, "███████", ConsoleColor.DarkMagenta);

            DrawAt(37, 21, " ██████████████████  ", ConsoleColor.DarkMagenta);
            DrawAt(37, 22, " ██████████████████  ", ConsoleColor.DarkMagenta);
            DrawAt(37, 23, " ██ ██ █    █ ██ ██  ", ConsoleColor.Magenta);
            DrawAt(37, 24, " ██ ██ █ ████ ██ ██  ", ConsoleColor.Magenta);
            DrawAt(37, 25, " ██ ██ █    █    ██  ", ConsoleColor.Magenta);
            DrawAt(37, 26, " ██ ██ █ ████ ██ ██  ", ConsoleColor.Magenta);
            DrawAt(37, 27, " ██    █    █ ██ ██  ", ConsoleColor.Magenta);
            DrawAt(37, 28, "████████████████████ ", ConsoleColor.DarkMagenta);

            DrawAt(64, 18, " █████████ ", ConsoleColor.DarkMagenta);
            DrawAt(64, 19, " █████████ ", ConsoleColor.DarkMagenta);
            DrawAt(64, 20, " █ █ █ █ █ ", ConsoleColor.Magenta);
            DrawAt(64, 21, " █████████ ", ConsoleColor.Magenta);
            DrawAt(64, 22, " █ █ █ █ █ ", ConsoleColor.Magenta);
            DrawAt(64, 23, " █████████ ", ConsoleColor.Magenta);
            DrawAt(64, 24, " █ █ █ █ █ ", ConsoleColor.Magenta);
            DrawAt(64, 25, " █████████ ", ConsoleColor.Magenta);
            DrawAt(64, 26, " █ █ █ █ █ ", ConsoleColor.Magenta);
            DrawAt(64, 27, " █████████ ", ConsoleColor.Magenta);
            DrawAt(64, 28, "███████████", ConsoleColor.DarkMagenta);

            DrawAt(83, 17, "   ███ ", ConsoleColor.DarkMagenta);
            DrawAt(83, 18, "  ████ ", ConsoleColor.DarkMagenta);
            DrawAt(83, 19, " █████ ", ConsoleColor.DarkMagenta);
            DrawAt(83, 20, " █ █ █ ", ConsoleColor.Magenta);
            DrawAt(83, 21, " █████ ", ConsoleColor.Magenta);
            DrawAt(83, 22, " █ █ █ ", ConsoleColor.Magenta);
            DrawAt(83, 23, " █████ ", ConsoleColor.Magenta);
            DrawAt(83, 24, " █ █ █ ", ConsoleColor.Magenta);
            DrawAt(83, 25, " █████ ", ConsoleColor.Magenta);
            DrawAt(83, 26, " █ █ █ ", ConsoleColor.Magenta);
            DrawAt(83, 27, " █████ ", ConsoleColor.Magenta);
            DrawAt(83, 28, "███████", ConsoleColor.DarkMagenta);

            DrawAt(95, 17, "  █████  ", ConsoleColor.DarkMagenta);
            DrawAt(95, 18, "  █████  ", ConsoleColor.DarkMagenta);
            DrawAt(95, 19, " ███████ ", ConsoleColor.Magenta);
            DrawAt(95, 20, " █ █ █ █ ", ConsoleColor.Magenta);
            DrawAt(95, 21, " ███████ ", ConsoleColor.Magenta);
            DrawAt(95, 22, " █ █ █ █ ", ConsoleColor.Magenta);
            DrawAt(95, 23, " ███████ ", ConsoleColor.Magenta);
            DrawAt(95, 24, " █ █ █ █ ", ConsoleColor.Magenta);
            DrawAt(95, 25, " ███████ ", ConsoleColor.Magenta);
            DrawAt(95, 26, " █ █ █ █ ", ConsoleColor.Magenta);
            DrawAt(95, 27, " ███████ ", ConsoleColor.Magenta);
            DrawAt(95, 28, "█████████", ConsoleColor.DarkMagenta);

            DrawAt(105, 23, "     ████", ConsoleColor.Magenta);
            DrawAt(105, 24, "   ██████", ConsoleColor.Magenta);
            DrawAt(105, 25, "  ███████", ConsoleColor.Magenta);
            DrawAt(105, 26, " ████████", ConsoleColor.Magenta);
            DrawAt(105, 27, " ████████", ConsoleColor.DarkMagenta);
            DrawAt(105, 28, "█████████", ConsoleColor.DarkMagenta);

            DrawAt(114, 28, "█", ConsoleColor.Magenta);




            Thread.Sleep(250);
            DrawAt(1, 3, "               ███████╗███╗   ██╗████████╗███████╗██████╗     ███╗   ██╗ █████╗ ███╗   ███╗███████╗", ConsoleColor.DarkMagenta);
            Thread.Sleep(250);
            DrawAt(1, 4, "               ██╔════╝████╗  ██║╚══██╔══╝██╔════╝██╔══██╗    ████╗  ██║██╔══██╗████╗ ████║██╔════╝      ", ConsoleColor.DarkMagenta);
            Thread.Sleep(250);
            DrawAt(1, 5, "               █████╗  ██╔██╗ ██║   ██║   █████╗  ██████╔╝    ██╔██╗ ██║███████║██╔████╔██║█████╗", ConsoleColor.Magenta);
            Thread.Sleep(250);
            DrawAt(1, 6, "               ██╔══╝  ██║╚██╗██║   ██║   ██╔══╝  ██╔══██╗    ██║╚██╗██║██╔══██║██║╚██╔╝██║██╔══╝           ", ConsoleColor.Magenta);
            Thread.Sleep(250);
            DrawAt(1, 7, "               ███████╗██║ ╚████║   ██║   ███████╗██║  ██║    ██║ ╚████║██║  ██║██║ ╚═╝ ██║███████╗", ConsoleColor.DarkMagenta);
            Thread.Sleep(250);
            DrawAt(1, 8, "               ╚══════╝╚═╝  ╚═══╝   ╚═╝   ╚══════╝╚═╝  ╚═╝    ╚═╝  ╚═══╝╚═╝  ╚═╝╚═╝     ╚═╝╚══════╝", ConsoleColor.DarkMagenta);




        }
        /// <summary>
        /// Vẽ màn hình vào game 
        /// </summary>
        public static void LoadContent()
        {
            DrawHLineAt(0, 0, 115, '\u2588', 1, false, ConsoleColor.Red);
            DrawVLineAt(114, 0, 30, '\u2588', 1, false, ConsoleColor.Red);
            DrawHLineAt(114, 29, 115, '\u2588', 1, true, ConsoleColor.Red);
            DrawVLineAt(0, 29, 30, '\u2588', 1, true, ConsoleColor.Red);

            DrawAt(1, 1, @"██████                                                                                                         ", ConsoleColor.Gray);
            DrawAt(1, 2, @"██████        •          •   •                               •                               ████              ", ConsoleColor.White);
            DrawAt(1, 3, @"█████               •                      •                           ███████         ███  █    █      ███████", ConsoleColor.White);
            DrawAt(1, 4, @"████                                               ──                    █████        █   █ ████        █████  ", ConsoleColor.Yellow);
            DrawAt(1, 5, @"██                                                                        ████████     ███████ █    ████████   ", ConsoleColor.Gray);
            DrawAt(1, 6, @"                                          ──                ──             ██████████  █ ███████ ██████████    ", ConsoleColor.Yellow);
            DrawAt(1, 7, @"                        •                                                              ████████████          ", ConsoleColor.White);
            DrawAt(1, 8, @"                                                   ──                             █          ██          █     ", ConsoleColor.Yellow);
            DrawAt(1, 9, @"                                    ──                     ──      ──   ──    █████          ██          █████ ", ConsoleColor.Yellow);
            DrawAt(1, 10, @"                                           ─             ──                  ██████  ███ ██████████ ███ ██████ ", ConsoleColor.Yellow);
            DrawAt(1, 11, @"                                                           ──                      █████ █ █████ ██ █████      ", ConsoleColor.Yellow);
            DrawAt(1, 12, @"                                      ──           ──             ──         █████████   ████  ████   █████████", ConsoleColor.Yellow);
            DrawAt(1, 13, @"                                                                            ████████  ██████  ██████  ████████", ConsoleColor.Gray);
            DrawAt(1, 14, @"                                              ──                            ████          █    █          ████", ConsoleColor.Yellow);
            DrawAt(1, 15, @"                                   ──                  ──                                                     ", ConsoleColor.Yellow);
            DrawAt(1, 16, @"                                                               ──                                             ", ConsoleColor.Yellow);
            DrawAt(1, 17, @"                                                         ──                                                   ", ConsoleColor.Yellow);
            DrawAt(1, 18, @"                                                                                                              ", ConsoleColor.Gray);
            DrawAt(1, 19, @"                                                                                        ██                    ", ConsoleColor.Gray);
            DrawAt(1, 20, @"                       ███                                                              ██                    ", ConsoleColor.Gray);
            DrawAt(1, 21, @"                       ███                                                              ██                    ", ConsoleColor.Gray);
            DrawAt(1, 22, @"██                      █                              ██                               ██                    ", ConsoleColor.Gray);
            DrawAt(1, 23, @"████                    █                              ██                             █████                   ", ConsoleColor.Gray);
            DrawAt(1, 24, @"█████                  ███                             ██             •              ██████                   ", ConsoleColor.Gray);
            DrawAt(1, 25, @"██████               •█████                           ████                         █████████               ██████", ConsoleColor.DarkRed);
            DrawAt(1, 26, @"███████              ███████                         ██████                     █████████████             ███████", ConsoleColor.Red);
            DrawAt(1, 27, @"█████████     ██████████████████████       █████████████████████        █████████████████████████        ████████", ConsoleColor.DarkRed);
            DrawAt(1, 28, @"██████████   ████████████████████████     ███████████████████████      ███████████████████████████      █████████", ConsoleColor.DarkRed);

            DrawAt(13, 6, @"█", ConsoleColor.Yellow);
            DrawAt(13, 7, @" █", ConsoleColor.Yellow);
            DrawAt(13, 8, @" ██", ConsoleColor.Yellow);
            DrawAt(13, 9, @"████", ConsoleColor.Yellow);
            DrawAt(13, 10, @" ██", ConsoleColor.Yellow);
            DrawAt(13, 11, @" █", ConsoleColor.Yellow);
            DrawAt(13, 12, @"█", ConsoleColor.Yellow);



            DrawAt(72, 2, @"                      ████", ConsoleColor.DarkMagenta);
            DrawAt(72, 3, @"███████         ███  █    █      ███████", ConsoleColor.DarkMagenta);
            DrawAt(72, 4, @"  █████        █   █ ████        █████  ", ConsoleColor.DarkMagenta);
            DrawAt(72, 5, @"   ████████     ███████ █     ████████  ", ConsoleColor.Magenta);
            DrawAt(72, 6, @"    ██████████  █ ███████ ██████████    ", ConsoleColor.Magenta);
            DrawAt(72, 7, @"                ████████████            ", ConsoleColor.Magenta);
            DrawAt(72, 8, @"           █          ██          █     ", ConsoleColor.Magenta);
            DrawAt(72, 9, @"       █████          ██          █████ ", ConsoleColor.Magenta);
            DrawAt(72, 10, @"       ██████  ███ ██████████ ███ ██████", ConsoleColor.Magenta);
            DrawAt(72, 11, @"             █████ █ █████ ██ █████     ", ConsoleColor.Magenta);
            DrawAt(72, 12, @"       █████████   ████  ████   █████████", ConsoleColor.Magenta);
            DrawAt(72, 13, @"      ████████  ██████  ██████  ████████ ", ConsoleColor.DarkMagenta);
            DrawAt(72, 14, @"      ████          █    █          ████ ", ConsoleColor.DarkMagenta);


            DrawAt(1, 22, @"██", ConsoleColor.DarkRed);
            DrawAt(1, 23, @"████", ConsoleColor.DarkRed);
            DrawAt(1, 24, @"█████", ConsoleColor.Red);
            DrawAt(1, 25, @"██████", ConsoleColor.Red);
            DrawAt(1, 26, @"███████", ConsoleColor.Red);
            DrawAt(1, 27, @"█████████", ConsoleColor.DarkRed);
            DrawAt(1, 28, @"██████████", ConsoleColor.DarkRed);

            DrawAt(14, 20, @"          ███", ConsoleColor.DarkRed);
            DrawAt(14, 21, @"          ███", ConsoleColor.Red);
            DrawAt(14, 22, @"           █ ", ConsoleColor.DarkRed);
            DrawAt(14, 23, @"           █ ", ConsoleColor.Red);
            DrawAt(14, 24, @"          ███", ConsoleColor.Red);
            DrawAt(14, 25, @"         █████", ConsoleColor.Red);
            DrawAt(14, 26, @"        ███████", ConsoleColor.Red);
            DrawAt(14, 27, @" ██████████████████████", ConsoleColor.DarkRed);
            DrawAt(14, 28, @"████████████████████████", ConsoleColor.DarkRed);

            DrawAt(43, 22, @"             ██", ConsoleColor.DarkRed);
            DrawAt(43, 23, @"             ██", ConsoleColor.Red);
            DrawAt(43, 24, @"             ██", ConsoleColor.DarkRed);
            DrawAt(43, 25, @"            ████", ConsoleColor.Red);
            DrawAt(43, 26, @"           ██████", ConsoleColor.Red);
            DrawAt(43, 27, @" █████████████████████", ConsoleColor.DarkRed);
            DrawAt(43, 28, @"███████████████████████", ConsoleColor.DarkRed);

            DrawAt(72, 19, @"                 ██", ConsoleColor.DarkRed);
            DrawAt(72, 20, @"                 ██", ConsoleColor.Red);
            DrawAt(72, 21, @"                 ██", ConsoleColor.Red);
            DrawAt(72, 22, @"                 ██", ConsoleColor.DarkRed);
            DrawAt(72, 23, @"               █████", ConsoleColor.DarkRed);
            DrawAt(72, 24, @"              ██████", ConsoleColor.Red);
            DrawAt(72, 25, @"            █████████", ConsoleColor.Red);
            DrawAt(72, 26, @"         █████████████", ConsoleColor.Red);
            DrawAt(72, 27, @" █████████████████████████", ConsoleColor.DarkRed);
            DrawAt(72, 28, @"███████████████████████████", ConsoleColor.DarkRed);

            DrawAt(1, 1, @"██████", ConsoleColor.DarkRed);
            DrawAt(1, 2, @"██████", ConsoleColor.DarkRed);
            DrawAt(1, 3, @"█████", ConsoleColor.DarkRed);
            DrawAt(1, 4, @"████", ConsoleColor.DarkRed);
            DrawAt(1, 5, @"██", ConsoleColor.DarkRed);
            DrawAt(35, 22, @"Tip: W,S,D,A - di chuyển", ConsoleColor.White);
            DrawAt(35, 23, @"Space - Bắn", ConsoleColor.White);
            DrawAt(30, 19, @"Loading content", ConsoleColor.White);
            DrawHLineAt(45, 19, 20, '\u2588', 100, false, ConsoleColor.Yellow);    
            Thread.Sleep(1000);

        }
        /// <summary>
        /// Vẽ màn hình  câu chuyện 
        /// </summary>
        public static void LoadStory()
        {
            DrawHLineAt(0, 0, 115, '\u2588', 1, false, ConsoleColor.Red);
            DrawVLineAt(114, 0, 30, '\u2588', 1, false, ConsoleColor.Red);
            DrawHLineAt(114, 29, 115, '\u2588', 1, true, ConsoleColor.Red);
            DrawVLineAt(0, 29, 30, '\u2588', 1, true, ConsoleColor.Red);

            DrawAt(1, 1, @"████████            ♦               ♦                            ♦                                       ████████", ConsoleColor.Cyan);
            DrawAt(1, 2, @"███████            ♦               ♦                            ♦                                         ███████", ConsoleColor.Cyan);
            DrawAt(1, 3, @"█████                                                                                 ♦                     █████", ConsoleColor.Cyan);
            DrawAt(1, 4, @"███                                                  ♦                        ♦                               ███", ConsoleColor.Cyan);
            DrawAt(1, 5, @"█                                                                                                               █", ConsoleColor.Cyan);
            DrawAt(1, 6, @"█                                                                                                               █", ConsoleColor.Cyan);
            DrawAt(1, 7, @"█                  ♦                                                                            ♦               █", ConsoleColor.Cyan);
            DrawAt(1, 8, @"█                                                                                                               █", ConsoleColor.Cyan);
            DrawAt(1, 9, @"█                                                                                                               █", ConsoleColor.Cyan);
            DrawAt(1, 10, @"█      ♦                                                                                ♦                       █", ConsoleColor.Cyan);
            DrawAt(1, 11, @"█                                                                                    █   █   █                  █", ConsoleColor.Cyan);
            DrawAt(1, 12, @"█              ♦                                                                    ███  █  ███       ♦         █", ConsoleColor.Cyan);
            DrawAt(1, 13, @"█                                                                                  ██ ███████ ██                █", ConsoleColor.Cyan);
            DrawAt(1, 14, @"█                                                                                 ██   ██ ██   ██               █", ConsoleColor.Cyan);
            DrawAt(1, 15, @"█                                                                                 ██           ██               █", ConsoleColor.Cyan);
            DrawAt(1, 16, @"█                        ♦                                                        ██ ██     ██ ██   ♦           █", ConsoleColor.Cyan);
            DrawAt(1, 17, @"█                                                                                 ██  ██   ██  ██               █", ConsoleColor.Cyan);
            DrawAt(1, 18, @"█                                                                                 ██   █████   ██               █", ConsoleColor.Cyan);
            DrawAt(1, 19, @"█        ♦                                                                        ██    ███    ██               █", ConsoleColor.Cyan);
            DrawAt(1, 20, @"█                    ♦        ♦                         ♦             ♦           ███    █    ███               █", ConsoleColor.Cyan);
            DrawAt(1, 21, @"█                                                                                  ███       ███                █", ConsoleColor.Cyan);
            DrawAt(1, 22, @"█                                                                                   ███     ███                 █", ConsoleColor.Cyan);
            DrawAt(1, 23, @"█                                                                                    ███   ███                  █", ConsoleColor.Cyan);
            DrawAt(1, 24, @"█                         ♦              ♦                                             █████                    █", ConsoleColor.Cyan);
            DrawAt(1, 25, @"█                                                                                                               █", ConsoleColor.Cyan);
            DrawAt(1, 26, @"██                                                                                                             ██", ConsoleColor.Cyan);
            DrawAt(1, 27, @"███                                                                                                           ███", ConsoleColor.Cyan);
            DrawAt(1, 28, @"████                                                                                                         ████", ConsoleColor.Cyan);


            DrawAt(16, 5, @"          ███", ConsoleColor.Blue);
            DrawAt(16, 6, @"         █  █", ConsoleColor.Blue);
            DrawAt(16, 7, @"        █  █", ConsoleColor.Blue);
            DrawAt(16, 8, @"       █  █", ConsoleColor.Blue);
            DrawAt(16, 9, @" █    █  █", ConsoleColor.Blue);
            DrawAt(16, 10, @"  █  █  █", ConsoleColor.Blue);
            DrawAt(16, 11, @"   ██  █", ConsoleColor.Blue);
            DrawAt(16, 12, @"   ████", ConsoleColor.Blue);
            DrawAt(16, 13, @"   █████", ConsoleColor.Blue);
            DrawAt(16, 14, @"  ███   █", ConsoleColor.Blue);
            DrawAt(16, 15, @" ███     █", ConsoleColor.Blue);
            DrawAt(16, 16, @"███", ConsoleColor.Blue);
            DrawAt(16, 17, @"██", ConsoleColor.Blue);

            DrawAt(83, 11, @"   █   █   █", ConsoleColor.White);
            DrawAt(83, 12, @"  ███  █  ███", ConsoleColor.White);
            DrawAt(83, 13, @" ██ ███████ ██", ConsoleColor.White);
            DrawAt(83, 14, @"██   ██ ██   ██", ConsoleColor.White);
            DrawAt(83, 15, @"██           ██", ConsoleColor.White);
            DrawAt(83, 16, @"██           ██", ConsoleColor.White);
            DrawAt(83, 17, @"██           ██", ConsoleColor.White);
            DrawAt(83, 18, @"██           ██", ConsoleColor.White);
            DrawAt(83, 19, @"██           ██", ConsoleColor.White);
            DrawAt(83, 20, @"███         ███", ConsoleColor.White);
            DrawAt(83, 21, @" ███       ███", ConsoleColor.White);
            DrawAt(83, 22, @"  ███     ███", ConsoleColor.White);
            DrawAt(83, 23, @"   ███   ███", ConsoleColor.White);
            DrawAt(83, 24, @"     █████", ConsoleColor.White);

            DrawAt(86, 16, @"██     ██", ConsoleColor.Red);
            DrawAt(86, 17, @" ██   ██ ", ConsoleColor.Red);
            DrawAt(86, 18, @"  █████  ", ConsoleColor.Red);
            DrawAt(86, 19, @"   ███   ", ConsoleColor.Red);
            DrawAt(86, 20, @"    █    ", ConsoleColor.Red);

            Thread.Sleep(650);

            DrawAt(36, 9, @"Hành tinh của chúng tôi đã bị phá hủy", ConsoleColor.DarkYellow);
            Thread.Sleep(1050);
            DrawAt(43, 10, @"không còn lại gì cả", ConsoleColor.DarkYellow);
            Thread.Sleep(1050);
            DrawAt(36, 11, @"bạn là hi vọng cuối cùng của chúng tôi", ConsoleColor.DarkYellow);
            Thread.Sleep(1050);
            DrawAt(43, 12, @"Chúc bạn may mắn", ConsoleColor.DarkYellow);
            Thread.Sleep(1050);
        }
        #endregion

        #region Phương thức xóa

        /// <summary>
        /// Xóa một ký tự tại vị trí đã cho
        /// </summary>
        /// <param name="x">Số cột</param>
        /// <param name="y">Số hàng</param>
        public static void ClearAtPosition(int x, int y)
        {
            try
            {
                Console.SetCursorPosition(x, y);
                Console.Write(' ');
            }
            catch { return; }

        }

        /// <summary>
        /// Xóa một ký tự tại vị trí đã cho
        /// </summary>
        /// <param name="point">Điểm Point2D cần xóa</param>
        public static void ClearAtPosition(Point2D point)
        {
            try
            {
                Console.SetCursorPosition(point.X, point.Y);
                Console.Write(' ');
            }
            catch { return; }
        }

        /// <summary>
        /// Xóa một vùng từ tọa độ bắt đầu đến tọa độ kết thúc
        /// </summary>
        /// <param name="fromX">Số cột bắt đầu</param>
        /// <param name="fromY">Số hàng bắt đầu</param>
        /// <param name="toX">Số cột kết thúc</param>
        /// <param name="toY">Số hàng kết thúc</param>
        public static void ClearFromTo(int fromX, int fromY, int toX, int toY)
        {
            try
            {
                Console.SetCursorPosition(fromX, fromY);
                string x = new string(' ', toX - fromX);
                for (int i = fromY; i < toY; i++)
                {
                    Console.WriteLine(x);
                }
            }
            catch { return; }
        }

        /// <summary>
        /// Xóa một vùng từ tọa độ bắt đầu đến tọa độ kết thúc
        /// </summary>
        /// <param name="startingPoint">Điểm bắt đầu Point2D</param>
        /// <param name="endingPoint">Điểm kết thúc Point2D</param>
        public static void ClearFromTo(Point2D startingPoint, Point2D endingPoint)
        {
            try
            {
                ClearFromTo(startingPoint.X, startingPoint.Y, endingPoint.X, endingPoint.Y);
            }
            catch { return; }
        }

        /// <summary>
        /// Xóa toàn bộ một hàng tại vị trí đã cho
        /// </summary>
        /// <param name="y">Số hàng</param>
        public static void ClearY(int y)
        {
            try
            {
                int gameWidth = 115; // Nên được gán từ một hằng số ở đâu đó
                for (int i = 0; i < gameWidth; i++)
                {
                    DrawAt(i, y, ' ');
                }
            }
            catch { return; }
        }

        /// <summary>
        /// Xóa toàn bộ một cột tại vị trí đã cho
        /// </summary>
        /// <param name="x">Số cột</param>
        public static void ClearX(int x)
        {
            try
            {
                int gameHeight = 30; // Nên được gán từ một hằng số ở đâu đó
                for (int i = 0; i < gameHeight; i++)
                {
                    DrawAt(x, i, ' ');
                }
            }
            catch { return; }

        }
        #endregion

    }
}