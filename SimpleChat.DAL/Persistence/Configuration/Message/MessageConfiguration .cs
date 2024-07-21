using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SimpleChat.DAL.Models.Messages;

namespace SimpleChat.DAL.Persistence.Configuration.Messages;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.ToTable("messages");
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Id).ValueGeneratedOnAdd();
        builder.HasOne(m => m.User)
               .WithMany() 
               .HasForeignKey(m => m.UserId)
               .OnDelete(DeleteBehavior.Restrict); 

        builder.HasOne(m => m.Chat)
               .WithMany(c => c.Messages) 
               .HasForeignKey(m => m.ChatId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
