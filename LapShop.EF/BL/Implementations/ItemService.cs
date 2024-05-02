using LapShop.Domains.Models;
using LapShop.EF.BL.Interfaces;
using LapShop.EF.DBContext;
using LapShop.EF.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace LapShop.EF.BL.Implementations
{
    public class ItemService : IItemService
    {
        #region Fields
        private readonly LapShopContext _context;
        #endregion
        #region Constructors
        public ItemService(LapShopContext context)
        {
            _context = context;
        }
        #endregion
        #region Handel Functions
        public async Task Create(TbItem model, IFormFile file)
        {
            model.ImageName = await SaveCover(file);
            model.CreatedBy = "1";
            model.CreatedDate = DateTime.Now;
            model.CurrentState = 1;
            _context.Add(model);
            _context.SaveChanges();
        }

        public IQueryable<TbItem> GetAll()
        {
            return _context.TbItems
            .AsNoTracking();
        }

        public IQueryable<VwItem> GetAllRelatedData(int? categoryId)
        {
            return _context.VwItems
            .AsNoTracking()
            .Where(i => (i.CategoryId == categoryId || categoryId == null || categoryId == 0));
        }

        public TbItem? GetById(int id)
        {
            return _context.TbItems
                .AsNoTracking()
                .SingleOrDefault(c => c.ItemId == id);
        }

        public async Task<TbItem?> Update(TbItem model, IFormFile file)
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
                    var cover = Path.Combine(FileSettings.ImagesPathItem, oldCover!);
                    File.Delete(cover);
                }
                return model;
            }
            else
            {
                if (hasNewCover)
                {
                    var cover = Path.Combine(FileSettings.ImagesPathItem, model.ImageName!);
                    File.Delete(cover);
                }
                return null;
            }
        }
        public bool Delete(int id)
        {
            var isDeleted = false;

            var item = _context.TbItems.Find(id);

            if (item is null)
                return isDeleted;

            _context.Remove(item);
            var effectedRows = _context.SaveChanges();

            if (effectedRows > 0)
            {
                isDeleted = true;
                if (item.ImageName != "" || item.ImageName != null)
                {
                    var cover = Path.Combine(FileSettings.ImagesPathItem, item.ImageName!);
                    File.Delete(cover);
                }
            }

            return isDeleted;
        }

        private async Task<string> SaveCover(IFormFile cover)
        {
            if (cover is null) return "";
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";

            var path = Path.Combine(FileSettings.ImagesPathItem, coverName);

            using var stream = File.Create(path);
            await cover.CopyToAsync(stream);

            return coverName;
        }

        public VwItem? GetItemDetailsById(int id)
        {
            return _context.VwItems
                .AsNoTracking()
                .SingleOrDefault(c => c.ItemId == id);
        }
        #endregion
    }
}
