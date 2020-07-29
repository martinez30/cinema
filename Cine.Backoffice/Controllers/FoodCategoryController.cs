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
    public class FoodCategoryController : Controller
    {
        private readonly FoodCategoryApplication _appFoodCategory;
        private readonly FoodApplication _appFood;
        public FoodCategoryController(FoodApplication appFood, FoodCategoryApplication appFoodCategory)
        {
            _appFood = appFood;
            _appFoodCategory = appFoodCategory;
        }
        public async Task<IActionResult> Index()
        {
            if (TempData["Success"] as bool? ?? false)
            {
                ViewBag.Success = TempData["Success"];
                ViewBag.Message = TempData["Message"];
            }
            var model = await _appFoodCategory.ListAsync();
            return View(model);
        }
        
        public IActionResult Register()
        {
            if (TempData["Success"] as bool? ?? false)
            {
                ViewBag.Success = true;
                ViewBag.Message = TempData["Message"];
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(FoodCategoryModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                await _appFoodCategory.RegisterAsync(model);
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
                var model = await _appFoodCategory.GetAsync(id);
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
        public async Task<IActionResult> Edit(FoodCategoryModel model)
        {
            try
            {
                await _appFoodCategory.EditAsync(model);
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
                var model = await _appFoodCategory.GetAsync(id);
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
        public async Task<IActionResult> Delete(FoodCategoryModel model)
        {
            try
            {
                await _appFoodCategory.DeleteAsync(model);
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
