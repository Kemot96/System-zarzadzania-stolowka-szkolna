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
    public class DzieckoController : Controller
    {
        private StolowkaEntities db = new StolowkaEntities();

        // GET: Dziecko
        public ActionResult Index()
        {
            var dziecko = db.Dziecko.Include(d => d.Adres).Include(d => d.AspNetUsers);
            return View(dziecko.ToList());
        }

        // GET: Dziecko/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dziecko dziecko = db.Dziecko.Find(id);
            if (dziecko == null)
            {
                return HttpNotFound();
            }
            return View(dziecko);
        }

        // GET: Dziecko/Create
        public ActionResult Create()
        {
            ViewBag.adres_id = new SelectList(db.Adres, "id", "miasto");
            ViewBag.konto_id = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: Dziecko/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,imie,nazwisko,klasa,adres_id,konto_id")] Dziecko dziecko)
        {
            if (ModelState.IsValid)
            {
                db.Dziecko.Add(dziecko);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.adres_id = new SelectList(db.Adres, "id", "miasto", dziecko.adres_id);
            ViewBag.konto_id = new SelectList(db.AspNetUsers, "Id", "Email", dziecko.konto_id);
            return View(dziecko);
        }

        // GET: Dziecko/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dziecko dziecko = db.Dziecko.Find(id);
            if (dziecko == null)
            {
                return HttpNotFound();
            }
            ViewBag.adres_id = new SelectList(db.Adres, "id", "miasto", dziecko.adres_id);
            ViewBag.konto_id = new SelectList(db.AspNetUsers, "Id", "Email", dziecko.konto_id);
            return View(dziecko);
        }

        // POST: Dziecko/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,imie,nazwisko,klasa,adres_id,konto_id")] Dziecko dziecko)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dziecko).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.adres_id = new SelectList(db.Adres, "id", "miasto", dziecko.adres_id);
            ViewBag.konto_id = new SelectList(db.AspNetUsers, "Id", "Email", dziecko.konto_id);
            return View(dziecko);
        }

        // GET: Dziecko/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dziecko dziecko = db.Dziecko.Find(id);
            if (dziecko == null)
            {
                return HttpNotFound();
            }
            return View(dziecko);
        }

        // POST: Dziecko/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dziecko dziecko = db.Dziecko.Find(id);
            db.Dziecko.Remove(dziecko);
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
