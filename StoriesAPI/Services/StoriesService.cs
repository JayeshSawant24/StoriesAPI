using StoriesAPI.Models.ViewModels;
using StoriesAPI.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
using StoriesAPI.Services.Interfaces;

namespace StoriesAPI.Services
{
    public class StoriesService : IStoriesService
    {
        //private readonly HttpClient _httpClient;
        private readonly IHackerNewsAPIService _hackerNewsAPIService;
        public StoriesService(IHackerNewsAPIService hackerNewsAPIService)
        {
            _hackerNewsAPIService = hackerNewsAPIService;
        }
        public async Task<StoryListViewModel> GetStories(int pageNumber, int pageSize)
        {
            var storiesIdsList = await _hackerNewsAPIService.GetStoriesIdsList();

            if(storiesIdsList.Count == 0)
            {
                return null;
            }
            StoryListViewModel storyListViewModel = new StoryListViewModel();
            storyListViewModel.TotalStories = storiesIdsList.Count;


            List<Story> storiesList = new List<Story>();
            var filteredList = storiesIdsList.Skip((pageNumber * pageSize) - pageSize).Take(pageSize).ToList();

            foreach (var storyId in filteredList)
            {
                Story story = await _hackerNewsAPIService.GetStoryById(storyId);
                storiesList.Add(story);
            }

            storyListViewModel.StoriesList = storiesList;

            return storyListViewModel;
        }

        //public async Task<Story> GetStoryById(int id)
        //{
        //    var storyUri = mainUri + "item/" + id + "json?print=pretty";
        //    var storyResponse = await _httpClient.GetAsync(storyUri);

        //    storyResponse.EnsureSuccessStatusCode();
        //    var storyString = await storyResponse.Content.ReadAsStringAsync();

        //    var story = JsonSerializer.Deserialize<Story>(storyString);

        //    return story;


        //}
    }
}
