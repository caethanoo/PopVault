using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PopVault.Domain.Entities;
using PopVault.Domain.Interfaces;
using PopVault.Infrastructure.Persistence;

namespace PopVault.Infrastructure.Repositories;

public class AlbumRepository : IAlbumRepository
{
    private readonly AppDbContext _context;

    public AlbumRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Album>> GetAllAsync()
    {
        return await _context.Albuns.ToListAsync();
    }

    public async Task<Album?> GetByIdAsync(Guid id)
    {
        return await _context.Albuns.FindAsync(id);
    }

    public async Task AddAsync(Album album)
    {
        await _context.Albuns.AddAsync(album);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var album = await GetByIdAsync(id);
        if (album != null)
        {
            _context.Albuns.Remove(album);
            await _context.SaveChangesAsync();
        }
    }
}
