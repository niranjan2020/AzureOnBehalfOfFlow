using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace NetCoreAPI
{
    public class DownStreamAPIService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ITokenAcquisition _tokenAcquisition;
        private readonly IConfiguration _configuration;

        public DownStreamAPIService(IHttpClientFactory clientFactory,
            ITokenAcquisition tokenAcquisition,
            IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _tokenAcquisition = tokenAcquisition;
            _configuration = configuration;
        }

        public async Task<JArray> GetApiDataAsync()
        {
            var client = _clientFactory.CreateClient();

            // user_impersonation access_as_user access_as_application .default
            var scope = _configuration["DownStreamAPI:ScopeForAccessToken"];
            var accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(new[] { scope }).ConfigureAwait(false);

            client.BaseAddress = new Uri(_configuration["DownStreamAPI:ApiBaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetAsync("weatherforecast").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var data = JArray.Parse(responseContent);

                return data;
            }

            throw new ApplicationException($"Status code: {response.StatusCode}, Error: {response.ReasonPhrase}");
        }
    }
}
