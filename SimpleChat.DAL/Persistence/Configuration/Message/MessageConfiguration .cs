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
        builder.Property(c => c.Id).ValueGeneratedOnAdd();
        builder.Property(m => m.Content)
               .IsRequired();

        builder.HasOne(m => m.User)
               .WithMany()
               .HasForeignKey(m => m.UserId);

        builder.HasOne(m => m.Chat)
               .WithMany(c => c.Messages)
               .HasForeignKey(m => m.ChatId);
    }
}
