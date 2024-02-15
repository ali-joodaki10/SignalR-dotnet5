using Microsoft.EntityFrameworkCore;
using SignalR_App.Models.Entities;

namespace SignalR_App.Context
{
    public class DatabaseContext:DbContext
    {

        public DatabaseContext(DbContextOptions options) : base(options) { }
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
