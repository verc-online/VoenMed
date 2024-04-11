using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoenMedLibrary.Models
{
    public class DrugModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double StandardDose { get; set; }
        public string Measurement { get; set; }
        public bool Favourite { get; set; }
    }
}
