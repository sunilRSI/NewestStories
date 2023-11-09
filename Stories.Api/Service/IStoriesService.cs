using Stories.Api.Model;

namespace Stories.Api.Service
{
    public interface IStoriesService
    {
        public Task<IEnumerable<NewestStories>> GetNewestStories(int numberOfNewestStory);
    }
}
