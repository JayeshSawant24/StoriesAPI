using System.Collections.Generic;

namespace StoriesAPI.Models.ViewModels
{
    public class StoryListViewModel
    {
        public List<Story> StoriesList { get; set; }
        public int TotalStories { get; set; }
    }
}
