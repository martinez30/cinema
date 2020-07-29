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
    public class SessionController : Controller
    {
        private readonly MovieApplication _appMovie;
        private readonly SessionApplication _appSession;
        private readonly RoomApplication _appRoom;
        public SessionController(SessionApplication appSession, MovieApplication appMovie, RoomApplication appRoom)
        {
            _appSession = appSession;
            _appRoom = appRoom;
            _appMovie = appMovie;
        }
        public async Task<IActionResult> Index()
        {
            if (TempData["Success"] as bool? ?? false)
            {
                ViewBag.Success = TempData["Success"];
                ViewBag.Message = TempData["Message"];
            }
            var model = await _appSession.ListAsync();
            return View(model);
        }
        
        public async Task<IActionResult> Register()
        {
            await LoadData();
            if (TempData["Success"] as bool? ?? false)
            {
                ViewBag.Success = true;
                ViewBag.Message = TempData["Message"];
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(SessionModel model)
        {
            await LoadData();
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                await _appSession.RegisterAsync(model);
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
            await LoadData();
            if (TempData["Success"] as bool? ?? false)
            {
                ViewBag.Success = true;
                ViewBag.Message = TempData["Message"];
            }
            try
            {
                var model = await _appSession.GetAsync(id);
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
        public async Task<IActionResult> Edit(SessionModel model)
        {
            await LoadData();
            try
            {
                await _appSession.EditAsync(model);
                ViewBag.Success = true;
                ViewBag.Message = "Sessão editada com sucesso";
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
                var model = await _appSession.GetAsync(id);
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
        public async Task<IActionResult> Delete(SessionModel model)
        {
            try
            {
                await _appSession.DeleteAsync(model);
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
       
        private async Task LoadData()
        {
            var movies = await _appMovie.ListAsync();

            var moviesSelectList = new SelectList(movies, "Id", "Name") ;

            var rooms = await _appRoom.ListAsync();

            var roomSelectList = new SelectList(rooms, "Id", "Name");

            ViewBag.MovieSelectList = moviesSelectList;
            ViewBag.RoomSelectList = roomSelectList;
        }
    }
}
