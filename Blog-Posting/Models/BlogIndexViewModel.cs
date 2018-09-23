using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog_Posting.Models
{
    public class BlogIndexViewModel
    {
        public IEnumerable<BlogPost> AllPosts { get; set; }
        public IEnumerable<BlogPost> RecentPosts { get; set; }
    }
}