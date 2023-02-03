using System.Net;

namespace Workouts.HttpClientX
{
    public class TokenHandler : DelegatingHandler
    {
        //AddHttpMessageHandler
        protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpStatusCode httpStatusCode = HttpStatusCode.Unauthorized;
            int retryCount = 0;

            HttpResponseMessage response = null;

            while (httpStatusCode == HttpStatusCode.Unauthorized && retryCount < 2)
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bareer", GetToken());
                response = base.Send(request, cancellationToken);
                httpStatusCode = response.StatusCode;
                retryCount++;
            }

            return response;
        }
        private string GetToken()
        {
            return string.Empty;
        }


    }
}
