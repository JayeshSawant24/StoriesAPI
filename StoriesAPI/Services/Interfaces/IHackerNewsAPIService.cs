using StoriesAPI.Models;
using StoriesAPI.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoriesAPI.Services.Interfaces
{
    public interface IHackerNewsAPIService
    {
        Task<Story> GetStoryById(int id);

        Task<List<int>> GetStoriesIdsList();

    }
}
