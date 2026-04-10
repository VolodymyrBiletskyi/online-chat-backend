using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using online_chat.Models;

namespace online_chat.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserChatRoom>()
                .HasKey(x => new { x.UserId, x.ChatRoomId });

            modelBuilder.Entity<UserChatRoom>()
                .HasOne(x => x.User)
                .WithMany(x => x.UserChatRooms)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<UserChatRoom>()
                .HasOne(x => x.ChatRoom)
                .WithMany(x => x.UserChatRooms)
                .HasForeignKey(x => x.ChatRoomId);

            modelBuilder.Entity<ChatMessage>()
                .HasKey(x => x.ChatMessageId);

            modelBuilder.Entity<ChatMessage>()
                .HasOne(x => x.ChatRoom)
                .WithMany(x => x.Messages)
                .HasForeignKey(x => x.ChatRoomId);

            modelBuilder.Entity<ChatMessage>()
                .HasOne(x => x.User)
                .WithMany(x => x.Messages)
                .HasForeignKey(x => x.UserId);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<UserChatRoom> UserChatRooms { get; set; }
    }
}