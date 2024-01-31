using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TravelBlogMVC.Models.Model
{
    [Table("Cities")]
    public class Cities
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string CityName { get; set; }
        public ICollection<BlogPosts> BlogPosts { get; set; }
    }
}