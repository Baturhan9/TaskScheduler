using Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Services.Repositories;

public class TasksRepository : ITasksRepository
{
    private readonly AppDbContext _context;

    public TasksRepository(AppDbContext context)
    {
        _context = context;
    }

    public void CreateTask(Tasks task)
    {
        _context.Tasks.Add(task);
        _context.SaveChanges();
    }

    public void DeleteTask(int id)
    {
        var task = _context.Tasks.Where(t => t.Id == id).SingleOrDefault();
        _context.Tasks.Remove(task);
        _context.SaveChanges();
    }

    public List<Tasks> GetAllTasks()
    {
        return _context.Tasks.AsNoTracking().ToList();
    }

    public Tasks GetTaskById(int id)
    {
        return _context.Tasks.AsNoTracking().Where(t => t.Id == id).SingleOrDefault();
    }

    public List<Tasks> GetTasksByAdmin(int adminId)
    {
        return _context.Tasks.AsNoTracking().Where(t => t.AcceptedUserAdminId == adminId).ToList();
    }

    public void UpdateTask(int id, Tasks task)
    {
        var taskObj = _context.Tasks.Where(t => t.Id == id).SingleOrDefault();
        taskObj.Title = task.Title;
        taskObj.Description = task.Description;
        taskObj.isCompleted = task.isCompleted;
        _context.Entry(taskObj).State = EntityState.Modified;
        _context.SaveChanges();
    }
}