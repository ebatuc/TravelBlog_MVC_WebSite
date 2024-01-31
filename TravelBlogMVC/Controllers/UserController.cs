using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using TravelBlogMVC.Models.DataContext;
using TravelBlogMVC.Models.Model;

namespace TravelBlogMVC.Controllers
{
    public class UserController : Controller
    {
        TravelBlogDB db = new TravelBlogDB();
        // GET: User
        public ActionResult Index()
        {
            return View(db.User.ToList());
        }



        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            var user = db.User.Where(x => x.Id == id).SingleOrDefault();
            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, User user, HttpPostedFileBase ImgUrl)
        {
            if (ModelState.IsValid)
            {
                var usercheck = db.User.Where(x => x.Id == id).SingleOrDefault();
                if (ImgUrl != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(user.ImgUrl)))
                    {
                        System.IO.File.Delete(Server.MapPath(user.ImgUrl));
                    }
                    WebImage img = new WebImage(ImgUrl.InputStream);
                    FileInfo imgInfo = new FileInfo(ImgUrl.FileName);

                    string imgName = Guid.NewGuid().ToString() + imgInfo.Extension;
                    img.Resize(150, 150);
                    img.Save("~/pictures/userpictures/" + imgName);

                    usercheck.ImgUrl = "/pictures/userpictures/" + imgName;
                }
                usercheck.UserName = user.UserName;
                usercheck.Password = user.Password;
                usercheck.Email = user.Email;
                usercheck.Role = user.Role;
                usercheck.RegistrationDate = user.RegistrationDate;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
