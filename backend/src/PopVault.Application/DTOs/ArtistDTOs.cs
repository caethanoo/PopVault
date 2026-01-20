using System.ComponentModel.DataAnnotations;

namespace PopVault.Application.DTOs;

public record CreateArtistRequest(
    [Required(ErrorMessage = "O nome é obrigatório.")] 
    [MaxLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")] 
    string Name, 
    
    [MaxLength(500, ErrorMessage = "A biografia deve ter no máximo 500 caracteres.")] 
    string Bio
);

public record ArtistResponse(int Id, string Name, string Bio);
