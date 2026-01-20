using System.ComponentModel.DataAnnotations;

namespace PopVault.Application.DTOs;

public record CreateAlbumRequest(
    [Required] [MaxLength(100)] string Title, 
    [Range(1900, 2100)] int Year, 
    [Required] [MaxLength(50)] string Genre, 
    [Range(1, 1000)] int DurationInMinutes, 
    [Required] int ArtistId
);

public record AlbumResponse(int Id, string Title, int Year, string Genre, int DurationInMinutes, decimal AverageScore);
