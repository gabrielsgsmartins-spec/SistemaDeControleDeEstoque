using Microsoft.AspNetCore.Identity;

namespace SistemaEstoque.Models
{
    public class ApplicationUserModel : IdentityUser
    {
        public string NomeCompleto { get; set; }
        public TipoUsuario Tipo { get; set; }
    }
}