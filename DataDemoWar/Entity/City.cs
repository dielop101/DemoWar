using RandomLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataDemoWar.Entity
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> AdjoiningCities { get; set; }
        public int? ConqueredBy { get; set; }

        public int GetRandomAdjoiningCity(int idExcluded = -1)
        {
            return AdjoiningCities.Where(i => i != idExcluded).Shuffle().FirstOrDefault();
        }
    }
}
