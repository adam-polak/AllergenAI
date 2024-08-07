using AllergenAI.GeminiAccess.Models;
using RestSharp;

namespace AllergenAI.GeminiAccess;

public class GeminiConnection
{
    IConfiguration _configuration;
    RestClient client;

    public GeminiConnection(IConfiguration configuration)
    {
        _configuration = configuration;
        client = new RestClient("http://localhost:5012/prompt/");
    }

    public bool CheckForAllergens(string food, List<Allergen> allergens)
    {
        var response = client.ExecuteAsync(new RestRequest(CreateSearchPrompt(food, allergens), Method.Get));
        if(response.Result.StatusCode != System.Net.HttpStatusCode.OK) return false;
        string result = response.Result.Content ?? "";
        return !(result.Count() == 0 || result.Substring(0, 1).Equals("n") || result.Substring(0, 1).Equals("N"));
    }

    private static string CreateSearchPrompt(string food, List<Allergen> allergens)
    {
        string ans = $"Answer yes or no if {food} contains ";
        switch(allergens.Count)
        {
            case 1:
                ans += $"{allergens.ElementAt(0).Label}?";
                break;
            case 2: 
                ans += $"{allergens.ElementAt(0).Label} or {allergens.ElementAt(1).Label}?";
                break;
            default:
                ans += $"{allergens.ElementAt(0).Label}, ";
                for(int i = 1; i < allergens.Count - 1; i++)
                {
                    ans += $"{allergens.ElementAt(i)}, ";
                }
                ans += $"or {allergens.ElementAt(allergens.Count - 1).Label}?";
                break;
        }
        return ans;
    }

}