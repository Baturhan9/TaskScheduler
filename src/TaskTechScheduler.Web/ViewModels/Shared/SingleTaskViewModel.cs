namespace TaskTechScheduler.Web.ViewModels.Shared;

public class SingleTaskViewModel
{
    public int Id {get;set;}
    public required string Title {get;set;}
    public required string Description {get;set;}
    public bool isCompleted {get;set;}
    public DateTime? CompletedDate {get;set;}
    
}