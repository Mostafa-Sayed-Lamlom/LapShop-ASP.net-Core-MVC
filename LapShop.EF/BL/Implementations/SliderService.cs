using LapShop.Domains.Models;
using LapShop.EF.BL.Interfaces;
using LapShop.EF.DBContext;
using Microsoft.EntityFrameworkCore;

namespace LapShop.EF.BL.Implementations
{
    public class SliderService : ISliderService
    {
        #region Fields
        private readonly LapShopContext _context;
        #endregion
        #region Constructors
        public SliderService(LapShopContext context)
        {
            _context = context;
        }

        public IEnumerable<TbSlider> GetAll()
        {
            return _context.TbSliders
           .AsNoTracking()
           .ToList();
        }
        #endregion
        #region Handel Functions

        #endregion
    }
}
