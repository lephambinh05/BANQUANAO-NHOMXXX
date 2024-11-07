using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        DBSportStoreEntities database = new DBSportStoreEntities();
         
        // GET: Product
        public ActionResult Index()
        {
            return View(database.Products.ToList());
        }
        public ActionResult Creat() 
        {
            Product pro = new Product();
            return View(pro);
        }
        public ActionResult SelectCate() 
        {
            Category se_cate = new Category();
            se_cate.listcate = database.Categories.ToList<Category>();
            return PartialView(se_cate);
        }
        [HttpPost]
        public ActionResult Create(Product pro)
        {
            try
            {
                if (pro.UploadImage != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(pro.UploadImage.FileName);
                    string extent = Path.GetExtension(pro.UploadImage.FileName);
                    filename = filename + extent;
                    pro.ImagePro = "~/Content/images/" + filename;
                    pro.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
                }
                database.Products.Add(pro);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}