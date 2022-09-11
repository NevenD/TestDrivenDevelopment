using Moq;
using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Core.Domain;
using RoomBookingApp.Core.Enums;
using RoomBookingApp.Core.Models;
using RoomBookingApp.Core.Processors;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace RoomBookingApp.Core.Tests
{
    public class RoomBookingRequestProcessorTest
    {
        private RoomBookingRequestProcessor _processor;
        private RoomBookingRequest _request;
        private Mock<IRoomBookingService> _roomBookingServiceMock;
        private List<Room> _availableRooms;

        public RoomBookingRequestProcessorTest()
        {
            // Arrange
            _request = new RoomBookingRequest
            {
                FullName = "Test Name",
                Email = "test@request.com",
                Date = new DateTime(2021, 10, 20)
            };
            _availableRooms = new List<Room>() { new Room() { Id = 1 } };

            _roomBookingServiceMock = new Mock<IRoomBookingService>();
            _roomBookingServiceMock.Setup(r => r.GetAvailableRooms(_request.Date))
                .Returns(_availableRooms);
            _processor = new RoomBookingRequestProcessor(_roomBookingServiceMock.Object);

        }

        [Fact]
        public void Should_Return_Room_Booking_Response_With_Request_Values()
        {

            // Act
            RoomBookingResult result = _processor.BookRoom(_request);

            // Assert
            Assert.NotNull(result);
            result.ShouldNotBeNull();

            Assert.Equal(_request.FullName, result.FullName);
            Assert.Equal(_request.Email, result.Email);
            Assert.Equal(_request.Date, result.Date);

            result.FullName.ShouldBe(_request.FullName);
            result.Email.ShouldBe(_request.Email);
            result.Date.ShouldBe(_request.Date);

        }

        [Fact]
        public void Should_Throw_Exception_For_Null_Request()
        {
            var exception = Should.Throw<ArgumentNullException>(() => _processor.BookRoom(null));
            //Assert.Throws<ArgumentNullException>(() => processor.BookRoom(null));
            exception.ParamName.ShouldBe("bookingRequest");
        }

        [Fact]
        public void Should_Save_Room_Booking_Request()
        {
            RoomBooking savedBooking = null;
            // it.IsAny -> give me any object as long as it is RoomBooking
            _roomBookingServiceMock.Setup(r => r.SaveRoom(It.IsAny<RoomBooking>()))
                .Callback<RoomBooking>(booking => { savedBooking = booking; });

            _processor.BookRoom(_request);

            // verifing that this method should be called only once
            _roomBookingServiceMock.Verify(r => r.SaveRoom(It.IsAny<RoomBooking>()), Times.Once);

            savedBooking.ShouldNotBeNull();

            savedBooking.FullName.ShouldBe(_request.FullName);
            savedBooking.Email.ShouldBe(_request.Email);
            savedBooking.Date.ShouldBe(_request.Date);
            savedBooking.RoomId.ShouldBe(_availableRooms.First().Id);
        }

        [Fact]
        public void Should_Not_Save_Room_Booking_Request_Id_None_Available()
        {
            _availableRooms.Clear();
            _processor.BookRoom(_request);
            _roomBookingServiceMock.Verify(r => r.SaveRoom(It.IsAny<RoomBooking>()), Times.Never);
        }


        // Example: Data driven tests
        [Theory]
        [InlineData(BookingResultFlag.Failure, false)]
        [InlineData(BookingResultFlag.Success, true)]
        public void Should_Return_Success_Failure_Flag_In_Result(BookingResultFlag bookingSuccessFlag, bool isAvailable)
        {

            if (!isAvailable)
            {
                _availableRooms.Clear();
            }

            var result = _processor.BookRoom(_request);
            bookingSuccessFlag.ShouldBe(result.Flag);
        }

        [Theory]
        [InlineData(1, true)] // available
        [InlineData(null, false)]
        public void Should_Return_RoomBookingId_In_Result(int? roomBookingId, bool isAvailable)
        {
            if (!isAvailable)
            {
                _availableRooms.Clear();
            }
            else
            {
                _roomBookingServiceMock.Setup(r => r.SaveRoom(It.IsAny<RoomBooking>()))
              .Callback<RoomBooking>(booking =>
              {
                  booking.Id = roomBookingId.Value;
              });
            }

            var result = _processor.BookRoom(_request);
            result.RoomBookingId.ShouldBe(roomBookingId);
        }
    }
}
