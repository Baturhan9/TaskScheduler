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

    public IActionResult ShowTask(int id)
    {
        var taskObj = _repositories.Tasks.GetTaskById(id);
        var taskViewModel = _mapper.Map<SingleTaskViewModel>(taskObj);
        string adminName = "----";
        if(taskObj.AcceptedUserAdminId is not null)
        {
            var admin =  _repositories.Users.GetAdminById((int)taskObj.AcceptedUserAdminId);
            adminName = admin.FirstName + " " + admin.LastName;
        }
        var taskToShow = new ShowTaskViewModel{Task = taskViewModel, FullNameUserAdmins = adminName};
        return View(taskToShow);
    }

    [HttpPost]
    public IActionResult UpdateTask(int id, SingleTaskViewModel task)
    {
        var taskObj = _mapper.Map<Tasks>(task);
        _repositories.Tasks.UpdateTask(id, taskObj);
        return RedirectToAction("ListTasks");
    }

    public IActionResult DeleteTask(int id)
    {
        _repositories.Tasks.DeleteTask(id);
        return RedirectToAction("ListTasks");
    }
}