using Contracts.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace TaskTechScheduler.Web.Controllers;

public class TechAdminController : Controller
{
    private readonly IRepositoryManager _repositories;

    public TechAdminController(IRepositoryManager repositories)
    {
        _repositories = repositories;
    }

    public IActionResult Index()
    {
        return View();
    }
}