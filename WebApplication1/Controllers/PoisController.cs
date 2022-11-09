using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AzureWebAppTest.Data;
using AzureWebAppTest.Models;

namespace AzureWebAppTest.Controllers
{
    [HandleError(View = "Error")]
    public class PoisController : Controller
    {
        private AzureWebAppTestContext db = new AzureWebAppTestContext();

        // GET: Pois
        public ActionResult Index()
        {
            var pois = db.Pois.ToList();
            /*var pois = new List<Poi>()
            {
                new Poi() { Id = 1, Category = Category.Peaks, CountryCode = "CZ", Name = "Lysa hora" },
                new Poi() { Id = 2, Category = Category.Peaks, CountryCode = "SK", Name = "Gerlachovsky stit" }
            };*/
            return View(pois);
        }

        // GET: Pois/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Poi poi = db.Pois.Find(id);
            if (poi == null)
            {
                return HttpNotFound();
            }
            return View(poi);
        }

        // GET: Pois/Create
        public ActionResult Create()
        {
            var poi = new Poi();
            return View(poi);
        }

        // POST: Pois/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Altitude,Latitude,Longitude,Category,CountryCode")] Poi poi)
        {
            if (ModelState.IsValid)
            {
                poi.AddedBy = this.User.Identity.Name;
                db.Pois.Add(poi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(poi);
        }

        // GET: Pois/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Poi poi = db.Pois.Find(id);
            if (poi == null)
            {
                return HttpNotFound();
            }

            if (poi.AddedBy != this.User.Identity.Name)
            {
                var msg = $"Modification is only allowed to user '{poi.AddedBy}'";
                throw new Exception(msg);
            }

            return View(poi);
        }

        // POST: Pois/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Altitude,Latitude,Longitude,Category,CountryCode,AddedBy")] Poi poi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(poi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(poi);
        }

        // GET: Pois/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Poi poi = db.Pois.Find(id);
            if (poi == null)
            {
                return HttpNotFound();
            }

            if (poi.AddedBy != this.User.Identity.Name)
            {
                var msg = $"Deleting is only allowed to user '{poi.AddedBy}'";
                throw new Exception(msg);
            }

            return View(poi);
        }

        // POST: Pois/Delete/5
        [HttpPost, ActionName("Delete")]
        //ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Poi poi = db.Pois.Find(id);
            db.Pois.Remove(poi);
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
