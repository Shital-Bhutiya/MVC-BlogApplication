using Blog_Posting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog_Posting
{
    public class Comment
    {

        public int Id { get; set; }
        public int PostId { get; set; }
        public int BlogPostID { get; set; }
        public virtual BlogPost BlogPost { get; set; }
        public string AuthorId { get; set; }
        public virtual ApplicationUser Author { get; set; }
        public string Body { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Updated { get; set; }
        public string UpdateReason { get; set; }
    }
}