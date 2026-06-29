using System.ComponentModel.DataAnnotations;

namespace University.Core.Forms
{
    public class UpdateStudentForm
    {
        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string Email { get; set; } = null!;
    }
}
