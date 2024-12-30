using System.ComponentModel.DataAnnotations;

namespace FaustWeb.Domain.Entities
{
    public class Artist : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public required string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public required string LastName { get; set; }

        public ICollection<Title>? Works { get; set; }
    }
}
