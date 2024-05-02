using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADO.HELP.Models
{
    public class Inimene
    {
        public int Id { get; set; }
        public string Nimi { get; set; }
        public int? Vanus { get; set; }
        public string Maakond { get; set; }   
    }
}