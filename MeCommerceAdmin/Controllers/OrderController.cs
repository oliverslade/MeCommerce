using DomainModels;
using Interfaces.Services;
using MeCommerceAdmin.Mapper;
using MeCommerceAdmin.Models;
using System.Collections.Generic;
using System.Linq;
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

        // GET: Order/Edit/5
        public ActionResult Edit(int id)
        {
            Orders order = _adminService.GetAllOrders().FirstOrDefault(x => x.OrderId == id);
            return View(order);
        }

        // POST: Order/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Orders order)
        {
            Orders oldOrder = _adminService.GetOrderById(id);

            Orders newOrder = new Orders
            {
                OrderId = id,
                UserId = oldOrder.UserId,
                CustomerTitle = order.CustomerTitle,
                CustomerName = order.CustomerName,
                HouseNameNumber = order.HouseNameNumber,
                AddressLine1 = order.AddressLine1,
                AddressLine2 = order.AddressLine2,
                AddressLine3 = order.AddressLine3,
                Town = order.Town,
                County = order.County,
                Postcode = order.Postcode,
                ContactNumber = order.ContactNumber,
                ContactEmail = order.ContactEmail,
                TotalPrice = oldOrder.TotalPrice,
                DatePlaced = oldOrder.DatePlaced,
                OrderLines = oldOrder.OrderLines
            };

            _adminService.UpdateOrder(newOrder);

            TempData["Success"] = "Order Updated";

            return RedirectToAction("Index");
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            _adminService.DeleteOrderById(id);

            TempData["Success"] = "Order Deleted";

            return RedirectToAction("Index");
        }
    }
}