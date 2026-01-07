using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PopVault.Application.DTOs;

namespace PopVault.Application.Interfaces;

public interface IAlbumService
{
    Task<IEnumerable<AlbumDto>> GetAllAlbumsAsync();
    Task<AlbumDto?> GetAlbumByIdAsync(Guid id);
    Task<AlbumDto> CreateAlbumAsync(CreateAlbumDto createAlbumDto);
    Task DeleteAlbumAsync(Guid id);
}
