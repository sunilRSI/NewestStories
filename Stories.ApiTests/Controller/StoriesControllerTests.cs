using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stories.Api.Controller;
using Stories.Api.Model;
using Stories.Api.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stories.Api.Controller.Tests
{
    [TestClass()]
    public class StoriesControllerTests
    {
        private IMemoryCache? _memoryCache;
        private IStoriesService? _storiesService;
        private HttpClient? _httpClient;
        /// <summary>
        /// test api data
        /// </summary>
        [TestMethod()]
        public void GetNewestStoriesTest()
        {
            var StoryUri = new Uri("https://hacker-news.firebaseio.com/v0");
            const Int32 numberOfNewestStory = 2;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = StoryUri;
            this._storiesService = new StoriesService(_httpClient);
            MemoryCacheOptions memoryCacheOptions = new MemoryCacheOptions();
            this._memoryCache = new MemoryCache(memoryCacheOptions);
            var controller = new StoriesController(_storiesService, this._memoryCache);
            var stories = controller.GetNewestStories(numberOfNewestStory).GetAwaiter().GetResult();
            //Here is the testcases
            Assert.IsNotNull(stories);
            Assert.IsTrue(stories.Any());
            Assert.AreEqual(stories.Count(), numberOfNewestStory);
        }
        /// <summary>
        /// test service data
        /// </summary>
        [TestMethod()]
        public void GetNewestStoriesFromService()
        {
            var StoryUri = new Uri("https://hacker-news.firebaseio.com/v0");
            const Int32 numberOfNewestStory = 2;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = StoryUri;
            this._storiesService = new StoriesService(_httpClient);
            var stories = this._storiesService.GetNewestStories(numberOfNewestStory).GetAwaiter().GetResult();
            //Here is the testcases
            Assert.IsNotNull(stories);
            Assert.IsTrue(stories.Any());
            Assert.AreEqual(stories.Count(), numberOfNewestStory);
        }
        /// <summary>
        /// test cached data
        /// </summary>
        [TestMethod()]
        public void GetNewestStoriesFromCache()
        { 
            const Int32 numberOfNewestStory = 2;
            IEnumerable<NewestStories>? stories = null; ; 
            string cacheKey = $"neweststories{numberOfNewestStory}";
            MemoryCacheOptions memoryCacheOptions = new MemoryCacheOptions();
            this._memoryCache = new MemoryCache(memoryCacheOptions);
            if (_memoryCache.TryGetValue(cacheKey, out var item))
            {
                stories=(IEnumerable<NewestStories>)item;
            }
            //Here is the testcases
            Assert.IsNotNull(stories);
            Assert.IsTrue(stories.Any());
            Assert.AreEqual(stories.Count(), numberOfNewestStory);
        }

    }
}