using Microsoft.AspNetCore.Mvc;

namespace LJJ_VITINHO.Area.admin.Controllers
{
    public class ClientesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
