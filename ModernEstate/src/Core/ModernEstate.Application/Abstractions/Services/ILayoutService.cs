namespace ModernEstate.Application.Abstractions.Services
{
    public interface ILayoutService
    {
        Task<Dictionary<string, string>> GetSettingsAsync();
    }
}
