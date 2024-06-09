namespace TaskTechScheduler.Web.ViewModels.Shared;

public class ListOfTasks
{
    public int TaskId {get;set;}
    public required string Title {get;set;}
    public StatusTask Status {get;set;}
    public enum StatusTask
    {
        Issued,
        Doing,
        Done
    }
}