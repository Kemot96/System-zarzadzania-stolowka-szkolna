using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Projekt.Models;

namespace Projekt.Controllers
{
    [Authorize(Roles = "admin")]
    public class FirmaController : Controller
    {
        private StolowkaEntities db = new StolowkaEntities();

        // GET: Firma
        public ActionResult Index()
        {
            var firma = db.Firma.Include(f => f.Adres).Include(f => f.Kontakt);
            return View(firma.ToList());
        }

        // GET: Firma/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Firma firma = db.Firma.Find(id);
            if (firma == null)
            {
                return HttpNotFound();
            }
            return View(firma);
        }

        // GET: Firma/Create
        public ActionResult Create()
        {
            ViewBag.adres_id = new SelectList(db.Adres, "id", "miasto");
            ViewBag.kontakt_id = new SelectList(db.Kontakt, "id", "email");
            return View();
        }

        // POST: Firma/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nazwa,adres_id,kontakt_id")] Firma firma)
        {
            if (ModelState.IsValid)
            {
                db.Firma.Add(firma);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.adres_id = new SelectList(db.Adres, "id", "miasto", firma.adres_id);
            ViewBag.kontakt_id = new SelectList(db.Kontakt, "id", "email", firma.kontakt_id);
            return View(firma);
        }

        // GET: Firma/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Firma firma = db.Firma.Find(id);
            if (firma == null)
            {
                return HttpNotFound();
            }
            ViewBag.adres_id = new SelectList(db.Adres, "id", "miasto", firma.adres_id);
            ViewBag.kontakt_id = new SelectList(db.Kontakt, "id", "email", firma.kontakt_id);
            return View(firma);
        }

        // POST: Firma/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nazwa,adres_id,kontakt_id")] Firma firma)
        {
            if (ModelState.IsValid)
            {
                db.Entry(firma).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.adres_id = new SelectList(db.Adres, "id", "miasto", firma.adres_id);
            ViewBag.kontakt_id = new SelectList(db.Kontakt, "id", "email", firma.kontakt_id);
            return View(firma);
        }

        // GET: Firma/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Firma firma = db.Firma.Find(id);
            if (firma == null)
            {
                return HttpNotFound();
            }
            return View(firma);
        }

        // POST: Firma/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Firma firma = db.Firma.Find(id);
            db.Firma.Remove(firma);
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
