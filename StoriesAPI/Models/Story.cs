using System.Collections.Generic;

namespace StoriesAPI.Models
{
    public class Story
    {
        public string by { get; set; }
        public int descendants { get; set; }
        public int id { get; set; }
        public int score { get; set; }
        public long time { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public string url { get; set; }

    }
}
