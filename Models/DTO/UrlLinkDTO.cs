namespace UrlShortener.Models.DTO
{
    public class UrlLinkDTO
    {
        public required string LongForm { get; set; }
        public required string CreatedBy { get; set; }
    }
}
