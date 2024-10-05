using System.ComponentModel.DataAnnotations.Schema;

namespace FaustWeb.Domain.Entities;

public class BaseEntity
{
    [Column(Order = 0)]
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}
