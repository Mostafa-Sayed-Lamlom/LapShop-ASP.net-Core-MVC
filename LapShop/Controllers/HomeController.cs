using LapShop.EF.BL.Interfaces;
using LapShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LapShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IItemService _itemService;
        private readonly ISliderService _sliderService;
        private readonly ICategoryService _categoryService;
        public HomeController(IItemService itemService,
                              ISliderService sliderService,
                              ICategoryService categoryService)
        {
            _itemService = itemService;
            _sliderService = sliderService;
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            VmHomePageItems vmHomePageItems = new VmHomePageItems();
            vmHomePageItems.lstAllItems = _itemService.GetAll().Take(12).ToList();
            vmHomePageItems.lstNewItems = _itemService.GetAll().Take(4).ToList();
            vmHomePageItems.lstFreeDeliveryItems1 = _itemService.GetAll().Skip(12).Take(4).ToList();
            vmHomePageItems.lstFreeDeliveryItems2 = _itemService.GetAll().Skip(5).Take(4).ToList();
            vmHomePageItems.lstFeaturedItems = _itemService.GetAll().Skip(10).Take(4).ToList();
            vmHomePageItems.lstSliders = _sliderService.GetAll().ToList();
            vmHomePageItems.lstCategories = _categoryService
                .GetAll()
                .Where(c => c.CategoryId == 1 || c.CategoryId == 2 || c.CategoryId == 3 || c.CategoryId == 5)
                .ToList();
            return View(vmHomePageItems);
        }

    }
}
