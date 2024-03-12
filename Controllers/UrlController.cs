
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UrlShortener.Models.Domain;
    using UrlShortener.Models.DTO;
    using UrlShortener.Repository.Interface;
    using UrlShortener.Services.Interface;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    namespace UrlShortener.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class UrlController : ControllerBase
    {
            private readonly IUrlRepository _urlRepository;
            private readonly ILinkShortener _linkShortener;

            public UrlController(IUrlRepository urlRepository, ILinkShortener linkShortener)
            {
                _urlRepository = urlRepository;
                _linkShortener = linkShortener;
            }

            [HttpGet]
            public async Task<List<UrlLink>> Get()
            {
                var urls = await _urlRepository.GetAllUrlsAsync();
                return urls;
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<UrlLink>> GetUrlByIdAsync(Guid id)
            {
                var url = await _urlRepository.GetUrlByIdAsync(id);
                if (url == null)
                {
                    return NotFound();
                }
                return Ok(url);
            }

        [HttpPost]
        public async Task<ActionResult<UrlLink>> AddUrlAsync(UrlLinkDTO urlDto)
        {
            try
            {
                string shortUrl = _linkShortener.GenerateShortUrl(urlDto.LongForm);
                var url = new UrlLink
                {
                    LongForm = urlDto.LongForm,
                    CreatedBy = urlDto.CreatedBy,
                    CreatedAt = DateTime.UtcNow,
                    ShortForm = shortUrl
                };

                var addedUrl = await _urlRepository.AddUrlAsync(url);

                return Ok(addedUrl);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
            public async Task<ActionResult> DeleteUrlByIdAsync(Guid id)
            {
                await _urlRepository.DeleteUrlByIdAsync(id);
                return NoContent();
            }
    }
    }


