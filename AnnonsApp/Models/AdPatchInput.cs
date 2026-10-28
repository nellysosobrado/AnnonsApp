using System.ComponentModel.DataAnnotations;

namespace AnnonsApp.Models;

public class AdPatchInput
{
    public string? Title { get; set; }
    public string? Description { get; set; }

    [Range(0, double.MaxValue)]
    public decimal? Price { get; set; }
}
