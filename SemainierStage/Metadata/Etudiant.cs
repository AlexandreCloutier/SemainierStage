using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

[MetadataType(typeof(Etudiant))]
public partial class Etudiant
{
    public class EtudiantMetaData
    {
        [Required]
        public string cat_description { get; set; }
   }
}