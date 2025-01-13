namespace restapi.Models
{
    public class InvoiceDetail
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string SelectedSize { get; set; }
        public string SelectedSugar { get; set; }
        public string SelectedIce { get; set; }
        public string Note { get; set; }
    }

}