using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RoomBookingApp.Persistance.Repositories
{
    public sealed class RoomBookingService : IRoomBookingService
    {
        private readonly RoomBookingAppDbContext _dbContext;
        public RoomBookingService(RoomBookingAppDbContext context)
        {
            _dbContext = context;
        }


        public IEnumerable<Room> GetAvailableRooms(DateTime date)
        {
            var availableRooms = _dbContext.Rooms.Where(r => !r.RoomBookings.Any(x => x.Date == date)).ToList();
            return availableRooms;
        }

        public void SaveRoom(RoomBooking roomBooking)
        {
            _dbContext.Add(roomBooking);
            _dbContext.SaveChanges();
        }
    }
}
