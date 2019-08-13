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
    public class PracownikController : Controller
    {
        private StolowkaEntities db = new StolowkaEntities();

        // GET: Pracownik
        public ActionResult Index()
        {
            var pracownik = db.Pracownik.Include(p => p.Adres).Include(p => p.Kontakt).Include(p => p.AspNetUsers);
            return View(pracownik.ToList());
        }

        // GET: Pracownik/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pracownik pracownik = db.Pracownik.Find(id);
            if (pracownik == null)
            {
                return HttpNotFound();
            }
            return View(pracownik);
        }

        // GET: Pracownik/Create
        public ActionResult Create()
        {
            ViewBag.adres_id = new SelectList(db.Adres, "id", "miasto");
            ViewBag.kontakt_id = new SelectList(db.Kontakt, "id", "email");
            ViewBag.konto_id = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: Pracownik/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,imie,nazwisko,stanowisko,adres_id,kontakt_id,konto_id")] Pracownik pracownik)
        {
            if (ModelState.IsValid)
            {
                db.Pracownik.Add(pracownik);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.adres_id = new SelectList(db.Adres, "id", "miasto", pracownik.adres_id);
            ViewBag.kontakt_id = new SelectList(db.Kontakt, "id", "email", pracownik.kontakt_id);
            ViewBag.konto_id = new SelectList(db.AspNetUsers, "Id", "Email", pracownik.konto_id);
            return View(pracownik);
        }

        // GET: Pracownik/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pracownik pracownik = db.Pracownik.Find(id);
            if (pracownik == null)
            {
                return HttpNotFound();
            }
            ViewBag.adres_id = new SelectList(db.Adres, "id", "miasto", pracownik.adres_id);
            ViewBag.kontakt_id = new SelectList(db.Kontakt, "id", "email", pracownik.kontakt_id);
            ViewBag.konto_id = new SelectList(db.AspNetUsers, "Id", "Email", pracownik.konto_id);
            return View(pracownik);
        }

        // POST: Pracownik/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,imie,nazwisko,stanowisko,adres_id,kontakt_id,konto_id")] Pracownik pracownik)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pracownik).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.adres_id = new SelectList(db.Adres, "id", "miasto", pracownik.adres_id);
            ViewBag.kontakt_id = new SelectList(db.Kontakt, "id", "email", pracownik.kontakt_id);
            ViewBag.konto_id = new SelectList(db.AspNetUsers, "Id", "Email", pracownik.konto_id);
            return View(pracownik);
        }

        // GET: Pracownik/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pracownik pracownik = db.Pracownik.Find(id);
            if (pracownik == null)
            {
                return HttpNotFound();
            }
            return View(pracownik);
        }

        // POST: Pracownik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pracownik pracownik = db.Pracownik.Find(id);
            db.Pracownik.Remove(pracownik);
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
