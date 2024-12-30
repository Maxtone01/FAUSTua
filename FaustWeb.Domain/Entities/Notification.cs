using System.ComponentModel.DataAnnotations;

namespace FaustWeb.Domain.Entities
{
    public class Notification : BaseEntity
    {
        public User? User { get; set; }
        [Required]
        public required Guid UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public required string Message { get; set; }

        public Uri? NotificationSourceUri { get; set; }

        public bool Acknowledged { get; set; }
    }
}
