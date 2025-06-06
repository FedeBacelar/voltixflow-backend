using System.ComponentModel.DataAnnotations;

namespace VoltixFlowAPI.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public required string Name { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
