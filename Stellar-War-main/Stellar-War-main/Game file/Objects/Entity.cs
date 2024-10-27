using System.Drawing; // Sử dụng thư viện System.Drawing
using System.Windows; // Sử dụng thư viện System.Windows
using TeamWork.Field; // Sử dụng thư viện TeamWork.Field

namespace TeamWork.Objects
{
    // Lớp trừu tượng Entity (Thực thể)
    abstract public class Entity
    {
        private Point2D point = new Point2D(55, 25); // Tọa độ mặc định của thực thể
        private int speed; // Tốc độ di chuyển
        private bool down; // Thêm trường boolean mới

        // Hàm khởi tạo mặc định
        protected Entity()
        {
            this.Point = point; // Thiết lập tọa độ
            this.Speed = this.speed; // Thiết lập tốc độ
            this.down = false; // Khởi tạo giá trị cho trường bool
        }

        // Hàm khởi tạo với tọa độ tùy chỉnh
        protected Entity(Point2D point)
        {
            this.Point = point; // Thiết lập tọa độ
            this.Speed = this.speed; // Thiết lập tốc độ
            this.down = false; // Khởi tạo giá trị cho trường bool
        }

        // Thuộc tính để lấy và đặt tọa độ
        public Point2D Point
        {
            get { return this.point; }
            set { this.point = value; }
        }

        // Thuộc tính để lấy và đặt tốc độ
        public int Speed { get; set; }

        // Thuộc tính để truy cập biến  down
        public bool Down
        {
            get { return this.down; }
            set { this.down = value; }
        }
    }
}
