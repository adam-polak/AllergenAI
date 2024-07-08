using RestSharp;
using Newtonsoft.Json;
using Junaid.GoogleGemini.Net;

namespace AllergenAI.GeminiAccess;

public class GeminiConnection
{
    private readonly string apiKey;
    IConfiguration _configuration;

    public GeminiConnection(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async void CheckForAllergens(string food, List<string> allergens)
    {
    }

}