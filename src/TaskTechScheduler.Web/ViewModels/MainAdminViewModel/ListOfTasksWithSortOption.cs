using TaskTechScheduler.Web.ViewModels.Shared;

namespace TaskTechScheduler.Web.ViewModels.MainAdminViewModel;

public class ListOfTasksWithSortOption
{
    public List<ListOfTasks> Tasks {get;set;}
    public string SortOption {get;set;} = "";
}