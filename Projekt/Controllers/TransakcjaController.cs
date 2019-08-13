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
    public class TransakcjaController : Controller
    {
        private StolowkaEntities db = new StolowkaEntities();

        // GET: Transakcja
        public ActionResult Index()
        {
            var transakcja = db.Transakcja.Include(t => t.Dziecko).Include(t => t.Oplata);
            return View(transakcja.ToList());
        }

        // GET: Transakcja/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transakcja transakcja = db.Transakcja.Find(id);
            if (transakcja == null)
            {
                return HttpNotFound();
            }
            return View(transakcja);
        }

        // GET: Transakcja/Create
        public ActionResult Create()
        {
            ViewBag.dziecko_id = new SelectList(db.Dziecko, "id", "imie");
            ViewBag.oplata_id = new SelectList(db.Oplata, "id", "id");
            return View();
        }

        // POST: Transakcja/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,dziecko_id,oplata_id,kwota,data_zaplaty")] Transakcja transakcja)
        {
            if (ModelState.IsValid)
            {
                db.Transakcja.Add(transakcja);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.dziecko_id = new SelectList(db.Dziecko, "id", "imie", transakcja.dziecko_id);
            ViewBag.oplata_id = new SelectList(db.Oplata, "id", "id", transakcja.oplata_id);
            return View(transakcja);
        }

        // GET: Transakcja/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transakcja transakcja = db.Transakcja.Find(id);
            if (transakcja == null)
            {
                return HttpNotFound();
            }
            ViewBag.dziecko_id = new SelectList(db.Dziecko, "id", "imie", transakcja.dziecko_id);
            ViewBag.oplata_id = new SelectList(db.Oplata, "id", "id", transakcja.oplata_id);
            return View(transakcja);
        }

        // POST: Transakcja/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,dziecko_id,oplata_id,kwota,data_zaplaty")] Transakcja transakcja)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transakcja).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.dziecko_id = new SelectList(db.Dziecko, "id", "imie", transakcja.dziecko_id);
            ViewBag.oplata_id = new SelectList(db.Oplata, "id", "id", transakcja.oplata_id);
            return View(transakcja);
        }

        // GET: Transakcja/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transakcja transakcja = db.Transakcja.Find(id);
            if (transakcja == null)
            {
                return HttpNotFound();
            }
            return View(transakcja);
        }

        // POST: Transakcja/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transakcja transakcja = db.Transakcja.Find(id);
            db.Transakcja.Remove(transakcja);
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
