using SistemaEstoque.Data;
using SistemaEstoque.Models;
using Microsoft.EntityFrameworkCore;

namespace SistemaEstoque.Repositorio
{
    public interface IProdutoRepositorio
    {
        List<ProdutoModel> ListarTodos();
        ProdutoModel? BuscarPorId(int id);
        void Adicionar(ProdutoModel produto);
        void Editar(ProdutoModel produto);
        void Excluir(int id);
    }
}