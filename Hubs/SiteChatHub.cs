using Microsoft.AspNetCore.SignalR;
using SignalR_App.Services;
using System;
using System.Threading.Tasks;

namespace SignalR_App.Hubs
{
    public class SiteChatHub : Hub
    {
        private readonly IChatRoomService _chatRoomService;
        public SiteChatHub(IChatRoomService chatRoomService)
        {
            _chatRoomService = chatRoomService;
        }

        public async Task SendNewMessage(string sender, string message)
        {
            var roomid = await _chatRoomService.GetChatRoomForConnection(Context.ConnectionId);

            await Clients.Groups(roomid.ToString()).SendAsync("getNewMessage", sender, message, DateTime.Now.ToShortDateString());
        }
        public override async Task OnConnectedAsync()
        {
            var roomid = await _chatRoomService.CreateChatRoom(Context.ConnectionId);

            await Groups.AddToGroupAsync(Context.ConnectionId, roomid.ToString());

            await Clients.Caller.SendAsync("getNewMessage", "پشتیبانی سایت", "من پشتیبان هستم چه کمکی میتوانم بکنم؟", DateTime.Now.ToShortDateString());

            await base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
