using System.ComponentModel.DataAnnotations;

namespace FaustWeb.Domain.Entities
{
    public class ChapterLike
    {
        public Chapter? Chapter { get; set; }

        [Required]
        public required Guid ChapterId { get; set; }

        public User? User { get; set; }

        [Required]
        public required Guid UserId { get; set; }

        [Required]
        public DateTime LikeDate { get; set; }
    }
}
