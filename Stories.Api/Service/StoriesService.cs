using Microsoft.DotNet.MSIdentity.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Protocol.Plugins;
using Stories.Api.Model;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;

namespace Stories.Api.Service
{

    //
    // Summary:
    //     Initializes a new instance of the Stories class.
    //
    public class StoriesService : BaseClient, IStoriesService
    {
        private readonly string uRL = string.Empty;
        public StoriesService(HttpClient httpClient) : base(httpClient)
        {
            uRL = httpClient.BaseAddress.ToString();
        }
        /// <summary>
        /// It return the newest sstories as per the number of latest story paramter
        /// </summary>
        /// <param name="numberOfNewestStory"></param>
        /// <returns></returns>
        public async Task<IEnumerable<NewestStories>> GetNewestStories(int numberOfNewestStory)
        {
            var storiesId = await GetNewestStoryIds();
			storiesId= storiesId.OrderByDescending(x => x.Substring(0));
            List<NewestStories> newestStories = new List<NewestStories>();
            foreach (var storyId in storiesId)
            {
                var newestStory = await GetStoryDetailsByStoryId(storyId);
                if (newestStory != null && newestStory?.title != null && newestStory?.url != null)
                {
                    newestStories.Add((newestStory));
                }
                if (newestStories.Count == numberOfNewestStory)
                {
                    break;
                }

            }

            return newestStories;
        }

        private async Task<IEnumerable<string>> GetNewestStoryIds()
        {
            return await GetAsync<IEnumerable<string>>(StoriesUrl());
        }
        private async Task<NewestStories> GetStoryDetailsByStoryId(string storyId)
        {
            return await GetAsync<NewestStories>(StoryDetailsUrl(storyId));
        }
        private string StoriesUrl()
        {
            return $"{uRL}/newstories.json?print=pretty";
        }
        private string StoryDetailsUrl(string StoryId)
        {
            return $"{uRL}/item/{StoryId}.json?print=pretty";
        }

    }
}
