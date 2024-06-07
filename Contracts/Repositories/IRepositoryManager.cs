namespace Contracts.Repositories;

public interface IRepositoryManager
{
    ITasksRepository Tasks {get;}
    IUserAdminRepository Users {get;}
}