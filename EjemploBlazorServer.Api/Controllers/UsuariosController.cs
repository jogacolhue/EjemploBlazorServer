using EjemploBlazorServer.Shared.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EjemploBlazorServer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private static readonly List<Usuario> usuarios = GenerarUsuarios();

        private static List<Usuario> GenerarUsuarios()
        {
            return new List<Usuario>()
            {
                new Usuario()
                {
                    UserName = "jcolque",
                    Password = "123456"
                },
                new Usuario()
                {
                    UserName = "jcolque2",
                    Password = "123456"
                }
            };
        }

        [HttpPost("login")]
        public async Task<Usuario> LoginUsuario([FromBody] Usuario Usuario)
        {
            var resultado = usuarios.FirstOrDefault(x => x.UserName == Usuario.UserName && x.Password == Usuario.Password);

            Usuario res = new Usuario();

            if (resultado != null)
            {
                var login = new Modelos.Login();
                login.client_secret = "C-7TbJ5FDzq8WmVyPcExqoBDHiboGBsePqPw0UiEj9ESQoo9e9p4rMvwNkDYh_4O";
                login.client_id = "XbYvdDCkr3wzH5JUztl678JySte8cDjl";
                login.grant_type = "client_credentials";
                login.audience = "https://localhost:44394/";

                var json = JsonSerializer.Serialize(login);

                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var url = "https://dev--u21dsv2.us.auth0.com/oauth/token";

                HttpClient httpClient = new HttpClient();

                var response = await httpClient.PostAsync(url, data);

                var responseBody = await response.Content.ReadAsStringAsync();

                res = JsonSerializer.Deserialize<Usuario>(responseBody);

                res.UserName = Usuario.UserName;
            }
            else { throw new Exception(); }

            return await Task.FromResult(res);
        }
    }
}