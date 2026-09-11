using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaEstoque.Data;
using SistemaEstoque.Enums;
using SistemaEstoque.Models;

namespace SistemaDeControleDeEstoque.Controllers
{
    public class MovimentacaoController : AdminController
    {
        private readonly EstoqueContext _bancoContext;

        public MovimentacaoController(
            EstoqueContext bancoContext,
            UserManager<ApplicationUserModel> userManager) : base(userManager)
        {
            _bancoContext = bancoContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var entradas = _bancoContext.MovimentacoesEntrada
                .Include(m => m.Produto)
                .Select(m => new
                {
                    Tipo = "Entrada",
                    Produto = m.Produto!.Nome,
                    Quantidade = m.Quantidade,
                    Data = m.DataMovimentacao
                })
                .ToList();

            var vendas = _bancoContext.MovimentacoesVenda
                .Include(m => m.Produto)
                .Select(m => new
                {
                    Tipo = "Venda",
                    Produto = m.Produto!.Nome,
                    Quantidade = m.Quantidade,
                    Data = m.DataMovimentacao
                })
                .ToList();

            var perdas = _bancoContext.MovimentacoesPerdas
                .Include(m => m.Produto)
                .Select(m => new
                {
                    Tipo = "Perda",
                    Produto = m.Produto!.Nome,
                    Quantidade = m.Quantidade,
                    Data = m.DataMovimentacao
                })
                .ToList();

            var movimentacoes = entradas
                .Concat(vendas)
                .Concat(perdas)
                .OrderByDescending(m => m.Data)
                .ToList();

            ViewBag.Movimentacoes = movimentacoes;

            return View();
        }

        [HttpGet]
        public IActionResult Gerar()
        {
            ViewBag.Produtos = _bancoContext.Produtos
                .OrderBy(p => p.Nome)
                .ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Gerar(
            int ProdutoId,
            TipoMovimentacaoEnum Tipo,
            int Quantidade,
            string? Motivo)
        {
            if (Quantidade <= 0)
            {
                ModelState.AddModelError(
                    "",
                    "A quantidade deve ser maior que zero."
                );
            }

            var produto = _bancoContext.Produtos
                .FirstOrDefault(p => p.Id == ProdutoId);

            if (produto == null)
            {
                ModelState.AddModelError(
                    "",
                    "Produto não encontrado."
                );
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Produtos = _bancoContext.Produtos
                    .OrderBy(p => p.Nome)
                    .ToList();

                return View();
            }

            int estoqueAntes = produto!.QuantidadeEmEstoque;

            if (Tipo == TipoMovimentacaoEnum.Entrada)
            {
                var entrada = new MovimentacaoEntradaModel
                {
                    ProdutoId = ProdutoId,
                    Quantidade = Quantidade,
                    DataMovimentacao = DateTime.Now
                };

                _bancoContext.MovimentacoesEntrada.Add(entrada);

                produto.QuantidadeEmEstoque += Quantidade;
            }
            else if (Tipo == TipoMovimentacaoEnum.Venda)
            {
                if (produto.QuantidadeEmEstoque < Quantidade)
                {
                    ModelState.AddModelError(
                        "",
                        "Não há estoque suficiente para realizar essa venda."
                    );

                    ViewBag.Produtos = _bancoContext.Produtos
                        .OrderBy(p => p.Nome)
                        .ToList();

                    return View();
                }

                var venda = new MovimentacaoVendaModel
                {
                    ProdutoId = ProdutoId,
                    Quantidade = Quantidade,
                    DataMovimentacao = DateTime.Now
                };

                _bancoContext.MovimentacoesVenda.Add(venda);

                produto.QuantidadeEmEstoque -= Quantidade;
            }
            else if (Tipo == TipoMovimentacaoEnum.Perda)
            {
                if (produto.QuantidadeEmEstoque < Quantidade)
                {
                    ModelState.AddModelError(
                        "",
                        "Não há estoque suficiente para registrar essa perda."
                    );

                    ViewBag.Produtos = _bancoContext.Produtos
                        .OrderBy(p => p.Nome)
                        .ToList();

                    return View();
                }

                if (string.IsNullOrWhiteSpace(Motivo))
                {
                    ModelState.AddModelError(
                        "",
                        "Informe o motivo da perda."
                    );

                    ViewBag.Produtos = _bancoContext.Produtos
                        .OrderBy(p => p.Nome)
                        .ToList();

                    return View();
                }

                var perda = new MovimentacaoPerdaModel
                {
                    ProdutoId = ProdutoId,
                    Quantidade = Quantidade,
                    Motivo = Motivo.Trim(),
                    DataMovimentacao = DateTime.Now
                };

                _bancoContext.MovimentacoesPerdas.Add(perda);

                produto.QuantidadeEmEstoque -= Quantidade;
            }
            else
            {
                ModelState.AddModelError(
                    "",
                    "Selecione um tipo de movimentação válido."
                );

                ViewBag.Produtos = _bancoContext.Produtos
                    .OrderBy(p => p.Nome)
                    .ToList();

                return View();
            }

            int estoqueDepois = produto.QuantidadeEmEstoque;

            _bancoContext.SaveChanges();

            TempData["TipoMovimentacao"] = Tipo.ToString();
            TempData["Produto"] = produto.Nome;
            TempData["ProdutoId"] = produto.Id;
            TempData["Quantidade"] = Quantidade;
            TempData["Data"] = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            TempData["EstoqueAntes"] = estoqueAntes;
            TempData["EstoqueDepois"] = estoqueDepois;
            TempData["Motivo"] = Motivo;

            return RedirectToAction("Nota");
        }

        [HttpGet]
        public IActionResult Nota()
        {
            return View();
        }
    }
}