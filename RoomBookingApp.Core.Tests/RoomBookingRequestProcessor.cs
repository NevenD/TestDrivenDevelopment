namespace RoomBookingApp.Core.Tests
{
    internal class RoomBookingRequestProcessor
    {
        public RoomBookingRequestProcessor()
        {
        }

        public RoomBookingResult BookRoom(RoomBookingRequest bookingRequest)
        {

            return new RoomBookingResult
            {
                FullName = bookingRequest.FullName,
                Date = bookingRequest.Date,
                Email = bookingRequest.Email
            };
        }
    }
}