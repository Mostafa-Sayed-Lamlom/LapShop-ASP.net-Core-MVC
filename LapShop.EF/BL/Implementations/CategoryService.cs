using LapShop.Domains.Models;
using LapShop.EF.BL.Interfaces;
using LapShop.EF.DBContext;
using LapShop.EF.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace LapShop.EF.BL.Implementations
{
    public class CategoryService : ICategoryService
    {
        #region Fields
        private readonly LapShopContext _context;
        #endregion
        #region Constructors
        public CategoryService(LapShopContext context)
        {
            _context = context;
        }
        #endregion
        #region Handel Functions
        public async Task Create(TbCategory model, IFormFile file)
        {
            model.ImageName = await SaveCover(file);
            model.CreatedBy = "1";
            model.CreatedDate = DateTime.Now;
            model.CurrentState = 1;
            _context.Add(model);
            _context.SaveChanges();
        }

        public IEnumerable<TbCategory> GetAll()
        {
            return _context.TbCategories
            .AsNoTracking()
            .ToList();
        }

        public TbCategory? GetById(int id)
        {
            return _context.TbCategories
                .AsNoTracking()
                .SingleOrDefault(c => c.CategoryId == id);
        }

        public async Task<TbCategory?> Update(TbCategory model, IFormFile file)
        {


            var hasNewCover = file is not null;
            var oldCover = model.ImageName;


            model.UpdatedBy = "1";
            model.UpdatedDate = DateTime.Now;
            if (hasNewCover)
            {
                model.ImageName = await SaveCover(file!);
            }
            _context.Update(model);
            var effectedRows = _context.SaveChanges();

            if (effectedRows > 0)
            {
                if (hasNewCover)
                {
                    var cover = Path.Combine(FileSettings.ImagesPathCategory, oldCover);
                    File.Delete(cover);
                }
                return model;
            }
            else
            {
                var cover = Path.Combine(FileSettings.ImagesPathCategory, model.ImageName);
                File.Delete(cover);
                return null;
            }
        }
        public bool Delete(int id)
        {
            var isDeleted = false;

            var category = _context.TbCategories.Find(id);

            if (category is null)
                return isDeleted;

            _context.Remove(category);
            var effectedRows = _context.SaveChanges();

            if (effectedRows > 0)
            {
                isDeleted = true;

                var cover = Path.Combine(FileSettings.ImagesPathCategory, category.ImageName);
                File.Delete(cover);
            }

            return isDeleted;
        }

        private async Task<string> SaveCover(IFormFile cover)
        {
            if (cover is null) return "";
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";

            var path = Path.Combine(FileSettings.ImagesPathCategory, coverName);

            using var stream = File.Create(path);
            await cover.CopyToAsync(stream);

            return coverName;
        }
        #endregion
    }
}
