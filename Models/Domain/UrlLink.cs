namespace UrlShortener.Models.Domain
{
    public class UrlLink
    {
        public Guid Id { get; set; }
        public required string LongForm { get; set; }
        public required string ShortForm { get; set; }
        public required string CreatedBy { get; set; }
        public required DateTime CreatedAt { get; set; }

    }
}
