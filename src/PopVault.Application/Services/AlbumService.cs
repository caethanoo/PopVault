using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PopVault.Application.DTOs;
using PopVault.Application.Interfaces;
using PopVault.Domain.Entities;
using PopVault.Domain.Interfaces;

namespace PopVault.Application.Services;

public class AlbumService : IAlbumService
{
    private readonly IAlbumRepository _albumRepository;

    public AlbumService(IAlbumRepository albumRepository)
    {
        _albumRepository = albumRepository;
    }

    public async Task<IEnumerable<AlbumDto>> GetAllAlbumsAsync()
    {
        var albums = await _albumRepository.GetAllAsync();
        return albums.Select(a => new AlbumDto
        {
            Id = a.Id,
            Titulo = a.Titulo,
            Artista = a.Artista,
            AnoLancamento = a.AnoLancamento,
            Genero = a.Genero,
            DescricaoIA = a.DescricaoIA
        });
    }

    public async Task<AlbumDto?> GetAlbumByIdAsync(Guid id)
    {
        var album = await _albumRepository.GetByIdAsync(id);
        if (album == null) return null;

        return new AlbumDto
        {
            Id = album.Id,
            Titulo = album.Titulo,
            Artista = album.Artista,
            AnoLancamento = album.AnoLancamento,
            Genero = album.Genero,
            DescricaoIA = album.DescricaoIA
        };
    }

    public async Task<AlbumDto> CreateAlbumAsync(CreateAlbumDto createAlbumDto)
    {
        var album = new Album(
            createAlbumDto.Titulo,
            createAlbumDto.Artista,
            createAlbumDto.AnoLancamento,
            createAlbumDto.Genero
        );

        await _albumRepository.AddAsync(album);

        return new AlbumDto
        {
            Id = album.Id,
            Titulo = album.Titulo,
            Artista = album.Artista,
            AnoLancamento = album.AnoLancamento,
            Genero = album.Genero,
            DescricaoIA = album.DescricaoIA
        };
    }

    public async Task DeleteAlbumAsync(Guid id)
    {
        await _albumRepository.DeleteAsync(id);
    }
}
