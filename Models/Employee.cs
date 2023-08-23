using System.ComponentModel.DataAnnotations;

namespace SampleWeb.API.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Experience { get; set; }
        [Required]
        public string PrimarySkill { get; set; }

    }
}
