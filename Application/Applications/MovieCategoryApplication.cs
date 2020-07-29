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
    public class MovieCategoryApplication
    {
        private readonly ICineContext _context;

        public MovieCategoryApplication(ICineContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MovieCategoryModel>> ListAsync()
        {
            var categories =  await _context.MovieCategories.ToListAsync();
            return categories.Select(x=> new MovieCategoryModel(x)).OrderBy(x=>x.Description);
        }

        public async Task RegisterAsync(MovieCategoryModel model)
        {
            if (_context.MovieCategories.Any(x => x.Description.Trim().ToLower() == model.Description.Trim().ToLower()))
                throw new DuplicatedExcpection("Categoria já cadastrada");
            var moviecategory = new MovieCategory(model.Description.Trim());
            try
            {
                await _context.MovieCategories.AddAsync(moviecategory);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Não foi possível cadastrar essa categoria");
            }
        }

        public async Task<MovieCategoryModel> GetAsync(int id)
        {
            var category = await _context.MovieCategories.FirstOrDefaultAsync(x=> x.Id == id);
            if (category == null)
                throw new NotFoundException("Categoria não encontrada");
            return new MovieCategoryModel(category);
        }

        public async Task EditAsync(MovieCategoryModel model)
        {
            if (await _context.MovieCategories.AnyAsync(x => x.Description.Trim().ToLower() == model.Description.Trim().ToLower() && x.Id != model.Id))
                throw new DuplicatedExcpection("Cadastrada já cadastrada");

            var category = await _context.MovieCategories.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (category == null)
                throw new NotFoundException("Categoria não encontrada");

            category.Description = model.Description.Trim();

            try
            {
                _context.MovieCategories.Update(category);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Não foi possível cadastrar essa categoria");
            }
        }

        public async Task DeleteAsync(MovieCategoryModel model)
        {
            var category = await _context.MovieCategories.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (category == null)
                throw new NotFoundException("Categoria não encontrada");

            try
            {
                _context.MovieCategories.Remove(category);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Não foi possível deletar essa categoria");
            }
        }

    }
}
