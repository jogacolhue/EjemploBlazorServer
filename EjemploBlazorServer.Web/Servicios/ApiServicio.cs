using Blazored.SessionStorage;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace EjemploBlazorServer.Web.Servicios
{
    public class ApiServicio<T> : IApiServicio<T>
    {
        public HttpClient _httpClient;
        public ISessionStorageService _sessionStorageService { get; }

        public ApiServicio(HttpClient client, ISessionStorageService sessionStorageService)
        {
            _httpClient = client;
            _sessionStorageService = sessionStorageService;
        }

        public async Task<bool> ActualizarAsync(string requestUri, int Id, T obj)
        {
            string serializedUser = JsonSerializer.Serialize(obj);

            var requestMessage = new HttpRequestMessage(HttpMethod.Put, requestUri + Id);

            var token = await _sessionStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            requestMessage.Content = new StringContent(serializedUser);

            requestMessage.Content.Headers.ContentType
                = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();

            T returnedObj;

            if (!String.IsNullOrEmpty(responseBody))
                returnedObj = JsonSerializer.Deserialize<T>(responseBody);

            return responseStatusCode.ToString() == "OK";// return await Task.FromResult(returnedObj);
        }

        public async Task<bool> BorrarAsync(string requestUri, int Id)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, requestUri + Id);

            var token = await _sessionStorageService.GetItemAsync<string>("accessToken");

            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;

            return await Task.FromResult(true);
        }

        public async Task<bool> GuardarAsync(string requestUri, T obj)
        {
            string serializedUser = JsonSerializer.Serialize(obj);

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri);

            var token = await _sessionStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            requestMessage.Content = new StringContent(serializedUser);

            requestMessage.Content.Headers.ContentType
                = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();

            T returnedObj;

            if (!String.IsNullOrEmpty(responseBody))
                returnedObj = JsonSerializer.Deserialize<T>(responseBody);

            return responseStatusCode.ToString() == "OK"; //Task.FromResult(returnedObj);
        }

        public async Task<List<T>> ObetenerTodoAsync(string requestUri)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);

            var token = await _sessionStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;

            if (responseStatusCode.ToString() == "OK")
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return await Task.FromResult(JsonSerializer.Deserialize<List<T>>(responseBody));
            }
            else
                return null;
        }

        public async Task<T> ObtenerPorIdAsync(string requestUri, int Id)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + Id);

            var token = await _sessionStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();

            return await Task.FromResult(JsonSerializer.Deserialize<T>(responseBody));
        }
    }
}