using DomainModels;
using Interfaces.Services;
using MeCommerceAdmin.Mapper;
using MeCommerceAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MeCommerceAdmin.Controllers
{
    public class OrderController : Controller
    {
        private readonly IAdminService _adminService;

        public OrderController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public ActionResult Index()
        {
            var orders = _adminService.GetAllOrders();

            return View(orders);
        }

        // GET: Order
        public ActionResult OrderDetails(int orderId)
        {
            Orders order = _adminService.GetAllOrders().FirstOrDefault(x => x.OrderId == orderId);

            ICollection<OrderLineViewModel> orderLineViewModels = new List<OrderLineViewModel>();

            foreach (var orderLine in order.OrderLines)
            {
                OrderLineViewModel orderLineViewModel = new OrderLineViewModel
                {
                    OrderId = order.OrderId,
                    OrderLineId = orderLine.OrderLineId,
                    ProductId = orderLine.ProductId,
                    Product = ViewModelMapper.ToViewModel(_adminService.GetProductById(orderLine.ProductId)),
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

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Order/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Order/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}