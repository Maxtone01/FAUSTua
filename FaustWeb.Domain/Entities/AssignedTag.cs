using System.ComponentModel.DataAnnotations;

namespace FaustWeb.Domain.Entities
{
    public class AssignedTag
    {
        public Title? Title { get; set; }

        [Required]
        public required Guid TitleId { get; set; }

        public Tag? Tag { get; set; }

        [Required]
        public required Guid TagId { get; set; }
    }
}
