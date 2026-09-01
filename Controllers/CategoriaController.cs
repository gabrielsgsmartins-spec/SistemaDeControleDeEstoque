using Microsoft.AspNetCore.Mvc;

namespace SistemaDeControleDeEstoque.Controllers
{
    public class CategoriaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
