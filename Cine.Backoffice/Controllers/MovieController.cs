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
    public class MovieController : Controller
    {
        private readonly MovieApplication _appMovie;
        private readonly MovieCategoryApplication _appMovieCategory;
        public MovieController(MovieApplication appMovie, MovieCategoryApplication appMovieCategory)
        {
            _appMovie = appMovie;
            _appMovieCategory = appMovieCategory;
        }
        public async Task<IActionResult> Index()
        {
            if (TempData["Success"] as bool? ?? false)
            {
                ViewBag.Success = TempData["Success"];
                ViewBag.Message = TempData["Message"];
            }
            var model = await _appMovie.ListAsync();
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
        public async Task<IActionResult> Register(MovieModel model)
        {
            await LoadData();
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                await _appMovie.RegisterAsync(model);
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
            await LoadData();
            if (TempData["Success"] as bool? ?? false)
            {
                ViewBag.Success = true;
                ViewBag.Message = TempData["Message"];
            }
            try
            {
                var model = await _appMovie.GetAsync(id);
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
        public async Task<IActionResult> Edit(MovieModel model)
        {
            await LoadData();
            try
            {
                await _appMovie.EditAsync(model);
                ViewBag.Success = true;
                ViewBag.Message = "Filme editado com sucesso";
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
                var model = await _appMovie.GetAsync(id);
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
        public async Task<IActionResult> Delete(MovieModel model)
        {
            try
            {
                await _appMovie.DeleteAsync(model);
                TempData["Success"] = true;
                TempData["Message"] = "Filme deletado com sucesso";
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
            var categories = await _appMovieCategory.ListAsync();

            var selectListCategory = new SelectList(categories, "Id", "Description");

            var classification = Enum.GetValues(typeof(MovieClassification)).Cast<MovieClassification>();

            var selectlistaclassification = new SelectList(classification.Select(x => new { Id = (int)x, Description = x.GetDescription() }), "Id", "Description");

            ViewBag.ClassificationList = selectlistaclassification;
            ViewBag.CategoryList = selectListCategory;
        }
    }
}
