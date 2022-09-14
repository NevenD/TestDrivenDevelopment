using Microsoft.AspNetCore.Mvc;
using RoomBookingApp.Core.Models;
using RoomBookingApp.Core.Processors;
using System;
using System.Threading.Tasks;

namespace RoomBookingApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomBookingController : ControllerBase
    {
        private IRoomBookingRequestProcessor _roomBookingRequestProcessor;

        public RoomBookingController(IRoomBookingRequestProcessor roomBookingRequestProcessort)
        {
            _roomBookingRequestProcessor = roomBookingRequestProcessort;
        }

        public async Task<IActionResult> BookRoom(RoomBookingRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
