using DataDemoWar.Entity;
using Newtonsoft.Json;
using RandomLibrary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataDemoWar.DataInit
{
    public class CityDao
    {
        private readonly List<City> CitiesData;

        public CityDao()
        {
            CitiesData = DataInit();
        }

        private List<City> DataInit()
        {
            var text = System.IO.File.ReadAllText(@"C:\Users\Diego\source\repos\Github\DemoWar\DataDemoWar\Source\Vueling.json");
            return JsonConvert.DeserializeObject<List<City>>(text);
        }

        public bool CheckIfExistCitiesNotConquered()
        {
            if (GetCityNotConquered().Count() == 1)
            {
                //checkeamos tambien que todas las conquistas sean del mismo
                return CitiesData.Select(x => x.ConqueredBy).Where(c => !c.HasValue).Count() != 1;
            }

            return true;
        }

        public IEnumerable<City> GetCityNotConquered()
        {
            return CitiesData.Where(c => !c.ConqueredBy.HasValue);
        }

        public City GetRandomCityNotConquered()
        {
            var id = GetCityNotConquered().Select(city => city.Id).Shuffle().FirstOrDefault();
            return GetCityById(id);
        }

        public City GetCityById(int cityDefeatedId)
        {

            return CitiesData.FirstOrDefault(city => city.Id == cityDefeatedId);
        }

        public bool SaveConqueredCity(int cityWinId, int cityDefeatedId)
        {
            var city = GetCityById(cityDefeatedId);

            city.ConqueredBy = cityWinId;

            return true;
        }
    }
}
