using Microsoft.AspNetCore.Mvc;
using SistemaEstoque.Data;

namespace SistemaDeControleDeEstoque.Controllers
{
    public class HomeController : Controller
    {
        private readonly EstoqueContext _bancoContext;

        public HomeController(EstoqueContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public IActionResult Index()
        {
            // Pega todos os produtos do banco
            var produtos = _bancoContext.Produtos.ToList();

            // Conta quantos produtos estão cadastrados
            ViewBag.TotalProdutos = produtos.Count;

            // Conta os produtos que estão com estoque baixo
            ViewBag.EstoqueBaixo = produtos
                .Count(p => p.QuantidadeEmEstoque <= p.QuantidadeMinima
                         && p.QuantidadeEmEstoque > 0);

            // Conta os produtos que estão sem estoque
            ViewBag.ProdutosEmFalta = produtos
                .Count(p => p.QuantidadeEmEstoque == 0);

            // Calcula o valor total do estoque
            ViewBag.ValorEstoque = produtos
                .Sum(p => p.QuantidadeEmEstoque * p.PrecoUnitario);

            // Pega os produtos que estão com estoque baixo
            ViewBag.ProdutosEstoqueBaixo = produtos
                .Where(p => p.QuantidadeEmEstoque <= p.QuantidadeMinima)
                .Take(10)
                .ToList();

            // Pega as últimas entradas
            var entradas = _bancoContext.MovimentacoesEntrada
                .Select(m => new
                {
                    Produto = m.Produto!.Nome,
                    Quantidade = m.Quantidade,
                    Data = m.DataMovimentacao
                })
                .OrderByDescending(m => m.Data)
                .Take(5)
                .ToList();

            // Pega as últimas vendas
            var vendas = _bancoContext.MovimentacoesVenda
                .Select(m => new
                {
                    Produto = m.Produto!.Nome,
                    Quantidade = m.Quantidade,
                    Data = m.DataMovimentacao
                })
                .OrderByDescending(m => m.Data)
                .Take(5)
                .ToList();

            // Pega as últimas perdas
            var perdas = _bancoContext.MovimentacoesPerdas
                .Select(m => new
                {
                    Produto = m.Produto!.Nome,
                    Quantidade = m.Quantidade,
                    Data = m.DataMovimentacao
                })
                .OrderByDescending(m => m.Data)
                .Take(5)
                .ToList();

            // Envia as movimentações para a View
            ViewBag.Entradas = entradas;
            ViewBag.Vendas = vendas;
            ViewBag.Perda = perdas;

            return View();
        }
    }
}