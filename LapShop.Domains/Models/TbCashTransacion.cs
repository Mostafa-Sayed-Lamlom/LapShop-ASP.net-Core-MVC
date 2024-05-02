namespace LapShop.Domains.Models
{
    public partial class TbCashTransacion
    {
        public int CashTransactionId { get; set; }
        public int CustomerId { get; set; }
        public DateTime CashDate { get; set; }
        public decimal CashValue { get; set; }
    }
}
