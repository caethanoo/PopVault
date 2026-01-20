using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PopVault.Domain.Entities;
using PopVault.Infrastructure;

// --- PopVault: Week 4 Code (EF Core Database) ---
// Focus: Persistence, DbContext, Transactions

// Setup Database Context
using var context = new AppDbContext();
// Ensure database is created (just in case)
context.Database.EnsureCreated();

// Seed Data if empty
SetupSeedData();

while (true)
{
    Console.Clear();
    Console.WriteLine("=== PopVault: Seu Crítico Musical (Semana 4 - Banco de Dados) ===");
    Console.WriteLine("1. Registrar Artista / Álbum");
    Console.WriteLine("2. Listar Discografia Completa");
    Console.WriteLine("3. Avaliar Álbum (Adicionar Review)");
    Console.WriteLine("4. Exibir Estatísticas");
    Console.WriteLine("5. Sair");
    Console.Write("\nEscolha uma opção: ");

    string option = Console.ReadLine();

    switch (option)
    {
        case "1": RegisterFlow(); break;
        case "2": ListAll(); break;
        case "3": AddReviewFlow(); break;
        case "4": ShowStatistics(); break;
        case "5": return;
        default:
            Console.WriteLine("Opção inválida!");
            Pause();
            break;
    }
}

void RegisterFlow()
{
    Console.WriteLine("\n--- Novo Registro ---");
    Console.Write("Nome do Artista: ");
    string artistName = Console.ReadLine();

    // DB: Search Artist
    Artist artist = context.Artists.FirstOrDefault(a => a.Name == artistName);
    
    if (artist == null)
    {
        Console.WriteLine($"Artista '{artistName}' não encontrado. Criando novo...");
        Console.Write("Bio do Artista: ");
        string bio = Console.ReadLine();
        artist = new Artist(artistName, bio);
        context.Artists.Add(artist);
        context.SaveChanges(); // Persist Artist immediately to get ID
    }
    else
    {
        Console.WriteLine($"Artista '{artist.Name}' selecionado (ID: {artist.Id}).");
    }

    Console.Write("Título do Álbum: ");
    string title = Console.ReadLine();
    Console.Write("Ano: ");
    int.TryParse(Console.ReadLine(), out int year);
    Console.Write("Gênero: ");
    string genre = Console.ReadLine();
    Console.Write("Duração (min): ");
    int.TryParse(Console.ReadLine(), out int duration);

    Album newAlbum = new Album(title, year, genre, duration);
    artist.AddAlbum(newAlbum);

    context.SaveChanges(); // Persist Album (Cascade insert works because we added to tracked Entity)

    Console.WriteLine($"\nÁlbum '{title}' salvo no Banco de Dados!");
    Pause();
}

void ListAll()
{
    Console.WriteLine("\n--- Discografia (Do Banco de Dados) ---");
    
    // DB: Eager Load Albums and Reviews
    var allArtists = context.Artists
        .Include(a => a.Albums)
        .ThenInclude(al => al.Reviews)
        .ToList();

    foreach (var artist in allArtists)
    {
        Console.WriteLine($"\n🎤 {artist.Name} ({artist.Bio})");
        if (artist.Albums.Count == 0)
        {
            Console.WriteLine("   (Nenhum álbum registrado)");
        }
        else
        {
            foreach (var album in artist.Albums)
            {
                Console.WriteLine($"   💿 {album.Title} ({album.Year}) - [{album.Genre}] - {album.DurationInMinutes} min - Nota Média: {album.AverageScore:F1}");
                if (album.Reviews.Count > 0)
                {
                    foreach(var review in album.Reviews)
                    {
                        Console.WriteLine($"      ⭐ {review.Score}: \"{review.Comment}\" - por {review.Author}");
                    }
                }
            }
        }
    }
    Pause();
}

void AddReviewFlow()
{
    Console.WriteLine("\n--- Avaliar Álbum ---");
    Console.Write("Nome do Artista: ");
    string artistName = Console.ReadLine();

    var artist = context.Artists
        .Include(a => a.Albums)
        .FirstOrDefault(a => a.Name == artistName);

    if (artist == null)
    {
        Console.WriteLine("Artista não encontrado.");
        Pause();
        return;
    }

    Console.Write("Nome do Álbum: ");
    string albumTitle = Console.ReadLine();
    
    var album = artist.Albums.FirstOrDefault(a => a.Title.Equals(albumTitle, StringComparison.OrdinalIgnoreCase));
    if (album == null)
    {
        Console.WriteLine("Álbum não encontrado.");
        Pause();
        return;
    }

    Console.Write("Sua nota (0-10): ");
    if (decimal.TryParse(Console.ReadLine(), out decimal score))
    {
        Console.Write("Comentário: ");
        string comment = Console.ReadLine();
        Console.Write("Seu Nome: ");
        string author = Console.ReadLine();

        Review review = new Review(author, score, comment);
        album.AddReview(review);
        context.SaveChanges(); // Persist Review

        Console.WriteLine("Avaliação registrada com sucesso!");
    }
    else
    {
        Console.WriteLine("Nota inválida.");
    }
    Pause();

}

void ShowStatistics()
{
    // DB: Count directly in database usually, but for complex average we can pull or map.
    // For simplicity, we'll pull all albums (careful with large data in real apps).
    
    var bestAlbum = context.Albums.Include(a => a.Reviews).ToList()
        .OrderByDescending(a => a.AverageScore)
        .FirstOrDefault();

    int artistCount = context.Artists.Count();
    int albumCount = context.Albums.Count();

    Console.WriteLine($"Total de Artistas: {artistCount}");
    Console.WriteLine($"Total de Álbuns: {albumCount}");
    
    if (bestAlbum != null)
    {
        Console.WriteLine($"\n🏆 Melhor Álbum: {bestAlbum.Title} ({bestAlbum.AverageScore:F1})");
    }
    Pause();
}

void SetupSeedData()
{
    if (!context.Artists.Any())
    {
        Console.WriteLine("Banco vazio. Populando dados iniciais...");

        Artist marina = new Artist("Marina Sena", "A diva do Pop Brasileiro");
        Album vicio = new Album("Vício Inerente", 2023, "Pop", 45);
        vicio.AddReview(new Review("Brennda", 10, "Perfeito!"));
        vicio.AddReview(new Review("Crítico", 9, "Muito bom production."));
        
        marina.AddAlbum(vicio);
        marina.AddAlbum(new Album("De Primeira", 2021, "Pop/MPB", 40));
        
        context.Artists.Add(marina);

        Artist dua = new Artist("Dua Lipa", "British Pop Star");
        dua.AddAlbum(new Album("Future Nostalgia", 2020, "Disco Pop", 38));
        context.Artists.Add(dua);

        context.SaveChanges();
        Console.WriteLine("Dados inseridos!");
    }
}

void Pause()
{
    Console.WriteLine("\nPressione qualquer tecla para continuar...");
    Console.ReadKey();
}
