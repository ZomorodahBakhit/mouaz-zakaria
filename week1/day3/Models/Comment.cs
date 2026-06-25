using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string? CommentContent { get; set; }
        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; } = null!;
    }
}
