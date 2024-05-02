namespace LapShop.Domains.Models
{
    public partial class TbSlider
    {
        public int SliderId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string ImageName { get; set; } = null!;
    }
}
