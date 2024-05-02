namespace LapShop.Domains.Models
{
    public class VwOrderDetails
    {
        public int InvoiceItemId { get; set; }
        public int InvoiceId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DelivryDate { get; set; }
        public Guid CustomerId { get; set; }
        public double Qty { get; set; }
        public decimal InvoicePrice { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; } = null!;
        public string? ImageName { get; set; }
    }
}
