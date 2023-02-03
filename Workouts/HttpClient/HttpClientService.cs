using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace Workouts.HttpClientX
{
    /// <summary>
    ///  created this cause i need something like that. no async, sample clientService with token and queryString builder
    ///  usage in Workouts.Program.cs
    /// </summary>
    public class HttpClientService : IDisposable
    {
        private readonly HttpClient _httpClient;

        private string _url;
        private bool isQueryStringAdded => _queryStringBuilder.Length > 0;
        private StringBuilder _queryStringBuilder = new StringBuilder();

        private int retryCount = 0;
        public HttpClientService(string url)
        {
            _url = url;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public TResponse SendRequest<TResponse>(HttpMethod httpMethod, object body = null)
        {
            if (isQueryStringAdded)
                _url = _url + _queryStringBuilder.ToString();

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage
            {
                Method = httpMethod,
                RequestUri = new Uri(_url)
            };

            if (httpMethod == HttpMethod.Post || httpMethod == HttpMethod.Put)
            {
                httpRequestMessage.Content = GenerateJsonBody(body);
            }

            HttpResponseMessage httpResponse = GetHttpResponse(httpRequestMessage);

            if (httpResponse.IsSuccessStatusCode)
            {
                string responseStr = httpResponse.Content.ReadAsStringAsync().Result;
                TResponse response = JsonConvert.DeserializeObject<TResponse>(responseStr);
                return response;
            }
            throw new Exception("HTTP REQ ERROR");

        }
        public void AddQueryParameter(string key, object value) =>
            _ = isQueryStringAdded == false ? _queryStringBuilder.Append($"?{key}={value}")
                                            : _queryStringBuilder.Append($"&{key}={value}");

        public void Dispose()
        {
            _httpClient?.Dispose();
        }

        private HttpResponseMessage GetHttpResponse(HttpRequestMessage httpRequestMessage)
        {
            httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bareer", GetToken());
            HttpResponseMessage httpResponse = _httpClient.Send(httpRequestMessage);
            if (httpResponse.StatusCode == HttpStatusCode.Unauthorized && retryCount <= 2)
            {
                retryCount++;
                GetHttpResponse(httpRequestMessage);
            }

            return httpResponse;
        }
        private StringContent GenerateJsonBody(object data)
        {
            string json = JsonConvert.SerializeObject(data);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private string GetToken()
        {
            //read current token or go get token
            return string.Empty;
        }
    }
}
