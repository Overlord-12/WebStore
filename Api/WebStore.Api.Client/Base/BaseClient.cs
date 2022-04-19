using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Api.Client.Base
{
    public abstract class BaseClient : IDisposable
    {
        protected HttpClient HttpClient { get; }
        protected string _Address { get; }
        public BaseClient(HttpClient client, string Address)
        {
            HttpClient = client;
            _Address = Address;
        }
        protected T? Get<T>(string url) => GetAsync<T>(url).Result;
        protected async Task<T?> GetAsync<T>(string url, CancellationToken cancel = default)
        {
            var response = await HttpClient.GetAsync(url, cancel).ConfigureAwait(false);


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
                           .ReadFromJsonAsync<T>(cancellationToken: cancel)
                           .ConfigureAwait(false);
                        return result;
                    }
            }
        }

        protected HttpResponseMessage Post<T>(string url, T value) => PostAsync(url, value).Result;
        protected async Task<HttpResponseMessage> PostAsync<T>(string url, T value, CancellationToken cancellationToken = default)
        {
            var response = await HttpClient.PostAsJsonAsync(url, value, cancellationToken).ConfigureAwait(false);
            return response.EnsureSuccessStatusCode();
        }

        protected HttpResponseMessage Put<T>(string url, T value) => PutAsync(url, value).Result;
        protected async Task<HttpResponseMessage> PutAsync<T>(string url, T value, CancellationToken cancellation = default)
        {
            var response = await HttpClient.PutAsJsonAsync(url, value, cancellation).ConfigureAwait(false);
            return response.EnsureSuccessStatusCode();
        }

        protected HttpResponseMessage Delete(string url) => DeleteAsync(url).Result;
        protected async Task<HttpResponseMessage> DeleteAsync(string url)
        {
            var response = await HttpClient.DeleteAsync(url).ConfigureAwait(false);
            return response;
        }

        public void Dispose()
        {
            if (_Disposed) return;
            Dispose(true);
            _Disposed = true;
            //GC.SuppressFinalize(this); // Нужно при наличии ~BaseClient()
        }

        private bool _Disposed;
        protected virtual void Dispose(bool Disposing)
        {
            if (_Disposed) return;

            if (Disposing)
            {

            }

        }
    }
}
