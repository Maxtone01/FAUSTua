using FaustWeb.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace FaustWeb.Domain.Entities
{
    public class TranslationTeamMember
    {
        public TranslationTeam? Team { get; set; }

        [Required]
        public required Guid TeamId { get; set; }

        public User? User { get; set; }

        [Required]
        public required Guid UserId { get; set; }


        [Required]
        public required TranslationTeamRole Role { get; set; }
    }
}
