using Microsoft.EntityFrameworkCore;
using PopVault.Application.DTOs;
using PopVault.Domain.Entities;
using PopVault.Infrastructure;

namespace PopVault.Application.Services;

public class AlbumService
{
    private readonly AppDbContext _context;

    public AlbumService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<AlbumResponse>> GetAllAlbumsAsync()
    {
        // Include Reviews to calculate AverageScore
        var albums = await _context.Albums.Include(a => a.Reviews).ToListAsync();
        return albums.Select(a => new AlbumResponse(a.Id, a.Title, a.Year, a.Genre, a.DurationInMinutes, a.AverageScore)).ToList();
    }

    public async Task<AlbumResponse?> CreateAlbumAsync(CreateAlbumRequest request)
    {
        var artist = await _context.Artists.FindAsync(request.ArtistId);
        if (artist == null) return null; // Or throw exception

        var album = new Album(request.Title, request.Year, request.Genre, request.DurationInMinutes);
        artist.AddAlbum(album); // Use Domain method to ensure relationship

        await _context.SaveChangesAsync();

        return new AlbumResponse(album.Id, album.Title, album.Year, album.Genre, album.DurationInMinutes, album.AverageScore);
    }
}
