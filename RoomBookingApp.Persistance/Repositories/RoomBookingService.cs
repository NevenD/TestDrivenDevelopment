using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Core.Domain;
using System;
using System.Collections.Generic;

namespace RoomBookingApp.Persistance.Repositories
{
    public sealed class RoomBookingService : IRoomBookingService
    {
        public IEnumerable<Room> GetAvailableRooms(DateTime date)
        {
            throw new NotImplementedException();
        }

        public void SaveRoom(RoomBooking roomBooking)
        {
            throw new NotImplementedException();
        }
    }
}
