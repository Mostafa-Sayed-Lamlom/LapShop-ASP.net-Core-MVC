using LapShop.Domains.Models;
using Microsoft.AspNetCore.Http;

namespace LapShop.EF.BL.Interfaces
{
    public interface ICategoryService
    {
        Task Create(TbCategory model, IFormFile file);
        IEnumerable<TbCategory> GetAll();
        TbCategory? GetById(int id);
        Task<TbCategory?> Update(TbCategory model, IFormFile file);
        bool Delete(int id);
    }
}
