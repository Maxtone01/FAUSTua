using System.ComponentModel.DataAnnotations;

namespace FaustWeb.Domain.Entities
{
    public class Volume : BaseEntity
    {
        public Title? Title { get; set; }

        [Required]
        public required Guid? TitleId { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required int VolumeOrder { get; set; }

        [Required]
        public required int ChaptersFrom { get; set; }

        public int? ChaptersTo { get; set; }

        public ICollection<Chapter>? Chapters { get; set; }
    }
}
