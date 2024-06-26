using AutoMapper;
using Contracts.Repositories;
using Microsoft.AspNetCore.Mvc;
using TaskTechScheduler.Web.ViewModels.Shared;

namespace TaskTechScheduler.Web.Controllers;

public class TechAdminController : Controller
{    
    private readonly IRepositoryManager _repositories;
    private readonly IMapper _mapper;

    public TechAdminController(IRepositoryManager repositories, IMapper mapper)
    {
        _repositories = repositories;
        _mapper = mapper;
    }

    public IActionResult Index()
    {
        var id = Request.Cookies["UserAdminId"];
        var admin = _repositories.Users.GetAdminById(int.Parse(id));
        string name = admin.FirstName + " " + admin.LastName;
        return View(model:name);

    }
    public IActionResult ListTasks(string sortOption = "")
    {
        var tasks = _repositories.Tasks.GetAllTasks();
        if(sortOption != "")
        {
            switch(sortOption)
            {
                case "FreeTasks":
                    tasks=tasks.Where(t => t.AcceptedUserAdminId is null).ToList();
                    break;
                case "MineTasks":
                    string id = Request.Cookies["UserAdminId"]!;
                    tasks = tasks.Where(t => t.AcceptedUserAdminId.ToString() == id).ToList();
                    break;
            }
        }

        var tasksViewModel = _mapper.Map<List<ListOfTasks>>(tasks);
        return View(tasksViewModel);
    }
    public IActionResult ShowTask(int id)
    {
        var taskObj = _repositories.Tasks.GetTaskById(id);
        if(taskObj is null)
            return NotFound();
        var taskViewModel = _mapper.Map<SingleTaskViewModel>(taskObj);
        string adminName = "----";
        bool isFree = true;
        bool isMine = false;
        if(taskObj.AcceptedUserAdminId is not null)
        {
            var admin =  _repositories.Users.GetAdminById((int)taskObj.AcceptedUserAdminId);
            adminName = admin.FirstName + " " + admin.LastName;
            isFree = false;
            string userAdminId = Request.Cookies["UserAdminId"] ?? "-1";
            isMine = taskObj.AcceptedUserAdminId.ToString() == userAdminId;
        }
        var taskToShow = new ShowTaskViewModel{Task = taskViewModel,IsFree= isFree,IsMine = isMine,FullNameUserAdmins = adminName};
        return View(taskToShow);
    }

    public IActionResult AcceptTask(int id)
    {
        var userAdminId = Request.Cookies["UserAdminId"];
        _repositories.Tasks.AcceptTask(id, int.Parse(userAdminId!));
        return RedirectToAction("ListTasks");
    }
    public IActionResult DoneTask(string description, int id)
    {
        _repositories.Tasks.DoneTask(id, description);
        return RedirectToAction("ListTasks");
    }

}