using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FaustWeb.Domain.Entities;

public abstract class BaseEntity
{
    [Key]
    [Column(Order = 0)]
    public Guid Id { get; set; }

    [Required]
    public DateTime CreatedDate { get; set; }

    [Required]
    public DateTime UpdatedDate { get; set; }
}
