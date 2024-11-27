using System.ComponentModel.DataAnnotations;

namespace Phuong.eShop.BlazorApp.Application.Models;

public class CatalogBrandDto
{
    public long Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;
}