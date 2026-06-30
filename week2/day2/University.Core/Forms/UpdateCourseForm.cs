using System.ComponentModel.DataAnnotations;

namespace University.Core.Forms
{
    public class UpdateCourseForm
    {
        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Range(0, 100)]
        public int Weight { get; set; }
    }
}
