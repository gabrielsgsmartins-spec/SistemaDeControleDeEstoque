using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaEstoque.Data;
using SistemaEstoque.Models;

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