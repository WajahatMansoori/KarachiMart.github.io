using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication8.Models;

namespace WebApplication8.Controllers
{
    public class ProductsController : Controller
    {
        private EcommerceDBEntities db = new EcommerceDBEntities();

        // GET: Products
        [Authorize]
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category);
            return View(products.ToList());
        }

        [Authorize]
        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [Authorize]
        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CatID_FK = new SelectList(db.Categories, "Cat_Id", "Cat_Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "P_Id,P_Name,P_Price,P_Quantity,P_Image,CatID_FK")] Product product,HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                string path = UploadImage(ImageFile);
                if (path == "-1")
                {

                }
                else
                {
                    product.P_Image = path;
                    db.Products.Add(product);
                    db.SaveChanges();
                }

                //db.Products.Add(product);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CatID_FK = new SelectList(db.Categories, "Cat_Id", "Cat_Name", product.CatID_FK);
            return View(product);
        }

        [Authorize]
        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CatID_FK = new SelectList(db.Categories, "Cat_Id", "Cat_Name", product.CatID_FK);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "P_Id,P_Name,P_Price,P_Quantity,P_Image,CatID_FK")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CatID_FK = new SelectList(db.Categories, "Cat_Id", "Cat_Name", product.CatID_FK);
            return View(product);
        }

        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult Delete(int id)
        {
            if (id > 0)
            {
                var RowDel = db.Products.Where(model => model.P_Id == id).FirstOrDefault();
                {
                    if (RowDel != null)
                    {
                        db.Entry(RowDel).State = EntityState.Deleted;
                        db.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Index", "Products");
        }

        public string UploadImage(HttpPostedFileBase File)
        {
            Random r = new Random();
            string path = "-1";
            int random = r.Next();
            if (File != null && File.ContentLength > 0)
            {
                string extension = Path.GetExtension(File.FileName);
                if (extension.ToLower() == ".jpg" || extension.ToLower() == ".png" || extension.ToLower() == ".jpeg")
                {
                    try
                    {
                        path = Path.Combine(Server.MapPath("~/upload/"), random + Path.GetFileName(File.FileName));
                        File.SaveAs(path);
                        path = "~/upload/" + random + Path.GetFileName(File.FileName);
                    }
                    catch(Exception ex)
                    {
                        path = "-1";
                    }
                }
                else
                {
                    TempData["error"] = "<script>alert(''Incorrect File Format)</script>";
                }
            }
            else
            {
                TempData["EmptyFeild"] = "<script>alert('Please select a file')</script>";
                path = "-1";
            }
            return path;
        }
    }
}
