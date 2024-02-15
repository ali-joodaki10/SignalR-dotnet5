using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalR_App.Services;
using System.Threading.Tasks;

namespace SignalR_App.Hubs
{
    [Authorize]
    public class SupportHub:Hub
    {
        private readonly IChatRoomService _chatRoomService;
        public SupportHub(IChatRoomService chatRoomService)
        {
            _chatRoomService = chatRoomService;
        }

        public override async Task OnConnectedAsync()
        {
            var rooms = await _chatRoomService.GetAllRooms();

            await Clients.Caller.SendAsync("GetRooms", rooms);

            await base.OnConnectedAsync();
        }
    }
}
