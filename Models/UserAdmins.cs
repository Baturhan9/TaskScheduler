using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public class UserAdmins
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public string? LastName { get; set; }
    public required string Email {get;set;}
    public required string Password {get;set;}
    public required UserRole Role {get;set;}

    public ICollection<Tasks> Tasks { get; set; }


    public enum UserRole
    {
        MainAdmin,
        TechAdmin
    }
}