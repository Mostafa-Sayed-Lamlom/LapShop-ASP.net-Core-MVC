using LapShop.Domains.Models;

namespace LapShop.ViewModels
{
    public class VmItemDetailsPage
    {
        public VmItemDetailsPage()
        {
            lstRecommendItems = new();
            lstItemImage = new();
            itemDetails = new();
        }
        public List<TbItem> lstRecommendItems { get; set; }
        public List<TbItemImage> lstItemImage { get; set; }
        public VwItem itemDetails { get; set; }
    }
}
