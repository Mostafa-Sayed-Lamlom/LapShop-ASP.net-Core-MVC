using LapShop.Domains.Models;

namespace LapShop.EF.BL.Interfaces
{
    public interface ISliderService
    {
        IEnumerable<TbSlider> GetAll();
    }
}
