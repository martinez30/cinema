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
    public class MovieApplication
    {
        private readonly ICineContext _context;

        public MovieApplication(ICineContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MovieModel>> ListAsync()
        {
            var movies = await _context.Movies
                .Include(x=>x.Category)
                .ToListAsync();
            return movies.Select(x => new MovieModel(x)).OrderByDescending(x => x.Id);
        }

        public async Task RegisterAsync(MovieModel model)
        {
            if (_context.Movies.Any(x => x.Name.Trim().ToLower() == model.Name.Trim().ToLower()))
                throw new DuplicatedExcpection("Filme já cadastrado");
            var category = await _context.MovieCategories.FirstOrDefaultAsync(x => x.Id == model.CategoryId.Value);
            var movie = new Movie(model.Name, model.Duration.Value, model.Classification.Value, model.Synopsis, category);
            try
            {
                await _context.Movies.AddAsync(movie);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Não foi possível cadastrar esse filme");
            }
        }

        public async Task<MovieModel> GetAsync(int id)
        {
            var movie = await _context.Movies
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (movie == null)
                throw new NotFoundException("Filme não encontrado");
            return new MovieModel(movie);
        }

        public async Task EditAsync(MovieModel model)
        {
            if (await _context.Movies.AnyAsync(x => x.Name.Trim().ToLower() == model.Name.Trim().ToLower() && x.Id != model.Id))
                throw new DuplicatedExcpection("Filme já cadastrado");

            var category = await _context.MovieCategories.FirstOrDefaultAsync(x => x.Id == model.CategoryId);
            if (category == null)
                throw new NotFoundException("Categoria não encontrada");

            var movie = await _context.Movies
                .Include(x=> x.Category)
                .FirstOrDefaultAsync(x => x.Id == model.Id);
            if (movie == null)
            {
                throw new NotFoundException("Filme não Encontrado");
            }
            movie.Name = model.Name.Trim();
            movie.Duration = model.Duration.Value;
            movie.Classification = model.Classification.Value;
            movie.Category = category;
            movie.Synopsis = model.Synopsis;

            try
            {
                _context.Movies.Update(movie);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Não foi possível cadastrar esse filme");
            }
        }

        public async Task DeleteAsync(MovieModel model)
        {
            var movie = await _context.Movies
                .Include(x => x.Sessions)
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (movie == null)
            {
                throw new NotFoundException("Filme não Encontrado");
            }

            if (movie.Sessions != null && movie.Sessions.Any())
            {
                throw new Exception("Para deletar esse filme primeiro delete as sessões cadastradas");
            }
            try
            {
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Não foi possível deletar esse filme");
            }
        }
    }
}
