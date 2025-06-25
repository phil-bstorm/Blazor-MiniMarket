using Microsoft.JSInterop;

namespace MiniMarket.Middlewares
{
    public class TokenInterceptor: DelegatingHandler
    {
        private readonly IJSRuntime _jsRuntime;

        public TokenInterceptor(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {
            // recupere le token
            string token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "token");
            if (!string.IsNullOrEmpty(token)){
                // ajouter le token au header de la requete
                request.Headers.Add("Authorization", "bearer " + token);
            }

            // continuer la requete
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
