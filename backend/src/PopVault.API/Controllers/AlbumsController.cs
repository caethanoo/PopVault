using Microsoft.AspNetCore.Mvc;
using PopVault.Application.DTOs;
using PopVault.Application.Services;

namespace PopVault.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlbumsController : ControllerBase
{
    private readonly AlbumService _service;

    public AlbumsController(AlbumService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var albums = await _service.GetAllAlbumsAsync();
        return Ok(albums);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAlbumRequest request)
    {
        var album = await _service.CreateAlbumAsync(request);
        
        if (album == null)
            return NotFound($"Artist with ID {request.ArtistId} not found.");

        return CreatedAtAction(nameof(GetAll), new { id = album.Id }, album);
    }
}
