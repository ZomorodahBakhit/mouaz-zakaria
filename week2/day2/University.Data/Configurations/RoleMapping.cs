using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using University.Data.Entities;

namespace University.Data.Configurations
{
    public class RoleMapping : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("RoleId");

            builder.HasData(
                new Role { Id = 2, Name = "Admin" , ConcurrencyStamp = "1"},
                new Role { Id = 3, Name = "Student", ConcurrencyStamp = "1"},
                new Role { Id = 4, Name = "Teacher", ConcurrencyStamp = "1" }
            );
        }
    }
}