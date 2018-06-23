using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace WebApplication5.Models
{
    [Authorize(Roles = "admin")]
    public class firstPagesController : Controller
    {
        
        private projectEntities4 db = new projectEntities4();

        // GET: firstPages
     
        public ActionResult Index()
        {
            return View(db.firstPages.ToList());
        }

        // GET: firstPages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            firstPage firstPage = db.firstPages.Find(id);
            if (firstPage == null)
            {
                return HttpNotFound();
            }
            return View(firstPage);
        }

        // GET: firstPages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: firstPages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,src,description,price")] firstPage firstPage)
        {
            if (ModelState.IsValid)
            {
                db.firstPages.Add(firstPage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(firstPage);
        }

        // GET: firstPages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            firstPage firstPage = db.firstPages.Find(id);
            if (firstPage == null)
            {
                return HttpNotFound();
            }
            return View(firstPage);
        }

        // POST: firstPages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,src,description,price")] firstPage firstPage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(firstPage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(firstPage);
        }

        // GET: firstPages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            firstPage firstPage = db.firstPages.Find(id);
            if (firstPage == null)
            {
                return HttpNotFound();
            }
            return View(firstPage);
        }

        // POST: firstPages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            firstPage firstPage = db.firstPages.Find(id);
            db.firstPages.Remove(firstPage);
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
