using System.ComponentModel.DataAnnotations;

namespace SampleWeb.API.Models.DTO
{
    public class CreateEmployeeDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name has to be a maximum of 100 characters")]
        public string Name { get; set; }
        [Required]
        public int Experience { get; set; }
        [Required]
        public string PrimarySkill { get; set; }
    }
}
