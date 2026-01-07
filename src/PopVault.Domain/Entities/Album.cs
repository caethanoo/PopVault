using System;

namespace PopVault.Domain.Entities;

public class Album
{
    public Guid Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Artista { get; set; } = string.Empty;
    public int AnoLancamento { get; set; }
    public string Genero { get; set; } = string.Empty;
    public string? DescricaoIA { get; set; }

    public Album(string titulo, string artista, int anoLancamento, string genero)
    {
        Id = Guid.NewGuid();
        Titulo = titulo;
        Artista = artista;
        AnoLancamento = anoLancamento;
        Genero = genero;
    }

    // Constructor for ORM
    protected Album() { }
}
