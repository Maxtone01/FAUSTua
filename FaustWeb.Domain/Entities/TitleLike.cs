using System.ComponentModel.DataAnnotations;

namespace FaustWeb.Domain.Entities
{
    public class TitleLike
    {
        public Title? Title { get; set; }

        [Required]
        public required Guid TitleId { get; set; }

        public User? User { get; set; }

        [Required]
        public required Guid UserId { get; set; }

        [Required]
        public DateTime LikeDate { get; set; }
    }
}
