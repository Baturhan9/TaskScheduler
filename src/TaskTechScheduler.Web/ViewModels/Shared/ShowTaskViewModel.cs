
using System.Runtime.CompilerServices;

namespace TaskTechScheduler.Web.ViewModels.Shared;

public class ShowTaskViewModel
{
    public SingleTaskViewModel Task {get;set;}
    public bool IsFree {get;set;}
    public bool IsMine {get;set;}
    public string? FullNameUserAdmins {get;set;}
}