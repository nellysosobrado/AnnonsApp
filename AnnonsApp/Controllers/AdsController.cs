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

    [HttpGet("{id}")]
    public async Task<ActionResult<Ad>> GetById(int id)
    {
        var ad = await _context.Ads.FindAsync(id);
        if (ad == null)
        {
            return NotFound();
        }
         return Ok(ad);
    }
    [HttpPost]
    public async Task<ActionResult<Ad>>Create (AdInput input)
    {
        var ad = new Ad
        {
            Title = input.Title,
            Description = input.Description,
            Price = input.Price
        };

        _context.Ads.Add(ad);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetById),
            new { id = ad.Id },
            ad
            );
 
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, AdInput updatedAd)
    {
        var ad = await _context.Ads.FindAsync(id);

        if(ad == null)
        {
            return NotFound();
        }

        ad.Title = updatedAd.Title;
        ad.Description = updatedAd.Description;
        ad.Price = updatedAd.Price;

        await _context.SaveChangesAsync();

        return NoContent();
    }


    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch(int id, AdPatchInput input)
    {
        if(input.Title != null && string.IsNullOrWhiteSpace(input.Title))
        {
            return BadRequest("Title får inte vara tom.");

        }
        if(input.Description != null && string.IsNullOrWhiteSpace(input.Description))
        {
            ModelState.AddModelError("Description", "Beskrivningen får inte vara tom");
        }

        var ad = await _context.Ads.FindAsync(id);

        if(ad == null)
        {
            return NotFound();
        }

        if(input.Title != null)
        {
            ad.Title = input.Title;
        }

        if(input.Description != null)
        {
            ad.Description = input.Description;
        }

        if(input.Price.HasValue)
        {
            ad.Price = input.Price.Value;
        }

        await _context.SaveChangesAsync();

        return NoContent();
    }
    public async Task<IActionResult> Delete(int id)
    {
        var ad = await _context.Ads.FindAsync(id);

        if (ad == null)
        {
            return NotFound();
        }

        _context.Ads.Remove(ad);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
