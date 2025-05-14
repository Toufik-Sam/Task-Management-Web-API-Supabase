
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using TaskManagementDataAccessLayer.Exceptions;

namespace TaskManagementDataAccessLayer.CustomSupabaseClient;

public class SupabaseClient : ISupabaseClient
{
    private readonly IHttpClientFactory _httpClient;
    private readonly IConfiguration _config;
    private readonly ITokenAccessor _tokenAccessor;

    public SupabaseClient(IHttpClientFactory httpClient, IConfiguration config,ITokenAccessor tokenAccessor)
    {
        this._httpClient = httpClient;
        this._config = config;
        this._tokenAccessor = tokenAccessor;
    }
    public async Task<string> Rpc(string FunctionName, object parameters,string OverrideToken="")
    {
        var client = _httpClient.CreateClient("SupabaseClient");
        // Set authentication headers for this request
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", 
            (string.IsNullOrEmpty(OverrideToken)?_tokenAccessor.Token:OverrideToken));

        client.DefaultRequestHeaders.Remove("apikey");

        client.DefaultRequestHeaders.Add("apikey", _config["Supabase:AnonKey"]);
        var content = new StringContent(JsonSerializer.Serialize(parameters), Encoding.UTF8, "application/json");

        var response = await client.PostAsync($"/rest/v1/rpc/{FunctionName}", content);
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw MapStatusCodeToException(response.StatusCode, error);
        }
        var resultJson = await response.Content.ReadAsStringAsync();
        return resultJson;
    }
    private Exception MapStatusCodeToException(HttpStatusCode StatusCode,string errorMessage)
    {
        return StatusCode switch
        {
            HttpStatusCode.NotFound => new NotFoundException(errorMessage),
            HttpStatusCode.BadRequest => new BadRequestException(errorMessage),
            HttpStatusCode.Unauthorized => new UnauthorizedAccessException(errorMessage),
            HttpStatusCode.Forbidden => new ForbiddenRequestException(errorMessage),
            _ => new Exception(errorMessage)
        };
    }
}
