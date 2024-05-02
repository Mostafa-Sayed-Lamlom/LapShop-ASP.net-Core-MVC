using LapShop.Domains.Models;

namespace LapShop.EF.BL.Interfaces
{
    public interface IItemTypesService
    {
        IEnumerable<TbItemType> GetAll();
    }
}
