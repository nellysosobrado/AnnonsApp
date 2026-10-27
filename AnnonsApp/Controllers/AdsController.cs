using AnnonsApp.Data;
using AnnonsApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnnonsApp.Controllers;

[ApiController]
[Route("Ads")]
public class AdsController: ControllerBase
{
    private readonly AppDbContext _context;

    public AdsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Ad>>> GetAll()
    {
        var ads = await _context.Ads.ToListAsync();
        return Ok(ads);
    }
}
