using StoriesAPI.Controllers;
using StoriesAPI.Services.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using StoriesAPI.Models.ViewModels;
using StoriesAPI.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StoriesAPI_Test.Controllers
{
    public class StoriesControllerTest
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
        public async Task GetAllStories_ShouldReturnStatusCode200OkWithStoryList()
        {
            int pageNumber = 1;
            int pageSize = 5;
            Mock<IStoriesService> mockStoriesService = new Mock<IStoriesService>();

            StoriesController storiesController = new StoriesController(mockStoriesService.Object);
            mockStoriesService.Setup(x => x.GetStories(pageNumber, pageSize)).ReturnsAsync(storyListViewModels);

            var result = await storiesController.GetAllStories(pageNumber, pageSize);
            var okbjectResult = (OkObjectResult)result.Result;
            var resultValues = (StoryListViewModel)okbjectResult.Value;


            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, okbjectResult.StatusCode);
            Assert.Equal(storyListViewModels.TotalStories, resultValues.TotalStories);
            Assert.Equal(storyListViewModels.StoriesList[0].title, resultValues.StoriesList[0].title);

        }
    }
}
