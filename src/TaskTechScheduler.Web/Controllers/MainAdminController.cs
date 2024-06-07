using Contracts.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace TaskTechScheduler.Web.Controllers;

public class MainAdminController : Controller
{
    private readonly IRepositoryManager _repositories;

    public MainAdminController(IRepositoryManager repositories)
    {
        _repositories = repositories;
    }

    public IActionResult Index()
    {
        return View();
    }
}