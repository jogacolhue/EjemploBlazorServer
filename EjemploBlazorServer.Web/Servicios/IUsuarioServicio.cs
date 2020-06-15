using EjemploBlazorServer.Shared.Modelos;
using System.Threading.Tasks;

namespace EjemploBlazorServer.Web.Servicios
{
    internal interface IUsuarioServicio
    {
        public Task<Usuario> LoginAsync(Usuario user);
    }
}