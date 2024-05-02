namespace LapShop.Domains.Models
{
    public partial class TbItemDiscount
    {
        public int ItemDiscountId { get; set; }
        public int ItemId { get; set; }
        public decimal DiscountPercent { get; set; }
        public DateTime EndDate { get; set; }

        public virtual TbItem Item { get; set; } = null!;
    }
}
