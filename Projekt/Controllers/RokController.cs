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
    public class RokController : Controller
    {
        private StolowkaEntities db = new StolowkaEntities();

        // GET: Rok
        public ActionResult Index()
        {
            return View(db.Rok.ToList());
        }

        // GET: Rok/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rok rok = db.Rok.Find(id);
            if (rok == null)
            {
                return HttpNotFound();
            }
            return View(rok);
        }

        // GET: Rok/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rok/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,rok1")] Rok rok)
        {
            if (ModelState.IsValid)
            {
                db.Rok.Add(rok);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rok);
        }

        // GET: Rok/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rok rok = db.Rok.Find(id);
            if (rok == null)
            {
                return HttpNotFound();
            }
            return View(rok);
        }

        // POST: Rok/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,rok1")] Rok rok)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rok).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rok);
        }

        // GET: Rok/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rok rok = db.Rok.Find(id);
            if (rok == null)
            {
                return HttpNotFound();
            }
            return View(rok);
        }

        // POST: Rok/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rok rok = db.Rok.Find(id);
            db.Rok.Remove(rok);
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
