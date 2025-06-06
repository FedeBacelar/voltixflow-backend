using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VoltixFlowAPI.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        [Required]
        [ForeignKey("ClientType")]
        public int ClientTypeId { get; set; }

        [MaxLength(20)]
        public required string Cuit { get; set; }

        [MaxLength(20)]
        public required string Phone { get; set; }

        [MaxLength(100)]
        public required string Contact { get; set; }

        [MaxLength(100)]
        public required string Email { get; set; }

        [MaxLength(200)]
        public required string Address { get; set; }

        public bool Status { get; set; }

        [MaxLength(500)]
        public required string Observations { get; set; }

		public ClientType ClientType { get; set; } = null!;

		public ICollection<DeliveryNote> DeliveryNotes { get; set; } = new List<DeliveryNote>();
    }
}
