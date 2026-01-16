using System.Collections.Generic;

namespace PopVault.Domain.Entities;

public class Artist
{
    public string Name { get; private set; }
    public string Bio { get; private set; }
    public List<Album> Albums { get; private set; } = new List<Album>();

    public Artist(string name, string bio)
    {
        Name = name;
        Bio = bio;
    }

    public void AddAlbum(Album album)
    {
        Albums.Add(album);
    }
}
