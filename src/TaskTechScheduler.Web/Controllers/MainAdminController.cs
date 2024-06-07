using AutoMapper;
using Contracts.Repositories;
using Microsoft.AspNetCore.Mvc;
using TaskTechScheduler.Web.ViewModels.MainAdminViewModel;

namespace TaskTechScheduler.Web.Controllers;

public class MainAdminController : Controller
{
    private readonly IRepositoryManager _repositories;
    private readonly IMapper _mapper;

    public MainAdminController(IRepositoryManager repositories, IMapper mapper)
    {
        _repositories = repositories;
        _mapper = mapper;
    }

    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult ListTasks()
    {
        var tasks = _repositories.Tasks.GetAllTasks();
        var tasksViewModel = _mapper.Map<List<ListOfTasks>>(tasks);
        return View(tasksViewModel);
    }
}