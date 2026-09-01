using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaEstoque.Data;
using SistemaEstoque.Models;

namespace SistemaDeControleDeEstoque.Controllers
{
    public class ProdutoController : Controller
    {

        private readonly EstoqueContext _bancoContext;

        public ProdutoController(EstoqueContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public IActionResult Index()
        {
            var produtos = _bancoContext.Produtos.ToList();

            return View(produtos);
        }
        public IActionResult Adicionar()
        {
            return View();

        }


        [HttpPost]
        public IActionResult Adicionar(ProdutoModel produto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var nomeCategoria = produto.Categoria?.Nome?.Trim();

                    if (string.IsNullOrEmpty(nomeCategoria))
                    {
                        ModelState.AddModelError(
                            "Categoria.Nome",
                            "Digite uma categoria."
                        );

                        return View(produto);
                    }

                    var categoriaExistente = _bancoContext.Categorias
                        .FirstOrDefault(c =>
                            c.Nome.ToLower() == nomeCategoria.ToLower());

                    if (categoriaExistente == null)
                    {
                        var novaCategoria = new CategoriaModel
                        {
                            Nome = nomeCategoria
                        };

                        _bancoContext.Categorias.Add(novaCategoria);
                        _bancoContext.SaveChanges();

                        produto.CategoriaId = novaCategoria.Id;
                    }
                    else
                    {
                        produto.CategoriaId = categoriaExistente.Id;
                    }

                    _bancoContext.Produtos.Add(produto);
                    _bancoContext.SaveChanges();

                    TempData["MensagemSucesso"] =
                        "Sucesso, novo produto cadastrado!";

                    return RedirectToAction("Index");
                }

                return View(produto);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] =
                    $"Erro ao cadastrar: {erro.InnerException?.Message ?? erro.Message}";

                return RedirectToAction("Index");
            }
        }
    }
}