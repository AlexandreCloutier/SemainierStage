using SemainierStage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SemainierStage.ViewModels
{
    public class EtudiantsIndexViewModel
    {
        public IEnumerable<Session> session { get; set; }
        public IEnumerable<Etudiant> etudiant { get; set; }
        public Etudiant etudiantSelectionne { get; set; }
        public IEnumerable<Tache> tacheDeLEtudiant { get; set; }

        public int? sessionID { get; set; }
    }
}