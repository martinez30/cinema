//using Application.Models;
//using Domain;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Application
//{
//    public class TicketApplication
//    {
//        private readonly ICineContext _context;

//        public TicketApplication(ICineContext context)
//        {
//            _context = context;
//        }

//        public async Task<IEnumerable<TicketModel>> ListAsync()
//        {
//            var tickets = await _context.Tickets.ToListAsync();
//            return tickets.Select(x => new TicketModel(x));
//        }

//        public async Task RegisterAsync(TicketModel model)
//        {
//            if (_context.Tickets.Any(x => x.Id == model.Id))
//                throw new DuplicatedExcpection("TIngresso já cadastrado");
//            var movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id == model.MovieId);
//            var room = await _context.Rooms.FirstOrDefaultAsync(x => x.Id == model.RoomId);
//            var session = new Session(movie,room,model.Date,model.Amount,model.DescontAllowed);
//            try
//            {
//                await _context.Sessions.AddAsync(session);
//                await _context.SaveChangesAsync();
//            }
//            catch
//            {
//                throw new Exception("Não foi possível cadastrar essa sessão");
//            }
//        }

//        public async Task<SessionModel> GetAsync(int id)
//        {
//            var session = await _context.Sessions
//                .Include(x=> x.Movie)
//                .Include(x=> x.Room)
//                .FirstOrDefaultAsync(x => x.Id == id);
//            if (session == null)
//                throw new NotFoundException("Sessão não encontrado");
//            return new SessionModel(session);
//        }

//        public async Task EditAsync(SessionModel model)
//        {
//            var movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id == model.MovieId);
//            if (movie == null)
//            {
//                throw new NotFoundException("Filme não Encontrado");
//            }
//            var room = await _context.Rooms.FirstOrDefaultAsync(x => x.Id == model.RoomId);
//            var session = await _context.Sessions.FirstOrDefaultAsync(x => x.Id == model.Id);
//            if (session == null)
//                throw new NotFoundException("Sessão não encontrada");

//            session.Movie = movie;
//            session.Room = room;
//            session.Date = model.Date;
//            session.Amount = model.Amount;
//            session.DescontAllowed = model.DescontAllowed;

//            try
//            {
//                _context.Sessions.Update(session);
//                await _context.SaveChangesAsync();
//            }
//            catch
//            {
//                throw new Exception("Não foi possível cadastrar essa sessão");
//            }
//        }

//        public async Task DeleteAsync(SessionModel model)
//        {
//            var session = await _context.Sessions
//                .FirstOrDefaultAsync(x => x.Id == model.Id);

//            if (session == null)
//            {
//                throw new NotFoundException("Filme não Encontrado");
//            }

//            if (session.Tickets != null && session.Tickets.Any())
//            {
//                throw new Exception("Não é possivel deletar esta sessão porque já existem ingressos vendidos");
//            }
//            try
//            {
//                _context.Sessions.Remove(session);
//                await _context.SaveChangesAsync();
//            }
//            catch
//            {
//                throw new Exception("Não foi possível deletar essa sessão");
//            }
//        }
//    }
//}
