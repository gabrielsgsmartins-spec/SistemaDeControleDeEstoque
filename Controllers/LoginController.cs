using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SistemaEstoque.Models;

namespace SistemaEstoque.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<ApplicationUserModel> _signInManager;
        private readonly UserManager<ApplicationUserModel> _userManager;

        public LoginController(
            SignInManager<ApplicationUserModel> signInManager,
            UserManager<ApplicationUserModel> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // Tela de Login
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // Fazer Login
        [HttpPost]
        public async Task<IActionResult> Entrar(LoginModel login)
        {
            var resultado = await _signInManager.PasswordSignInAsync(
                login.Usuario,
                login.Senha,
                false,
                false);

            if (resultado.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Erro = "Usuário ou senha inválidos.";

            return View("Index");
        }

        // Sair
        public async Task<IActionResult> Sair()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index");
        }

        // Tela para criar usuário
        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

       

        // Criar usuário
        [HttpPost]
        public async Task<IActionResult> Cadastrar(
            ApplicationUserModel usuario,
            string senha)
        {
            // Todo cadastro público será Funcionário
            usuario.Tipo = TipoUsuario.Funcionario;

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
    }
}