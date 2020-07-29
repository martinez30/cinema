using Application.Models;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class FoodApplication
    {
        private readonly ICineContext _context;

        public FoodApplication(ICineContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FoodModel>> ListAsync()
        {
            var foods = await _context.Foods
                .Include(x=> x.FoodCategory)
                .ToListAsync();
            return foods.Select(x => new FoodModel(x));
        }

        public async Task RegisterAsync(FoodModel model)
        {
            if (_context.Foods.Any(x => x.Name == model.Name && x.Description == model.Description))
                throw new DuplicatedExcpection("Bomboniere já cadastrada");
            var category = await _context.FoodCategories.FirstOrDefaultAsync(x => x.Id == model.CategoryId);
            var food = new Food(model.Description, category, model.Name, ProductCategory.Food,model.Amount);
            try
            {
                await _context.Foods.AddAsync(food);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Não foi possível cadastrar esta bomboniere");
            }
        }

        public async Task<FoodModel> GetAsync(int id)
        {
            var food = await _context.Foods
                .Include(x=> x.FoodCategory)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (food == null)
                throw new NotFoundException("Bomboniere não encontrado");
            return new FoodModel(food);
        }

        public async Task EditAsync(FoodModel model)
        {
            var category = await _context.FoodCategories.FirstOrDefaultAsync(x => x.Id == model.CategoryId);
            if (category == null)
            {
                throw new NotFoundException("Categoria dessa bomboniere não foi encontrada");
            }
            var food = await _context.Foods.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (food == null)
                throw new NotFoundException("Bomboniere não encontrada");

            food.Name = model.Name;
            food.FoodCategory.Name = category.Name;
            food.Description = model.Description;
            food.Amount = model.Amount;
            food.Category = model.ProductCategory.Value;
            try
            {
                _context.Foods.Update(food);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Não foi possível editar essa bomboniere");
            }
        }

        public async Task DeleteAsync(FoodModel model)
        {
            var food = await _context.Foods
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (food == null)
            {
                throw new NotFoundException("Bomboniere não Encontrada");
            }
            try
            {
                _context.Foods.Remove(food);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Não foi possível deletar essa bomboniere");
            }
        }
    }
}
