using AutoMapper;
using Contracts;
using Contracts.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Models;
using TaskTechScheduler.Web.ViewModels.MainAdminViewModel;
using TaskTechScheduler.Web.ViewModels.Shared;

namespace TaskTechScheduler.Web.Controllers;

public class MainAdminController : Controller
{
    private readonly IRepositoryManager _repositories;
    private readonly IMapper _mapper;
    private readonly IReportMaker _reportMaker;

    public MainAdminController(IRepositoryManager repositories, IMapper mapper, IReportMaker reportMaker)
    {
        _repositories = repositories;
        _mapper = mapper;
        _reportMaker = reportMaker;
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
        
        var listOfTasksViewModel = _mapper.Map<List<ListOfTasks>>(tasks);
        if(sortOption != "")
        {
            switch(sortOption)
            {
                case "Issued":
                    listOfTasksViewModel = listOfTasksViewModel.Where(t => t.Status == ListOfTasks.StatusTask.Issued).ToList();
                    break;
                case "Doing":
                    listOfTasksViewModel = listOfTasksViewModel.Where(t => t.Status == ListOfTasks.StatusTask.Doing).ToList();
                    break;
                case "Done":
                    listOfTasksViewModel = listOfTasksViewModel.Where(t => t.Status == ListOfTasks.StatusTask.Done).ToList();
                    break;
            }
        }
        var tasksViewModel = new ListOfTasksWithSortOption{Tasks = listOfTasksViewModel, SortOption= sortOption};
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
        if(taskObj is null)
            return NotFound();
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

    public FileResult DownloadReport()
    {
        string currentUserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split('\\').Last();
        var tasks = _repositories.Tasks.GetAllTasks();
        string fileName = "reportTasks.docx";
        string filePath = @$"C:\Users\{currentUserName}\Documents\{fileName}";
        _reportMaker.MakeReports(tasks, filePath);
        byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
        return File(fileBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", fileName);
    }
}