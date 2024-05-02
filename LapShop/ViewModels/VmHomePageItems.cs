using LapShop.Domains.Models;

namespace LapShop.ViewModels
{
    public class VmHomePageItems
    {
        public VmHomePageItems()
        {
            lstAllItems = new List<TbItem>();
            lstFreeDeliveryItems1 = new List<TbItem>();
            lstFreeDeliveryItems2 = new List<TbItem>();
            lstNewItems = new List<TbItem>();
            lstFeaturedItems = new List<TbItem>();
            lstSliders = new List<TbSlider>();
            lstCategories = new List<TbCategory>();
        }
        public List<TbItem> lstAllItems { get; set; }
        public List<TbItem> lstFreeDeliveryItems1 { get; set; }
        public List<TbItem> lstFreeDeliveryItems2 { get; set; }
        public List<TbItem> lstNewItems { get; set; }
        public List<TbItem> lstFeaturedItems { get; set; }
        public List<TbSlider> lstSliders { get; set; }
        public List<TbCategory> lstCategories { get; set; }
    }
}
