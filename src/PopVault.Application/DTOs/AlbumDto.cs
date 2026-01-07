using System;

namespace PopVault.Application.DTOs;

public class AlbumDto
{
    public Guid Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Artista { get; set; } = string.Empty;
    public int AnoLancamento { get; set; }
    public string Genero { get; set; } = string.Empty;
    public string? DescricaoIA { get; set; }
}
