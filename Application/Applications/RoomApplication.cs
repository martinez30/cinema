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
    public class RoomApplication
    {
        private readonly ICineContext _context;

        public RoomApplication(ICineContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RoomModel>> ListAsync()
        {
            var rooms = await _context.Rooms
                .Include(x=> x.Seats)
                .ToListAsync();

            return rooms.Select(x => new RoomModel(x)).OrderByDescending(x => x.Name);
        }

        public async Task RegisterAsync(RoomModel model)
        {
            if (_context.Rooms.Any(x => x.Id == model.Id))
                throw new DuplicatedExcpection("Sala já cadastrada");
            var room = new Room(model.Name);
            foreach (var item in model.Seats)
            {
                room.Seats.Add(new Seat(room, item.Row, item.Column));
            }
            try
            {
                await _context.Rooms.AddAsync(room);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Não foi possível cadastrar essa sessão");
            }
        }

        public async Task<RoomModel> GetAsync(int id)
        {
            var room = await _context.Rooms
                .Include(x=> x.Seats)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (room == null)
                throw new NotFoundException("Sala não encontrada");
            return new RoomModel(room);
        }

        public async Task EditAsync(RoomModel model)
        {
            var room = await _context.Rooms
                .Include(x=> x.Seats)
                .FirstOrDefaultAsync(x => x.Id == model.Id);
            if (room == null)
            {
                throw new NotFoundException("Sala não Encontrada");
            }
            room.Name = model.Name;
            foreach (var item in model.Seats)
            {
                if (!room.Seats.Any(x=>x.Column == item.Column && x.Row == item.Row))
                {
                    room.Seats.Add(new Seat(room, item.Row, item.Column));
                }
            }
            try
            {
                _context.Rooms.Update(room);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Não foi possível cadastrar esta sala");
            }
        }

        public async Task DeleteAsync(RoomModel model)
        {
            var room = await _context.Rooms
                .Include(x=> x.Seats)
                .Include(x=> x.Sessions)
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (room == null)
            {
                throw new NotFoundException("Sala não Encontrada");
            }
            if(room.Sessions.Any())
            {
                throw new Exception("Não foi possível deletar esta sala pois há sessões vinculadas a essa sala");
            }
            try
            {
                _context.Seats.RemoveRange(room.Seats);
                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Não foi possível deletar esta sala");
            }
        }
    }
}
