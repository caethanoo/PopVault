using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PopVault.Domain.Entities;

namespace PopVault.Domain.Interfaces;

public interface IAlbumRepository
{
    Task<IEnumerable<Album>> GetAllAsync();
    Task<Album?> GetByIdAsync(Guid id);
    Task AddAsync(Album album);
    Task DeleteAsync(Guid id);
}
