using System.ComponentModel.DataAnnotations;

namespace VoltixFlowAPI.Models
{
    public class ClientType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public required string Name { get; set; }

        public ICollection<Client> Clients { get; set; } = new List<Client>();
    }
}
