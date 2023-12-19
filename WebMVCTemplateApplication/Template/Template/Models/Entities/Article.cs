using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Template.Models.Entities
{
    public class Article
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Designation is Required")]
        public string Designation { get; set; }
        public string Categorie { get; set; }
        public float? Prix { get; set; }
        public DateTime? DateFabrication { get; set; }

        public Article()
        {
        }
        public Article(string pDesignation, string pCategorie, float? pPrix, DateTime? pDateFabrication)
        {
            Designation = pDesignation;
            Categorie = pCategorie;
            Prix = pPrix;
            DateFabrication = pDateFabrication;
        }
    }
}
