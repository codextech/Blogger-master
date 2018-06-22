using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blogger.Models;

namespace Blogger.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();

        }
        public ActionResult Index()
        {
            var allposts = _context.PostDetails.ToList();

            return View(allposts);
        }

       
        public ActionResult GetPostbyCategory(int categoryId)
        {

            var post = _context.PostDetails.Where(c => c.CategoryId == categoryId).ToList();
            return View("ViewAllPost",post);
        }
        public ActionResult GetDetails(int postId)
        {
            var post = _context.PostDetails.Single(p => p.PostId == postId);
            return View(post);
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}