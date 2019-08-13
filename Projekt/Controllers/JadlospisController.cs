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
    public class JadlospisController : Controller
    {
        private StolowkaEntities db = new StolowkaEntities();

        // GET: Jadlospis
        public ActionResult Index()
        {
            var jadlospis = db.Jadlospis.Include(j => j.Posilek).Include(j => j.Posilek1).Include(j => j.Posilek2).Include(j => j.Posilek3).Include(j => j.Posilek4);
            return View(jadlospis.ToList());
        }

        // GET: Jadlospis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jadlospis jadlospis = db.Jadlospis.Find(id);
            if (jadlospis == null)
            {
                return HttpNotFound();
            }
            return View(jadlospis);
        }

        // GET: Jadlospis/Create
        public ActionResult Create()
        {
            ViewBag.czwartek = new SelectList(db.Posilek, "id", "nazwa");
            ViewBag.piatek = new SelectList(db.Posilek, "id", "nazwa");
            ViewBag.poniedzialek = new SelectList(db.Posilek, "id", "nazwa");
            ViewBag.sroda = new SelectList(db.Posilek, "id", "nazwa");
            ViewBag.wtorek = new SelectList(db.Posilek, "id", "nazwa");
            return View();
        }

        // POST: Jadlospis/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,od,do,poniedzialek,wtorek,sroda,czwartek,piatek")] Jadlospis jadlospis)
        {
            if (ModelState.IsValid)
            {
                db.Jadlospis.Add(jadlospis);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.czwartek = new SelectList(db.Posilek, "id", "nazwa", jadlospis.czwartek);
            ViewBag.piatek = new SelectList(db.Posilek, "id", "nazwa", jadlospis.piatek);
            ViewBag.poniedzialek = new SelectList(db.Posilek, "id", "nazwa", jadlospis.poniedzialek);
            ViewBag.sroda = new SelectList(db.Posilek, "id", "nazwa", jadlospis.sroda);
            ViewBag.wtorek = new SelectList(db.Posilek, "id", "nazwa", jadlospis.wtorek);
            return View(jadlospis);
        }

        // GET: Jadlospis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jadlospis jadlospis = db.Jadlospis.Find(id);
            if (jadlospis == null)
            {
                return HttpNotFound();
            }
            ViewBag.czwartek = new SelectList(db.Posilek, "id", "nazwa", jadlospis.czwartek);
            ViewBag.piatek = new SelectList(db.Posilek, "id", "nazwa", jadlospis.piatek);
            ViewBag.poniedzialek = new SelectList(db.Posilek, "id", "nazwa", jadlospis.poniedzialek);
            ViewBag.sroda = new SelectList(db.Posilek, "id", "nazwa", jadlospis.sroda);
            ViewBag.wtorek = new SelectList(db.Posilek, "id", "nazwa", jadlospis.wtorek);
            return View(jadlospis);
        }

        // POST: Jadlospis/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,od,do,poniedzialek,wtorek,sroda,czwartek,piatek")] Jadlospis jadlospis)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jadlospis).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.czwartek = new SelectList(db.Posilek, "id", "nazwa", jadlospis.czwartek);
            ViewBag.piatek = new SelectList(db.Posilek, "id", "nazwa", jadlospis.piatek);
            ViewBag.poniedzialek = new SelectList(db.Posilek, "id", "nazwa", jadlospis.poniedzialek);
            ViewBag.sroda = new SelectList(db.Posilek, "id", "nazwa", jadlospis.sroda);
            ViewBag.wtorek = new SelectList(db.Posilek, "id", "nazwa", jadlospis.wtorek);
            return View(jadlospis);
        }

        // GET: Jadlospis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jadlospis jadlospis = db.Jadlospis.Find(id);
            if (jadlospis == null)
            {
                return HttpNotFound();
            }
            return View(jadlospis);
        }

        // POST: Jadlospis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Jadlospis jadlospis = db.Jadlospis.Find(id);
            db.Jadlospis.Remove(jadlospis);
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
