using Application.Models;
using Application.Models.MovieCategory;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class FoodCategoryApplication
    {
        private readonly ICineContext _context;

        public FoodCategoryApplication(ICineContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FoodCategoryModel>> ListAsync()
        {
            var categories =  await _context.FoodCategories.ToListAsync();
            return categories.Select(x=> new FoodCategoryModel(x)).OrderBy(x=>x.Name);
        }

        public async Task RegisterAsync(FoodCategoryModel model)
        {
            if (_context.FoodCategories.Any(x => x.Name.Trim().ToLower() == model.Name.Trim().ToLower()))
                throw new DuplicatedExcpection("Categoria de bomboniere já cadastrada");
            var foodcategory = new FoodCategory(model.Name.Trim());
            try
            {
                await _context.FoodCategories.AddAsync(foodcategory);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Não foi possível cadastrar essa categoria");
            }
        }

        public async Task<FoodCategoryModel> GetAsync(int id)
        {
            var category = await _context.FoodCategories.FirstOrDefaultAsync(x=> x.Id == id);
            if (category == null)
                throw new NotFoundException("Categoria não encontrada");
            return new FoodCategoryModel(category);
        }

        public async Task EditAsync(FoodCategoryModel model)
        {
            if (await _context.FoodCategories.AnyAsync(x => x.Name.Trim().ToLower() == model.Name.Trim().ToLower() && x.Id != model.Id))
                throw new DuplicatedExcpection("Categoria de comida já cadastrada");

            var category = await _context.FoodCategories.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (category == null)
                throw new NotFoundException("Categoria não encontrada");

            category.Name = model.Name.Trim();

            try
            {
                _context.FoodCategories.Update(category);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Não foi possível cadastrar essa categoria");
            }
        }

        public async Task DeleteAsync(FoodCategoryModel model)
        {
            var category = await _context.FoodCategories.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (category == null)
                throw new NotFoundException("Categoria não encontrada");

            try
            {
                _context.FoodCategories.Remove(category);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Não foi possível deletar essa categoria");
            }
        }

    }
}
