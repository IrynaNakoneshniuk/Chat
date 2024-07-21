using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SimpleChat.DAL.Models.Users;

namespace SimpleChat.DAL.Persistence.Configuration.Users;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.Property(c => c.Id).ValueGeneratedOnAdd();
        builder.HasKey(u => u.Id);
        builder.Property(u => u.UserName)
               .IsRequired()
               .HasMaxLength(100);
        builder.Property(u => u.Email)
               .IsRequired()
               .HasMaxLength(100);
        builder.Property(c => c.ConnectionId)
        .HasMaxLength(200);
    }
}