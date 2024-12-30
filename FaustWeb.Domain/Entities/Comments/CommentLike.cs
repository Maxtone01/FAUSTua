using System.ComponentModel.DataAnnotations;

namespace FaustWeb.Domain.Entities.Comments
{
    public class CommentLike<TComment>
        where TComment : BaseComment<TComment>
    {
        public User? User { get; set; }

        [Required]
        public required Guid UserId { get; set; }

        public TComment? Comment { get; set; }

        [Required]
        public required Guid CommentId { get; set; }
    }
}
