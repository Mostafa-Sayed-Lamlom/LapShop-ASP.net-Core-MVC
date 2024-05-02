namespace LapShop.Domains.Models
{
    public partial class VwItemCategory
    {
        public string ItemName { get; set; } = null!;
        public decimal SalesPrice { get; set; }
        public string CategoryName { get; set; } = null!;
        public decimal PurchasePrice { get; set; }
        public string? ImageName { get; set; }
        public int ItemId { get; set; }
        public int CategoryId { get; set; }
    }
}
