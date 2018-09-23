using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog_Posting.Models
{
    public class BlogPost
    {
        public BlogPost()
        {
            Created = DateTime.Now;
            Comments = new List<Comment>();
        }
        public int Id { get; set; }
        
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Updated { get; set; }
        [Required(ErrorMessage = "The Title field is required for Post.")]
        public string Title { get; set; }
        public string Slug { get; set; }

        [AllowHtml]
        public string Body { get; set; }
        public string  MediaUrl { get; set; }
        public  bool Published { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}