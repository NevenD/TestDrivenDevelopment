using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Core.Domain;
using RoomBookingApp.Core.Models;
using System;

namespace RoomBookingApp.Core.Processors
{
    public class RoomBookingRequestProcessor
    {

        public readonly IRoomBookingService _roomBookingService;

        public RoomBookingRequestProcessor(IRoomBookingService roomBookingService)
        {
            _roomBookingService = roomBookingService;
        }


        public RoomBookingResult BookRoom(RoomBookingRequest bookingRequest)
        {

            if (bookingRequest is null)
            {
                throw new ArgumentNullException(nameof(bookingRequest));
            }

            _roomBookingService.SaveRoom(CreateRoomBookingObject<RoomBooking>(bookingRequest));

            return CreateRoomBookingObject<RoomBookingResult>(bookingRequest);
        }

        /// <summary>
        /// Anything inheriting RoomBookingBase will be used as generic.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bookingRequest"></param>
        /// <returns></returns>
        private static T CreateRoomBookingObject<T>(RoomBookingRequest bookingRequest) where T : RoomBookingBase, new()
        {
            return new T
            {
                FullName = bookingRequest.FullName,
                Date = bookingRequest.Date,
                Email = bookingRequest.Email
            };
        }
    }
}