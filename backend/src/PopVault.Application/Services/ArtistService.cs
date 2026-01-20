using Microsoft.EntityFrameworkCore;
using PopVault.Application.DTOs;
using PopVault.Domain.Entities;
using PopVault.Infrastructure;

namespace PopVault.Application.Services;

public class ArtistService
{
    private readonly AppDbContext _context;

    public ArtistService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ArtistResponse>> GetAllArtistsAsync()
    {
        var artists = await _context.Artists.ToListAsync();
        return artists.Select(a => new ArtistResponse(a.Id, a.Name, a.Bio)).ToList();
    }

    public async Task<ArtistResponse> CreateArtistAsync(CreateArtistRequest request)
    {
        var artist = new Artist(request.Name, request.Bio);
        
        _context.Artists.Add(artist);
        await _context.SaveChangesAsync();

        return new ArtistResponse(artist.Id, artist.Name, artist.Bio);
    }
}
