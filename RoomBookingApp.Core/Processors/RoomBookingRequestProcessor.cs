using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Core.Domain;
using RoomBookingApp.Core.Enums;
using RoomBookingApp.Core.Models;
using RoomBookingApp.Domain.BaseModels;
using System;
using System.Linq;

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

            var availableRooms = _roomBookingService.GetAvailableRooms(bookingRequest.Date);
            var result = CreateRoomBookingObject<RoomBookingResult>(bookingRequest);
            if (availableRooms.Any())
            {
                var room = availableRooms.FirstOrDefault();
                var roomBooking = CreateRoomBookingObject<RoomBooking>(bookingRequest);
                roomBooking.RoomId = room.Id;
                _roomBookingService.SaveRoom(roomBooking);

                result.RoomBookingId = roomBooking.Id;
                result.Flag = BookingResultFlag.Success;
            }
            else
            {
                result.Flag = BookingResultFlag.Failure;
            }

            return result;
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