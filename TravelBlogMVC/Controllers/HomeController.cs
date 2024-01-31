using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Mvc;
using TravelBlogMVC.Models.DataContext;
using PagedList;
using PagedList.Mvc;
using System.Web.Helpers;
using TravelBlogMVC.Models.Model;

namespace TravelBlogMVC.Controllers
{

    public class HomeController : Controller
    {
        private TravelBlogDB db = new TravelBlogDB();
    
        public ActionResult Index( int page = 1)
        {
            var pagedResult = db.BlogPosts.Include("City").OrderByDescending(x => x.Id).ToList().ToPagedList(page, 3);

            return View(pagedResult);
        }

        public ActionResult citiesBlog(int id)
        {
            var blog=db.BlogPosts.Include("City").Where(x=>x.City.Id==id).ToList();
            return View(blog);
        }
        public ActionResult IndexPartial()
        {
            return PartialView(db.Cities.Include("BlogPosts").ToList().OrderBy(x => x.CityName));
        }
        public ActionResult blogPartial()
        {
            return PartialView(db.BlogPosts.OrderByDescending(x => x.Id).Take(3).ToList());
        }

        public ActionResult blogDetail(int id)
        {
            var blogId = db.BlogPosts.Include("City").Include("Comments").Where(x => x.Id == id).SingleOrDefault();
            return View(blogId);
        }

        //comment area 
        public JsonResult submitComment(string name, string email, string comment, int blogId)
        {
            if ((comment == null))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            db.Comments.Add(new Comments { Name = name, Email = email, Comment = comment, BlogPostsId = blogId, IsApproved = false, dateTime = DateTime.Now });
            db.SaveChanges();
            return Json(false, JsonRequestBehavior.AllowGet);
        }


    }
}