using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoriesAPI.Services.Interfaces;
using System;
using System.Threading.Tasks;
using StoriesAPI.Models;
using StoriesAPI.Models.ViewModels;

namespace StoriesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoriesController : ControllerBase
    {
        private readonly IStoriesService _storiesService; 
        public StoriesController(IStoriesService storiesService)
        {
            _storiesService = storiesService;   
        }

        [HttpGet]
        [Route("getallstories/{pageNumber}/{pageSize}")]
        [ResponseCache(Duration=120, Location=ResponseCacheLocation.Any, NoStore=false)]
        public async Task<ActionResult<StoryListViewModel>> GetAllStories(int pageNumber, int pageSize) {
            try
            {
                var response = await _storiesService.GetStories(pageNumber, pageSize);
                return Ok(response);

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(ex.ToString());  
            }
        }

        //[HttpGet]
        //[Route("getstorybyid/{id}")]
        //public async Task<ActionResult<Story>> GetStoryById(int id)
        //{
        //    try
        //    {
        //        var response = await _storiesService.GetStoryById(id);
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //        return BadRequest(ex.ToString());
        //    }
        //}

    }
}
