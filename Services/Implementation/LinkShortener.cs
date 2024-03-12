using System.Text;
using UrlShortener.Services.Interface;

namespace UrlShortener.Services.Implementation
{
    public class LinkShortener : ILinkShortener
    {
        public string GenerateShortUrl(string longUrl)
        {
            string allowedChars = "abcdefghijklmnopqrstuvwxyz0123456789";
            int shortUrlLength = 7; 
            Random random = new Random();
            StringBuilder shortUrlBuilder = new StringBuilder();

            for (int i = 0; i < shortUrlLength; i++)
            {
                if (i == 2)
                {
                    shortUrlBuilder.Append('.');
                }
                else if (i == 5)
                {
                    shortUrlBuilder.Append('/');
                }
                else
                {
                    char randomChar = allowedChars[random.Next(0, allowedChars.Length)];
                    shortUrlBuilder.Append(randomChar);
                }
            }

            return shortUrlBuilder.ToString();
        }


    }
}
