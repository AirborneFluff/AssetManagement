using System.ComponentModel.DataAnnotations;

namespace API.Domain.Authentication.Dtos;

public class NewAppUserDto
{
    [MaxLength(254)]
    public required string Email { get; set; }

    [MaxLength(254)]
    public required string Password { get; set; }
}