using ConsoleApp1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ConsoleApp1.ClassMapping
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.UserName).IsRequired();
            builder.Property(u => u.FirstName).IsRequired();
            builder.Property(u => u.LastName).IsRequired();
            builder.Property(u => u.EmailAddress).IsRequired();
            builder.Property(u => u.PhoneNumber).IsRequired();
            builder.Property(u => u.Role).IsRequired();
            builder.HasIndex(u => u.EmailAddress).IsUnique();
        }
    }
}
