using System;

namespace SignalR_App.Models.Entities
{
    public class ChatRoom
    {
        public Guid Id { get; set; }
        public string ConnectionId { get; set; }
    }
}
