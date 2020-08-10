using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Application.Models;
using Domain;
using Infra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cine.Backoffice.Controllers
{
    public class OrderController : Controller
    {
        private readonly SessionApplication _appSession;
        private readonly OrderApplication _appOrder;
        private readonly FoodApplication _appFood;
        public OrderController(SessionApplication appSession, OrderApplication appOrder, FoodApplication appFood)
        {
            _appSession = appSession;
            _appOrder = appOrder;
            _appFood = appFood;
        }
        public async Task<IActionResult> Index(SessionListModel model)
        {
            await LoadData();
            if (TempData["Success"] as bool? ?? false)
            {
                ViewBag.Success = TempData["Success"];
                ViewBag.Message = TempData["Message"];
            }
            var sessions = await _appSession.ListAsync(model.MovieName, model.Date);
            model.Sessions = sessions.ToList();
            foreach (var item in model.Sessions)
            {
                var seats = await _appSession.ListAvaliableSeats(item.Id);
                item.AvaliableSeatCount = seats.Count();
            }
            return View(model);
        }
        public async Task<IActionResult> OrderList(OrderListModel model)
        {
            await LoadData();
            if (TempData["Success"] as bool? ?? false)
            {
                ViewBag.Success = TempData["Success"];
                ViewBag.Message = TempData["Message"];
            }

            if (!model.IsSearch)
            {
                return View(model);
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var orderList = await _appOrder.ListAsync(model.Status, model.OrderDate);
            model.Orders = orderList.ToList();
            return View(model);
        }
        public async Task<IActionResult> BuyTicket(int id)
        {
            var session = await _appSession.GetSessionToBuyTicket(id);

            return View(session);
        }
        [HttpPost]
        public async Task<IActionResult> BuyTicket(SessionBuyTicketModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                var orderid = await _appOrder.Create(model);
                return RedirectToAction("Checkout", "Order", new { id = orderid });
            }
            catch (Exception e)
            {
                ViewBag.Success = false;
                ViewBag.Message = e.Message;
                return View(model);
            }
        }
        public async Task<IActionResult> Checkout(int id, string productName)
        {
            var model = await _appOrder.Checkout(id, productName);

            return View(model);
        }
        public async Task<IActionResult> Finalization(int id)
        {
            await _appOrder.FinalizeOrder(id);
            var order = await _appOrder.GetAsync(id);
            return View(order);
        }
        public async Task<IActionResult> CheckoutAddNewItem(int id, int foodId)
        {

            await _appOrder.AddOrderItem(id, foodId);

            return RedirectToAction("Checkout", "Order", new { id });
        }
        public async Task<IActionResult> CheckoutDeleteItem(int id,int orderId)
        {
            await _appOrder.DeleteItem(id, orderId);

            return RedirectToAction("Checkout", "Order", new { id = orderId, productName = string.Empty });
        }
        [HttpPost]
        //public async Task<IActionResult> Delete(CheckoutModel model)
        public IActionResult Delete(CheckoutModel model)
        {
            //await _appOrder.Delete(model.Order);        
            return RedirectToAction("Index","Order",null);
        }
        private async Task LoadData()
        {
            var statusList = Enum.GetValues(typeof(OrderStatus)).Cast<OrderStatus>();

            var selectliststatus = new SelectList(statusList.Select(x => new { Id = (int)x, Description = x.GetDescription() }), "Id", "Description");

            ViewBag.StatusSelectList = selectliststatus;
        }
    }
}
