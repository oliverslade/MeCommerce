using DomainModels;
using Interfaces.Services;
using MeCommerce.Mapper;
using MeCommerce.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Threading.Tasks;
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
            if (string.IsNullOrWhiteSpace(form["CardNumber"]))
            {
                ViewData["Error"] = "Please insert your Card Number";
                return RedirectToAction("Index");
            }

            if (string.IsNullOrWhiteSpace(form["CardName"]))
            {
                ViewData["Error"] = "Please insert the name on your card";
                return RedirectToAction("Index");
            }

            if (string.IsNullOrWhiteSpace(form["SecurityCode"]))
            {
                ViewData["Error"] = "Please insert the 3 digit code on the back of your card";
                return RedirectToAction("Index");
            }

            int? userId = null;

            if (Request.IsAuthenticated)
            {
                userId = System.Web.HttpContext.Current.User.Identity.GetUserId<int>();
            }

            var basket = Chaching.CacheManager.Get<ShoppingCartViewModel>("Basket");
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

            if (Chaching.CacheManager.Exists("Basket")) { Chaching.CacheManager.Delete("Basket"); }

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