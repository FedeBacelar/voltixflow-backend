using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace VoltixFlowAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Username { get; set; }

        [Required]
		public string PasswordHash { get; set; } = null!;

		[Required]
        [ForeignKey("Role")]
        public int RoleId { get; set; }

        public bool Active { get; set; }

		public Role Role { get; set; } = null!;
	}
}
