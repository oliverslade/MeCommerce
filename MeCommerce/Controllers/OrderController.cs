﻿using DomainModels;
using Interfaces.Services;
using MeCommerce.Mapper;
using MeCommerce.ViewModels;
using Microsoft.AspNet.Identity;
using System;
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
            return View();
        }

        public ActionResult CheckoutPost(FormCollection form)
        {
            if (string.IsNullOrWhiteSpace(form["CustomerTitle"]))
            {
                TempData["Error"] = "Please insert your Title";
                return RedirectToAction("Index");
            }

            if (string.IsNullOrWhiteSpace(form["CustomerName"]))
            {
                TempData["Error"] = "Please insert your Full Name";
                return RedirectToAction("Index");
            }

            if (string.IsNullOrWhiteSpace(form["HouseNameNumber"]))
            {
                TempData["Error"] = "Please insert your House Name or Number";
                return RedirectToAction("Index");
            }

            if (string.IsNullOrWhiteSpace(form["AddressLine1"]))
            {
                TempData["Error"] = "Please insert the first line of your Address";
                return RedirectToAction("Index");
            }

            if (string.IsNullOrWhiteSpace(form["AddressLine2"]))
            {
                TempData["Error"] = "Please insert the second line of your Address";
                return RedirectToAction("Index");
            }

            if (string.IsNullOrWhiteSpace(form["AddressLine3"]))
            {
                form["AddressLine3"] = form["AddressLine2"];
            }

            if (string.IsNullOrWhiteSpace(form["County"]))
            {
                TempData["Error"] = "Please insert the County of your Address";
                return RedirectToAction("Index");
            }

            if (string.IsNullOrWhiteSpace(form["Postcode"]))
            {
                TempData["Error"] = "Please insert the Postcode of your Address";
                return RedirectToAction("Index");
            }

            if (string.IsNullOrWhiteSpace(form["ContactNumber"]))
            {
                TempData["Error"] = "Please insert a number we contact you on about your order";
                return RedirectToAction("Index");
            }

            if (string.IsNullOrWhiteSpace(form["ContactEmail"]))
            {
                TempData["Error"] = "Please insert an email address we contact you on about your order";
                return RedirectToAction("Index");
            }

            if (string.IsNullOrWhiteSpace(form["Town"]))
            {
                TempData["Error"] = "Please insert the Town of your Address";
                return RedirectToAction("Index");
            }

            if (string.IsNullOrWhiteSpace(form["CardName"]))
            {
                TempData["Error"] = "Please insert the name on your card";
                return RedirectToAction("Index");
            }

            if (string.IsNullOrWhiteSpace(form["CarNumber"]))
            {
                TempData["Error"] = "Please insert the long number on your card";
                return RedirectToAction("Index");
            }

            if (string.IsNullOrWhiteSpace(form["SecurityCode"]))
            {
                TempData["Error"] = "Please insert the 3 digit code on the back of your card";
                return RedirectToAction("Index");
            }

            int? userId = null;

            if (Request.IsAuthenticated)
            {
                userId = System.Web.HttpContext.Current.User.Identity.GetUserId<int>();
            }

            var basket = Caching.CacheManager.Get<ShoppingCartViewModel>("Basket" + Request.UserHostAddress);
            ICollection<OrderLine> orderLines = new List<OrderLine>();

            foreach (var item in basket.ShoppingCartItems)
            {
                OrderLine line = new OrderLine
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };

                orderLines.Add(line);
            }

            int totalPrice = 0;
            totalPrice = basket.ShoppingCartItems.Aggregate(totalPrice, (current, i) => current + i.Product.Price * i.Quantity);

            var order = new Orders
            {
                UserId = userId,
                CustomerTitle = form["CustomerTitle"],
                CustomerName = form["CustomerName"],
                HouseNameNumber = form["HouseNameNumber"],
                AddressLine1 = form["AddressLine1"],
                AddressLine2 = form["AddressLine2"],
                AddressLine3 = form["AddressLine3"],
                Town = form["Town"],
                County = form["County"],
                Postcode = form["ContactNumber"],
                ContactNumber = form["ContactNumber"],
                ContactEmail = form["ContactEmail"],
                OrderLines = orderLines,
                TotalPrice = totalPrice,
                DatePlaced = DateTime.Now
            };

            _userService.CreateOrder(order);

            if (Caching.CacheManager.Exists("Basket" + Request.UserHostAddress)) { Caching.CacheManager.Delete("Basket" + Request.UserHostAddress); }

            return View(order);
        }

        public ActionResult OrderHistory()
        {
            int userId = System.Web.HttpContext.Current.User.Identity.GetUserId<int>();

            var userOrders = _userService.GetOrdersByUserId(userId);

            return View(userOrders.Reverse());
        }

        public ActionResult OrderDetails(int orderId)
        {
            var orders = _userService.GetOrdersByUserId(System.Web.HttpContext.Current.User.Identity.GetUserId<int>());
            var order = orders.FirstOrDefault(x => x.OrderId == orderId);

            ICollection<OrderLineViewModel> orderLineViewModels = new List<OrderLineViewModel>();

            foreach (var orderLine in order.OrderLines)
            {
                OrderLineViewModel orderLineViewModel = new OrderLineViewModel
                {
                    OrderId = order.OrderId,
                    OrderLineId = orderLine.OrderLineId,
                    ProductId = orderLine.ProductId,
                    Product = ViewModelMapper.ToViewModel(_catalogService.GetProductById(orderLine.ProductId)),
                    Quantity = orderLine.Quantity
                };

                orderLineViewModels.Add(orderLineViewModel);
            }

            OrderViewModel orderViewModel = new OrderViewModel
            {
                OrderId = order.OrderId,
                UserId = order.UserId,
                CustomerTitle = order.CustomerTitle,
                CustomerName = order.CustomerName,
                HouseNameNumber = order.HouseNameNumber,
                AddressLine1 = order.AddressLine1,
                AddressLine2 = order.AddressLine2,
                AddressLine3 = order.AddressLine3,
                Town = order.Town,
                County = order.County,
                Postcode = order.Postcode,
                ContactEmail = order.ContactEmail,
                ContactNumber = order.ContactNumber,
                DatePlaced = order.DatePlaced,
                TotalPrice = order.TotalPrice,
                OrderLines = orderLineViewModels
            };

            return View(orderViewModel);
        }

        public ActionResult AddProductToBasket(int productId, int quantity)
        {
            ShoppingCartViewModel basket;

            if (Caching.CacheManager.Exists("Basket" + Request.UserHostAddress))
            {
                basket = Caching.CacheManager.Get<ShoppingCartViewModel>("Basket" + Request.UserHostAddress);

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

            Caching.CacheManager.Add(basket, "Basket" + Request.UserHostAddress);

            TempData["Success"] = "Product Added To Basket!";

            if (HttpContext.Request.UrlReferrer != null) return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
            else return RedirectToAction("Index");
        }

        public ActionResult DeleteProductFromCart(int productId)
        {
            var oldBasket = Caching.CacheManager.Get<ShoppingCartViewModel>("Basket" + Request.UserHostAddress);

            ICollection<ShoppingCartItemViewModel> newBasketItems = new List<ShoppingCartItemViewModel>();

            foreach (var item in oldBasket.ShoppingCartItems)
            {
                if (item.Product.ProductId != productId)
                {
                    ShoppingCartItemViewModel itemViewModel = new ShoppingCartItemViewModel
                    {
                        Product = item.Product,
                        CartId = item.CartId,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity
                    };

                    newBasketItems.Add(itemViewModel);
                }
                else if (item.Quantity > 1)
                {
                    ShoppingCartItemViewModel itemViewModel = new ShoppingCartItemViewModel
                    {
                        Product = item.Product,
                        CartId = item.CartId,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity - 1
                    };

                    newBasketItems.Add(itemViewModel);
                }
            }

            var newBasket = new ShoppingCartViewModel
            {
                TotalPrice = oldBasket.TotalPrice,
                CartId = oldBasket.CartId,
                ShoppingCartItems = newBasketItems
            };

            Caching.CacheManager.Delete("Basket" + Request.UserHostAddress);

            if (newBasket.ShoppingCartItems != null)
            {
                Caching.CacheManager.Add(newBasket, "Basket" + Request.UserHostAddress);
            }

            TempData["Success"] = "Product Removed!";

            if (HttpContext.Request.UrlReferrer != null) return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
            else return RedirectToAction("Index");
        }

        public ActionResult AddOneToQuantity(int productId)
        {
            var oldBasket = Caching.CacheManager.Get<ShoppingCartViewModel>("Basket" + Request.UserHostAddress);

            ICollection<ShoppingCartItemViewModel> newBasketItems = new List<ShoppingCartItemViewModel>();

            foreach (var item in oldBasket.ShoppingCartItems)
            {
                if (item.Product.ProductId == productId)
                {
                    ShoppingCartItemViewModel itemViewModel = new ShoppingCartItemViewModel
                    {
                        Product = item.Product,
                        CartId = item.CartId,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity + 1
                    };

                    newBasketItems.Add(itemViewModel);
                }
            }

            var newBasket = new ShoppingCartViewModel
            {
                TotalPrice = oldBasket.TotalPrice,
                CartId = oldBasket.CartId,
                ShoppingCartItems = newBasketItems
            };

            Caching.CacheManager.Delete("Basket" + Request.UserHostAddress);

            if (newBasket.ShoppingCartItems != null)
            {
                Caching.CacheManager.Add(newBasket, "Basket" + Request.UserHostAddress);
            }

            TempData["Success"] = "Basket Updated!";

            if (HttpContext.Request.UrlReferrer != null) return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
            else return RedirectToAction("Index");
        }

        public ActionResult ClearBasket()
        {
            if (Caching.CacheManager.Exists("Basket" + Request.UserHostAddress))
            {
                Caching.CacheManager.Delete("Basket" + Request.UserHostAddress);
            }

            TempData["Success"] = "Basket Cleared!";

            return RedirectToAction("Index", "Home", null);
        }
    }
}