using LapShop.Domains.Models;
using Microsoft.AspNetCore.Http;

namespace LapShop.EF.BL.Interfaces
{
    public interface IItemService
    {
        Task Create(TbItem model, IFormFile file);
        IQueryable<TbItem> GetAll();
        IQueryable<VwItem> GetAllRelatedData(int? categoryId);
        TbItem? GetById(int id);
        VwItem? GetItemDetailsById(int id);
        Task<TbItem?> Update(TbItem model, IFormFile file);
        bool Delete(int id);
    }
}
