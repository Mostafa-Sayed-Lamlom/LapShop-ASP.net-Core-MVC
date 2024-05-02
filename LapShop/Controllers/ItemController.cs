using LapShop.EF.BL.Interfaces;
using LapShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LapShop.Controllers
{
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;
        private readonly IItemImageService _itemImageService;
        public ItemController(IItemService itemService,
                              IItemImageService itemImageService,
                              ISalesInvoiceItemsService salesInvoiceItemsService)
        {
            _itemService = itemService;
            _itemImageService = itemImageService;
        }
        public IActionResult ItemDetails(int itemId)
        {
            VmItemDetailsPage vmItemDetailsPage = new();
            vmItemDetailsPage.itemDetails = _itemService.GetItemDetailsById(itemId)!;
            vmItemDetailsPage.lstItemImage = _itemImageService.GetById(itemId)!;
            vmItemDetailsPage.lstRecommendItems = _itemService.GetAll().Take(6).ToList();
            return View(vmItemDetailsPage);
        }

        public IActionResult AllItems()
        {
            ViewBag.Title = "All Products";
            return View();
        }


    }
}
