using System.ComponentModel.DataAnnotations;

namespace FaustWeb.Domain.Entities.Comments
{
    public class ChapterComment : BaseComment<ChapterComment>
    {
        public Chapter? Chapter { get; set; }

        [Required]
        public required Guid ChapterId { get; set; }

        public override ICollection<ChapterComment>? Replies { get; set; }
    }
}
