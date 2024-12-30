using System.ComponentModel.DataAnnotations;

namespace FaustWeb.Domain.Entities
{
    public class TranslationTeam : BaseEntity
    {
        public User? Owner { get; set; }

        [Required]
        public required Guid OwnerId { get; set; }

        [Required]
        [MaxLength(50)]
        public required string Name { get; set; }
        [MaxLength(50)]
        public string? Nickname { get; set; } // do we need it? how is it different from name?

        [Required]
        [MaxLength(500)]
        public required string Description { get; set; }

        public Uri? BackgroundImageUri { get; set; } //not sure how this should work, subject to change
        public Uri? AvatarImageUri { get; set; } //not sure how this should work, subject to change

        public ICollection<Chapter>? Chapters { get; set; }
        public ICollection<TranslationTeamMember>? Members { get; set; }

        public bool HideLikeCount { get; set; }
        public bool HideViewCount { get; set; }
        public bool HideCommentCount { get; set; }
        public bool HideHighestActivityOnTitle { get; set; }


        public bool HideCommentsInProfile { get; set; }
        public bool HideSaved { get; set; }
        public bool HideTeamMembers { get; set; }

        public Uri? InstagramUri { get; set; }
        public Uri? TelegramUrl { get; set; }
        public Uri? TikTokUri { get; set; }
        public Uri? DiscordUri { get; set; }
        public Uri? DonateUri { get; set; }
    }
}
