using Contracts.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace TaskTechScheduler.Web.Controllers;

public class AuthController : Controller
{
    private readonly IRepositoryManager _repositories;

    public AuthController(IRepositoryManager repositories)
    {
        _repositories = repositories;
    }
}