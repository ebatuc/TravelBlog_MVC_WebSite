using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using TravelBlogMVC.Models;
using TravelBlogMVC.Models.DataContext;
using TravelBlogMVC.Models.Model;

namespace TravelBlogMVC.Controllers
{
    public class AdminController : Controller
    {
        TravelBlogDB db = new TravelBlogDB();
        // GET: Admin
        public ActionResult Index()
        {
            var cities = db.Cities.ToList();

            return View(cities);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            var login = db.User.Where(x => x.Email == user.Email).SingleOrDefault();
            if (login != null && login.Email == user.Email && login.Password == user.Password)
            {
                Session["userid"] = login.Id;
                Session["email"] = login.Email;
                Session["username"] = login.UserName;
                Session["role"] = login.Role;
                Session["ImgUrl"] = login.ImgUrl;
                return RedirectToAction("Index", "Admin");
            }
           
                ViewBag.error = "User Email or Password is wrong";
                return View(user);
            
           

        }
        public ActionResult Logout()
        {
            Session["userid"] = null;
            Session["email"] = null;
            Session["username"] = null;
            Session["role"] = null;
            Session["ImgUrl"] = null;
            Session.Abandon();
            return RedirectToAction("Login", "Admin");

        }
    }
}