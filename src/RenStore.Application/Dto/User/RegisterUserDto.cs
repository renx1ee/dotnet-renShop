using System.ComponentModel.DataAnnotations;

namespace RenStore.Application.Dto.User;

public record RegisterUserDto(
    [Required] string? Email, 
    [Required] string? Password, 
    [Required] string? ConfirmPassword);