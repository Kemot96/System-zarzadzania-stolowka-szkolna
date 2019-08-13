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
    public class OplataController : Controller
    {
        private StolowkaEntities db = new StolowkaEntities();

        // GET: Oplata
        public ActionResult Index()
        {
            var oplata = db.Oplata.Include(o => o.Miesiac).Include(o => o.Rok);
            return View(oplata.ToList());
        }

        // GET: Oplata/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Oplata oplata = db.Oplata.Find(id);
            if (oplata == null)
            {
                return HttpNotFound();
            }
            return View(oplata);
        }

        // GET: Oplata/Create
        public ActionResult Create()
        {
            ViewBag.miesiac_id = new SelectList(db.Miesiac, "id", "nazwa");
            ViewBag.rok_id = new SelectList(db.Rok, "id", "rok1");
            return View();
        }

        // POST: Oplata/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,miesiac_id,rok_id,stawka_za_dzien,dni_w_miesiacu")] Oplata oplata)
        {
            if (ModelState.IsValid)
            {
                db.Oplata.Add(oplata);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.miesiac_id = new SelectList(db.Miesiac, "id", "nazwa", oplata.miesiac_id);
            ViewBag.rok_id = new SelectList(db.Rok, "id", "rok1", oplata.rok_id);
            return View(oplata);
        }

        // GET: Oplata/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Oplata oplata = db.Oplata.Find(id);
            if (oplata == null)
            {
                return HttpNotFound();
            }
            ViewBag.miesiac_id = new SelectList(db.Miesiac, "id", "nazwa", oplata.miesiac_id);
            ViewBag.rok_id = new SelectList(db.Rok, "id", "rok1", oplata.rok_id);
            return View(oplata);
        }

        // POST: Oplata/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,miesiac_id,rok_id,stawka_za_dzien,dni_w_miesiacu")] Oplata oplata)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oplata).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.miesiac_id = new SelectList(db.Miesiac, "id", "nazwa", oplata.miesiac_id);
            ViewBag.rok_id = new SelectList(db.Rok, "id", "rok1", oplata.rok_id);
            return View(oplata);
        }

        // GET: Oplata/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Oplata oplata = db.Oplata.Find(id);
            if (oplata == null)
            {
                return HttpNotFound();
            }
            return View(oplata);
        }

        // POST: Oplata/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Oplata oplata = db.Oplata.Find(id);
            db.Oplata.Remove(oplata);
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
