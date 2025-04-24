using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace InsuranceTgBot.Services
{
    public class GeminiService(
            IConfiguration config,
            ILogger<GeminiService> logger
        ) : IAIService
    {
        public async Task<string> GetCompletion(string text)
        {
            var apiEndpoint = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={config["GeminiApiKey"]}";
            var responseFromGemini = "fuck";
            using (var client = new HttpClient())
            {
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer ", config["GeminiApiKey"]);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var requestBody = new
                {
                    contents = new[]
                    {
                        new
                        {
                            parts = new[]
                            {
                                new { text = text }
                            }
                        }
                    }
                };

                var jsonRequest = System.Text.Json.JsonSerializer.Serialize(requestBody);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "appplication/json");

                try
                {
                    var response = await client.PostAsync(apiEndpoint, content);
                    response.EnsureSuccessStatusCode();
                    var responseContent = await response.Content.ReadAsStringAsync();
                    dynamic responseText = JsonConvert.DeserializeObject(responseContent);
                    return responseText.candidates[0].content.parts[0].text;
                }
                catch (Exception ex)
                {
                    logger.LogWarning($"Exception:'{ex.Message}'\n with inner:{ex.InnerException?.Message}");
                }
            }
            return responseFromGemini;
        }
    }
}
