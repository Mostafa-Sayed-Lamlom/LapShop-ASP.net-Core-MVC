using LapShop.Domains.Models;
using LapShop.EF.BL.Interfaces;
using LapShop.EF.DBContext;
using Microsoft.EntityFrameworkCore;

namespace LapShop.EF.BL.Implementations
{
    public class ItemImageServic : IItemImageService
    {
        #region Fields
        private readonly LapShopContext _context;
        #endregion
        #region Constructors
        public ItemImageServic(LapShopContext context)
        {
            _context = context;
        }
        #endregion
        public List<TbItemImage>? GetById(int id)
        {
            return _context.TbItemImages
                .AsNoTracking()
                .Where(c => c.ItemId == id)
                .ToList();
        }
    }
}
