using System.ComponentModel.DataAnnotations;

namespace PopVault.Application.DTOs;

public class CreateAlbumDto
{
    [Required]
    public string Titulo { get; set; } = string.Empty;

    [Required]
    public string Artista { get; set; } = string.Empty;

    [Range(1900, 2100)]
    public int AnoLancamento { get; set; }

    [Required]
    public string Genero { get; set; } = string.Empty;
}
