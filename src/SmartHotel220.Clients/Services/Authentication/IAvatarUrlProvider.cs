namespace SmartHotel220.Clients.Core.Services.Authentication
{
    /// <summary>
    /// Описывает поставщик аватара
    /// </summary>
    public interface IAvatarUrlProvider
    {
        string GetAvatarUrl(string email);
    }
}
