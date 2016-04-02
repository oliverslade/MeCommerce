using Interfaces.Services;
using MeCommerce.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MeCommerce.Controllers
{
    public class OrderController : Controller
    {
        private readonly ICatalogService _catalogService;

        public OrderController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        public ActionResult Index()
        {
            decimal totalPrice = 0;
            ShoppingCartViewModel cart = Chaching.CacheManager.Get<ShoppingCartViewModel>("Basket");

            IEnumerable<ShoppingCartItemViewModel> items = cart.ShoppingCartItems.ToList();
            totalPrice = cart.ShoppingCartItems.Aggregate(totalPrice, (current, i) => current + (decimal)i.Product.Price * i.Quantity / 100);

            ShoppingCartViewModel cartViewModel = new ShoppingCartViewModel
            {
                CartId = cart.CartId,
                ShoppingCartItems = items.ToList(),
                TotalPrice = totalPrice
            };
            return View(cartViewModel);
        }

        public ActionResult AddProductToBasket(int productId, int quantity)
        {
            ShoppingCartViewModel basket;

            if (Chaching.CacheManager.Exists("Basket"))
            {
                basket = Chaching.CacheManager.Get<ShoppingCartViewModel>("Basket");

                if (basket.ShoppingCartItems.Any(x => x.ProductId == productId))
                {
                    var shoppingCartItem = basket.ShoppingCartItems.FirstOrDefault(x => x.ProductId == productId);
                    if (shoppingCartItem != null)
                    {
                        shoppingCartItem.Quantity += 1;
                    }
                }
                else
                {
                    ShoppingCartItemViewModel newItem = new ShoppingCartItemViewModel
                    {
                        ProductId = productId,
                        Quantity = quantity,
                        CartId = basket.CartId,
                        Product = _catalogService.GetProductById(productId)
                    };

                    basket.ShoppingCartItems = basket.ShoppingCartItems.Concat(new List<ShoppingCartItemViewModel> { newItem }).ToList();
                }
            }
            else
            {
                basket = new ShoppingCartViewModel();

                ShoppingCartItemViewModel newItem = new ShoppingCartItemViewModel
                {
                    ProductId = productId,
                    Quantity = quantity,
                    CartId = basket.CartId,
                    Product = _catalogService.GetProductById(productId)
                };

                basket.ShoppingCartItems = new List<ShoppingCartItemViewModel> { newItem };
            }

            Chaching.CacheManager.Add(basket, "Basket");

            if (HttpContext.Request.UrlReferrer != null) return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
            else return RedirectToAction("Index");
        }
    }
}