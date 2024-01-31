using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TravelBlogMVC.Models.Model
{
    [Table("Comments")]
    public class Comments
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        [Required, StringLength(250, ErrorMessage = "Max. 250 char")]
        public string Comment { get; set; }

        public int BlogPostsId { get; set; }

        public BlogPosts blogPosts { get; set; }
        public DateTime dateTime { get; set; }
        public bool IsApproved { get; set; }
    }
}



