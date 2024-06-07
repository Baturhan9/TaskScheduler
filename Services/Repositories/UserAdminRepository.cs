using Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Services.Repositories;

public class UserAdminRepository : IUserAdminRepository
{
    private readonly AppDbContext _context;

    public UserAdminRepository(AppDbContext context)
    {
        _context = context;
    }

    public void CreateTechAdmin(UserAdmins userAdmin)
    {
        _context.Users.Add(userAdmin);
        _context.SaveChanges();
    }

    public void DeleteTechAdmin(int id)
    {
        var admin = _context.Users.Where(u => u.Id == id).SingleOrDefault();
        _context.Users.Remove(admin);
        _context.SaveChanges();
    }

    public UserAdmins GetAdminById(int id)
    {
        return _context.Users.AsNoTracking().Where(u => u.Id == id).SingleOrDefault();
    }

    public UserAdmins GetAdminByLoginAndPassword(string login, string password)
    {
        return _context.Users.AsNoTracking().Where(u=> u.Email == login && u.Password == password).SingleOrDefault();
    }

    public List<UserAdmins> GetAllAdmin()
    {
        return _context.Users.AsNoTracking().ToList();
    }

    public List<UserAdmins> GetAllTechAdmins()
    {
        return _context.Users.AsNoTracking().Where(u => u.Role == UserAdmins.UserRole.TechAdmin).ToList();
    }

    public void UpdateTechAdmin(int id, UserAdmins userAdmin)
    {
        var admin = _context.Users.Where(u => u.Id == id).SingleOrDefault();
        admin = userAdmin;
        _context.Entry(admin).State = EntityState.Modified;
        _context.SaveChanges();
    }
}