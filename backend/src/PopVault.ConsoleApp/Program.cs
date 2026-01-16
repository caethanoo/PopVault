using System;
using System.Collections.Generic;
using System.Linq;
using PopVault.Domain.Entities;


// State: Now we manage ARTISTS, not just Albums.
List<Artist> artistList = new List<Artist>();

// Mock Data
SetupMockData();

while (true)
{
    Console.Clear();
    Console.WriteLine("=== PopVault: Seu Crítico Musical (Semana 2 - POO) ===");
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

    // Find or Create Artist
    Artist artist = artistList.FirstOrDefault(a => a.Name.Equals(artistName, StringComparison.OrdinalIgnoreCase));
    
    if (artist == null)
    {
        Console.WriteLine($"Artista '{artistName}' não encontrado. Criando novo...");
        Console.Write("Bio do Artista: ");
        string bio = Console.ReadLine();
        artist = new Artist(artistName, bio);
        artistList.Add(artist);
    }
    else
    {
        Console.WriteLine($"Artista '{artist.Name}' selecionado.");
    }

    Console.Write("Título do Álbum: ");
    string title = Console.ReadLine();
    Console.Write("Ano: ");
    int.TryParse(Console.ReadLine(), out int year);
    Console.Write("Gênero: ");
    string genre = Console.ReadLine();

    Album newAlbum = new Album(title, year, genre);
    artist.AddAlbum(newAlbum);

    Console.WriteLine($"\nÁlbum '{title}' adicionado à discografia de {artist.Name}!");
    Pause();
}

void ListAll()
{
    Console.WriteLine("\n--- Discografia ---");
    foreach (var artist in artistList)
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
                Console.WriteLine($"   💿 {album.Title} ({album.Year}) - [{album.Genre}] - Nota Média: {album.AverageScore:F1}");
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

    Artist artist = artistList.FirstOrDefault(a => a.Name.Equals(artistName, StringComparison.OrdinalIgnoreCase));
    if (artist == null)
    {
        Console.WriteLine("Artista não encontrado.");
        Pause();
        return;
    }

    Console.Write("Nome do Álbum: ");
    string albumTitle = Console.ReadLine();
    
    Album album = artist.Albums.FirstOrDefault(a => a.Title.Equals(albumTitle, StringComparison.OrdinalIgnoreCase));
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
    // Flatten logic: Get all albums from all artists
    var allAlbums = artistList.SelectMany(a => a.Albums).ToList();

    if (allAlbums.Count == 0)
    {
        Console.WriteLine("Nenhum dado.");
        Pause();
        return;
    }

    Console.WriteLine($"Total de Artistas: {artistList.Count}");
    Console.WriteLine($"Total de Álbuns: {allAlbums.Count}");
    
    // LINQ makes this easy now
    var bestAlbum = allAlbums.OrderByDescending(a => a.AverageScore).FirstOrDefault();
    if (bestAlbum != null)
    {
        Console.WriteLine($"\n🏆 Melhor Álbum: {bestAlbum.Title} ({bestAlbum.AverageScore:F1})");
    }
    Pause();
}

void SetupMockData()
{
    Artist marina = new Artist("Marina Sena", "A diva do Pop Brasileiro");
    Album vicio = new Album("Vício Inerente", 2023, "Pop");
    vicio.AddReview(new Review("Brennda", 10, "Perfeito!"));
    vicio.AddReview(new Review("Crítico", 9, "Muito bom production."));
    
    marina.AddAlbum(vicio);
    marina.AddAlbum(new Album("De Primeira", 2021, "Pop/MPB"));
    
    artistList.Add(marina);

    Artist dua = new Artist("Dua Lipa", "British Pop Star");
    dua.AddAlbum(new Album("Future Nostalgia", 2020, "Disco Pop"));
    artistList.Add(dua);
}

void Pause()
{
    Console.WriteLine("\nPressione qualquer tecla para continuar...");
    Console.ReadKey();
}
