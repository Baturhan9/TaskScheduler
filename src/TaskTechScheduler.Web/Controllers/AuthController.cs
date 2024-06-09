using Contracts.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TaskTechScheduler.Web.ViewModels.AuthViewModel;

namespace TaskTechScheduler.Web.Controllers;

public class AuthController : Controller
{
    private readonly IRepositoryManager _repositories;

    public AuthController(IRepositoryManager repositories)
    {
        _repositories = repositories;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(UserRegisterViewModel user)
    {
        var admin = _repositories.Users.GetAdminByLoginAndPassword(user.Login, user.Password);
        if(admin is null)
            return View();
        Response.Cookies.Append("UserAdminId", admin.Id.ToString());
        if(admin.Role == Models.UserAdmins.UserRole.MainAdmin)
            return RedirectToAction("Index", "MainAdmin");
        else 
            return RedirectToAction("Index", "TechAdmin");
    }
    
}