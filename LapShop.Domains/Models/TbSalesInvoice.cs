namespace LapShop.Domains.Models
{
    public partial class TbSalesInvoice
    {
        public TbSalesInvoice()
        {
            TbSalesInvoiceItems = new HashSet<TbSalesInvoiceItem>();
        }

        public int InvoiceId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DelivryDate { get; set; }
        public int? DelivryManId { get; set; }
        public string? Notes { get; set; }
        public Guid CustomerId { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public int CurrentState { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<TbSalesInvoiceItem> TbSalesInvoiceItems { get; set; }
    }
}
