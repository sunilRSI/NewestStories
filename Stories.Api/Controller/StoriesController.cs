using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Stories.Api.Model;
using Stories.Api.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Stories.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoriesController : ControllerBase
    {
        private readonly IStoriesService storiesService;
        private readonly IMemoryCache _memoryCache;

        public StoriesController(IStoriesService storiesService, IMemoryCache memoryCache)
        {
            this.storiesService = storiesService;
            this._memoryCache = memoryCache;
        }
        /// <summary>
        /// This endpoint return the newest sstories as per the number of latest story paramter
        /// </summary>
        /// <param name="numberOfNewestStory"></param>
        /// <returns></returns>
        // GET api/<StoriesController>/GetNewestStories/{}
        [HttpGet("GetNewestStories/{numberOfNewestStory}")]
        public async Task<IEnumerable<NewestStories>> GetNewestStories(int numberOfNewestStory)
        {
            try
            {
                string cacheKey = $"neweststories{numberOfNewestStory}";
                if (_memoryCache.TryGetValue(cacheKey, out var item))
                {
                    return (IEnumerable<NewestStories>)item;
                }
                var neweststories = await storiesService.GetNewestStories(numberOfNewestStory);

                var options = new MemoryCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow =
                                    TimeSpan.FromSeconds(3600),
                    SlidingExpiration = TimeSpan.FromSeconds(1200)
                };

                _memoryCache.Set(cacheKey, neweststories, options);
                return neweststories;
            }
            catch (Exception ex)
            {
                throw;
            }
           
        }


    }
}
