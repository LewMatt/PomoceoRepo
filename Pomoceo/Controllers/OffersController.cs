using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Pomoceo.Models;
using Microsoft.AspNet.Identity;

namespace Pomoceo.Controllers
{
    public class OffersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Offers
        public ActionResult Index(int CategoryID=-1,string searchString="", string sortOrder = "" )
        {
            var offers = db.Offers.ToList();
           
            switch (sortOrder)
            {
                case "oferowanie":
                    offers = offers.Where(p => p.Type =="oferowanie").ToList();
                    break;
                case "potrzeba":
                    offers = offers.Where(p => p.Type == "potrzeba").ToList();
                    break;
                default:
                    offers = offers.OrderBy(p => p.OfferID).ToList();
                    break;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                offers = offers.Where(p => p.City.Contains(searchString)).ToList();
            }
            if (CategoryID != -1)
            { 
                offers = offers.Where(p => p.CategoryID == CategoryID).ToList();    
            }

            PopulateCategoriesDropDownList();

            return View(offers);
        }

        // GET: Offers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offer offer = db.Offers.Find(id);
            if (offer == null)
            {
                return HttpNotFound();
            }
            return View(offer);
        }

        // GET: Offers/Create
        [Authorize]
        public ActionResult Create()
        {
            PopulateCategoriesDropDownList();
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryID,Description,Type,Title,City")] Offer offer)
        {

            
            if (ModelState.IsValid)
            {

                offer.UserID = User.Identity.GetUserId();
                db.Offers.Add(offer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            PopulateCategoriesDropDownList(offer.CategoryID);
            return View(offer);
        }

        // GET: Offers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offer offer = db.Offers.Find(id);
            if (offer == null)
            {
                return HttpNotFound();
            }
            PopulateCategoriesDropDownList();
            return View(offer);
        }

        // POST: Offers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryID,OfferID,Description,Type,Title,City")] Offer offer)
        {
            if (ModelState.IsValid)
            {

                //db.Entry(offer).State = EntityState.Modified;
                var of = db.Offers.Where(p => p.OfferID == offer.OfferID).FirstOrDefault();
                of = offer;
                 db.SaveChanges();
                return RedirectToAction("Index");
            }
            PopulateCategoriesDropDownList(offer.CategoryID);
            return View(offer);
        }

        // GET: Offers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offer offer = db.Offers.Find(id);
            if (offer == null)
            {
                return HttpNotFound();
            }
            return View(offer);
        }

        // POST: Offers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Offer offer = db.Offers.Find(id);
            db.Offers.Remove(offer);
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

        private void PopulateCategoriesDropDownList(object selectCategorie = null)
        {
            var catQuery = db.Categories.ToList();
            ViewBag.CategoryID = new SelectList(catQuery, "CategoryID", null, selectCategorie);
        }
    }
}
