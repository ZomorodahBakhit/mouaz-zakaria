using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Models
{
    public class Grade
    {
        public int Id { get; set; }
        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; } = null!;
        public int StudentId { get; set; }
        public User Student { get; set; } = null!;
        public float Score { get; set; }
    }
}
