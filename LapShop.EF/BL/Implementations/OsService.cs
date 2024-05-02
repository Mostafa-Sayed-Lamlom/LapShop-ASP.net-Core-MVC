using LapShop.Domains.Models;
using LapShop.EF.BL.Interfaces;
using LapShop.EF.DBContext;
using Microsoft.EntityFrameworkCore;

namespace LapShop.EF.BL.Implementations
{
    public class OsService : IOsService
    {
        #region Fields
        private readonly LapShopContext _context;
        #endregion
        #region Constructors
        public OsService(LapShopContext context)
        {
            _context = context;
        }
        #endregion
        #region Handel Functions
        public IEnumerable<TbO> GetAll()
        {
            return _context.TbOs
            .AsNoTracking()
            .ToList();
        }
        #endregion
    }
}
