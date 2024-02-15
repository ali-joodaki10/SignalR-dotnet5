using SignalR_App.Context;
using SignalR_App.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR_App.Services
{
    public interface IChatRoomService
    {
        Task<Guid> CreateChatRoom(string connectionId);
        Task<Guid> GetChatRoomForConnection(string connectionId);
        Task<List<Guid>> GetAllRooms();
    }

    public class ChatRoomService : IChatRoomService
    {
        private readonly DatabaseContext _context;
        public ChatRoomService(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<Guid> CreateChatRoom(string connectionId)
        {
            var existChatRoom = _context.ChatRooms.SingleOrDefault(p => p.ConnectionId == connectionId);
            if (existChatRoom != null)
            {
                return await Task.FromResult(existChatRoom.Id);
            }

            ChatRoom chatRoom = new ChatRoom()
            {
                ConnectionId = connectionId,
                Id = Guid.NewGuid(),
            };
            _context.Add(chatRoom);
            _context.SaveChanges();

            return await Task.FromResult(chatRoom.Id);
        }

        public async Task<List<Guid>> GetAllRooms()
        {
            var rooms=_context.ChatRooms.Select(r=>r.Id).ToList();
            return await Task.FromResult(rooms);
        }

        public async Task<Guid> GetChatRoomForConnection(string connectionId)
        {
            var chatRoom = _context.ChatRooms.SingleOrDefault(p => p.ConnectionId == connectionId);

            return await Task.FromResult(chatRoom.Id);

        }
    }
}
