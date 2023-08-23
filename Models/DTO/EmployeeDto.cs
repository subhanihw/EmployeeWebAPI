using System.ComponentModel.DataAnnotations;

namespace SampleWeb.API.Models.DTO
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Experience { get; set; }
        public string PrimarySkill { get; set; }
    }
}
