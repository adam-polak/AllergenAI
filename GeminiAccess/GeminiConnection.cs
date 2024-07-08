using RestSharp;
using Newtonsoft.Json;

namespace AllergenAI.GeminiAccess;

public class GeminiConnection
{

    IConfiguration _configuration;
    RestClient client;

    public GeminiConnection(IConfiguration configuration)
    {
        _configuration = configuration;
        client = new RestClient(_configuration.GetConnectionString("Gemini") ?? "");
        Console.WriteLine(GeneratePayload("test"));
    }

    private static string GeneratePayload(string text)
    {
        var payload = new
        {
            contents = new
            {
                role = "USER",
                parts = new object[] {
                    new {text = text}
                }
            },
            generation_config = new
            {
                temperature = 0.4,
                top_p = 1,
                top_k = 32,
                max_output_tokens = 2048
            }
        };
        return JsonConvert.SerializeObject(payload, Formatting.Indented);
    }


}