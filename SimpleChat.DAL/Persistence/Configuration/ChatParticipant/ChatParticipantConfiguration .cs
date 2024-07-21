using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SimpleChat.DAL.Models.ChatParticipants;

namespace SimpleChat.DAL.Persistence.Configuration.ChatParticipants;
public class ChatParticipantConfiguration : IEntityTypeConfiguration<ChatParticipant>
{
    public void Configure(EntityTypeBuilder<ChatParticipant> builder)
    {
        builder.ToTable("chat_participants");
        builder.HasKey(cp => cp.Id);
        builder.Property(c => c.Id).ValueGeneratedOnAdd();
        builder.HasOne(cp => cp.Chat)
               .WithMany(c => c.ChatParticipant)
               .HasForeignKey(cp => cp.ChatId);

        builder.HasOne(cp => cp.User)
               .WithMany(u => u.ChatParticipant)
               .HasForeignKey(cp => cp.UserId);
    }
}
