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

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var produto = _bancoContext.Produtos
                .FirstOrDefault(p => p.Id == id);

            if (produto == null)
            {
                return NotFound();
            }

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

        [HttpPost]
        [HttpPost]
        public IActionResult Adicionar(ProdutoModel produto, string nomeCategoria)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(produto);
                }

                if (string.IsNullOrWhiteSpace(nomeCategoria))
                {
                    ModelState.AddModelError(
                        "nomeCategoria",
                        "Informe uma categoria."
                    );

                    return View(produto);
                }

                string nome = nomeCategoria.Trim();

                // Procura a categoria
                var categoria = _bancoContext.Categorias
                    .FirstOrDefault(c => c.Nome.ToLower() == nome.ToLower());

                // Se não existir, cria
                if (categoria == null)
                {
                    categoria = new CategoriaModel
                    {
                        Nome = nome
                    };

                    _bancoContext.Categorias.Add(categoria);
                    _bancoContext.SaveChanges();
                }

                // RELACIONA O PRODUTO COM A CATEGORIA
                produto.CategoriaId = categoria.Id;

                // Salva o produto
                _bancoContext.Produtos.Add(produto);
                _bancoContext.SaveChanges();

                TempData["MensagemSucesso"] = "Produto cadastrado com sucesso!";

                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] =
                    $"Erro ao cadastrar: {erro.InnerException?.Message ?? erro.Message}";

                return View(produto);
            }
        }

        [HttpPost]
        public IActionResult Editar(ProdutoModel produto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
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

                _bancoContext.Produtos.Update(produtoBanco);
                _bancoContext.SaveChanges();

                TempData["MensagemSucesso"] = "Produto editado com sucesso!";

                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] =
                    $"Erro ao editar: {erro.InnerException?.Message ?? erro.Message}";

                return RedirectToAction("Index");
            }
        }
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

