using LapShop.Domains.Models;

namespace LapShop.EF.BL.Interfaces
{
    public interface IItemImageService
    {
        List<TbItemImage>? GetById(int id);
    }
}
