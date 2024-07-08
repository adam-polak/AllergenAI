using RestSharp;
using Newtonsoft.Json;
using GenerativeAI.Models;

namespace AllergenAI.GeminiAccess;

public class GeminiConnection
{
    private readonly string apiKey;
    GenerativeModel model;
    IConfiguration _configuration;

    public GeminiConnection(IConfiguration configuration)
    {
        _configuration = configuration;
        apiKey = _configuration.GetConnectionString("Key") ?? "";
        model = new GenerativeModel(apiKey);
    }

    public async void CheckForAllergens(string food, List<string> allergens)
    {
        var result = await model.GenerateContentAsync($"Does {food} contain peanuts?");
        Console.WriteLine(result);
    }

}