using LapShop.Domains.Models;
using LapShop.EF.BL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LapShop.Areas.admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("admin")]
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;
        private readonly ICategoryService _categoryService;
        private readonly IItemTypesService _itemTypesService;
        private readonly IOsService _osService;
        public ItemController(IItemService itemService,
                              ICategoryService categoryService,
                              IItemTypesService itemTypesService,
                              IOsService osService)
        {
            _itemService = itemService;
            _categoryService = categoryService;
            _itemTypesService = itemTypesService;
            _osService = osService;
        }

        [HttpGet]
        public IActionResult List(int? categoryId)
        {
            ViewData["categoryId"] = categoryId;
            ViewBag.lstCategories = _categoryService.GetAll();
            var items = _itemService.GetAllRelatedData(categoryId).ToList();
            return View(items);
        }

        [HttpGet]
        public IActionResult Edit(int? itemId, TbItem? item)
        {
            ViewBag.lstCategories = _categoryService.GetAll();
            ViewBag.lstItemTypes = _itemTypesService.GetAll();
            ViewBag.lstOs = _osService.GetAll();

            if (itemId is null) return View(new TbItem());
            if (itemId == 0) return View(item);

            var isItemFound = _itemService.GetById((int)itemId);

            if (isItemFound is null) return View("/Areas/admin/Views/404NotFound.cshtml");
            return View(isItemFound);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(TbItem item, IFormFile? file)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Edit), item);

            if (item.ItemId == 0)
            {
                await _itemService.Create(item, file!);
            }
            else
            {
                var itemUpdated = await _itemService.Update(item, file!);
                if (itemUpdated is null) return BadRequest();
            }
            return RedirectToAction(nameof(List));
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var isDeleted = _itemService.Delete(id);

            return isDeleted ? Ok() : BadRequest();
        }
    }
}
