using FaustWeb.Domain.Entities.Comments;
using FaustWeb.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace FaustWeb.Domain.Entities
{
    public class Title : BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public required string Name { get; set; }

        [Required]
        [MaxLength(5000)]
        public required string Description { get; set; }

        public MangaType MangaType { get; set; }
        public TranslationStatus TranslationStatus { get; set; }
        public AgeBracket AgeBracket { get; set; }

        public DateOnly? PublicationStart { get; set; }
        public DateOnly? PublicationFinished { get; set; }

        public Artist? Artist { get; set; }
        public Guid? ArtistId { get; set; }

        public Author? Author { get; set; }
        public Guid? AuthorId { get; set; }


        public ICollection<AssignedTag>? Tags { get; set; }
        public ICollection<TitleSaved>? Saved { get; set; }
        public ICollection<TitleComment>? Comments { get; set; }
        public ICollection<TitleDislike>? Dislikes { get; set; }
        public ICollection<TitleLike>? Likes { get; set; }
        public ICollection<Volume>? Volumes { get; set; }

        public int? ReleasedChaptersCount { get; set; }
        public int? TranslatedChaptersCount { get; set; } //TODO - consider making it a computed column
        public string? FolderPath { get; set; } // not sure what to do with this yet
    }
}
