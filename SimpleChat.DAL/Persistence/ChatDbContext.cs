using Microsoft.EntityFrameworkCore;
using SimpleChat.DAL.Models.ChatParticipants;
using SimpleChat.DAL.Models.Chats;
using SimpleChat.DAL.Models.Messages;
using SimpleChat.DAL.Models.Users;

namespace SimpleChat.DAL.Persistence;

public class ChatDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<ChatParticipant> ChatUsers { get; set; }
    public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=DESKTOP-TA43178;Initial Catalog=simple_chat_db;Integrated Security=True;Connect Timeout=30;TrustServerCertificate=True");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ChatDbContext).Assembly);
    }
}
