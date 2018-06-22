using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Blogger.Models;
using Microsoft.AspNet.Identity;

namespace Blogger.Controllers
{
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _context;
      
        public PostController()
        {
            _context = new ApplicationDbContext();
            
        }







        public ActionResult Index()
        {
            return View();

        }





        public ActionResult Blogs()
        {

         var blogs =  _context.PostDetails.ToList();
            return View(blogs);

        }



       


        public ActionResult Details(int postId)
        {
            var post = _context.PostDetails.Single(p => p.PostId == postId);
           
            return View(post);

        }


        public ActionResult Edit(int postId)
        {
            var post = _context.PostDetails.Single(p=>p.PostId==postId);
            post.CategoryDetails = _context.Categories;
            return View(post);

        }
       



        
       
        // GET: Post
        //[Authorize(Roles = "canManagePost")]
        public ActionResult NewPost()
        {
            PostDetail model = new PostDetail();
            model.PostId = 0;//Becouse HttpPost NewPost, Need Id to know Edit or AddNew .
            model.CategoryDetails = _context.Categories;
            
            return View(model);

        }
        [ValidateInput(false)]
        [HttpPost]
        //[Authorize(Roles ="canManagePost")]
        public ActionResult NewPost(PostDetail data)
        {


            if (ModelState.IsValid)
            {

                var _CurrentUserId = User.Identity.GetUserId();
                if (_CurrentUserId == null)
                {
                    //becouse Sometime id = 0 ?????!!!! maybe session die???????
                    data.CategoryDetails = _context.Categories;
                    return View(data);
                }
                if (data.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(data.ImageUpload.FileName);
                    string extension = Path.GetExtension(data.ImageUpload.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    data.FeaturedImage = "~/Content/Images/" + fileName;
                    data.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/Images/"), fileName));
                }



                if (data.PostId == 0)
                {
                    var _post = new PostDetail(); //To get Post ID From AddPost to use it for Details
                    _post.Title = data.Title;
                    _post.Post_Content = data.Post_Content;
                    _post.FeaturedImage = data.FeaturedImage;
                    _post.Create_time=DateTime.Now;
                    _post.Tages = data.Tages;
                    _post.CategoryId = data.CategoryId;
                   
                    _context.PostDetails.Add(_post);
                    _context.SaveChanges();
                    data.PostId = _post.PostId;
                }
                else
                {
                    var dbEntry = _context.PostDetails.Find(data.PostId);
                    if (dbEntry != null)
                    {
                       
                        dbEntry.PostId = data.PostId;
                        dbEntry.Title = data.Title;
                        dbEntry.Post_Content = data.Post_Content;
                      
                        dbEntry.Tages = data.Tages;
                        dbEntry.CategoryId = data.CategoryId;
                       
                        _context.SaveChanges();

                    }
                }
                return RedirectToAction("Index", "Home");
            }
            data.CategoryDetails = _context.Categories;
            return View(data);
        }



        public ActionResult Contat()
        {
            return View();

        }


        public ActionResult About()
        {
            return View();

        }

    }
}