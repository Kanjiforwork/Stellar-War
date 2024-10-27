using System; // Sử dụng thư viện System
using TeamWork.Field; // Sử dụng thư viện TeamWork.Field
using TeamWork.Objects; // Sử dụng thư viện TeamWork.Objects

namespace TeamWork
{
    public class Player : Entity, IPlayer
    {
        private int lives = 10; // Số mạng ban đầu: 10
        private int score = 0; // Điểm khởi đầu: 0
        private int level = 1; // Cấp độ khởi đầu: 1
        private int speed = 2; // Tốc độ di chuyển: 2

        public static Point2D PlayerPoint = new Point2D(55, 25); // Điểm bắt đầu mặc định của người chơi

        /// <summary>
        /// Hàm khởi tạo với các giá trị mặc định
        /// </summary>
        public Player()
        {
            this.Lifes = this.lives; // Khởi tạo số mạng
            this.Score = this.score; // Khởi tạo điểm
            this.Level = this.level; // Khởi tạo cấp độ
        }

        public int Score { get; set; } // Điểm của người chơi
        public int Lifes { get; set; } // Số mạng của người chơi
        public string Name { get; set; } // Tên của người chơi
        public int Level { get; set; } // Cấp độ của người chơi

        /// <summary>
        /// Người chơi di chuyển lên và vẽ lại
        /// </summary>
        public void MoveUp()
        {
            // Giới hạn di chuyển của người chơi trên trục Y
            if (this.Point.Y - speed < 3) return;
            Clear();
            this.Point.Y -= speed;
            Print();
        }

        /// <summary>
        /// Người chơi di chuyển xuống và vẽ lại
        /// </summary>
        public void MoveDown()
        {
            // Giới hạn di chuyển của người chơi trên trục Y
            if (this.Point.Y + speed >= Engine.WindowHeight - 4) return;
            Clear();
            this.Point.Y += speed;
            Print();
        }

        /// <summary>
        /// Người chơi di chuyển sang phải và vẽ lại
        /// </summary>
        public void MoveRight()
        {
            // Giới hạn di chuyển của người chơi trên trục X
            if (this.Point.X + speed >= Engine.WindowWidth) return;
            Clear();
            this.Point.X += speed;
            Print();
        }

        /// <summary>
        /// Người chơi di chuyển sang trái và vẽ lại
        /// </summary>
        public void MoveLeft()
        {
            // Giới hạn di chuyển của người chơi trên trục X
            if (this.Point.X - speed < 1) return;
            Clear();
            this.Point.X -= speed;
            Print();
        }

        public void setName(string newName) // Hàm thiết lập tên người chơi
        {
            this.Name = newName;
        }

        // Phương thức để vẽ người chơi tại vị trí hiện tại
        public void Print()
        {
            Printing.DrawAt(this.Point.X, this.Point.Y - 1, @"   █    ", ConsoleColor.Yellow);
            Printing.DrawAt(this.Point.X, this.Point.Y, @"  ███  ", ConsoleColor.Yellow);
            Printing.DrawAt(this.Point.X + 3, this.Point.Y, @"█", ConsoleColor.Cyan);
            Printing.DrawAt(this.Point.X, this.Point.Y + 1, @" █████ ", ConsoleColor.Yellow);
            Printing.DrawAt(this.Point.X, this.Point.Y + 2, @"█  █  █", ConsoleColor.Yellow);
        }

        // Phương thức xóa vị trí cũ của người chơi
        public void Clear()
        {
            // Sử dụng chuỗi để loại bỏ các hiện vật
            Printing.DrawAt(this.Point.X, this.Point.Y - 1, @"       ");
            Printing.DrawAt(this.Point.X, this.Point.Y, @"       ");
            Printing.DrawAt(this.Point.X, this.Point.Y + 1, @"       ");
            Printing.DrawAt(this.Point.X, this.Point.Y + 2, @"       ");
        }

        /// <summary>
        /// Tăng điểm thêm 1 và tính toán cấp độ
        /// </summary>
        public void IncreasePoints()
        {
            this.Score++;
            Engine.Player.Level = Engine.Player.Score / 50 + 1;
            if (Engine.Player.Level > 1)
            {
                // Thiết lập độ khó cho người chơi
                Engine.Chance = Engine.StartingDifficulty - Engine.Player.Level * 2;
            }
        }

        /// <summary>
        /// Tăng điểm theo số lượng nhất định và tính toán cấp độ
        /// </summary>
        /// <param name="points">Số điểm cần tăng</param>
        public void IncreasePoints(int points)
        {
            this.Score += points;
            Engine.Player.Level = Engine.Player.Score / 50 + 1;
        }

        /// <summary>
        /// Giảm số mạng
        /// </summary>
        public void DecreaseLifes()
        {
            this.Lifes--;
        }

        /// <summary>
        /// Kiểm tra va chạm tàu với tọa độ X và Y
        /// </summary>
        /// <param name="x">Số cột</param>
        /// <param name="y">Số hàng</param>
        /// <returns>Nếu có va chạm</returns>
        public bool ShipCollided(int x, int y)
        {
            // Kiểm tra một số điểm của mô hình người chơi
            if ((x == this.Point.X + 3 && y == this.Point.Y - 1) ||  // Điểm trên cùng
                    (x >= this.Point.X + 2 && x <= this.Point.X + 4 && y == this.Point.Y) ||  // Hàng giữa (Point.Y)
                    (x >= this.Point.X + 1 && x <= this.Point.X + 5 && y == this.Point.Y + 1) ||  // Hàng rộng hơn (Point.Y + 1)
                    (x == this.Point.X && y == this.Point.Y + 2) ||  // Điểm bên trái (Point.Y + 2)
                    (x == this.Point.X + 2 && y == this.Point.Y + 2) ||  // Điểm giữa bên trái (Point.Y + 2)
                    (x == this.Point.X + 6 && y == this.Point.Y + 2))  // Điểm bên phải (Point.Y + 2))
            {
                // Nếu có điểm chồng lên nhau, giảm số mạng và vẽ lại giao diện
                Engine.Player.DecreaseLifes();
                Menu.Table();
                Menu.UIDescription();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Kiểm tra va chạm tàu với Point2D
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool ShipCollided(Point2D p)
        {
            return ShipCollided(p.X, p.Y);
        }
    }
}
