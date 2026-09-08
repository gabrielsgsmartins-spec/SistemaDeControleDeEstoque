using Microsoft.AspNetCore.Mvc;
using SistemaEstoque.Data;
using SistemaEstoque.Models;

namespace SistemaDeControleDeEstoque.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly EstoqueContext _bancoContext;

        public CategoriaController(EstoqueContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        [HttpPost]
        public IActionResult Adicionar(string nomeCategoria)
        {
            if (string.IsNullOrWhiteSpace(nomeCategoria))
            {
                return RedirectToAction("Adicionar", "Produto");
            }

            nomeCategoria = nomeCategoria.Trim();

            var categoriaExistente = _bancoContext.Categorias
                .FirstOrDefault(c => c.Nome.ToLower() == nomeCategoria.ToLower());

            if (categoriaExistente != null)
            {
                return RedirectToAction("Adicionar", "Produto");
            }

            var categoria = new CategoriaModel
            {
                Nome = nomeCategoria
            };

            _bancoContext.Categorias.Add(categoria);
            _bancoContext.SaveChanges();

            return RedirectToAction("Adicionar", "Produto");
        }
    }
}