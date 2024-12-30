using FaustWeb.Domain.Entities.Comments;
using System.ComponentModel.DataAnnotations;

namespace FaustWeb.Domain.Entities
{
    public class Chapter : BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public required string Name { get; set; }

        [Required]
        public required int Number { get; set; }

        public TranslationTeam? TranslationTeam { get; set; }
        [Required]
        public required Guid TranslationTeamId { get; set; }

        public Volume? Volume { get; set; }
        [Required]
        public required Guid VolumeId { get; set; }

        public ICollection<ChapterComment>? Comments { get; set; }

        public ICollection<ChapterDislike>? Dislikes { get; set; }
        public ICollection<ChapterLike>? Likes { get; set; }

        public string? FolderPath { get; set; } // not sure if this is needed
    }
}
