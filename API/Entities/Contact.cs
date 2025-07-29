using System.ComponentModel.DataAnnotations;

namespace API.Entities;

public class Contact : BaseEntity
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    
    [EmailAddress]
    public required string Email { get; set; }   
}