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
        public HackerNewsAPIService(HttpClient httpClient, IConfiguration configuration) 
        { 
            _httpClient = httpClient;
            _configuration = configuration;
            mainUri = _configuration["ExternalAPI:HackerNewsURI"];
        }


        /// <summary>
        /// GetStoriesIdsList() call the external api endpoint and fetches the storiesIds
        /// </summary>
        /// <returns>List of StoryIds</returns>
        public async Task<List<int>> GetStoriesIdsList()
        {
            var storiesIdsResponse = await _httpClient.GetAsync($"{mainUri}newstories.json?print=pretty");

            storiesIdsResponse.EnsureSuccessStatusCode();

            var storiesIdsString = await storiesIdsResponse.Content.ReadAsStringAsync();

            var storiesIdsList = JsonSerializer.Deserialize<List<int>>(storiesIdsString);
            return storiesIdsList;
        }

        /// <summary>
        /// GetStoryById(int storyId) call the external api and fetches the story response using the storyId
        /// </summary>
        /// <param name="storyId"></param>
        /// <returns>Returns a story object</returns>
        public async Task<Story> GetStoryById(int storyId)
        {
            var storyUri = $"{mainUri}item/{storyId}.json?print=pretty";
            var storyResponse = await _httpClient.GetAsync(storyUri);

            storyResponse.EnsureSuccessStatusCode();
            var storyString = await storyResponse.Content.ReadAsStringAsync();

            var story = JsonSerializer.Deserialize<Story>(storyString);

            return story;


        }
    }
}
