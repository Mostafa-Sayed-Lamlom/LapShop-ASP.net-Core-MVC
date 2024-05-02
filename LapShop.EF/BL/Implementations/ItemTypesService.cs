using LapShop.Domains.Models;
using LapShop.EF.BL.Interfaces;
using LapShop.EF.DBContext;
using Microsoft.EntityFrameworkCore;

namespace LapShop.EF.BL.Implementations
{
    public class ItemTypesService : IItemTypesService
    {
        #region Fields
        private readonly LapShopContext _context;
        #endregion
        #region Constructors
        public ItemTypesService(LapShopContext context)
        {
            _context = context;
        }
        #endregion
        #region Handel Functions
        public IEnumerable<TbItemType> GetAll()
        {
            return _context.TbItemTypes
            .AsNoTracking()
            .ToList();
        }
        #endregion
    }
}
