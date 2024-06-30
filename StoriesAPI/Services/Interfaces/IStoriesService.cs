using StoriesAPI.Models.ViewModels;
using StoriesAPI.Models;
using System.Threading.Tasks;

namespace StoriesAPI.Services.Interfaces
{
    public interface IStoriesService
    {
        Task<StoryListViewModel> GetStories(int pageNumber, int pageSize);
        //Task<Story> GetStoryById(int id);
    }
}
