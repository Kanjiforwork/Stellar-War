using System;
using System.Collections.Generic;
using TeamWork.Field;
using System.Threading;
using System.Linq;

namespace TeamWork.Objects
{
    public class GameObject : Entity
    {/// <summary>
     /// Loại đối tượng GameObject - mỗi loại có ngoại hình, mạng sống, phát hiện va chạm, điểm số và hiệu ứng khác nhau
     /// </summary>
        public enum ObjectType
        {
            Bullet,
            Enemy1,
            Enemy2,
            Enemy3,
            Enemy4,
            Meteor1,
            Meteor2,
            Meteor3
        }

        public ObjectType objectType;
        public int life;
        /// <summary>
        /// Hàm khởi tạo cơ bản
        /// </summary>
        public GameObject()
        {
            base.Speed = 1;
            /*  lastFireTime = DateTime.Now; // Khởi tạo thời gian bắn cuối cùng
       IsCharging = false; // Khởi tạo là chưa sạc   */
        }


        /// <summary>
        /// Hàm khởi tạo với điểm Point2D được gán (loại đạn)
        /// </summary>
        /// <param name="point">Điểm để tạo đối tượng</param>
        public GameObject(Point2D point)
            : base(point)
        {
            base.Speed = 1;
            base.Point = point;
            objectType = ObjectType.Bullet;
            base.Down = false;
        }


        /// <summary>
        /// Hàm khởi tạo với điểm Point2D được gán với loại đã cho
        /// </summary>
        /// <param name="point">Điểm để tạo đối tượng</param>
        /// <param name="type">Loại đối tượng</param>
        public GameObject(Point2D point, int type)
            : base(point)
        {
            base.Speed = 1;
            base.Point = point;
            objectType = (ObjectType)type;
        }
        /// <summary>
        /// Hàm khởi tạo với loại đối tượng, mọi thứ khác sử dụng giá trị mặc định cho loại đã cho
        /// </summary>
        /// <param name="type">Loại đối tượng</param>
        public GameObject(int type)
        {
            base.Speed = 1;
            objectType = (ObjectType)type;

            switch (objectType)
            {
                case ObjectType.Enemy1:
                    life = 7;
                    base.Point = new Point2D(Engine.Rnd.Next(Engine.WindowWidth - 4), 5);
                    this.Down = true;
                    break;
                case ObjectType.Enemy2:
                    life = 7;
                    base.Point = new Point2D(Engine.WindowWidth - 2, Engine.Rnd.Next(6, Engine.WindowHeight / 2 - 4));
                    this.Down = false;
                    break;
                case ObjectType.Enemy3:
                    life = 7;
                    base.Point = new Point2D(Engine.WindowWidth - 2, Engine.Rnd.Next(6, Engine.WindowHeight / 2 - 4));
                    this.Down = false;
                    break;
                case ObjectType.Enemy4:
                    life = 7;
                    base.Point = new Point2D(Engine.WindowWidth - 2, Engine.Rnd.Next(6, Engine.WindowHeight / 2 - 4));
                    this.Down = false;
                    break;
                case ObjectType.Meteor1:
                case ObjectType.Meteor2:
                case ObjectType.Meteor3:
                    life = 5;
                    if (Engine.Rnd.Next(2) == 0)
                    {
                        base.Point = new Point2D(Engine.Rnd.Next(Engine.WindowWidth - 4), 5);
                        this.Down = true;
                    }
                    else
                    {
                        base.Point = new Point2D(Engine.WindowWidth - 2, Engine.Rnd.Next(Engine.WindowHeight / 2, Engine.WindowHeight - 4));
                        this.Down = false;
                    }
                    break;

            }
        }

        public bool toBeDeleted; // Kích hoạt để xóa đối tượng này
        private bool Moveable = true; // Tình trạng di chuyển có thể bật/tắt
        public override string ToString()
        {
            return string.Format("Object type:{0}, X:{1}, Y:{2},Moveable:{3}", objectType, Point.X, Point.Y, Moveable);
        }


        private int Frames = 1;// Bộ đếm khung hình cho hiệu ứng nổ và một số tính toán
        private Point2D diagonalInc = new Point2D(1, 1); // Điểm Point2D trợ giúp tính toán đường chéo
        private Point2D diagonalDec = new Point2D(-1, 1); // Điểm Point2D hỗ trợ tính toán đường chéo
        private Point2D upRight; // Điểm chéo lên phải để lưu trữ hiệu ứng nổ
        private Point2D upLeft; // Điểm chéo lên trái để lưu trữ hiệu ứng nổ
        private Point2D downLeft; // Điểm chéo xuống trái để lưu trữ hiệu ứng nổ
        private Point2D downRight; // Điểm chéo xuống phải để lưu trữ hiệu ứng nổ

        private int projectileCounter = 1; // Bộ đếm để kiểm tra xem kẻ địch có nên bắn hay không
        private int projectileChance = Engine.Rnd.Next(20, 50); // Cơ hội ngẫu nhiên để kẻ địch bắn đạn

        public bool GotHit = false; // Công tắc bật/tắt hỗ trợ hiệu ứng nổ
                                    /// <summary>
                                    /// In đối tượng dựa trên loại của nó
                                    /// </summary>
        public void PrintObject()
        {
            switch (objectType)
            {
                case ObjectType.Bullet:
                    if (this.Point.Y > Engine.WindowHeight - 2)
                    {
                        return;
                    }
                    else
                    {

                        Printing.DrawAt(this.Point, '█', ConsoleColor.Yellow); // Standart print for bullets
                        break;
                    }
                case ObjectType.Enemy1:
                    if (!this.GotHit)
                    {
                        if (projectileCounter % projectileChance == 0) // Check if enemy has to shoot
                        {
                            // Nếu đúng, tạo một đạn trong danh sách đạn chính

                        }
                        else
                        {
                            // Nếu sai, chỉ tăng bộ đếm
                            projectileCounter++;
                        }
                        if (this.Point.X + 2 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X, Point.Y, @"  █  █", ConsoleColor.Magenta);
                        }
                        else if (this.Point.X + 3 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X, Point.Y - 1, @" ███████", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y, @"  █ █ █", ConsoleColor.Magenta);
                        }
                        else if (this.Point.X + 4 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X + 1, Point.Y - 2, @"  █████", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y - 1, @"  █ █ █", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y, @"   █ █", ConsoleColor.Magenta);
                        }
                        else
                        {
                            Printing.DrawAt(this.Point.X, Point.Y - 4, @" █     █", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X, Point.Y - 3, @" ███████", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y - 2, @"  █ █ █", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y - 1, @"  █████", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y, @"   █ █", ConsoleColor.Magenta);
                        }
                    }
                    else
                    {
                        upLeft = this.Point - diagonalInc * Frames;
                        upRight = this.Point - diagonalDec * Frames;
                        downRight = this.Point + diagonalInc * Frames;
                        downLeft = this.Point + diagonalDec * Frames;
                        PrintAndClearExplosion(false);
                    }
                    break;
                case ObjectType.Enemy2:
                    if (!this.GotHit)
                    {
                        if (projectileCounter % projectileChance == 0) // Kiểm tra xem kẻ địch có phải bắn hay không
                        {
                            // Nếu đúng, tạo một đạn trong danh sách đạn chính
                            Engine._objectProjectiles.Add(new GameObject(new Point2D(this.Point.X - 1, this.Point.Y), 0));
                            projectileCounter++; // Tăng bộ đếm
                        }
                        else
                        {
                            //Nếu sai chỉ tăng bộ đếm
                            projectileCounter++;
                        }
                        if (this.Point.X + 2 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X, Point.Y, " █", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "██", ConsoleColor.DarkRed);
                        }
                        if (this.Point.X + 3 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X, Point.Y, " ██", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "███", ConsoleColor.DarkRed);
                        }
                        if (this.Point.X + 4 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X, Point.Y, " ██ ", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "████", ConsoleColor.DarkRed);
                            Printing.DrawAt(this.Point.X + 3, Point.Y - 1, "█", ConsoleColor.DarkRed);
                        }
                        if (this.Point.X + 5 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X, Point.Y, " ██ █", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "█████", ConsoleColor.DarkRed);
                            Printing.DrawAt(this.Point.X + 3, Point.Y - 1, "█ ", ConsoleColor.DarkRed);
                        }
                        if (this.Point.X + 6 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X, Point.Y, " ██ ██", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "██████", ConsoleColor.DarkRed);
                            Printing.DrawAt(this.Point.X + 3, Point.Y - 1, "█  ", ConsoleColor.DarkRed);
                        }
                        if (this.Point.X + 7 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X, Point.Y, " ██ ██ ", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "███████", ConsoleColor.DarkRed);
                            Printing.DrawAt(this.Point.X + 3, Point.Y - 1, "█   ", ConsoleColor.DarkRed);
                        }
                        else
                        {
                            Printing.DrawAt(this.Point.X, Point.Y, " ██ ██ ", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "███████", ConsoleColor.DarkRed);
                            Printing.DrawAt(this.Point.X + 3, Point.Y - 1, "█   ", ConsoleColor.DarkRed);
                        }
                    }
                    else
                    {
                        upLeft = this.Point - diagonalInc * Frames;
                        upRight = this.Point - diagonalDec * Frames;
                        downRight = this.Point + diagonalInc * Frames;
                        downLeft = this.Point + diagonalDec * Frames;
                        PrintAndClearExplosion(false);
                    }
                    break;
                case ObjectType.Enemy3:
                    if (!this.GotHit)
                    {
                        if (projectileCounter % projectileChance == 0) // Kiểm tra enemy có phải bắn hay không 
                        {
                            // Nếu đúng, tạo một đạn trong danh sách đạn chính
                            Engine._objectProjectiles.Add(new GameObject(new Point2D(this.Point.X - 1, this.Point.Y), 0));
                            projectileCounter++; //Tăng bộ đếm
                        }
                        else
                        {
                            // Nếu sai, chỉ tăng bộ đếm
                            projectileCounter++;
                        }

                        #region Enemy Entry animation
                        if (this.Point.X + 2 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "█ ", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y, "██", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y - 1, " █", ConsoleColor.Magenta);
                        }
                        if (this.Point.X + 3 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "█ █", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y, "██ ", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y - 1, " ██", ConsoleColor.Magenta);
                            Printing.DrawAt(this.Point.X + 2, Point.Y + 2, "█", ConsoleColor.Yellow);
                        }
                        if (this.Point.X + 4 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X + 2, Point.Y + 2, "█", ConsoleColor.Magenta);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "█ ██", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y, "██ █", ConsoleColor.DarkRed);
                            Printing.DrawAt(this.Point.X, Point.Y - 1, " ███", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X + 3, Point.Y - 2, "█", ConsoleColor.Magenta);
                        }
                        if (this.Point.X + 5 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X + 2, Point.Y + 2, "█ █", ConsoleColor.Magenta);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "█ ███", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y, "██ █ ", ConsoleColor.DarkRed);
                            Printing.DrawAt(this.Point.X, Point.Y - 1, " ████", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X + 3, Point.Y - 2, "█ ", ConsoleColor.Magenta);
                        }
                        if (this.Point.X + 6 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X + 2, Point.Y + 2, "█ █ ", ConsoleColor.Magenta);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "█ ███ ", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y, "██ █ █", ConsoleColor.DarkRed);
                            Printing.DrawAt(this.Point.X, Point.Y - 1, " █████", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X + 3, Point.Y - 2, "█  ", ConsoleColor.Magenta);
                        }
                        if (this.Point.X + 7 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X + 2, Point.Y + 2, "█ █  ", ConsoleColor.Magenta);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "█ ███ █", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y, "██ █ ██", ConsoleColor.DarkRed);
                            Printing.DrawAt(this.Point.X, Point.Y - 1, " █████ ", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X + 3, Point.Y - 2, "█   ", ConsoleColor.Magenta);
                        }
                        else
                        {
                            Printing.DrawAt(this.Point.X + 2, Point.Y + 2, "█ █  ", ConsoleColor.Magenta);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "█ ███ █", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y, "██ █ ██", ConsoleColor.DarkRed);
                            Printing.DrawAt(this.Point.X, Point.Y - 1, " █████ ", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X + 3, Point.Y - 2, "█   ", ConsoleColor.Magenta);
                        }
                        #endregion
                    }
                    else
                    {
                        upLeft = this.Point - diagonalInc * Frames;
                        upRight = this.Point - diagonalDec * Frames;
                        downRight = this.Point + diagonalInc * Frames;
                        downLeft = this.Point + diagonalDec * Frames;
                        PrintAndClearExplosion(false);
                    }
                    break;
                case ObjectType.Enemy4:


                    if (!this.GotHit)
                    {
                        if (projectileCounter % projectileChance == 0) // Kiểm tra xem kẻ địch có phải bắn hay không
                        {
                            // Nếu đúng, tạo một đạn trong danh sách đạn chính
                            Engine._objectProjectiles.Add(new GameObject(new Point2D(this.Point.X - 1, this.Point.Y), 0));
                            projectileCounter++; // Increase the counter
                        }
                        else
                        {
                            // Nếu sai, chỉ tăng bộ đếm
                            projectileCounter++;
                        }

                        #region Quadcopter Entry animation

                        // Điều này làm cho sự xuất hiện của máy bay trực thăng trơn tru, không phải xuất hiện tức thời ở giữa màn hình
                        if (this.Point.X + 2 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X, Point.Y - 1, "██", ConsoleColor.Red);
                            Printing.DrawAt(this.Point, "█ ", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "██", ConsoleColor.Magenta);
                        }
                        else if (this.Point.X + 3 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X + 1, Point.Y - 2, "█", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X, Point.Y - 1, "██", ConsoleColor.Red);
                            Printing.DrawAt(this.Point, "█ ", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "██", ConsoleColor.Magenta);
                        }
                        else if (this.Point.X + 4 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X + 2, Point.Y - 2, "█", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X, Point.Y - 1, "███", ConsoleColor.Red);
                            Printing.DrawAt(this.Point, "█ █", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "███", ConsoleColor.Magenta);
                            Printing.DrawAt(this.Point.X + 1, Point.Y + 2, "█", ConsoleColor.DarkMagenta);
                        }
                        else if (this.Point.X + 5 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X + 2, Point.Y - 2, "█ ", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X, Point.Y - 1, "████", ConsoleColor.Red);
                            Printing.DrawAt(this.Point, "█ ██", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "████", ConsoleColor.Magenta);
                            Printing.DrawAt(this.Point.X + 1, Point.Y + 2, "█ ", ConsoleColor.DarkMagenta);
                        }
                        else if (this.Point.X + 6 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X + 2, Point.Y - 2, "█ █", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X, Point.Y - 1, "█████", ConsoleColor.Red);
                            Printing.DrawAt(this.Point, "█ ██ ", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "█████", ConsoleColor.Magenta);
                            Printing.DrawAt(this.Point.X + 1, Point.Y + 2, "█ █", ConsoleColor.DarkMagenta);
                        }
                        else if (this.Point.X + 7 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X + 2, Point.Y - 2, "█ █ ", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X, Point.Y - 1, "██████", ConsoleColor.Red);
                            Printing.DrawAt(this.Point, "█ ███ ", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "██████", ConsoleColor.Magenta);
                            Printing.DrawAt(this.Point.X + 1, Point.Y + 2, "█ █ ", ConsoleColor.DarkMagenta);
                        }
                        else
                        {
                            Printing.DrawAt(this.Point.X + 2, Point.Y - 2, "█ █  ", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X, Point.Y - 1, "███████", ConsoleColor.Red);
                            Printing.DrawAt(this.Point, "█ ███ █", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "███████", ConsoleColor.Magenta);
                            Printing.DrawAt(this.Point.X + 1, Point.Y + 2, "█ █ █", ConsoleColor.DarkMagenta);
                        }
                        #endregion

                    }
                    else
                    {
                        upLeft = this.Point - diagonalInc * Frames;
                        upRight = this.Point - diagonalDec * Frames;
                        downRight = this.Point + diagonalInc * Frames;
                        downLeft = this.Point + diagonalDec * Frames;
                        PrintAndClearExplosion(false);
                    }
                    break;
                case ObjectType.Meteor1:
                    if (!this.GotHit)
                    {
                        Printing.DrawAt(this.Point.X, Point.Y - 1, @"███", ConsoleColor.DarkRed);
                        Printing.DrawAt(this.Point.X, Point.Y, @"████", ConsoleColor.Red);
                    }
                    else
                    {
                        upLeft = this.Point - diagonalInc * Frames;
                        upRight = this.Point - diagonalDec * Frames;
                        downRight = this.Point + diagonalInc * Frames;
                        downLeft = this.Point + diagonalDec * Frames;
                        PrintAndClearExplosion(false);
                    }
                    break;
                case ObjectType.Meteor2:
                    if (!this.GotHit)
                    {
                        Printing.DrawAt(this.Point.X + 1, Point.Y - 2, @"███", ConsoleColor.DarkMagenta);
                        Printing.DrawAt(this.Point.X, Point.Y - 1 , @"████", ConsoleColor.Magenta);
                        Printing.DrawAt(this.Point.X, Point.Y, @"██", ConsoleColor.DarkMagenta);
                    }
                    else
                    {
                        upLeft = this.Point - diagonalInc * Frames;
                        upRight = this.Point - diagonalDec * Frames;
                        downRight = this.Point + diagonalInc * Frames;
                        downLeft = this.Point + diagonalDec * Frames;
                        PrintAndClearExplosion(false);
                    }
                    break;
                case ObjectType.Meteor3:
                    if (!this.GotHit)
                    {
                        Printing.DrawAt(this.Point.X + 1, Point.Y - 2, @"██", ConsoleColor.DarkGray);
                        Printing.DrawAt(this.Point.X, Point.Y - 1 , @"████", ConsoleColor.Gray);
                        Printing.DrawAt(this.Point.X, Point.Y, @"███", ConsoleColor.DarkGray);
                    }
                    else
                    {
                        upLeft = this.Point - diagonalInc * Frames;
                        upRight = this.Point - diagonalDec * Frames;
                        downRight = this.Point + diagonalInc * Frames;
                        downLeft = this.Point + diagonalDec * Frames;
                        PrintAndClearExplosion(false);
                    }
                    break;
            }
        }

        /// <summary>
        /// Xóa đối tượng dựa trên loại của nó
        /// </summary>
        public void ClearObject()
        {
            switch (objectType)
            {
                case ObjectType.Bullet:
                    Printing.DrawAt(this.Point.X, this.Point.Y, ' ');
                    Engine.Player.Print();
                    break;
                case ObjectType.Enemy1:
                    #region Enemy1 object clearing and breaking effect
                    if (!this.GotHit)
                    {
                        if (this.Point.X + 2 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X, Point.Y, @"      ");
                        }
                        else if (this.Point.X + 3 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X, Point.Y - 1, @"        ");
                            Printing.DrawAt(this.Point.X, Point.Y, @"       ");
                        }
                        else if (this.Point.X + 4 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X + 1, Point.Y - 2, @"       ");
                            Printing.DrawAt(this.Point.X, Point.Y - 1, @"       ");
                            Printing.DrawAt(this.Point.X, Point.Y, @"      ");
                        }
                        else
                        {
                            Printing.DrawAt(this.Point.X, Point.Y - 4, @"        ");
                            Printing.DrawAt(this.Point.X, Point.Y - 3, @"        ");
                            Printing.DrawAt(this.Point.X, Point.Y - 2, @"       ");
                            Printing.DrawAt(this.Point.X, Point.Y - 1, @"       ");
                            Printing.DrawAt(this.Point.X, Point.Y, @"      ");
                        }
                    }
                    else
                    {
                        upLeft = this.Point - diagonalInc * Frames;
                        upRight = this.Point - diagonalDec * Frames;
                        downRight = this.Point + diagonalInc * Frames;
                        downLeft = this.Point + diagonalDec * Frames;
                        PrintAndClearExplosion(true);
                        if (Frames == 5)
                        {
                            this.toBeDeleted = true;
                            Engine.Player.IncreasePoints(10);

                            Menu.Table();
                            Menu.UIDescription();
                        }
                        Frames++;
                    }
                    #endregion
                    break;
                case ObjectType.Enemy2:
                    #region Enemy2 object clearing and breaking effect math
                    if (!this.GotHit)
                    {
                        if (this.Point.X + 2 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X, Point.Y, "  ");
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "  ");
                        }
                        if (this.Point.X + 3 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X, Point.Y, "   ");
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "   ");
                        }
                        if (this.Point.X + 4 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X, Point.Y, "    ");
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "    ");
                            Printing.DrawAt(this.Point.X + 3, Point.Y - 1, " ");
                        }
                        if (this.Point.X + 5 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X, Point.Y, "     ");
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "     ");
                            Printing.DrawAt(this.Point.X + 3, Point.Y - 1, "  ");
                        }
                        if (this.Point.X + 6 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X, Point.Y, "      ");
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "      ");
                            Printing.DrawAt(this.Point.X + 3, Point.Y - 1, "   ");
                        }
                        if (this.Point.X + 7 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X, Point.Y, "       ");
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "       ");
                            Printing.DrawAt(this.Point.X + 3, Point.Y - 1, "    ");
                        }
                        else
                        {
                            Printing.DrawAt(this.Point.X, Point.Y, "       ");
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "       ");
                            Printing.DrawAt(this.Point.X + 3, Point.Y - 1, "    ");
                        }
                    }
                    else
                    {
                        upLeft = this.Point - diagonalInc * Frames;
                        upRight = this.Point - diagonalDec * Frames;
                        downRight = this.Point + diagonalInc * Frames;
                        downLeft = this.Point + diagonalDec * Frames;
                        PrintAndClearExplosion(true);
                        if (Frames == 5)
                        {
                            this.toBeDeleted = true;
                            Engine.Player.IncreasePoints(10);

                            Menu.Table();
                            Menu.UIDescription();
                        }
                        Frames++;
                    }
                    #endregion
                    break;
                case ObjectType.Enemy3:
                    #region Enemy3 object clear and breaking effect math
                    if (!GotHit)
                    {

                        if (this.Point.X + 2 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "  ");
                            Printing.DrawAt(this.Point.X, Point.Y, "  ");
                            Printing.DrawAt(this.Point.X, Point.Y - 1, "  ");
                        }
                        if (this.Point.X + 3 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "   ");
                            Printing.DrawAt(this.Point.X, Point.Y, "   ");
                            Printing.DrawAt(this.Point.X, Point.Y - 1, "   ");
                            Printing.DrawAt(this.Point.X + 2, Point.Y + 2, " ");
                        }
                        if (this.Point.X + 4 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X + 2, Point.Y + 2, " ");
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "    ");
                            Printing.DrawAt(this.Point.X, Point.Y, "    ");
                            Printing.DrawAt(this.Point.X, Point.Y - 1, "    ");
                            Printing.DrawAt(this.Point.X + 3, Point.Y - 2, " ");
                        }
                        if (this.Point.X + 5 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X + 2, Point.Y + 2, "   ");
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "     ");
                            Printing.DrawAt(this.Point.X, Point.Y, "     ");
                            Printing.DrawAt(this.Point.X, Point.Y - 1, "     ");
                            Printing.DrawAt(this.Point.X + 3, Point.Y - 2, "  ");
                        }
                        if (this.Point.X + 6 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X + 2, Point.Y + 2, "    ");
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "      ");
                            Printing.DrawAt(this.Point.X, Point.Y, "      ");
                            Printing.DrawAt(this.Point.X, Point.Y - 1, "      ");
                            Printing.DrawAt(this.Point.X + 3, Point.Y - 2, "   ");
                        }
                        if (this.Point.X + 7 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X + 2, Point.Y + 2, "     ");
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "       ");
                            Printing.DrawAt(this.Point.X, Point.Y, "       ");
                            Printing.DrawAt(this.Point.X, Point.Y - 1, "       ");
                            Printing.DrawAt(this.Point.X + 3, Point.Y - 2, "    ");
                        }
                        else
                        {
                            Printing.DrawAt(this.Point.X + 2, Point.Y + 2, "     ");
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "       ");
                            Printing.DrawAt(this.Point.X, Point.Y, "       ");
                            Printing.DrawAt(this.Point.X, Point.Y - 1, "       ");
                            Printing.DrawAt(this.Point.X + 3, Point.Y - 2, "    ");
                        }
                    }
                    else
                    {
                        upLeft = this.Point - diagonalInc * Frames;
                        upRight = this.Point - diagonalDec * Frames;
                        downRight = this.Point + diagonalInc * Frames;
                        downLeft = this.Point + diagonalDec * Frames;
                        PrintAndClearExplosion(true);
                        if (Frames == 5)
                        {
                            this.toBeDeleted = true;
                            Engine.Player.IncreasePoints(10);

                            Menu.Table();
                            Menu.UIDescription();
                        }
                        Frames++;
                    }
                    #endregion
                    break;
                case ObjectType.Enemy4:
                    #region Enemy4 object clear and breaking effect
                    if (!GotHit)
                    {
                        if (this.Point.X + 2 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X, Point.Y - 1, "  ");
                            Printing.DrawAt(this.Point, "  ");
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "   ");
                        }
                        else if (this.Point.X + 3 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X + 1, Point.Y - 2, " ");
                            Printing.DrawAt(this.Point.X, Point.Y - 1, "  ");
                            Printing.DrawAt(this.Point, "  ");
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "  ");
                        }
                        else if (this.Point.X + 4 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X + 2, Point.Y - 2, " ");
                            Printing.DrawAt(this.Point.X, Point.Y - 1, "   ");
                            Printing.DrawAt(this.Point, "   ");
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "   ");
                            Printing.DrawAt(this.Point.X + 1, Point.Y + 2, " ");
                        }
                        else if (this.Point.X + 5 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X + 2, Point.Y - 2, "  ");
                            Printing.DrawAt(this.Point.X, Point.Y - 1, "    ");
                            Printing.DrawAt(this.Point, "    ");
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "    ");
                            Printing.DrawAt(this.Point.X + 1, Point.Y + 2, "  ");
                        }
                        else if (this.Point.X + 6 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X + 2, Point.Y - 2, "   ");
                            Printing.DrawAt(this.Point.X, Point.Y - 1, "     ");
                            Printing.DrawAt(this.Point, "     ");
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "     ");
                            Printing.DrawAt(this.Point.X + 1, Point.Y + 2, "   ");
                        }
                        else if (this.Point.X + 7 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X + 2, Point.Y - 2, "    ");
                            Printing.DrawAt(this.Point.X, Point.Y - 1, "      ");
                            Printing.DrawAt(this.Point, "      ");
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "      ");
                            Printing.DrawAt(this.Point.X + 1, Point.Y + 2, "    ");
                        }
                        else
                        {
                            Printing.DrawAt(this.Point.X + 2, Point.Y - 2, "     ");
                            Printing.DrawAt(this.Point.X, Point.Y - 1, "       ");
                            Printing.DrawAt(this.Point, "       ");
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "       ");
                            Printing.DrawAt(this.Point.X + 1, Point.Y + 2, "     ");
                        }
                    }
                    else
                    {
                        upLeft = this.Point - diagonalInc * Frames;
                        upRight = this.Point - diagonalDec * Frames;
                        downRight = this.Point + diagonalInc * Frames;
                        downLeft = this.Point + diagonalDec * Frames;
                        PrintAndClearExplosion(true);
                        if (Frames == 5)
                        {
                            this.toBeDeleted = true;
                            Engine.Player.IncreasePoints(10);

                            Menu.Table();
                            Menu.UIDescription();
                        }
                        Frames++;
                    }
                    #endregion
                    break;
                case ObjectType.Meteor1:
                    #region Small object clearing and breaking effect
                    if (!this.GotHit)
                    {
                        Printing.DrawAt(this.Point.X, Point.Y - 1, @"   ");
                        Printing.DrawAt(this.Point.X, Point.Y, @"    ");
                    }
                    else
                    {
                        upLeft = this.Point - diagonalInc * Frames;
                        upRight = this.Point - diagonalDec * Frames;
                        downRight = this.Point + diagonalInc * Frames;
                        downLeft = this.Point + diagonalDec * Frames;
                        Moveable = false;
                        PrintAndClearExplosion(true);
                        if (Frames == 5)
                        {
                            toBeDeleted = true;
                            Engine.Player.IncreasePoints(1);

                            Menu.Table();
                            Menu.UIDescription();
                        }
                        Frames++;
                    }
                    #endregion
                    break;
                case ObjectType.Meteor2:
                    #region Small object clearing and breaking effect
                    if (!this.GotHit)
                    {
                        Printing.DrawAt(this.Point.X + 1, Point.Y - 2, @"   ");
                        Printing.DrawAt(this.Point.X, Point.Y - 1, @"    ");
                        Printing.DrawAt(this.Point.X, Point.Y, @"  ");
                    }
                    else
                    {
                        upLeft = this.Point - diagonalInc * Frames;
                        upRight = this.Point - diagonalDec * Frames;
                        downRight = this.Point + diagonalInc * Frames;
                        downLeft = this.Point + diagonalDec * Frames;
                        Moveable = false;
                        PrintAndClearExplosion(true);
                        if (Frames == 5)
                        {
                            toBeDeleted = true;
                            Engine.Player.IncreasePoints(1);

                            Menu.Table();
                            Menu.UIDescription();
                        }
                        Frames++;
                    }
                    #endregion
                    break;
                case ObjectType.Meteor3:
                    #region Small object clearing and breaking effect
                    if (!this.GotHit)
                    {
                        Printing.DrawAt(this.Point.X + 1, Point.Y - 2, @"  ");
                        Printing.DrawAt(this.Point.X, Point.Y - 1, @"    ");
                        Printing.DrawAt(this.Point.X, Point.Y, @"   ");
                    }
                    else
                    {
                        upLeft = this.Point - diagonalInc * Frames;
                        upRight = this.Point - diagonalDec * Frames;
                        downRight = this.Point + diagonalInc * Frames;
                        downLeft = this.Point + diagonalDec * Frames;
                        Moveable = false;
                        PrintAndClearExplosion(true);
                        if (Frames == 5)
                        {
                            toBeDeleted = true;
                            Engine.Player.IncreasePoints(1);

                            Menu.Table();
                            Menu.UIDescription();
                        }
                        Frames++;
                    }
                    #endregion
                    break;

            }
        }

        /// <summary>
        /// Di chuyển đối tượng sang trái 1 ô
        /// </summary>
        public void MoveObject(bool down)
        {
            if (Moveable && down)
            {
                this.Point.Y += Speed;
                return;
            }
            this.Point.X -= Speed;
        }


        /// <summary>
        /// Xóa và In hiệu ứng nổ
        /// </summary>
        /// <param name="clear">Đặt thành true nếu bạn muốn xóa, false nếu bạn muốn in</param>
        /// <param name="c">Đặt ký tự bạn muốn in</param>
        /// <param name="clr">Đặt màu bạn muốn in</param>
        private void PrintAndClearExplosion(bool clear)
        {
            char[] c = new char[4];
            var colors = Enum.GetValues(typeof(ConsoleColor))
                 .Cast<ConsoleColor>()
                 .Where(color => color != ConsoleColor.White &&
                             color != ConsoleColor.Gray &&
                             color != ConsoleColor.DarkGray)
                 .ToList();

            // Tạo một đối tượng Random để chọn màu ngẫu nhiên
            Random random = new Random();

            if (!clear) // Nếu không có mảng ký tự được truyền và yêu cầu in, hãy tạo một mảng tiêu chuẩn
            {
                c = new[] { '█', '█', '█', '█' };
            }
            else if (clear) // Nếu xóa, tạo mảng ký tự với khoảng trắng
            {
                c = new[] { ' ', ' ', ' ', ' ' };
            }
            // Sau đó in/xóa các đường chéo
            if ((upLeft.X > 1 && upLeft.X < 114) && (upLeft.Y > 1 && upLeft.Y < 30))
            {
                Printing.DrawAt(upLeft, c[0], colors[random.Next(colors.Count)]);
            }
            if ((upRight.X > 1 && upRight.X < 114) && (upRight.Y > 1 && upRight.Y < 30))
            {
                Printing.DrawAt(upRight, c[1], colors[random.Next(colors.Count)]);
            }
            if ((downLeft.X > 1 && downLeft.X < 114) && (downLeft.Y > 1 && downLeft.Y < 30))
            {
                Printing.DrawAt(downLeft, c[2], colors[random.Next(colors.Count)]);
            }
            if ((downRight.X > 1 && downRight.X < 114) && (downRight.Y > 1 && downRight.Y < 30))
            {
                Printing.DrawAt(downRight, c[3], colors[random.Next(colors.Count)]);
            }

        }
        /// <summary>
        /// Kiểm tra va chạm
        /// </summary>
        /// <param name="x">Tọa độ X để kiểm tra</param>
        /// <param name="y">Tọa độ Y để kiểm tra</param>
        /// <returns>Có va chạm hay không</returns>
        public bool Collided(int x, int y)
        {
            if (GotHit)
            {
                return false;
            }
            switch (objectType)
            {
                case ObjectType.Enemy1:
                    /*  
                     * CFI
                     * ADG
                     * BEH
                     */
                    if ((x == this.Point.X + 1 && y == this.Point.Y - 4) ||  // Left upper dot
         (x == this.Point.X + 7 && y == this.Point.Y - 4) ||  // Right upper dot
         (x >= this.Point.X && x <= this.Point.X + 6 && y == this.Point.Y - 3) ||  // Full row (Point.Y - 3)
         (x == this.Point.X + 2 && y == this.Point.Y - 2) ||  // Middle left dot
         (x == this.Point.X + 4 && y == this.Point.Y - 2) ||  // Center dot
         (x == this.Point.X + 6 && y == this.Point.Y - 2) ||  // Middle right dot
         (x >= this.Point.X + 1 && x <= this.Point.X + 5 && y == this.Point.Y - 1) ||  // Full row (Point.Y - 1)
         (x == this.Point.X + 3 && (y == this.Point.Y || y == this.Point.Y)))  // Bottom dots

                        return true;
                    return false;
                case ObjectType.Enemy2:
                    /*  
                     * CF(.)
                     * ADG
                     * BE(.)
                     */

                    if ((x >= this.Point.X && x <= this.Point.X + 7 && y == this.Point.Y + 1)) // Hàng đầy đủ (Point.Y + 1)
                                                                                               // Góc trên bên phải
                        return true;
                    return false;
                case ObjectType.Enemy3:
                    /*
                     * ABCD
                     */
                    if ((x == this.Point.X + 3 && y == this.Point.Y - 2) ||  // Điểm trên cùng
              (x >= this.Point.X && x <= this.Point.X + 6 && y == this.Point.Y - 1) ||  // Hàng đầy đủ (Point.Y - 1)
              (x >= this.Point.X && x <= this.Point.X + 6 && y == this.Point.Y) ||  // Hàng đẩy đủ (Point.Y)
              (x == this.Point.X && y == this.Point.Y + 1) ||  // Điểm bên trái
              (x >= this.Point.X + 2 && x <= this.Point.X + 4 && y == this.Point.Y + 1) ||  // Hàng ở giữa
              (x == this.Point.X + 6 && y == this.Point.Y + 1) ||  // Điểm bên phải
              (x == this.Point.X + 2 && y == this.Point.Y + 2) ||  // Điểm bên trái dưới
              (x == this.Point.X + 4 && y == this.Point.Y + 2))  // Điểm bên phải dưới
                        return true;
                    return false;
                case ObjectType.Enemy4:
                    if ((x == this.Point.X + 2 && y == this.Point.Y - 2) ||   // Điểm trên cùng
        (x >= this.Point.X && x <= this.Point.X + 6 && y == this.Point.Y - 1) ||  // Hàng đầy đủ (Point.Y - 1)
        (x >= this.Point.X && x <= this.Point.X + 6 && y == this.Point.Y) || // Hàng đẩy đủ (Point.Y)
        (x >= this.Point.X && x <= this.Point.X + 6 && y == this.Point.Y + 1) ||  //Hàng đầy đủ (Point.Y + 1)
        (x == this.Point.X + 1 && y == this.Point.Y + 2) ||  //  Điểm bên trái dưới
        (x == this.Point.X + 3 && y == this.Point.Y + 2) ||  // Điểm ở giữa ở dưới
        (x == this.Point.X + 5 && y == this.Point.Y + 2))  // Điểm ở dưới bên phải
                        return true;
                    return false;
                default:
                    return false;
                case ObjectType.Meteor1:
                    // Shape:
                    //  (.)ABC
                    //  ....DEFGH
                   
                    if (((x >= this.Point.X && x <= this.Point.X + 4) && (y == this.Point.Y)) ||
     ((x >= this.Point.X && x <= this.Point.X + 3) && (y == this.Point.Y - 1)))
                    {
                        return true;
                    }
                    return false;

                case ObjectType.Meteor2:
                    // Shape:
                    //   ..A
                    //   BCDEF
                    //   .G.
                 
                    if (((x >= this.Point.X && x <= this.Point.X + 4) && (y == this.Point.Y)) ||
         ((x >= this.Point.X + 1 && x <= this.Point.X + 4) && (y == this.Point.Y - 1)) ||
         ((x >= this.Point.X && x <= this.Point.X + 2) && (y == this.Point.Y + 1)))
                    {
                        return true;
                    }
                    return false;

                case ObjectType.Meteor3:
                    // Shape:
                    //  ..A
                    //  BCDEF
                    //  .G.
                   
                    if (((x >= this.Point.X && x <= this.Point.X + 4) && (y == this.Point.Y)) ||
        ((x >= this.Point.X + 1 && x <= this.Point.X + 3) && (y == this.Point.Y - 1)) ||
        ((x >= this.Point.X && x <= this.Point.X + 3) && (y == this.Point.Y + 1)))
                    {
                        return true;
                    }
                    return false;

            }
        }
        /// <summary>
        /// Kiểm tra va chạm với Point2D
        /// </summary>
        /// <param name="point">Điểm Point2D để kiểm tra</param>
        /// <returns>Có va chạm hay không</returns>
        public bool Collided(Point2D point)
        {
            return Collided(point.X, point.Y);
        }
    }
}

