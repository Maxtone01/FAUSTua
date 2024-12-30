using System.ComponentModel.DataAnnotations;

namespace FaustWeb.Domain.Entities.Comments
{
    public class TitleComment : BaseComment<TitleComment>
    {
        public Title? Title { get; set; }

        [Required]
        public required Guid TitleId { get; set; }

        public override ICollection<TitleComment>? Replies { get; set; }
    }
}
