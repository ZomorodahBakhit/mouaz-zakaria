using System;
using System.Collections.Generic;
using System.Text;
using ConsoleApp1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConsoleApp1.ClassMapping
{
    internal class GradeConfiguration : IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.HasOne(g => g.Assignment)
                .WithMany(a => a.Grades)
                .HasForeignKey(g => g.AssignmentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(g => g.Student)
                .WithMany(u => u.Grades)
                .HasForeignKey(g => g.StudentId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
