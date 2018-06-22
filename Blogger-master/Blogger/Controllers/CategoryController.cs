using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blogger.Models;

namespace Blogger.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController()
        {
            _context = new ApplicationDbContext();

        }
        // GET: Category
        public PartialViewResult CategoryPartial()
        {
           var categories= _context.Categories.OrderBy(c=>c.CategoryName).ToList();
            return PartialView(categories);
        }


        public ActionResult Index()
        {
            var categories = _context.Categories.OrderBy(c => c.CategoryName).ToList();
            return View(categories);
        }
        //get
        public ActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category cat)
        {
            var dbentry = _context.Categories.Find(cat.CategoryId);

            if (dbentry == null)
            {
                _context.Categories.Add(cat);
                _context.SaveChanges();
            }
            else
                _context.Entry(cat).State = EntityState.Modified;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        //get/edit
        public ActionResult Edit(int id)
        {
            var dbentry = _context.Categories.Find(id);
            if (dbentry==null)
            {
                return HttpNotFound();
            }

            return View(dbentry);
        }

    }
}