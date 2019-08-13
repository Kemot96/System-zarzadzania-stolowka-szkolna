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
    public class MiesiacController : Controller
    {
        private StolowkaEntities db = new StolowkaEntities();

        // GET: Miesiac
        public ActionResult Index()
        {
            return View(db.Miesiac.ToList());
        }

        // GET: Miesiac/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Miesiac miesiac = db.Miesiac.Find(id);
            if (miesiac == null)
            {
                return HttpNotFound();
            }
            return View(miesiac);
        }

        // GET: Miesiac/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Miesiac/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nazwa")] Miesiac miesiac)
        {
            if (ModelState.IsValid)
            {
                db.Miesiac.Add(miesiac);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(miesiac);
        }

        // GET: Miesiac/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Miesiac miesiac = db.Miesiac.Find(id);
            if (miesiac == null)
            {
                return HttpNotFound();
            }
            return View(miesiac);
        }

        // POST: Miesiac/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nazwa")] Miesiac miesiac)
        {
            if (ModelState.IsValid)
            {
                db.Entry(miesiac).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(miesiac);
        }

        // GET: Miesiac/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Miesiac miesiac = db.Miesiac.Find(id);
            if (miesiac == null)
            {
                return HttpNotFound();
            }
            return View(miesiac);
        }

        // POST: Miesiac/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Miesiac miesiac = db.Miesiac.Find(id);
            db.Miesiac.Remove(miesiac);
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
