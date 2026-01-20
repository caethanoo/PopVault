using Microsoft.AspNetCore.Mvc;
using PopVault.Application.DTOs;
using PopVault.Application.Services;

namespace PopVault.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArtistsController : ControllerBase
{
    private readonly ArtistService _service;

    public ArtistsController(ArtistService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var artists = await _service.GetAllArtistsAsync();
        return Ok(artists);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateArtistRequest request)
    {
        var artist = await _service.CreateArtistAsync(request);
        return CreatedAtAction(nameof(GetAll), new { id = artist.Id }, artist);
    }
}
