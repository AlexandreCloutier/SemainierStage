using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SemainierStage.Models;
using SemainierStage.ViewModels;
using System.Collections;

namespace SemainierStage.Controllers
{
    public class EtudiantsController : Controller
    {
        private SemainierStageEntities db = new SemainierStageEntities();

        // GET: Etudiants
        public ActionResult Index()
        {
            EtudiantsIndexViewModel etudiantsIndexViewModel = new EtudiantsIndexViewModel
            {
                session = RetrouverToutesLesSessions()
            };
            return View(etudiantsIndexViewModel);
        }
        // postback-> afficher la liste des étudiants de la session passée en ID
        [HttpPost]
        public ActionResult Index(int? SessionId, int? EtudiantSelectionneId)
        {
            ModelState.Clear();
            EtudiantsIndexViewModel etudiantsIndexViewModel = new EtudiantsIndexViewModel
            {

                session = RetrouverToutesLesSessions(),
                etudiant = RetrouverEtudiantSelonSessions(SessionId),
                sessionID = SessionId,
                etudiantSelectionne = RetrouverEtudiantParId(EtudiantSelectionneId)
            };
            return View(etudiantsIndexViewModel);
        }
        // GET: Etudiants/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Etudiant etudiant = db.Etudiants.Find(id);
            if (etudiant == null)
            {
                return HttpNotFound();
            }
            return View(etudiant);
        }

        // GET: Etudiants/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Etudiants/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nom,Prenom,Email,User_Id")] Etudiant etudiant)
        {
            if (ModelState.IsValid)
            {
                db.Etudiants.Add(etudiant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(etudiant);
        }

        // GET: Etudiants/Edit/5
        public ActionResult EditEtudiant(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Etudiant etudiant = db.Etudiants.Find(id);
            if (etudiant == null)
            {
                return HttpNotFound();
            }
            return View(etudiant);
        }
        public ActionResult EditTache(int? id)
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
        // POST: Etudiants/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEtudiant([Bind(Include = "Id,Nom,Prenom,Email,User_Id")] Etudiant etudiant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(etudiant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(etudiant);
        }
        // POST: Taches/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTache([Bind(Include = "Id,Commentaire,Date,Etudiant_ID,NombreHeures")] Tache tache)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tache).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tache);
        }

        // GET: Etudiants/Delete/5
        public ActionResult DeleteEtudiant(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Etudiant etudiant = db.Etudiants.Find(id);
            if (etudiant == null)
            {
                return HttpNotFound();
            }
            return View(etudiant);
        }

        // POST: Etudiants/Delete/5
        [HttpPost, ActionName("DeleteEtudiant")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedEtudiant(int id)
        {
            Etudiant etudiant = db.Etudiants.Find(id);
            db.Etudiants.Remove(etudiant);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Taches/Delete/5
        public ActionResult DeleteTache(int? id)
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
        [HttpPost, ActionName("DeleteTache")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedTache(int id)
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

        protected IEnumerable<Session> RetrouverToutesLesSessions()
        {
            return db.Sessions;
        }
        protected IEnumerable<Etudiant> RetrouverEtudiantSelonSessions(int? id)
        {
            return (from e in db.EtudiantSessions
                    where e.Session_ID == id
                    select e.Etudiant).Distinct();
        }
        protected IEnumerable<Tache> RetrouverTachesDeLEtudiant(int? id)
        {
            return from t in db.Taches
                   where t.Etudiant_ID == id
                   orderby t.Date
                   select t; 
        }
        protected Etudiant RetrouverEtudiantParId(int? id)
        {
            return (from e in db.Etudiants
                    where e.Id == id
                    select e).FirstOrDefault();
        }
    }
}
