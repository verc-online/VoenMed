using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoenMedLibrary.Models
{
    public class ProvidedDrugModel
    {
        public int Id { get; set; }
        public int DrugId { get; set; }
        public string Title { get; set; }
        public double Dose { get; set; }
        public string Measurement { get; set; }
    }
}
