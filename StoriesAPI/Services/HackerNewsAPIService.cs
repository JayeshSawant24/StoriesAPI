using StoriesAPI.Services.Interfaces;
using System.Threading.Tasks;
using System.Net.Http;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json;
using StoriesAPI.Models;
using static System.Net.WebRequestMethods;
using StoriesAPI.Models.ViewModels;
using Microsoft.Extensions.Configuration;

namespace StoriesAPI.Services
{
    public class HackerNewsAPIService : IHackerNewsAPIService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        string mainUri;
        //string uri = "https://hacker-news.firebaseio.com/v0/newstories.json?print=pretty";
        //string storyUri = string.Format("https://hacker-news.firebaseio.com/v0/item/{0}.json?print=pretty",);
        public HackerNewsAPIService(HttpClient httpClient, IConfiguration configuration) 
        { 
            _httpClient = httpClient;
            _configuration = configuration;
            mainUri = _configuration["ExternalAPI:HackerNewsURI"];
        }



        public async Task<List<int>> GetStoriesIdsList()
        {
            var storiesIdsResponse = await _httpClient.GetAsync($"{mainUri}newstories.json?print=pretty");

            storiesIdsResponse.EnsureSuccessStatusCode();

            var storiesIdsString = await storiesIdsResponse.Content.ReadAsStringAsync();

            var storiesIdsList = JsonSerializer.Deserialize<List<int>>(storiesIdsString);
            return storiesIdsList;
        }

        public async Task<Story> GetStoryById(int storyId)
        {
            var storyUri = $"{mainUri}item/{storyId}.json?print=pretty";
            var storyResponse = await _httpClient.GetAsync(storyUri);

            storyResponse.EnsureSuccessStatusCode();
            var storyString = await storyResponse.Content.ReadAsStringAsync();

            var story = JsonSerializer.Deserialize<Story>(storyString);

            return story;


        }


        //public async Task<StoryListViewModel> GetStories(int pageNumber, int pageSize)
        //{
        //    //var storiesIdsResponse = await _httpClient.GetAsync($"{mainUri}newstories.json?print=pretty");

        //    //storiesIdsResponse.EnsureSuccessStatusCode();

        //    //var storiesIdsString = await storiesIdsResponse.Content.ReadAsStringAsync();

        //    //var storiesIdsList = JsonSerializer.Deserialize<List<int>>(storiesIdsString);

        //    var storiesIdsList = await GetStoriesIdsList();

        //    StoryListViewModel storyListViewModel = new StoryListViewModel();
        //    storyListViewModel.TotalStories = storiesIdsList.Count;


        //    List<Story> storiesList = new List<Story>();
        //    var filteredList = storiesIdsList.Skip((pageNumber * pageSize) - pageSize).Take(pageSize).ToList();

        //    foreach(var storyId in filteredList)
        //    {
        //        //var storyResponse = await _httpClient.GetAsync($"{mainUri}item/{storyId}.json?print=pretty");
        //        //var storyString = await storyResponse.Content.ReadAsStringAsync();
        //        //Story story = JsonSerializer.Deserialize<Story>(storyString);
        //        Story story = await GetStoryById(storyId);
        //        storiesList.Add(story);
        //    }

        //    storyListViewModel.StoriesList = storiesList;

        //    return storyListViewModel;
        //}

    }
}
