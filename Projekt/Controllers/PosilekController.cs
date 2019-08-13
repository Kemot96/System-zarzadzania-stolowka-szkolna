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
    public class PosilekController : Controller
    {
        private StolowkaEntities db = new StolowkaEntities();

        // GET: Posilek
        public ActionResult Index()
        {
            return View(db.Posilek.ToList());
        }

        // GET: Posilek/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Posilek posilek = db.Posilek.Find(id);
            if (posilek == null)
            {
                return HttpNotFound();
            }
            return View(posilek);
        }

        // GET: Posilek/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Posilek/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nazwa,waga")] Posilek posilek)
        {
            if (ModelState.IsValid)
            {
                db.Posilek.Add(posilek);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(posilek);
        }

        // GET: Posilek/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Posilek posilek = db.Posilek.Find(id);
            if (posilek == null)
            {
                return HttpNotFound();
            }
            return View(posilek);
        }

        // POST: Posilek/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nazwa,waga")] Posilek posilek)
        {
            if (ModelState.IsValid)
            {
                db.Entry(posilek).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(posilek);
        }

        // GET: Posilek/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Posilek posilek = db.Posilek.Find(id);
            if (posilek == null)
            {
                return HttpNotFound();
            }
            return View(posilek);
        }

        // POST: Posilek/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Posilek posilek = db.Posilek.Find(id);
            db.Posilek.Remove(posilek);
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
