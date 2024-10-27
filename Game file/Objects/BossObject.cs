using System;
using System.ComponentModel;
using System.Data.SqlClient;
using TeamWork.Field;

namespace TeamWork.Objects
{
    public class BossObject : Entity
    {
        public enum ObjectType // Loại đối tượng
        {
            Rocket,
            Bullet,
            Laser,
            Mine,
        }

        private ObjectType objectType;
        private int _lifeOnScreen; // "Khung hình" trên màn hình

        /// <summary>
        /// Khởi tạo đối tượng Boss với tọa độ bắt đầu (Point2D) và loại số từ 0 đến 3
        /// </summary>
        /// <param name="point"></param>
        /// <param name="type"></param>
        public BossObject(Point2D point, int type)
            : base(point)
        {
            base.Speed = 1;
            base.Point = point;
            objectType = (ObjectType)type;
            // Dựa trên loại được tạo, đặt các giá trị lifeOnScreen khác nhau
            switch (objectType)
            {
                case ObjectType.Rocket:
                    _lifeOnScreen = 45;
                    break;
                case ObjectType.Bullet:
                    _lifeOnScreen = 60;
                    break;
                case ObjectType.Laser:
                    _lifeOnScreen = 30;
                    break;
                case ObjectType.Mine:
                    _lifeOnScreen = 30;
                    break;
            }
        }

        #region Get Methods

        /// <summary>
        /// Lấy giá trị LifeOnScreen của BossObject
        /// </summary>
        /// <returns>int</returns>
        public int GetLifeOnScreen()
        {
            return _lifeOnScreen;
        }

        /// <summary>
        /// Lấy loại của BossObject
        /// </summary>
        /// <returns>ObjectType</returns>
        public ObjectType GetObjectType()
        {
            return objectType;
        }

        /// <summary>
        /// Lấy tất cả thông tin liên quan về đối tượng này
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return string.Format("X:{0} Y:{1}\nLife on screen Left:{2}\nType:{3}", this.Point.X, this.Point.Y,
                this._lifeOnScreen, this.objectType);
        }

        #endregion

        /// <summary>
        /// Di chuyển đối tượng dựa trên loại của nó
        /// </summary>
        public void MoveObject()
        {
            int direction = Engine.Rnd.Next(1, 4); // Số ngẫu nhiên, xác định chuyển động của một số loại đối tượng
            switch (objectType)
            {

                case ObjectType.Rocket:

                    #region Rocket Movement
                    // Chuyển động của tên lửa dựa trên số ngẫu nhiên
                    if (direction == 1)
                    {
                        this.Point.X -= 3;
                        this.Point.Y++;
                        _lifeOnScreen--;
                    }
                    if (direction == 2)
                    {
                        this.Point.Y++;
                        _lifeOnScreen--;
                    }
                    if (direction == 3)
                    {
                        this.Point.X += 3;
                        this.Point.Y++;
                        _lifeOnScreen--;
                    }

                    if (direction == 4)
                    {
                        this.Point.X += 5;
                        this.Point.Y++;
                        _lifeOnScreen--;

                    }
                    break;
                #endregion

                case ObjectType.Bullet:

                    #region Bullet Movement
                    // Chuyển động đạn tiêu chuẩn
                    this.Point.Y += 1;
                    _lifeOnScreen -= 1;
                    break;

                #endregion

                case ObjectType.Laser:

                    #region Laser Movement

                    if (_lifeOnScreen > 8) // Hiệu ứng "sạc" tia laser
                    {
                        if (_lifeOnScreen % 2 == 0)
                        {
                            this.Point.Y--;
                        }
                        else
                        {
                            this.Point.Y++;
                        }
                    }
                    this._lifeOnScreen--;
                    break;

                #endregion

                case ObjectType.Mine:

                    #region Mine Movement

                    // Chuyển động của vật thể mìn, "tốc độ" và chuyển động khác nhau dựa trên giá trị lifeOnScreen còn lại của đối tượng
                    if (_lifeOnScreen > 25)
                    {
                        if (direction == 1)
                        {
                            this.Point.X -= 6;
                            this.Point.Y -= 2;
                            _lifeOnScreen--;
                        }
                        if (direction == 2)
                        {
                            this.Point.X -= 6;
                            _lifeOnScreen--;
                        }
                        if (direction == 3)
                        {
                            this.Point.X -= 6;
                            this.Point.Y += 2;
                            _lifeOnScreen--;
                        }
                    }
                    else if (_lifeOnScreen > 15)
                    {
                        if (direction == 1)
                        {
                            this.Point.X -= 3;
                            this.Point.Y -= 1;
                            _lifeOnScreen--;
                        }
                        if (direction == 2)
                        {
                            this.Point.X -= 3;
                            _lifeOnScreen--;
                        }
                        if (direction == 3)
                        {
                            this.Point.X -= 3;
                            this.Point.Y += 1;
                            _lifeOnScreen--;
                        }
                    }
                    else if (_lifeOnScreen >= 10)
                    {

                        if (_lifeOnScreen % 2 == 0)
                        {
                            if (direction == 1)
                            {
                                this.Point.X--;
                                this.Point.Y--;
                                _lifeOnScreen--;
                            }
                            if (direction == 2)
                            {
                                this.Point.X--;
                                _lifeOnScreen--;
                            }
                            if (direction == 3)
                            {
                                this.Point.X--;
                                this.Point.Y++;
                                _lifeOnScreen--;
                            }
                        }
                    }
                    _lifeOnScreen--;
                    break;

                #endregion
            }
        }

        private int Frames = 1; // "Khung hình đã qua được sử dụng cho tính toán hiệu ứng nổ"

        private bool mineHit, mineHit2, mineHit3, mineHit4; // Bộ kích hoạt cho các hạt nổ mìn
        private readonly Point2D diagonalInc = new Point2D(1, 1); //Trợ giúp di chuyển chéo
        private readonly Point2D diagonalDec = new Point2D(-1, 1); // Trợ giúp di chuyển chéo

        /// <summary>
        /// In đối tượng dựa trên loại và lifeOnScreen của nó
        /// </summary>





        public void PrintObject()
        {
            switch (objectType)
            {
                case ObjectType.Rocket:

                    #region Rocket Print

                    Printing.DrawAt(this.Point, "<>");
                    break;

                #endregion

                case ObjectType.Bullet:

                    #region Bullet Print


                    Printing.DrawAt(this.Point, '*', ConsoleColor.DarkCyan);
                    Printing.DrawAt(this.Point.X - 7, this.Point.Y, '*', ConsoleColor.DarkCyan);
                    Printing.DrawAt(this.Point.X + 7, this.Point.Y, '*', ConsoleColor.DarkCyan);
                    Printing.DrawAt(this.Point.X - 5, this.Point.Y, '*', ConsoleColor.DarkCyan);
                    Printing.DrawAt(this.Point.X + 5, this.Point.Y, '*', ConsoleColor.DarkCyan);
                    break;

                #endregion

                case ObjectType.Laser:

                    #region Laser Print

                    if (this._lifeOnScreen > 8) // in hiệu ứng sạc
                    {
                        Engine.boss.Movealbe = false; // làm con Boss chuyển động không được khi in hiệu ứng sạc
                        Printing.DrawAtBG(80, this.Point.Y + 8, @"<----.     __ / __   \",
                            ConsoleColor.DarkGray);
                        Printing.DrawAtBG(80, this.Point.Y + 9, @"<----|====O)))==) \) /", ConsoleColor.Gray);
                        Printing.DrawAtBG(80, this.Point.Y + 10, "<----'    `--' `.__,'",
                            ConsoleColor.DarkGray);
                        Console.ResetColor();
                    }
                    else // in nguyên laser
                    {
                        Printing.DrawAtBG(20, this.Point.Y + 8,
                            "-----------------------------------------------------------------------------<----.", ConsoleColor.DarkGray);
                        Printing.DrawAtBG(20, this.Point.Y + 9,
                            "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~<----|", ConsoleColor.Gray);
                        Printing.DrawAtBG(20, this.Point.Y + 10,
                            "---------------------------------------------------------------------------<----'", ConsoleColor.DarkGray);
                        Console.ResetColor();

                        for (int i = 0; i < 53; i++)
                        {
                            if (Engine.Player.ShipCollided(Point.X - 50 + i, Point.Y + 9)) // Kiểm tra xem có va chạm với người chơi ở giữa tia laser không
                            {
                                break;
                            }
                        }
                        if (_lifeOnScreen < 1) // Khi hiệu ứng hoạt hình laser kết thúc, làm cho boss có thể di chuyển lại
                        {
                            Engine.boss.Movealbe = true;
                        }
                    }
                    break;

                #endregion

                case ObjectType.Mine:

                    #region Mine Print

                    if (this._lifeOnScreen > 6)// In tiêu chuẩn
                    {
                        Printing.DrawAt(this.Point.X, this.Point.Y - 1, " \u25B2", ConsoleColor.Yellow);
                        Printing.DrawAt(this.Point, "\u25C4\u25A0\u25BA", ConsoleColor.Yellow);
                        Printing.DrawAt(this.Point.X, this.Point.Y + 1, " \u25BC", ConsoleColor.Yellow);
                    }
                    else // In hiệu ứng nổ 
                    {
                        Point2D upRight = this.Point - diagonalDec * Frames;
                        // Nếu hạt va chạm với người chơi, di chuyển nó sang phải 1000 lần để không được in ra
                        if (mineHit) upRight.X += 1000;

                        Point2D upLeft = this.Point + diagonalDec * Frames;
                        if (mineHit2) upLeft.X += 1000;

                        Point2D downLeft = this.Point - diagonalInc * Frames;
                        if (mineHit3) downLeft.X += 1000;

                        Point2D downRight = this.Point + diagonalInc * Frames;
                        if (mineHit4) downRight.X += 1000;
                        // Nếu hạt nằm trong ranh giới màn hình, thì in ra
                        if ((upLeft.X > 1 && upLeft.X < 79) && (upLeft.Y > 1 && upLeft.Y < 30))
                        {
                            Printing.DrawAt(upLeft, '*');
                        }
                        if ((upRight.X > 1 && upRight.X < 79) && (upRight.Y > 1 && upRight.Y < 30))
                        {
                            Printing.DrawAt(upRight, '*');
                        }
                        if ((downLeft.X > 1 && downLeft.X < 79) && (downLeft.Y > 1 && downLeft.Y < 30))
                        {
                            Printing.DrawAt(downLeft, '*');
                        }
                        if ((downRight.X > 1 && downRight.X < 79) && (downRight.Y > 1 && downRight.Y < 30))
                        {
                            Printing.DrawAt(downRight, '*');
                        }
                    }
                    break;

                #endregion

            
            }
        }

        /// <summary>
        /// Xóa GameObject dựa trên loại của nó và kiểm tra va chạm với người chơi
        /// </summary>
        public void ClearObjectCheckColision()
        {
            switch (objectType)
            {
                case ObjectType.Bullet:

                    #region Bullet Clear
                    // Xóa đối tượng 
                    Printing.DrawAt(this.Point.X, this.Point.Y, ' ');
                    Printing.DrawAt(this.Point.X + 7, this.Point.Y, ' ');
                    Printing.DrawAt(this.Point.X - 7, this.Point.Y, ' ');
                    Printing.DrawAt(this.Point.X + 5, this.Point.Y, ' ');
                    Printing.DrawAt(this.Point.X - 5, this.Point.Y, ' ');
                    Engine.Player.Print();
                    // kiểm tra va chạm
                    if (Engine.Player.ShipCollided(this.Point) ||
                        Engine.Player.ShipCollided(Point.X + 7, Point.Y) ||
                        Engine.Player.ShipCollided(Point.X - 7, Point.Y) ||
                        Engine.Player.ShipCollided(Point.X + 5, Point.Y) ||
                        Engine.Player.ShipCollided(Point.X -5 , Point.Y)) 

                    {
                        // Nếu có va chạm, đặt lifeOnScreen của đối tượng này thành 0
                        this._lifeOnScreen = 0;
                    }
                    break;

                #endregion

                case ObjectType.Rocket:

                    #region Rocket Clear

                    Printing.DrawAt(this.Point, "  ");
                    Engine.Player.Print();
                    if (Engine.Player.ShipCollided(this.Point))
                    {
                        this._lifeOnScreen = 0;
                    }
                    break;

                #endregion

                case ObjectType.Laser:

                    #region Laser Clear

                    if (this._lifeOnScreen > 5)
                    {
                        Printing.DrawAt(80, this.Point.Y + 7, "                      ");
                        Printing.DrawAt(80, this.Point.Y + 8, "                      ");
                        Printing.DrawAt(80, this.Point.Y + 9, "                     ");
                        Printing.DrawAt(80, this.Point.Y + 10, "                      ");



                    }
                    else
                    {
                        Printing.DrawAt(20, this.Point.Y + 8,
                            "                                                                                                  ");
                        Printing.DrawAt(20, this.Point.Y + 9,
                            "                                                                                                                                                                  ");
                        Printing.DrawAt(20, this.Point.Y + 10,
                            "                                                                                               ");
                    } 
                    break;

                #endregion

                case ObjectType.Mine:

                    #region Mine Clear

                    if (this._lifeOnScreen > 6)
                    {
                        Printing.DrawAt(this.Point.X, this.Point.Y - 1, "  ");
                        Printing.DrawAt(this.Point, "   ");
                        Printing.DrawAt(this.Point.X, this.Point.Y + 1, "  ");
                    }
                    else
                    {
                        Point2D upRight = this.Point - diagonalDec * Frames;
                        if (mineHit) upRight.X += 1000;
                        if (Engine.Player.ShipCollided(upRight)) mineHit = true;

                        Point2D upLeft = this.Point + diagonalDec * Frames;
                        if (mineHit2) upLeft.X += 1000;
                        if (Engine.Player.ShipCollided(upLeft)) mineHit2 = true;

                        Point2D downLeft = this.Point - diagonalInc * Frames;
                        if (mineHit3) downLeft.X += 1000;
                        if (Engine.Player.ShipCollided(downLeft)) mineHit3 = true;

                        Point2D downRight = this.Point + diagonalInc * Frames;
                        if (mineHit4) downRight.X += 1000;
                        if (Engine.Player.ShipCollided(downRight)) mineHit4 = true;

                        if ((upLeft.X > 1 && upLeft.X < 79) && (upLeft.Y > 1 && upLeft.Y < 30))
                        {
                            Printing.DrawAt(upLeft, ' ');
                        }
                        if ((upRight.X > 1 && upRight.X < 79) && (upRight.Y > 1 && upRight.Y < 30))
                        {
                            Printing.DrawAt(upRight, ' ');
                        }
                        if ((downLeft.X > 1 && downLeft.X < 79) && (downLeft.Y > 1 && downLeft.Y < 30))
                        {
                            Printing.DrawAt(downLeft, ' ');
                        }
                        if ((downRight.X > 1 && downRight.X < 79) && (downRight.Y > 1 && downRight.Y < 30))
                        {
                            Printing.DrawAt(downRight, ' ');
                        }
                        Frames++;
                    }
                    break;

                #endregion
            }
        }
    }
}
