using LSC.WebApi.Client.Entity;
using LSC.WebApi.Client.Exception;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LSC.WebApi.Client
{

    public class Consumer : IDisposable
    {

        #region Objetos/Variáveis locais

        protected readonly string _urlApi;
        protected readonly string _contentType;
        object _resp;

        #endregion

        #region Construtores

        public Consumer(string urlApi, string contentType)
        {
            _contentType = contentType;
            _urlApi = urlApi;
        }

        #endregion

        #region Métodos Públicos

        public TResponse Get<TResponse>(string apiMethod, string queryString)
        {
            GetAsync<TResponse>(apiMethod, queryString);
            return (TResponse)_resp;
        }

        public IEnumerable<TResponse> GetList<TResponse>(string apiMethod)
        {
            return GetList<TResponse>(apiMethod, null);
        }

        public IEnumerable<TResponse> GetList<TResponse>(string apiMethod, string queryString)
        {
            GetListAsync<TResponse>(apiMethod, queryString);
            return (IEnumerable<TResponse>)_resp;
        }

        public TResponse Post<TRequest, TResponse>(TRequest request, string apiMethod)
            where TResponse : class
        {
            return Post<TRequest, TResponse>(request, apiMethod, null);
        }

        public TResponse Post<TRequest, TResponse>(TRequest request, string apiMethod, string queryString)
            where TResponse : class
        {
            PostAsync<TRequest, TResponse>(request, apiMethod, queryString).Wait();
            return (TResponse)_resp;
        }

        public TResponse Put<TRequest, TResponse>(TRequest request, string apiMethod)
            where TResponse : class
        {
            return Put<TRequest, TResponse>(request, apiMethod, null);
        }

        public TResponse Put<TRequest, TResponse>(TRequest request, string apiMethod, string queryString)
            where TResponse : class
        {
            PutAsync<TRequest, TResponse>(request, apiMethod, queryString).Wait();
            return (TResponse)_resp;
        }

        public TResponse Delete<TResponse>(string apiMethod, string queryString)
            where TResponse : class
        {
            DeleteAsync<TResponse>(apiMethod, queryString).Wait();
            return (TResponse)_resp;
        }

        #endregion

        #region Métodos Locais

        private void HttpClientPrepare(HttpClient client)
        {

            client.BaseAddress = new System.Uri(_urlApi);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_contentType));

        }

        private ClientApiException GetApiException(HttpResponseMessage response)
        {
            object retorno = response.Content.ReadAsAsync<ApiErrorResponse>().Result;
            return new ClientApiException(((ApiErrorResponse)retorno).Message + " - " + ((ApiErrorResponse)retorno).MessageDetail);
        }

        #endregion

        #region Métodos Async

        protected void GetAsync<TResponse>(string apiMethod, string queryString)
        {
            using (var client = new HttpClient())
            {

                _resp = null;

                string callString = string.Format("{0}{1}", apiMethod, (string.IsNullOrWhiteSpace(queryString) ? string.Empty : queryString));

                // Preparando objeto client
                HttpClientPrepare(client);

                // Preparando e executando request
                HttpResponseMessage response = client.GetAsync(callString).Result;

                // Capturando o resultado
                if (response.IsSuccessStatusCode)
                {
                    _resp = response.Content.ReadAsAsync<TResponse>().Result;
                }
                else
                {
                    throw GetApiException(response);
                }

                _resp = (TResponse)(_resp is TResponse ? _resp : null);
            }
        }

        protected void GetListAsync<TResponse>(string apiMethod, string queryString)
        {
            using (var client = new HttpClient())
            {

                string callString = string.Format("{0}{1}", apiMethod, (string.IsNullOrWhiteSpace(queryString) ? string.Empty : queryString));

                _resp = null;

                // Preparando objeto client
                HttpClientPrepare(client);

                // Preparando e executando request
                HttpResponseMessage response = client.GetAsync(callString).Result;

                // Capturando o resultado
                if (response.IsSuccessStatusCode)
                {
                    _resp = response.Content.ReadAsAsync<IEnumerable<TResponse>>().Result;
                }
                else
                {
                    throw GetApiException(response);
                }

                _resp = _resp as IEnumerable<TResponse>;
            }
        }

        protected async Task PostAsync<TRequest, TResponse>(TRequest request, string apiMethod, string queryString)
            where TResponse : class
        {

            string urlRequest = string.Format("{0}{1}", apiMethod, (string.IsNullOrWhiteSpace(queryString) ? string.Empty : queryString.Trim()));

            using (var client = new HttpClient())
            {

                _resp = null;

                // Preparando objeto client
                HttpClientPrepare(client);

                // Preparando e executando request
                HttpResponseMessage response = client.PostAsJsonAsync<TRequest>(urlRequest, request).Result;

                // Capturando o resultado
                if (response.IsSuccessStatusCode)
                {
                    _resp = await response.Content.ReadAsAsync<TResponse>();
                }
                else
                {
                    throw GetApiException(response);
                }

                _resp = _resp as TResponse;
            }

        }

        protected async Task PutAsync<TRequest, TResponse>(TRequest request, string apiMethod, string queryString)
            where TResponse : class
        {

            string urlRequest = string.Format("{0}{1}", apiMethod, (string.IsNullOrWhiteSpace(queryString) ? string.Empty : queryString.Trim()));

            using (var client = new HttpClient())
            {

                _resp = null;

                // Preparando objeto client
                HttpClientPrepare(client);

                // Preparando e executando request
                HttpResponseMessage response = client.PutAsJsonAsync(urlRequest, request).Result;

                // Capturando o resultado
                if (response.IsSuccessStatusCode)
                {
                    _resp = await response.Content.ReadAsAsync<TResponse>();
                }
                else
                {
                    throw GetApiException(response);
                }

                _resp = _resp as TResponse;
            }

        }

        protected async Task DeleteAsync<TResponse>(string apiMethod, string queryString)
            where TResponse : class
        {

            using (var client = new HttpClient())
            {

                string callString = string.Format("{0}{1}", apiMethod, (string.IsNullOrWhiteSpace(queryString) ? string.Empty : queryString));

                _resp = null;

                // Preparando objeto client
                HttpClientPrepare(client);

                // Preparando e executando request
                HttpResponseMessage response = client.DeleteAsync(callString).Result;

                // Capturando o resultado
                if (response.IsSuccessStatusCode)
                {
                    _resp = await response.Content.ReadAsAsync<TResponse>();
                }
                else
                {
                    throw GetApiException(response);
                }

                _resp = _resp as TResponse;
            }

        }

        #endregion

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // dispose managed state (managed objects).
                }

                // free unmanaged resources (unmanaged objects) and override a finalizer below.
                // set large fields to null.

                _resp = null;

                disposedValue = true;
            }
        }

        // override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Consumer() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion


    }

}

