using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoenMedLibrary.Models
{
    public class DefaultsModel
    {
        public string IssuedBy { get; set; }
        public string Doc { get; set; }
        public string EvacAddress { get; set; }
        public int EvacTransport { get; set; }
        public int EvacWay { get; set; }
        public int FavDrug1Id { get; set; }
        public int FavDrug2Id { get; set; }
        public int FavDrug3Id { get; set; }
        public int FavDrug4Id { get; set; }

        public string SavePath { get; set; }

        public string GetDefaultSavePath()
        {
            return Path.Combine(Environment.CurrentDirectory, "Forms100");
        }
    }
}
