using FaustWeb.Domain.Entities.Comments;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FaustWeb.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public User() : base()
        {
            Id = Guid.NewGuid();
            SecurityStamp = Guid.NewGuid().ToString();
        }

        public Uri? AvatarImageUri { get; set; }
        public Uri? BackgroundImageUri { get; set; }

        [Required]
        public required string Tag { get; set; }

        public TranslationTeam? TranslationTeamOwned { get; set; }
        public Guid? TranslationTeamId { get; set; }
        public ICollection<TranslationTeamMember>? TranslationTeamsMember { get; set; }

        public ICollection<CommentDislike<TitleComment>>? TitleCommentDislikes { get; set; }
        public ICollection<CommentLike<TitleComment>>? TitleCommentLikes { get; set; }
        public ICollection<TitleComment>? TitleComments { get; set; }
        public ICollection<TitleDislike>? TitleDislikes { get; set; }
        public ICollection<TitleLike>? TitleLikes { get; set; }
        public ICollection<TitleSaved>? SavedTitles { get; set; }

        public ICollection<ChapterComment>? ChapterComments { get; set; }
        public ICollection<ChapterDislike>? ChapterDislikes { get; set; }
        public ICollection<ChapterLike>? ChapterLikes { get; set; }
        public ICollection<CommentLike<ChapterComment>>? ChapterCommentLikes { get; set; }
        public ICollection<CommentDislike<ChapterComment>>? ChapterCommentDislikes { get; set; }

        public ICollection<Notification>? Notifications { get; set; }

        public bool IsPrivate { get; set; }
        public bool HideDislikedChapters { get; set; }
        public bool HideLikedChapters { get; set; }
        public bool HideDislikedTitles { get; set; }
        public bool HideLikedTitles { get; set; }
        public bool HideSavedTitles { get; set; }
        public bool HideComments { get; set; }
        public bool HideCommentReplies { get; set; }
        public bool HideKarma { get; set; }

        //can be used to show comment reactions, sort of twitter like reposts if we want it (if we do we should also add dateTime to CommentLike/Dislike)
        //public bool HideCommentReaction { get; set; } 
    }
}
