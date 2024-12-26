namespace restapi.Controllers
{
    public class AddToCartResponse
    {
        public int Value { get; set; } // Giá trị trạng thái (0 hoặc 1)
        public string Message { get; set; } // Thông báo
        public decimal CalculatedPrice { get; set; } // Giá đã tính toán
    }

}
