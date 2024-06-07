using Models;

namespace Contracts.Repositories;

public interface IUserAdminRepository
{
    List<UserAdmins> GetAllAdmin();
    List<UserAdmins> GetAllTechAdmins();
    UserAdmins GetAdminById(int id);
    UserAdmins GetAdminByLoginAndPassword(string login, string password);
    void CreateTechAdmin(UserAdmins userAdmin);
    void UpdateTechAdmin(int id, UserAdmins userAdmin);
    void DeleteTechAdmin(int id);
}