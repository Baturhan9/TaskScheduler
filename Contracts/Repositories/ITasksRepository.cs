using Models;

namespace Contracts.Repositories;

public interface ITasksRepository
{
    List<Tasks> GetAllTasks();
    List<Tasks> GetTasksByAdmin(int adminId);
    Tasks GetTaskById(int id);
    void CreateTask(Tasks task);
    void UpdateTask(int id, Tasks task);
    void DeleteTask(int id);
}