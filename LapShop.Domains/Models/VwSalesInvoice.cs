namespace LapShop.Domains.Models
{
    public partial class VwSalesInvoice
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int InvoiceId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DelivryDate { get; set; }
        public int? DelivryManId { get; set; }
        public string? Notes { get; set; }
        public Guid CustomerId { get; set; }
    }
}
