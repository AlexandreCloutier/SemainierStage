using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SemainierStage.Models;
using System.Diagnostics;
using Microsoft.AspNet.Identity;

namespace SemainierStage.Controllers
{
    public class TachesController : Controller
    {
        private SemainierStageEntities db = new SemainierStageEntities();

        // GET: Taches
        public ActionResult Index()
        {
            return View();
        }

        //POST: Taches
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Index(DateTime? datepickerInput)
        {
            if (datepickerInput != null)
            {
                IEnumerable<Tache> tache = db.Taches.Where(t => t.Date == datepickerInput);
                ViewBag.Date = datepickerInput;
                return View(tache);
            }
            else
            {
                return View();
            }
        }

        // GET: Taches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tache tache = db.Taches.Find(id);
            if (tache == null)
            {
                return HttpNotFound();
            }
            return View(tache);
        }

        // GET: Taches/Create
        public ActionResult Create(DateTime? date)
        {
            if (date == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else {
                string user = User.Identity.GetUserId();
                Tache tache = new Tache(); ;
                tache.Etudiant_ID = db.Etudiants.Where(e => e.User_Id == user).Select(e => e.Id).First();
                tache.Date = (DateTime)date;
                return View(tache);
            }
        }

        // POST: Taches/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Commentaire,Date,Etudiant_ID,NombreHeures")] Tache tache)
        {
            if (ModelState.IsValid)
            {
                db.Taches.Add(tache);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Etudiant_ID = new SelectList(db.Etudiants, "Id", "Nom", tache.Etudiant_ID);
            return View(tache);
        }

        // GET: Taches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tache tache = db.Taches.Find(id);
            if (tache == null)
            {
                return HttpNotFound();
            }
            ViewBag.Etudiant_ID = new SelectList(db.Etudiants, "Id", "Nom", tache.Etudiant_ID);
            return View(tache);
        }

        // POST: Taches/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Commentaire,Date,Etudiant_ID,NombreHeures")] Tache tache)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tache).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Etudiant_ID = new SelectList(db.Etudiants, "Id", "Nom", tache.Etudiant_ID);
            return View(tache);
        }

        // GET: Taches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tache tache = db.Taches.Find(id);
            if (tache == null)
            {
                return HttpNotFound();
            }
            return View(tache);
        }

        // POST: Taches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tache tache = db.Taches.Find(id);
            db.Taches.Remove(tache);
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
