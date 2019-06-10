using DataDemoWar.DataInit;
using DataDemoWar.Entity;
using LogicDemoWar.DTO;
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

                var cityDefeatedId = cityWin.GetRandomAdjoiningCity(cityWin.Id);
                var cityDefeated = _cityDao.GetCityById(cityDefeatedId);

                //check si está conquistada por la cityWin. Si no lo está => lo estará. Si lo está => cogemos los AdjoiningCities de la cityDefeated
                if (cityDefeated.ConqueredBy == cityWin.Id)
                {
                    cityDefeatedId = cityDefeated.GetRandomAdjoiningCity(cityWin.Id);
                    cityDefeated = _cityDao.GetCityById(cityDefeatedId);
                }

                var result = new InfoCityReport();
                result.CityWin.Name = cityWin.Name;
                result.CityDefeated.Name = cityDefeated.Name;
                if (cityDefeated.ConqueredBy.HasValue)
                {
                    var previousCityConquer = _cityDao.GetCityById(cityDefeated.ConqueredBy.Value);
                    result.CityDefeated.Conquered = previousCityConquer.Name;
                }

                _cityDao.SaveConqueredCity(cityWin.Id, cityDefeatedId);

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
    }
}
