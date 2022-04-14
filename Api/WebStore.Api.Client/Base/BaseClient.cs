using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Api.Client.Base
{
    public abstract class BaseClient
    {
        protected HttpClient HttpClient { get; }
        protected string _Adress { get; }
        public BaseClient(HttpClient client, string Adress)
        {
            HttpClient = client;
            _Adress = Adress;
        }
        protected T? Get<T>(string url) => GetAsync<T>(url).Result;
        protected async Task<T?> GetAsync<T>(string url)
        {
            var response = await HttpClient.GetAsync(url).ConfigureAwait(false);


            switch (response.StatusCode)
            {
                case HttpStatusCode.NoContent:
                case HttpStatusCode.NotFound:
                    return default;
                default:
                    {
                        var result = await response
                           .EnsureSuccessStatusCode()
                           .Content
                           .ReadFromJsonAsync<T>()
                           .ConfigureAwait(false);
                        return result;
                    }
            }
        }

        protected HttpResponseMessage Post<T>(string url, T value) => PostAsync(url, value).Result;
        protected async Task<HttpResponseMessage> PostAsync<T>(string url, T value)
        {
            var response = await HttpClient.PostAsJsonAsync(url, value).ConfigureAwait(false);
            return response.EnsureSuccessStatusCode();
        }

        protected HttpResponseMessage Put<T>(string url, T value) => PutAsync(url, value).Result;
        protected async Task<HttpResponseMessage> PutAsync<T>(string url, T value)
        {
            var response = await HttpClient.PutAsJsonAsync(url, value).ConfigureAwait(false);
            return response.EnsureSuccessStatusCode();
        }

        protected HttpResponseMessage Delete(string url) => DeleteAsync(url).Result;
        protected async Task<HttpResponseMessage> DeleteAsync(string url)
        {
            var response = await HttpClient.DeleteAsync(url).ConfigureAwait(false);
            return response;
        }
    }
}
