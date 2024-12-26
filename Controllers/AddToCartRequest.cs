public class AddToCartRequest
{
    public int UserId { get; set; } // ID người dùng
    public int ProductId { get; set; } // ID sản phẩm
    public int Quantity { get; set; } // Số lượng sản phẩm
    public string? SelectedSize { get; set; } // Kích thước
    public string? SelectedSugar { get; set; } // Độ ngọt
    public string? SelectedIce { get; set; } // Độ lạnh
    public string? Note { get; set; } // Ghi chú
}
