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
    public class RoomController : Controller
    {
        private readonly RoomApplication _appRoom;
        public RoomController(RoomApplication appRoom)
        {
            _appRoom = appRoom;
        }
        public async Task<IActionResult> Index()
        {
            if (TempData["Success"] as bool? ?? false)
            {
                ViewBag.Success = TempData["Success"];
                ViewBag.Message = TempData["Message"];
            }
            var model = await _appRoom.ListAsync();
            
            return View(model);
        }
        
        public async Task<IActionResult> Register()
        {
            if (TempData["Success"] as bool? ?? false)
            {
                ViewBag.Success = true;
                ViewBag.Message = TempData["Message"];
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RoomModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                var seats = new List<SeatModel>();
                for (int i = 0; i < model.SeatRowCount; i++)
                {
                    var row = (char)(i + 65);
                    for (int j = 0; j < model.SeatColumnCount; j++)
                    {
                        seats.Add(new SeatModel(row.ToString(),j+1));
                    }
                }
                model.Seats = seats;
                await _appRoom.RegisterAsync(model);
                TempData["Success"] = true;
                TempData["Message"] = "Sessão Cadastrada com sucesso";
                return RedirectToAction("Register");
            }
            catch (Exception e)
            {
                ViewBag.Success = false;
                ViewBag.Message = e.Message;
                return View(model);
            }
        }
        
        public async Task<IActionResult> Edit(int id)
        {
            if (TempData["Success"] as bool? ?? false)
            {
                ViewBag.Success = true;
                ViewBag.Message = TempData["Message"];
            }
            try
            {
                var model = await _appRoom.GetAsync(id);
                model.SeatRowCount = model.Seats.Select(x => x.Row).Distinct().Count();
                model.SeatColumnCount = model.Seats.Select(x => x.Column).Distinct().Count();
                return View(model);
            }
            catch (Exception e)
            {
                TempData["Success"] = false;
                TempData["Message"] = e.Message;
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(RoomModel model)
        {
            try
            {
                var seats = new List<SeatModel>();
                for (int i = 0; i < model.SeatRowCount; i++)
                {
                    var row = (char)(i + 65);
                    for (int j = 0; j < model.SeatColumnCount; j++)
                    {
                        seats.Add(new SeatModel(row.ToString(), j + 1));
                    }
                }
                model.Seats = seats;
                await _appRoom.EditAsync(model);
                ViewBag.Success = true;
                ViewBag.Message = "Sala editada com sucesso";
                return View(model);
            }
            catch (Exception e)
            {
                ViewBag.Success = false;
                ViewBag.Message = e.Message;
                return View(model);
            }
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            if (TempData["Success"] as bool? ?? false)
            {
                ViewBag.Success = true;
                ViewBag.Message = TempData["Message"];
            }
            try
            {
                var model = await _appRoom.GetAsync(id);
                return View(model);
            }
            catch (Exception e)
            {
                ViewBag.Success = false;
                ViewBag.Message = e.Message;
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(RoomModel model)
        {
            try
            {
                await _appRoom.DeleteAsync(model);
                TempData["Success"] = true;
                TempData["Message"] = "Sessão deletada com sucesso";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.Success = false;
                ViewBag.Message = e.Message;
                return View(model);
            }
        }
       
    }
}
