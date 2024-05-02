namespace LapShop.Domains.Models
{
    public partial class TbBusinessInfo
    {
        public int BusinessInfoId { get; set; }
        public string? BusinessCardNumber { get; set; }
        public string? OfficePhone { get; set; }
        public decimal Budget { get; set; }
        public int CutomerId { get; set; }

        public virtual TbCustomer Cutomer { get; set; } = null!;
    }
}
