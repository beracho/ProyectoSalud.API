using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoSalud.API.Controllers
{
    [AllowAnonymous]
    [Controller]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "Templates", "RegisterTemplate.html"), "text/HTML");
            return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html"), "text/HTML");
        }
    }
}