using Microsoft.AspNetCore.Identity;
using SistemaEstoque.Models;

namespace SistemaEstoque.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly UserManager<ApplicationUserModel> _userManager;

        public UsuarioRepositorio(UserManager<ApplicationUserModel> userManager)
        {
            _userManager = userManager;
        }

        public List<ApplicationUserModel> ListarTodos()
        {
            return _userManager.Users.ToList();
        }

        public ApplicationUserModel? BuscarPorId(string id)
        {
            return _userManager.FindByIdAsync(id).Result;
        }

        public void Adicionar(ApplicationUserModel usuario, string senha)
        {
            var resultado = _userManager.CreateAsync(usuario, senha).Result;

            if (!resultado.Succeeded)
            {
                throw new Exception(
                    string.Join(", ", resultado.Errors.Select(e => e.Description))
                );
            }
        }

        public void Editar(ApplicationUserModel usuario)
        {
            var resultado = _userManager.UpdateAsync(usuario).Result;

            if (!resultado.Succeeded)
            {
                throw new Exception(
                    string.Join(", ", resultado.Errors.Select(e => e.Description))
                );
            }
        }

        public void Excluir(string id)
        {
            var usuario = BuscarPorId(id);

            if (usuario == null)
            {
                throw new Exception("Usuário não encontrado");
            }

            var resultado = _userManager.DeleteAsync(usuario).Result;

            if (!resultado.Succeeded)
            {
                throw new Exception(
                    string.Join(", ", resultado.Errors.Select(e => e.Description))
                );
            }
        }
    }
}