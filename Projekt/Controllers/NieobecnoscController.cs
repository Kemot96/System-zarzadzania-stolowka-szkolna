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
    public class NieobecnoscController : Controller
    {
        private StolowkaEntities db = new StolowkaEntities();

        // GET: Nieobecnosc
        public ActionResult Index()
        {
            var nieobecnosc = db.Nieobecnosc.Include(n => n.Dziecko);
            return View(nieobecnosc.ToList());
        }

        // GET: Nieobecnosc/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nieobecnosc nieobecnosc = db.Nieobecnosc.Find(id);
            if (nieobecnosc == null)
            {
                return HttpNotFound();
            }
            return View(nieobecnosc);
        }

        // GET: Nieobecnosc/Create
        public ActionResult Create()
        {
            ViewBag.dziecko_id = new SelectList(db.Dziecko, "id", "imie");
            return View();
        }

        // POST: Nieobecnosc/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,od,do,dziecko_id")] Nieobecnosc nieobecnosc)
        {
            if (ModelState.IsValid)
            {
                db.Nieobecnosc.Add(nieobecnosc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.dziecko_id = new SelectList(db.Dziecko, "id", "imie", nieobecnosc.dziecko_id);
            return View(nieobecnosc);
        }

        // GET: Nieobecnosc/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nieobecnosc nieobecnosc = db.Nieobecnosc.Find(id);
            if (nieobecnosc == null)
            {
                return HttpNotFound();
            }
            ViewBag.dziecko_id = new SelectList(db.Dziecko, "id", "imie", nieobecnosc.dziecko_id);
            return View(nieobecnosc);
        }

        // POST: Nieobecnosc/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,od,do,dziecko_id")] Nieobecnosc nieobecnosc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nieobecnosc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.dziecko_id = new SelectList(db.Dziecko, "id", "imie", nieobecnosc.dziecko_id);
            return View(nieobecnosc);
        }

        // GET: Nieobecnosc/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nieobecnosc nieobecnosc = db.Nieobecnosc.Find(id);
            if (nieobecnosc == null)
            {
                return HttpNotFound();
            }
            return View(nieobecnosc);
        }

        // POST: Nieobecnosc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Nieobecnosc nieobecnosc = db.Nieobecnosc.Find(id);
            db.Nieobecnosc.Remove(nieobecnosc);
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
