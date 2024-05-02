using LapShop.Domains.Models;
using LapShop.Ef.Identity;
using LapShop.EF.BL.Interfaces;
using LapShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LapShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly IItemService _itemService;
        private readonly ISalesInvoiceService _salesInvoiceService;

        private readonly UserManager<ApplicationUser> _userManager;
        public OrderController(IItemService itemService,
                               UserManager<ApplicationUser> userManager,
                               ISalesInvoiceService salesInvoiceService)
        {
            _itemService = itemService;
            _userManager = userManager;
            _salesInvoiceService = salesInvoiceService;

        }
        public IActionResult Cart()
        {
            var cart = new ShoppingCart();
            if (HttpContext.Request.Cookies["Cart"] != null)
            {
                string sesstionCart = HttpContext.Request.Cookies["Cart"]!;
                cart = JsonConvert.DeserializeObject<ShoppingCart>(sesstionCart);
            }
            return View(cart);
        }
        public IActionResult AddToCart(int itemId)
        {
            ShoppingCart cart;

            if (HttpContext.Request.Cookies["Cart"] != null)
                cart = JsonConvert.DeserializeObject<ShoppingCart>(HttpContext.Request.Cookies["Cart"]);
            else
                cart = new ShoppingCart();

            var item = _itemService.GetById(itemId);
            if (item == null)
                return NotFound();

            var itemInList = cart.lstItems.Where(a => a.ItemId == itemId).FirstOrDefault();

            if (itemInList != null)
            {
                itemInList.Qty++;
                itemInList.Total = itemInList.Qty * itemInList.Price;
            }
            else
            {
                cart.lstItems.Add(new ShoppingCartItem
                {
                    ItemId = item.ItemId,
                    ItemName = item.ItemName,
                    ImageName = item.ImageName,
                    Price = item.SalesPrice,
                    Qty = 1,
                    Total = item.SalesPrice
                });
            }
            cart.Total = cart.lstItems.Sum(a => a.Total);

            HttpContext.Response.Cookies.Append("Cart", JsonConvert.SerializeObject(cart));

            return RedirectToAction("Cart");
        }
        public IActionResult DeleteFromCart(int itemId)
        {
            ShoppingCart cart = JsonConvert.DeserializeObject<ShoppingCart>(HttpContext.Request.Cookies["Cart"]);

            var itemInList = cart.lstItems.Where(a => a.ItemId == itemId).FirstOrDefault();

            cart.lstItems.Remove(itemInList!);

            cart.Total = cart.lstItems.Sum(a => a.Total);

            HttpContext.Response.Cookies.Delete("Cart");
            HttpContext.Response.Cookies.Append("Cart", JsonConvert.SerializeObject(cart));

            return RedirectToAction("Cart");
        }
        public IActionResult DeleteFromCartFromHome(int itemId)
        {
            ShoppingCart cart = JsonConvert.DeserializeObject<ShoppingCart>(HttpContext.Request.Cookies["Cart"]);

            var itemInList = cart.lstItems.Where(a => a.ItemId == itemId).FirstOrDefault();

            cart.lstItems.Remove(itemInList!);

            cart.Total = cart.lstItems.Sum(a => a.Total);

            HttpContext.Response.Cookies.Delete("Cart");
            HttpContext.Response.Cookies.Append("Cart", JsonConvert.SerializeObject(cart));

            return Redirect("~/");
        }

        [Authorize]
        public async Task<IActionResult> OrderSuccess()
        {
            var cart = new ShoppingCart();
            if (HttpContext.Request.Cookies["Cart"] != null)
            {
                string sesstionCart = HttpContext.Request.Cookies["Cart"]!;
                cart = JsonConvert.DeserializeObject<ShoppingCart>(sesstionCart);
            }
            await SaveOrderAsync(cart);
            Response.Cookies.Delete("Cart");
            return View();
        }

        [Authorize]
        public async Task<IActionResult> MyOrders()
        {
            var user = await _userManager.GetUserAsync(User);
            var lstOrder = _salesInvoiceService.GetAllOrderDetails(new Guid(user.Id));
            return View(lstOrder);
        }
        [Authorize]
        public IActionResult DeleteOrder(int invoiceItemId, int invoiceId)
        {
            var isDeleted = _salesInvoiceService.DeleteSalesInvoiceItemWithSalesInvoice(invoiceItemId, invoiceId);
            if (isDeleted)
                return Redirect("/Order/MyOrders");
            return BadRequest();
        }
        async Task SaveOrderAsync(ShoppingCart oShopingCart)
        {
            try
            {
                List<TbSalesInvoiceItem> lstInvoiceItems = new List<TbSalesInvoiceItem>();
                foreach (var item in oShopingCart.lstItems)
                {
                    lstInvoiceItems.Add(new TbSalesInvoiceItem()
                    {
                        ItemId = item.ItemId,
                        Qty = item.Qty,
                        InvoicePrice = item.Price
                    });
                }

                var user = await _userManager.GetUserAsync(User);

                TbSalesInvoice oSalesInvoice = new TbSalesInvoice()
                {
                    InvoiceDate = DateTime.Now,
                    CustomerId = Guid.Parse(user.Id),
                    DelivryDate = DateTime.Now.AddDays(5),
                    CreatedBy = user.Id,
                    CreatedDate = DateTime.Now
                };

                _salesInvoiceService.Save(oSalesInvoice, lstInvoiceItems, true);
            }
            catch (Exception ex)
            {
                new Exception(ex.ToString());
            }
        }

    }
}
