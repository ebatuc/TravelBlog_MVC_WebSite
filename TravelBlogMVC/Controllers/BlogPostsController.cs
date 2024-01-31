using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using TravelBlogMVC.Models.DataContext;
using TravelBlogMVC.Models.Model;

namespace TravelBlogMVC.Controllers
{
    public class BlogPostsController : Controller
    {
        private TravelBlogDB db = new TravelBlogDB();

        // GET: BlogPosts
        public ActionResult Index()
        {
            var blogPosts = db.BlogPosts.Include(b => b.City).Include(b => b.User);
            return View(blogPosts.ToList());
        }

        // GET: BlogPosts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPosts blogPosts = db.BlogPosts.Find(id);
            if (blogPosts == null)
            {
                return HttpNotFound();
            }
            return View(blogPosts);
        }

        // GET: BlogPosts/Create
        public ActionResult Create()
        {

            ViewBag.CityId = new SelectList(db.Cities, "Id", "CityName");
            ViewBag.UserId = new SelectList(db.User, "Id", "UserName");
            BlogPosts newBlogPost = new BlogPosts
            {
                DatePosted = DateTime.Now
            };
            return View(newBlogPost);
        }

        // POST: BlogPosts/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(BlogPosts blogPosts, HttpPostedFileBase ResimURL)
        {


            if (ModelState.IsValid)
            {
                if (ResimURL != null && ResimURL.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(ResimURL.FileName);
                    var path = Server.MapPath("/pictures/blogpictures/");
                    var lastpath = Path.Combine(path, fileName);
                    ResimURL.SaveAs(lastpath);
                    //blogPosts.ResimURL = "/pictures/blogpictures/" + fileName;
                }
                db.BlogPosts.Add(blogPosts);
                db.SaveChanges();   
                return RedirectToAction("Index");
            }
            return View(blogPosts);
        
        //ViewBag.CityId = new SelectList(db.Cities, "Id", "CityName", blogPosts.CityId);
        //    ViewBag.UserId = new SelectList(db.User, "Id", "UserName", blogPosts.UserId);
        }

        // GET: BlogPosts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPosts blogPosts = db.BlogPosts.Find(id);
            if (blogPosts == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityId = new SelectList(db.Cities, "Id", "CityName", blogPosts.CityId);
            ViewBag.UserId = new SelectList(db.User, "Id", "UserName", blogPosts.UserId);
            blogPosts.DatePosted = DateTime.Now;
            return View(blogPosts);
        }

        // POST: BlogPosts/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Id,Title,Content,DatePosted,ResimURL,UserId,CityId")] BlogPosts blogPosts, HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {

                if (ResimURL != null)
                {
                    string resimName = Guid.NewGuid().ToString() + Path.GetFullPath(ResimURL.FileName);

                    string path = Path.Combine(Server.MapPath("~/pictures/blogpictures/"), resimName);

                    ResimURL.SaveAs(path);

                    blogPosts.ResimURL = "~/pictures/blogpictures/" + resimName;
                }

                // Diğer güncellemeleri yap
                blogPosts.DatePosted = DateTime.Now;
                db.Entry(blogPosts).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");


            };

            ViewBag.CityId = new SelectList(db.Cities, "Id", "CityName", blogPosts.CityId);
            ViewBag.UserId = new SelectList(db.User, "Id", "UserName", blogPosts.UserId);

            return View(blogPosts);
        }






        // GET: BlogPosts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPosts blogPosts = db.BlogPosts.Find(id);
            if (blogPosts == null)
            {
                return HttpNotFound();
            }
            return View(blogPosts);
        }

        // POST: BlogPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogPosts blogPosts = db.BlogPosts.Find(id);
            db.BlogPosts.Remove(blogPosts);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
