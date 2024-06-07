using Contracts.Repositories;
using Models;

namespace Services.Repositories;

public class RepositoryManager : IRepositoryManager
{

    private readonly AppDbContext _context;
    private readonly Lazy<ITasksRepository> _taskRepository;
    private readonly Lazy<IUserAdminRepository> _userAdminRepository;
    public RepositoryManager(AppDbContext context)
    {
        _context = context;
        _taskRepository = new Lazy<ITasksRepository>(() => new TasksRepository(_context));
        _userAdminRepository = new Lazy<IUserAdminRepository>(() => new UserAdminRepository(_context));
    }

    public ITasksRepository Tasks => _taskRepository.Value;
    public IUserAdminRepository Users  => _userAdminRepository.Value;
}