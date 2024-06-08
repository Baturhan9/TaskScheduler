namespace TaskTechScheduler.Web.ViewModels.MainAdminViewModel;

public class UpdateTaskViewModel
{
    public required string Title{get;set;}
    public required string Description{get;set;}
    public bool isCompleted{get;set;}
}