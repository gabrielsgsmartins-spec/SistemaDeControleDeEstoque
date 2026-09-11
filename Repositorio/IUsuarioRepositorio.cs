using SistemaEstoque.Models;

namespace SistemaEstoque.Repositorio
{
    public interface IUsuarioRepositorio
    {
        List<ApplicationUserModel> ListarTodos();
        ApplicationUserModel? BuscarPorId(string id);
        void Adicionar(ApplicationUserModel usuario, string senha);
        void Editar(ApplicationUserModel usuario);
        void Excluir(string id);
    }
}