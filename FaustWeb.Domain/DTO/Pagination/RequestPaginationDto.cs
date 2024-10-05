using System.ComponentModel.DataAnnotations;

namespace FaustWeb.Domain.DTO.Pagination;

public class RequestPaginationDto
{
    [Required]
    public int PageNumber { get; set; } = 1;

    [Required]
    public int PageSize { get; set; } = 20;
}
