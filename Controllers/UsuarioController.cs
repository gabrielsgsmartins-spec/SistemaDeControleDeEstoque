using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SistemaEstoque.Models;
using SistemaEstoque.Repositorio;

namespace SistemaEstoque.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        private readonly UserManager<ApplicationUserModel> _userManager;
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(
            UserManager<ApplicationUserModel> userManager,
            IUsuarioRepositorio usuarioRepositorio)
        {
            _userManager = userManager;
            _usuarioRepositorio = usuarioRepositorio;
        }

        public IActionResult Index()
        {
            var usuarios = _usuarioRepositorio.ListarTodos();

            return View(usuarios);
        }

        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar(
            ApplicationUserModel usuario,
            string senha)
        {
            await _userManager.CreateAsync(usuario, senha);

            return RedirectToAction("Index");
        }

        public IActionResult Editar(string id)
        {
            var usuario = _usuarioRepositorio.BuscarPorId(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        [HttpGet]
        public IActionResult ExcluirConfirmacao(string id)
        {
            var usuario = _usuarioRepositorio.BuscarPorId(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return View("ApagarConfirmacaoUser", usuario);
        }

        [HttpPost]
        public IActionResult ExcluirUser(string id)
        {
            _usuarioRepositorio.Excluir(id);

            return RedirectToAction("Index");
        }
    }
}