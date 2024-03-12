
namespace UrlShortener.Repository.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using global::UrlShortener.Data;
    using global::UrlShortener.Models.Domain;
    using global::UrlShortener.Repository.Interface;
    using Microsoft.EntityFrameworkCore;

    namespace UrlShortener.Repository
    {
        public class UrlRepository : IUrlRepository
        {
            private readonly ApplicationDbContext _context;

            public UrlRepository(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<List<UrlLink>> GetAllUrlsAsync()
            {
                return await _context.UrlLinks.ToListAsync();
            }

            public async Task<UrlLink> GetUrlByIdAsync(Guid id)
            {
                return await _context.UrlLinks.FindAsync(id);
            }

            public async Task<UrlLink> AddUrlAsync(UrlLink url)
            {
                url.CreatedAt = DateTime.UtcNow;
                _context.UrlLinks.Add(url);
                await _context.SaveChangesAsync();
                return url;
            }

            public async Task DeleteUrlByIdAsync(Guid id)
            {
                var url = await _context.UrlLinks.FindAsync(id);
                if (url != null)
                {
                    _context.UrlLinks.Remove(url);
                    await _context.SaveChangesAsync();
                }
            }

            public async Task DeleteAllUrlsForUserAsync(string userId)
            {
                var urls = await _context.UrlLinks.Where(u => u.CreatedBy == userId).ToListAsync();
                if (urls != null && urls.Count > 0)
                {
                    _context.UrlLinks.RemoveRange(urls);
                    await _context.SaveChangesAsync();
                }
            }

            public async Task<bool> CheckUserPermissionsAsync(string userId, Guid urlId)
            {
                var url = await _context.UrlLinks.FindAsync(urlId);
                return url != null && url.CreatedBy == userId;
            }
        }
    }

}
