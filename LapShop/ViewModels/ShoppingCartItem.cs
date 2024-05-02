namespace LapShop.ViewModels
{
    public class ShoppingCartItem
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public string? ImageName { get; set; } = string.Empty;
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
    }
}
