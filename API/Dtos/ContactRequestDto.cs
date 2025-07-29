using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class ContactRequestDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Surname { get; set; }
    [Required, EmailAddress]
    public string Email { get; set; }
}