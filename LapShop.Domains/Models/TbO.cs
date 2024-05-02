namespace LapShop.Domains.Models
{
    public partial class TbO
    {
        public TbO()
        {
            TbItems = new HashSet<TbItem>();
        }

        public int OsId { get; set; }
        public string OsName { get; set; } = null!;
        public string ImageName { get; set; } = null!;
        public bool ShowInHomePage { get; set; }
        public int CurrentState { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }

        public virtual ICollection<TbItem> TbItems { get; set; }
    }
}
