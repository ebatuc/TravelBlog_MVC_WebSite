using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace TravelBlogMVC.Models.Model
{
    [Table("User")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string UserName { get; set; }
        [Required, StringLength(50)]
        public string Password { get; set; }
        [Required, StringLength(100)]
        public string Email { get; set; }
        [Required, StringLength(50)]
        public string Role { get; set; }
        [Required]
        public DateTime RegistrationDate { get; set; }
        public string ImgUrl { get; set; }

        public ICollection<BlogPosts> BlogPosts { get; set; }

    }
}