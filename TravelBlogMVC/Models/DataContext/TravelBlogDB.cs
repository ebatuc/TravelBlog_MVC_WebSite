using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TravelBlogMVC.Models.Model;

namespace TravelBlogMVC.Models.DataContext
{
    public class TravelBlogDB:DbContext
    {
        public TravelBlogDB():base("TravelBlogDB")
        {
            
        }
        public DbSet<User> User {  get; set; }
        public DbSet<BlogPosts> BlogPosts {  get; set; }
        public DbSet<Cities> Cities {  get; set; }
        public DbSet<Comments> Comments {  get; set; }
    }
}