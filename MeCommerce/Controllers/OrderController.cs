using DomainModels;
using Interfaces.Services;
using MeCommerce.Mapper;
using MeCommerce.ViewModels;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MeCommerce.Controllers
{
    public class OrderController : Controller
    {
        private readonly ICatalogService _catalogService;

        private readonly IUserService _userService;

        public OrderController(ICatalogService catalogService, IUserService userService)
        {
            _catalogService = catalogService;
            _userService = userService;
        }

        public ActionResult Index()
        {
            int totalPrice = 0;
            ShoppingCart cart = Chaching.CacheManager.Get<ShoppingCart>("Basket");

            if (cart != null)
            {
                IEnumerable<ShoppingCartItem> items = cart.ShoppingCartItems.ToList();
                ICollection<ShoppingCartItemViewModel> itemsList = new List<ShoppingCartItemViewModel>();
                foreach (var i in items)
                {
                    ShoppingCartItemViewModel itemsViewModel = new ShoppingCartItemViewModel
                    {
                        CartId = cart.CartId,
                        ProductId = i.ProductId,
                        ShoppingCartItemsId = i.ShoppingCartItemsId,
                        Quantity = i.Quantity,
                        Product = ViewModelMapper.ToViewModel(i.Product)
                    };

                    itemsList.Add(itemsViewModel);

                    totalPrice = totalPrice + i.Product.Price;
                }

                ShoppingCartViewModel cartViewModel = new ShoppingCartViewModel
                {
                    CartId = cart.CartId,
                    ShoppingCartItems = itemsList,
                    TotalPrice = totalPrice
                };
                return View(cartViewModel);
            }
            else
            {
                ShoppingCart cacheCart = Chaching.CacheManager.Get<ShoppingCart>("Basket");
                if (cacheCart != null)
                {
                    IEnumerable<ShoppingCartItem> items = cacheCart.ShoppingCartItems.ToList();
                    ICollection<ShoppingCartItemViewModel> itemsList = new List<ShoppingCartItemViewModel>();
                    foreach (var i in items)
                    {
                        ShoppingCartItemViewModel itemsViewModel = new ShoppingCartItemViewModel
                        {
                            CartId = cacheCart.CartId,
                            ProductId = i.ProductId,
                            ShoppingCartItemsId = i.ShoppingCartItemsId,
                            Quantity = i.Quantity,
                            Product = ViewModelMapper.ToViewModel(i.Product)
                        };

                        itemsList.Add(itemsViewModel);

                        totalPrice = totalPrice + i.Product.Price;
                    }

                    ShoppingCartViewModel cartViewModel = new ShoppingCartViewModel
                    {
                        CartId = cacheCart.CartId,
                        ShoppingCartItems = itemsList,
                        TotalPrice = totalPrice
                    };

                    return View(cartViewModel);
                }
            }
            return View();
        }

        public ActionResult AddProductToBasket(int productId, int quantity)
        {
            ShoppingCart basket;

            if (Chaching.CacheManager.Exists("Basket"))
            {
                basket = Chaching.CacheManager.Get<ShoppingCart>("Basket");

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
                    ShoppingCartItem newItem = new ShoppingCartItem
                    {
                        ProductId = productId,
                        Quantity = quantity,
                        CartId = basket.CartId,
                        Product = _catalogService.GetProductById(productId)
                    };

                    basket.ShoppingCartItems = basket.ShoppingCartItems.Concat(new List<ShoppingCartItem> { newItem }).ToList();
                }
            }
            else
            {
                basket = new ShoppingCart();

                ShoppingCartItem newItem = new ShoppingCartItem
                {
                    ProductId = productId,
                    Quantity = quantity,
                    CartId = basket.CartId,
                    Product = _catalogService.GetProductById(productId)
                };

                basket.ShoppingCartItems = new List<ShoppingCartItem> { newItem };
            }

            Chaching.CacheManager.Add(basket, "Basket");

            ViewData["Success"] = "Product Added To Basket!";

            return RedirectToAction("Index");
        }
    }
}