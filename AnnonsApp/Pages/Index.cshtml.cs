using AnnonsApp.Data;
using AnnonsApp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AnnonsApp.Pages;

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;

    public IndexModel(AppDbContext context)
    {
        _context = context;
    }

    public List<Ad> Ads { get; set; } = new();

    public async Task OnGetAsync()
    {
        Ads = await _context.Ads
            .AsNoTracking()
            .OrderByDescending(ad => ad.Id)
            .ToListAsync();
    }
}