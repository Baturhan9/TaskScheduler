using AutoMapper;
using Contracts.Repositories;
using Microsoft.AspNetCore.Mvc;
using Models;
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

    public IActionResult CreateTask()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CreateTask(CreateTaskViewModel task)
    {
        var taskObj = _mapper.Map<Tasks>(task);
        _repositories.Tasks.CreateTask(taskObj);
        return RedirectToAction("ListTasks");
    }
}