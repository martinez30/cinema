using Application.Models;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class SessionApplication
    {
        private readonly ICineContext _context;
        public SessionApplication(ICineContext context)
        {
            _context = context;
        }
        public async Task RegisterAsync(SessionModel model)
        {
            if (_context.Sessions.Any(x => x.Date == model.Date && x.Movie.Id == model.MovieId && x.Room.Id == model.RoomId))
                throw new DuplicatedExcpection("Sessão já cadastrada");
            var movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id == model.MovieId);
            var room = await _context.Rooms.FirstOrDefaultAsync(x => x.Id == model.RoomId);
            var session = new Session(movie, room, model.Date, model.Amount, model.DescontAllowed);
            try
            {
                await _context.Sessions.AddAsync(session);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Não foi possível cadastrar essa sessão");
            }
        }
        public async Task<SessionModel> GetAsync(int id)
        {
            var session = await _context.Sessions
                .Include(x => x.Movie)
                .Include(x => x.Room)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (session == null)
                throw new NotFoundException("Sessão não encontrado");
            return new SessionModel(session);
        }
        public async Task<IEnumerable<SeatModel>> ListAvaliableSeats(int sessionid)
        {
            var session = await _context.Sessions
               .Include(x => x.Room)
                    .ThenInclude(x => x.Seats)
               .FirstOrDefaultAsync(x => x.Id == sessionid);

            var tickets = await _context.Tickets
                .Include(x => x.Seat)
                .Where(x => x.Session.Id == sessionid).ToListAsync();

            var model = new List<SeatModel>();

            foreach (var item in session.Room.Seats)
            {
                if (!tickets.Select(x => x.Seat.Id).Contains(item.Id))
                {
                    model.Add(new SeatModel(item));
                    model.Last(x => x.Avaliable = true);   
                }
            }
            return model;

        }
        public async Task EditAsync(SessionModel model)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id == model.MovieId);
            if (movie == null)
            {
                throw new NotFoundException("Filme não Encontrado");
            }
            var room = await _context.Rooms.FirstOrDefaultAsync(x => x.Id == model.RoomId);
            var session = await _context.Sessions.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (session == null)
                throw new NotFoundException("Sessão não encontrada");

            session.Movie = movie;
            session.Room = room;
            session.Date = model.Date;
            session.Amount = model.Amount;
            session.DescontAllowed = model.DescontAllowed;

            try
            {
                _context.Sessions.Update(session);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Não foi possível cadastrar essa sessão");
            }
        }
        public async Task DeleteAsync(SessionModel model)
        {
            var session = await _context.Sessions
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (session == null)
            {
                throw new NotFoundException("Filme não Encontrado");
            }

            if (session.Tickets != null && session.Tickets.Any())
            {
                throw new Exception("Não é possivel deletar esta sessão porque já existem ingressos vendidos");
            }
            try
            {
                _context.Sessions.Remove(session);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Não foi possível deletar essa sessão");
            }
        }
        public async Task<IEnumerable<SessionModel>> ListAsync()
        {
            var sessions = await _context.Sessions
                .Include(x => x.Movie)
                .Include(x => x.Room)
                .ToListAsync();
            return sessions.Select(x => new SessionModel(x)).OrderByDescending(x => x.Id);
        }
        public async Task<IEnumerable<SessionModel>> ListAsync(string movieName, DateTime? date)
        {
            var query = _context.Sessions
                .Include(x => x.Movie)
                .Include(x => x.Room)
                .Where(x => x.Date >= DateTime.Now)
                .AsQueryable();

            if (!string.IsNullOrEmpty(movieName))
            {
                query = query.Where(x => x.Movie.Name.ToLower().Contains(movieName.Trim().ToLower()));
            }
            if (date.HasValue)
            {
                query = query.Where(x => x.Date.Date == date.Value.Date);
            }

            var sessions = await query
                .ToListAsync();
            return sessions.Select(x => new SessionModel(x)).OrderBy(x => x.Date);
        }
        public async Task<SessionBuyTicketModel> GetSessionToBuyTicket(int id)
        {
            var session = await _context.Sessions
               .Include(x => x.Movie)
               .Include(x => x.Room)
                    .ThenInclude(x => x.Seats)                    
               .FirstOrDefaultAsync(x => x.Id == id);

            var tickets = await _context.Tickets
                .Include(x => x.Seat)
                .Where(x => x.Session.Id == id)
                .ToListAsync();

            var seats = new List<Seat>();
            foreach (var seat in session.Room.Seats)
            {
                    seats.Add(seat);
            }
            
            return new SessionBuyTicketModel(session, seats, tickets);
        }
    }
}
