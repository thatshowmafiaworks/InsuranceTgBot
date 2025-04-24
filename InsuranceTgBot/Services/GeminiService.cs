using InsuranceTgBot.Models;
using InsuranceTgBot.Services.Interfaces;
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


        private readonly string apiEndpoint = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={config["GeminiApiKey"]}";

        public async Task<string> GetCompletion(string text, UserProgress progress)
        {
            using (var client = new HttpClient())
            {
                dynamic thingsToAsk = new
                {
                    ProvidedDriverLicense = progress.ProvidedDriverLicense,
                    ProvidedVehicleIdentificationDocument = progress.ProvidedVehicleIdentificationDocument,
                    IsPaid = progress.IsPaid
                };
                string prompt = $"""
                    You are a helpful and professional insurance agent assistant. You receive a JSON object that contains information about a client's insurance request.
                    Your task is to guide the client through completing all the required fields in the JSON by asking them relevant and friendly questions, one at a time.
                    The JSON may have missing fields or fields marked as null, "", or placeholder values like "TBD".
                    Things that needed to be asked marked as false something that dont need to be asked marked as true.
                    If every field is empty say that you are about to help them to apply for a car insurance.
                    Dont ask yes or no questions, if you need driver license or car documentation just ask to provide a photo, if both empty always first to ask is driver license.
                    Assume that you dont know about what you were talking with user before you should focus on JSON object.
                    If some of the values is true please dont say hello.
                    For each missing or incomplete field, ask the user to provide the correct information, using natural and polite language.
                    Once all required fields are filled, tell the user that the form is complete and ready to proceed to the next step (e.g., review, payment, or generation of the final document).
                    Always use the following rules:
                    1) Do not ask for fields that are already filled and valid.
                    2) Be concise, friendly, and professional.
                    3) Wait for the user's response after each question before continuing.
                    4) You do not need to reprint the full JSON — just work with the information inside.
                    5) You are only responsible for the conversational flow, not for validation logic or final submission.
                    6) Always answer in users language.
                    Here is users message:
                    {text}

                    Here is the JSON object:
                    {System.Text.Json.JsonSerializer.Serialize(thingsToAsk)}
                    """;

                logger.LogInformation(prompt);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var requestBody = new
                {
                    contents = new[]
                    {
                        new
                        {
                            parts = new[]
                            {
                                new { text = prompt }
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
            return "Sorry didn't understand question, repeat please";
        }
    }
}
