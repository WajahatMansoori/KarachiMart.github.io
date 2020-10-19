using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication8.Models;

namespace WebApplication8.Controllers
{
    public class OrderTablesController : Controller
    {
        private EcommerceDBEntities db = new EcommerceDBEntities();

        // GET: OrderTables
       [Authorize]
        public ActionResult Index()
        {
            var orderTables = db.OrderTables.Include(o => o.Customer);
            return View(orderTables.ToList());
        }

        [Authorize]
        // GET: OrderTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderTable orderTable = db.OrderTables.Find(id);
            if (orderTable == null)
            {
                return HttpNotFound();
            }
            return View(orderTable);
        }

        [Authorize]
        // GET: OrderTables/Create
        public ActionResult Create()
        {
            ViewBag.CusID_Fk = new SelectList(db.Customers, "Cus_Id", "Name");
            return View();
        }

        // POST: OrderTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Ord_Id,CusID_Fk,Ord_Date")] OrderTable orderTable)
        {
            if (ModelState.IsValid)
            {
                db.OrderTables.Add(orderTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CusID_Fk = new SelectList(db.Customers, "Cus_Id", "Name", orderTable.CusID_Fk);
            return View(orderTable);
        }

        [Authorize]
        // GET: OrderTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderTable orderTable = db.OrderTables.Find(id);
            if (orderTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.CusID_Fk = new SelectList(db.Customers, "Cus_Id", "Name", orderTable.CusID_Fk);
            return View(orderTable);
        }

        // POST: OrderTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Ord_Id,CusID_Fk,Ord_Date")] OrderTable orderTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CusID_Fk = new SelectList(db.Customers, "Cus_Id", "Name", orderTable.CusID_Fk);
            return View(orderTable);
        }

        // GET: OrderTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderTable orderTable = db.OrderTables.Find(id);
            if (orderTable == null)
            {
                return HttpNotFound();
            }
            return View(orderTable);
        }

        // POST: OrderTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderTable orderTable = db.OrderTables.Find(id);
            db.OrderTables.Remove(orderTable);
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
