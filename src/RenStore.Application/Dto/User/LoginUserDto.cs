using System.ComponentModel.DataAnnotations;

namespace RenStore.Application.Dto.User;

public record LoginUserDto(
    [Required] string? Email, 
    [Required] string? Password);