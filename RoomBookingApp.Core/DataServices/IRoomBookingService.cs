using RoomBookingApp.Core.Domain;
using System;
using System.Collections.Generic;

namespace RoomBookingApp.Core.DataServices
{
    public interface IRoomBookingService
    {
        void SaveRoom(RoomBooking roomBooking);

        IEnumerable<Room> GetAvailableRooms(DateTime date);
    }
}
