using System;
using UrlShortener.Models.Domain;
using UrlShortener.Models.DTO;

namespace UrlShortener.Repository.Interface
{
    public interface IUrlRepository
    {
        Task<List<UrlLink>> GetAllUrlsAsync();

        Task<UrlLink> GetUrlByIdAsync(Guid id);

        Task<UrlLink> AddUrlAsync(UrlLink url);

        Task DeleteUrlByIdAsync(Guid id);

        Task DeleteAllUrlsForUserAsync(string userId);

        Task<bool> CheckUserPermissionsAsync(string userId, Guid urlId);
    }

}
