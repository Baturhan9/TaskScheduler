using System.Diagnostics.CodeAnalysis;

namespace TaskTechScheduler.Web.ViewModels.AuthViewModel;

public class UserAuthViewModel
{
    public required string Login {get;set;}
    public required string Password {get;set;}
    public required string ConfirmPassword {get;set;}
}