using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public class Tasks
{
    public int Id {get;set;}
    public required string Title {get;set;}
    public required string Description {get;set;}
    public int? AcceptedUserAdminId {get;set;}
    public bool isCompleted {get;set;}
    public DateTime? CompletedDate {get;set;}


    [ForeignKey(nameof(AcceptedUserAdminId))]
    public virtual UserAdmins TechAdmin {get;set;}

}