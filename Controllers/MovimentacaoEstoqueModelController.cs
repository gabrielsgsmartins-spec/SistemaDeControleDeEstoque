using Microsoft.AspNetCore.Mvc;

namespace SistemaDeControleDeEstoque.Controllers
{
    public class MovimentacaoEstoqueModelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
