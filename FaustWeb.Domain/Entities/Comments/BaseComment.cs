using System.ComponentModel.DataAnnotations;

namespace FaustWeb.Domain.Entities.Comments
{
    public abstract class BaseComment<TComment> : BaseEntity
        where TComment : BaseComment<TComment>
    {
        public User? Author { get; set; }

        [Required]
        public required Guid AuthorId { get; set; }

        [Required]
        [MaxLength(500)]
        public required string Content { get; set; }

        public TComment? ReplyToComment { get; set; }
        public Guid? ReplyToCommentId { get; set; }
        public ICollection<CommentLike<TComment>>? Likes { get; set; }
        public ICollection<CommentDislike<TComment>>? Dislikes { get; set; }

        public abstract ICollection<TComment>? Replies { get; set; }
    }
}
