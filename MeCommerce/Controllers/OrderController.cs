using DomainModels;
using Interfaces.Services;
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
            int userId = System.Web.HttpContext.Current.User.Identity.GetUserId<int>();
            int totalPrice = 0;
            ShoppingCart cart = _userService.GetUserCart(userId);

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
                        Quantity = i.Quantity
                    };

                    itemsList.Add(itemsViewModel);

                    totalPrice = totalPrice + i.Product.Price;
                }

                ShoppingCartViewModel cartViewModel = new ShoppingCartViewModel
                {
                    CartId = cart.CartId,
                    UserId = userId,
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
                            Quantity = i.Quantity
                        };

                        itemsList.Add(itemsViewModel);

                        totalPrice = totalPrice + i.Product.Price;
                    }

                    ShoppingCartViewModel cartViewModel = new ShoppingCartViewModel
                    {
                        CartId = cacheCart.CartId,
                        UserId = userId,
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
            if (Request.IsAuthenticated)
            {
                var cart = _userService.GetUserCart(System.Web.HttpContext.Current.User.Identity.GetUserId<int>());

                if (Chaching.CacheManager.Exists("Basket") || cart != null)
                {
                    basket = Chaching.CacheManager.Get<ShoppingCart>("Basket");

                    if (basket.ShoppingCartItems.Any(x => x.ProductId == productId))
                    {
                        var shoppingCartItem = basket.ShoppingCartItems.FirstOrDefault(x => x.ProductId == productId);
                        if (shoppingCartItem != null)
                        {
                            shoppingCartItem.Quantity += 1;
                        }
                        _userService.UpdateBasket(basket);
                    }
                    else
                    {
                        ShoppingCartItem newItem = new ShoppingCartItem
                        {
                            ProductId = productId,
                            Quantity = quantity,
                            CartId = basket.CartId
                        };

                        basket.ShoppingCartItems = basket.ShoppingCartItems.Concat(new List<ShoppingCartItem> { newItem }).ToList();
                        _userService.UpdateBasket(basket);
                    }
                }
                else
                {
                    basket = new ShoppingCart { };

                    ShoppingCartItem newItem = new ShoppingCartItem
                    {
                        ProductId = productId,
                        Quantity = quantity,
                        CartId = basket.CartId
                    };

                    basket.ShoppingCartItems = new List<ShoppingCartItem> { newItem };
                }

                Chaching.CacheManager.Add(basket, "Basket");
                _userService.CreateBasket(basket);
            }
            //else
            //{
            //    ShoppingCart domainCart = new ShoppingCart
            //    {
            //        CartId = basket.CartId,
            //        UserId = basket.UserId,
            //        ShoppingCartItems = basket.ShoppingCartItems.ToList()
            //    };

            //    if (Chaching.CacheManager.Exists("Basket"))
            //    {
            //        Chaching.CacheManager.Delete("Basket");
            //    }

            //    Chaching.CacheManager.Add(domainCart, "Basket");
            //}

            ViewData["Success"] = "Product Added To Basket!";

            return RedirectToAction("Index");
        }
    }
}