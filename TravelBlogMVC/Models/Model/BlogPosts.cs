using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TravelBlogMVC.Models.Model
{
    [Table("BlogPosts")]
    public class BlogPosts
    {

        [Key]
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public string ResimURL { get; set; }
        [Required]
        public DateTime DatePosted { get; set; }
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
        [Required]
        public int CityId { get; set; }
        public Cities City { get; set; }
        public  ICollection<Comments> Comments { get; set; }

    }
}