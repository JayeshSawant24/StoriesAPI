using StoriesAPI.Models.ViewModels;
using StoriesAPI.Models;
using System;
using System.Collections.Generic;
using Xunit;
using System.Text;
using Moq;
using StoriesAPI.Services.Interfaces;
using StoriesAPI.Services;

namespace StoriesAPI_Test.Services
{
    public class StoriesServiceTest
    {
        List<Story> stories = new List<Story>()
        {
            new Story { title = "Test Title", url="www.testurl.com"},
            new Story { title = "Test Title 1", url="www.test1url.com"},
        };

        StoryListViewModel storyListViewModels = new StoryListViewModel()
        {
            StoriesList = new List<Story>()
                        {
                            new Story { title = "Test Title", url="www.testurl.com"},
                            new Story { title = "Test Title 1", url="www.test1url.com"},
                        },
            TotalStories = 2
        };

        [Fact]
        public void GetStories_ShouldReturnStoryListViewModel()
        {
            Mock<IHackerNewsAPIService> mockHackerNewsAPIService = new Mock<IHackerNewsAPIService>();

            StoriesService storyService = new StoriesService(mockHackerNewsAPIService.Object);
            mockHackerNewsAPIService.Setup(x => x.GetStoriesIdsList()).ReturnsAsync(new List<int>() { 1, 2 });
            mockHackerNewsAPIService.Setup(x => x.GetStoryById(It.IsAny<int>())).ReturnsAsync(storyListViewModels.StoriesList[0]);

            int pageNumber = 1;
            int pageSize = 5;
            var result = storyService.GetStories(pageNumber, pageSize);

            Assert.Equal(storyListViewModels.TotalStories, result.Result.TotalStories);
            Assert.Equal(storyListViewModels.StoriesList[0].title, result.Result.StoriesList[0].title);
        }

        [Fact]
        public void GetStories_ShouldReturnNull_WhenThereAreNoStories()
        {
            Mock<IHackerNewsAPIService> mockHackerNewsAPIService = new Mock<IHackerNewsAPIService>();

            StoriesService storyService = new StoriesService(mockHackerNewsAPIService.Object);
            mockHackerNewsAPIService.Setup(x => x.GetStoriesIdsList()).ReturnsAsync(new List<int>() { });
            mockHackerNewsAPIService.Setup(x => x.GetStoryById(It.IsAny<int>())).ReturnsAsync(storyListViewModels.StoriesList[0]);

            int pageNumber = 1;
            int pageSize = 5;
            var result = storyService.GetStories(pageNumber, pageSize);

            Assert.Null(result.Result);
        }
    }
}
