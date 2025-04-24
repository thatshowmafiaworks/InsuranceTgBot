namespace InsuranceTgBot.Services
{
    public interface IAIService
    {
        Task<string> GetCompletion(string text);
    }
}
