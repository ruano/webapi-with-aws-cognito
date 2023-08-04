using Microsoft.AspNetCore.Mvc;
using WebAPICongitoDemo.Api.Models.Request;
using WebAPICongitoDemo.Api.Models.Response;

namespace WebAPICongitoDemo.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TokenController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        public async Task<IActionResult> GetToken([FromBody] TokenRequest tokenRequest)
        {
            try
            {
                HttpClient httpClient = _httpClientFactory.CreateClient("CognitoToken");

                var data = new[]
                {
                    new KeyValuePair<string, string>("grant_type", "client_credentials"),
                    new KeyValuePair<string, string>("client_id", tokenRequest.ClientId),
                    new KeyValuePair<string, string>("client_secret", tokenRequest.ClientSecret),
                };

                HttpRequestMessage httpRequestMessage = new(HttpMethod.Post, "oauth2/token")
                {
                    Content = new FormUrlEncodedContent(data)
                };

                HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
                httpResponseMessage.EnsureSuccessStatusCode();
                var tokenResponse = await httpResponseMessage.Content.ReadFromJsonAsync<TokenResponse>();

                return Ok(new { tokenResponse!.AccessToken, tokenResponse.ExpiresIn, tokenResponse.TokenType });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
