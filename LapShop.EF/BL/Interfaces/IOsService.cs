using LapShop.Domains.Models;

namespace LapShop.EF.BL.Interfaces
{
    public interface IOsService
    {
        IEnumerable<TbO> GetAll();
    }
}
