using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Role { get; set; } = null!;

        public ICollection<Course>? Courses { get; set; } = new List<Course>();
        public ICollection<Grade>? Grades { get; set; } = new List<Grade>();
        public ICollection<Comment>? Comments { get; set; } = new List<Comment>();

    }
}
