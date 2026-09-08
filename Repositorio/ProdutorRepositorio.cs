using SistemaEstoque.Data;
using SistemaEstoque.Models;
using Microsoft.EntityFrameworkCore;

namespace SistemaEstoque.Repositorio
{
    public class ProdutoRepositorio : IProdutoRepositorio
    {
        private readonly EstoqueContext _bancoContext;

        public ProdutoRepositorio(EstoqueContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public List<ProdutoModel> ListarTodos()
        {
            return _bancoContext.Produtos
                .Include(p => p.Categoria)   
                .ToList();
        }

        public ProdutoModel? BuscarPorId(int id)
        {
            return _bancoContext.Produtos
                .Include(p => p.Categoria)
                .FirstOrDefault(p => p.Id == id);
        }

        public void Adicionar(ProdutoModel produto)
        {
            _bancoContext.Produtos.Add(produto);
            _bancoContext.SaveChanges();
        }

        public void Editar(ProdutoModel produto)
        {
            _bancoContext.Produtos.Update(produto);
            _bancoContext.SaveChanges();
        }

        public void Excluir(int id)
        {
            ProdutoModel? produto = BuscarPorId(id);
            if (produto == null) throw new Exception("Produto não encontrado");

            _bancoContext.Produtos.Remove(produto);
            _bancoContext.SaveChanges();
        }

    }
}