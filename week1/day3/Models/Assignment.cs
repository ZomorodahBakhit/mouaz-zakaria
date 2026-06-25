using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        public string AssignmentTitle { get; set; } = null!;
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
        public string? Description { get; set; }
        public float Weight { get; set; }
        public int MaxGrade { get; set; }
        public DateOnly DueDate { get; set; }

        public ICollection<Grade>? Grades { get; set; }
        public ICollection<Comment>? Comments { get; set; }
    }
}
