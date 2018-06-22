using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Blogger.Models;
using Microsoft.AspNet.Identity;

namespace Blogger.Controllers
{
    public class AdminPanelPostController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AdminPanelPost
        public ActionResult Index()
        {
            var postDetails = db.PostDetails.Include(p => p.CategoryDetail);
            return View(postDetails.ToList());
        }

        // GET: AdminPanelPost/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostDetail postDetail = db.PostDetails.Find(id);
            if (postDetail == null)
            {
                return HttpNotFound();
            }
            return View(postDetail);
        }

        // GET: AdminPanelPost/Create
        public ActionResult Create()
        {
            PostDetail model = new PostDetail();
           
            model.CategoryDetails = db.Categories;
            return View(model);
        }

        // POST: AdminPanelPost/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create( PostDetail postDetail)
        {
            if (ModelState.IsValid)
            {
               
                if (postDetail.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(postDetail.ImageUpload.FileName);
                    string extension = Path.GetExtension(postDetail.ImageUpload.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    postDetail.FeaturedImage = "~/Content/Images/" + fileName;
                    postDetail.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/Images/"), fileName));
                }
                postDetail.Create_time = DateTime.Now;
                db.PostDetails.Add(postDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           

                
               

                ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", postDetail.CategoryId);
            return View(postDetail);
        }

        // GET: AdminPanelPost/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostDetail postDetail = db.PostDetails.Find(id);
            if (postDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", postDetail.CategoryId);
            return View(postDetail);
        }

        // POST: AdminPanelPost/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PostId,Title,Post_Content,Create_time,Tages,FeaturedImage,CategoryId,UserId")] PostDetail postDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(postDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", postDetail.CategoryId);
            return View(postDetail);
        }

        // GET: AdminPanelPost/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostDetail postDetail = db.PostDetails.Find(id);
            if (postDetail == null)
            {
                return HttpNotFound();
            }
            return View(postDetail);
        }

        // POST: AdminPanelPost/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PostDetail postDetail = db.PostDetails.Find(id);
            db.PostDetails.Remove(postDetail);
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
