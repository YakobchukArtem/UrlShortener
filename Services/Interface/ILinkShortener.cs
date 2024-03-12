namespace UrlShortener.Services.Interface
{
    public interface ILinkShortener
    {
        string GenerateShortUrl(string LongForm);
    }
}
