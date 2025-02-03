namespace ModernEstateDemo.Services.Intefaces
{
    public interface ILayoutService
    {
        Task<Dictionary<string, string>> GetSettingsAsync();
    }
}
