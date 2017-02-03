using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SemainierStage.Models
{
    [MetadataType(typeof(EtudiantMetaData))]
    public partial class Etudiant
    {
        public class EtudiantMetaData
        {
            //Validation pour le nom de l'étudiants.
            [StringLength(50, ErrorMessage = "Le nom ne peut pas avoir plus de 50 caractères.")]
            [Required(ErrorMessage = "Vous devez entrer un nom pour l'étudiants.")]
            public string Nom { get; set; }

            //Validation pour le prenom de l'étudiants.
            [StringLength(50, ErrorMessage = "Le prénom ne peut pas avoir plus de 50 caractères.")]
            [Required(ErrorMessage = "Vous devez entrer un Prénom pour l'étudiants.")]
            public string Prenom { get; set; }

            //Validation pour le couriel.
            [StringLength(50, ErrorMessage = "L'adresse courriel ne peut pas avoir plus de 50 caractères.")]
            [Required(ErrorMessage = "Vous devez entrer un Prénom pour l'étudiants.")]
            public string Email { get; set; }
        }
    }
}