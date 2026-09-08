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
            var produtos = _bancoContext.Produtos
                .Include(p => p.Categoria)
                .ToList();

            return View(produtos);
        }
        [HttpGet]
        public IActionResult Adicionar()
        {
            ViewBag.Categorias = _bancoContext.Categorias
                .OrderBy(c => c.Nome)
                .ToList();

            return View();
            // passando as categoria que ja existemk no banco


        }
        [HttpGet]
        public IActionResult Editar(int id)
        {
            var produto = _bancoContext.Produtos
                .FirstOrDefault(p => p.Id == id);

            if (produto == null)
            {
                return NotFound();
            }

            ViewBag.Categorias = _bancoContext.Categorias
                .OrderBy(c => c.Nome)
                .ToList();

            return View(produto);
        }



        [HttpGet]
        public IActionResult ApagarConfirmacao(int id)
        {
            var produto = _bancoContext.Produtos
                .FirstOrDefault(p => p.Id == id);

            if (produto == null)
            {
                return NotFound();
            }

            return View("ApagarConfirmacao", produto);
        }
        public IActionResult BuscarProduto(int id)
        {
            var produto = _bancoContext.Produtos
               .FirstOrDefault(p => p.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);

        }


        [HttpPost]
        public IActionResult Adicionar(ProdutoModel produto, string? novaCategoria)
        {
            try
            {
                if (produto.CategoriaId == -1)
                {
                    if (string.IsNullOrWhiteSpace(novaCategoria))
                    {
                        ModelState.AddModelError(
                            "CategoriaId",
                            "Digite o nome da nova categoria."
                        );
                    }
                    else
                    {
                        var categoria = new CategoriaModel
                        {
                            Nome = novaCategoria.Trim()
                        };


                        _bancoContext.Categorias.Add(categoria);
                        _bancoContext.SaveChanges();

                        produto.CategoriaId = categoria.Id;
                    }
                }

                if (!ModelState.IsValid)
                {
                    ViewBag.Categorias = _bancoContext.Categorias
                        .OrderBy(c => c.Nome)
                        .ToList();

                    return View(produto);
                }

                _bancoContext.Produtos.Add(produto);
                _bancoContext.SaveChanges();

                TempData["MensagemSucesso"] = "Produto cadastrado com sucesso!";

                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] =
                    $"Erro ao cadastrar: {erro.InnerException?.Message ?? erro.Message}";

                ViewBag.Categorias = _bancoContext.Categorias
                    .OrderBy(c => c.Nome)
                    .ToList();

                return View(produto);
            }
        }
        [HttpPost]
        public IActionResult AdicionarCategoria(string nomeCategoria)
        {
            if (string.IsNullOrWhiteSpace(nomeCategoria))
            {
                TempData["MensagemErro"] = "Digite o nome da categoria.";
                return RedirectToAction("Adicionar");
            }

            nomeCategoria = nomeCategoria.Trim();

            var categoriaExistente = _bancoContext.Categorias
                .FirstOrDefault(c => c.Nome.ToLower() == nomeCategoria.ToLower());

            if (categoriaExistente != null)
            {
                TempData["MensagemErro"] = "Essa categoria já existe.";
                return RedirectToAction("Adicionar");
            }

            var categoria = new CategoriaModel
            {
                Nome = nomeCategoria
            };

            _bancoContext.Categorias.Add(categoria);
            _bancoContext.SaveChanges();

            TempData["MensagemSucesso"] =
                "Categoria adicionada com sucesso!";

            return RedirectToAction("Adicionar");
        }
        [HttpPost]
        public IActionResult Editar(ProdutoModel produto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Categorias = _bancoContext.Categorias
                        .OrderBy(c => c.Nome)
                        .ToList();

                    return View(produto);
                }

                var produtoBanco = _bancoContext.Produtos
                    .FirstOrDefault(p => p.Id == produto.Id);

                if (produtoBanco == null)
                {
                    return NotFound();
                }

                produtoBanco.Nome = produto.Nome;
                produtoBanco.Descricao = produto.Descricao;
                produtoBanco.QuantidadeEmEstoque = produto.QuantidadeEmEstoque;
                produtoBanco.QuantidadeMinima = produto.QuantidadeMinima;
                produtoBanco.PrecoUnitario = produto.PrecoUnitario;
                produtoBanco.CategoriaId = produto.CategoriaId;

                _bancoContext.SaveChanges();

                TempData["MensagemSucesso"] = "Produto editado com sucesso!";

                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] =
                    $"Erro ao editar: {erro.InnerException?.Message ?? erro.Message}";

                ViewBag.Categorias = _bancoContext.Categorias
                    .OrderBy(c => c.Nome)
                    .ToList();

                return View(produto);
            }
        }
        [HttpPost]
        public IActionResult Excluir(int id)
        {
            try
            {
                var produto = _bancoContext.Produtos
                    .FirstOrDefault(p => p.Id == id);

                if (produto == null)
                {
                    return NotFound();
                }

                _bancoContext.Produtos.Remove(produto);
                _bancoContext.SaveChanges();

                TempData["MensagemSucesso"] =
                    "Produto excluído com sucesso!";

                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] =
                    $"Erro ao excluir: {erro.InnerException?.Message ?? erro.Message}";

                return RedirectToAction("Index");
            }
        }
    }

}

