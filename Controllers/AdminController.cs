using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SistemaEstoque.Models;

namespace SistemaDeControleDeEstoque.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUserModel> _userManager;

        public AdminController(UserManager<ApplicationUserModel> userManager)
        {
            _userManager = userManager;
        }

        // Verifica se é Admin
        public override async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            var usuario = await _userManager.GetUserAsync(
                context.HttpContext.User);

            if (usuario == null || usuario.Tipo != TipoUsuario.Admin)
            {
                context.Result = new RedirectToActionResult(
                    "AcessoNegado",
                    "Home",
                    null);

                return;
            }

            await next();
        }

        // Lista os usuários
        public IActionResult Index()
        {
            var usuarios = _userManager.Users.ToList();

            return View(usuarios);
        }

        // Abre a tela de adicionar
        [HttpGet]
        public IActionResult Adicionar()
        {
            return View();
        }

        // Adiciona o usuário
        [HttpPost]
        public async Task<IActionResult> Adicionar(
            ApplicationUserModel usuario,
            string senha)
        {
            if (!ModelState.IsValid)
            {
                return View(usuario);
            }

            var resultado = await _userManager.CreateAsync(usuario, senha);

            if (resultado.Succeeded)
            {
                return RedirectToAction("Index");
            }

            foreach (var erro in resultado.Errors)
            {
                ModelState.AddModelError("", erro.Description);
            }

            return View(usuario);
        }

        // Abre a tela de editar
        [HttpGet]
        public async Task<IActionResult> Editar(string id)
        {
            var usuario = await _userManager.FindByIdAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // Salva a edição
        [HttpPost]
        public async Task<IActionResult> Editar(
            ApplicationUserModel usuario)
        {
            if (!ModelState.IsValid)
            {
                return View(usuario);
            }

            var resultado = await _userManager.UpdateAsync(usuario);

            if (resultado.Succeeded)
            {
                return RedirectToAction("Index");
            }

            foreach (var erro in resultado.Errors)
            {
                ModelState.AddModelError("", erro.Description);
            }

            return View(usuario);
        }

        // Abre a confirmação de exclusão
        [HttpGet]
        public async Task<IActionResult> Excluir(string id)
        {
            var usuario = await _userManager.FindByIdAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // Exclui o usuário
        [HttpPost]
        public async Task<IActionResult> ExcluirUsuario(string id)
        {
            var usuario = await _userManager.FindByIdAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            var resultado = await _userManager.DeleteAsync(usuario);

            if (resultado.Succeeded)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}