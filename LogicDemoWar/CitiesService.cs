using DataDemoWar.DataInit;
using DataDemoWar.Entity;
using LogicDemoWar.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LogicDemoWar
{
    public class CitiesService
    {

        public CityDao _cityDao;

        public CitiesService()
        {
            _cityDao = new CityDao();
        }

        public InfoCityReport DoAttack(bool reboot)
        {
            if (reboot)
            {
                _cityDao = new CityDao();
            }
            //si solo existe uno, es el ganador
            if (_cityDao.CheckIfExistCitiesNotConquered())
            {

                var cityWin = _cityDao.GetRandomCityNotConquered();

                var listExcluded = new List<int>() { cityWin.Id };
                var city = new List<City>() { cityWin };
                var cityDefeated = GetCityDefeated(city, cityWin.Id, listExcluded, cityWin.Id);

                var result = new InfoCityReport();
                result.CityWin.Name = cityWin.Name;
                result.CityDefeated.Name = cityDefeated.Name;
                if (cityDefeated.ConqueredBy.HasValue)
                {
                    var previousCityConquer = _cityDao.GetCityById(cityDefeated.ConqueredBy.Value);
                    result.CityDefeated.Conquered = previousCityConquer.Name;
                }

                _cityDao.SaveConqueredCity(cityWin.Id, cityDefeated.Id);

                return result;
            }


            return new InfoCityReport()
            {
                CityWin = new InfoCity()
                {
                    Name = _cityDao.GetCityNotConquered().First().Name
                }
            };
        }

        private City GetCityDefeated(List<City> city, int idCity, List<int> citiesIdExcluded, int cityWin)
        {
            var cityDefeatedId = city.First(c => c.Id == idCity).GetRandomAdjoiningCity(citiesIdExcluded);
            var cityDefeated = _cityDao.GetCityById(cityDefeatedId);

            if (cityDefeated.ConqueredBy != cityWin)
                return cityDefeated;

            citiesIdExcluded.Add(cityDefeated.Id);

            if (cityDefeated.AdjoiningCities.Intersect(citiesIdExcluded).Count() == cityDefeated.AdjoiningCities.Count)    //No hay ciudades próximas. Volvemos a la ciudad anterior quitando del random el actual
            {
                var idPrevious = 0;
                for (int i = 0; i < city.Count; i++)
                {
                    var newcity = city[i];
                    if (newcity.AdjoiningCities.Intersect(citiesIdExcluded).Count() != newcity.AdjoiningCities.Count)
                    {
                        idPrevious = newcity.Id;
                        break;
                    }
                }

                city.Add(cityDefeated);

                return GetCityDefeated(city, idPrevious, citiesIdExcluded, cityWin);
            }

            city.Add(cityDefeated);
            //hay ciudades próximas. Seguimos buscando
            return GetCityDefeated(city, cityDefeated.Id, citiesIdExcluded, cityWin);
        }
    }
}
