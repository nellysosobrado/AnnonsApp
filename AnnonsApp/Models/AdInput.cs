using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AnnonsApp.Models;

public class AdInput
{
    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [JsonRequired]
    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }
}
