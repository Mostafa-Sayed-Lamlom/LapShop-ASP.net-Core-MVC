using LapShop.Domains.Models;
using LapShop.EF.BL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LapShop.Areas.admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public IActionResult List()
        {
            return View(_categoryService.GetAll());
        }
        [HttpGet]
        public IActionResult Edit(int? categoryId)
        {
            if (categoryId is null) return View(new TbCategory());

            var category = _categoryService.GetById((int)categoryId);

            if (category is null) return View("/Areas/admin/Views/404NotFound.cshtml");
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(TbCategory category, IFormFile? file)
        {
            if (!ModelState.IsValid)
                return View(nameof(Edit), category);

            if (category.CategoryId == 0)
            {
                await _categoryService.Create(category, file!);
            }
            else
            {
                var categoryUpdated = await _categoryService.Update(category, file!);
                if (categoryUpdated is null) return BadRequest();
            }
            return RedirectToAction(nameof(List));
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var isDeleted = _categoryService.Delete(id);

            return isDeleted ? Ok() : BadRequest();
        }
    }
}
