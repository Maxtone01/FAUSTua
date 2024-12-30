using System.ComponentModel.DataAnnotations;

namespace FaustWeb.Domain.Entities
{
    public class Tag : BaseEntity
    {
        [Required]
        [MaxLength(30)]
        public required string Name { get; set; }

        public ICollection<AssignedTag>? AssignedTags { get; set; }
    }
}
