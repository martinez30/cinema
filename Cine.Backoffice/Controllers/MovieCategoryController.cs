using System;
using System.Threading.Tasks;
using Application;
using Application.Models.MovieCategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cine.Backoffice.Controllers
{
    [Authorize]
    public class MovieCategoryController : Controller
    {
        private readonly MovieCategoryApplication _appmovieCategory;
        public MovieCategoryController(MovieCategoryApplication movieCategoryApplication)
        {
            _appmovieCategory = movieCategoryApplication;
        }
        public async Task<IActionResult> Index()
        {
            if (TempData["Success"] as bool? ?? false)
            {
                ViewBag.Success = TempData["Success"];
                ViewBag.Message = TempData["Message"];
            }
            var model = await _appmovieCategory.ListAsync();
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
        public async Task<IActionResult> Register(MovieCategoryModel model)
        {
            try
            {
                await _appmovieCategory.RegisterAsync(model);
                TempData["Success"] = true;
                TempData["Message"] = "Categoria Cadastrada com sucesso";
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
                var model = await _appmovieCategory.GetAsync(id);
                return View(model);
            }
            catch(Exception e)
            {
                ViewBag.Success = false;
                ViewBag.Message = e.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MovieCategoryModel model)
        {
            try
            {
                await _appmovieCategory.EditAsync(model);
                ViewBag.Success = true;
                ViewBag.Message = "Categoria editada com sucesso";
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
                var model = await _appmovieCategory.GetAsync(id);
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
        public async Task<IActionResult> Delete(MovieCategoryModel model)
        {
            try
            {
                await _appmovieCategory.DeleteAsync(model);
                TempData["Success"] = true;
                TempData["Message"] = "Categoria deletada com sucesso";
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
