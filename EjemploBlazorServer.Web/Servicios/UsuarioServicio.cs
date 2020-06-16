using EjemploBlazorServer.Shared.Modelos;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace EjemploBlazorServer.Web.Servicios
{
    public class UsuarioServicio : IUsuarioServicio
    {
        public HttpClient _httpClient { get; }

        public UsuarioServicio(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Usuario> LoginAsync(Usuario user)
        {
            string serializedUser = JsonSerializer.Serialize(user);

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "api/usuarios/login");
            requestMessage.Content = new StringContent(serializedUser);

            requestMessage.Content.Headers.ContentType
              = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();

            Usuario returnedUser = new Usuario();

            if (responseStatusCode.ToString() == "OK")
                returnedUser = JsonSerializer.Deserialize<Usuario>(responseBody);

            //var returnedUser = JsonSerializer.Deserialize<Usuario>(responseBody);

            return await Task.FromResult(returnedUser);
        }
    }
}