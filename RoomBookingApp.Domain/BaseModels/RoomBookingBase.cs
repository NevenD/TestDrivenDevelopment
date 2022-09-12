using System;

namespace RoomBookingApp.Domain.BaseModels
{
    /// <summary>
    /// We don't want to create an instance from Room booking base class so we add abstract
    /// </summary>
    public abstract class RoomBookingBase
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
    }
}