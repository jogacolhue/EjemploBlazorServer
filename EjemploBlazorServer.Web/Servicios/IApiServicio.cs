using System.Collections.Generic;
using System.Threading.Tasks;

namespace EjemploBlazorServer.Web.Servicios
{
    internal interface IApiServicio<T>
    {
        Task<List<T>> ObetenerTodoAsync(string requestUri);

        Task<T> ObtenerPorIdAsync(string requestUri, int Id);

        Task<bool> GuardarAsync(string requestUri, T obj);

        Task<bool> ActualizarAsync(string requestUri, int Id, T obj);

        Task<bool> BorrarAsync(string requestUri, int Id);
    }
}