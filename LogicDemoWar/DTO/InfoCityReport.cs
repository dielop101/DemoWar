using System;
using System.Collections.Generic;
using System.Text;

namespace LogicDemoWar.DTO
{
    public class InfoCityReport
    {
        public InfoCityReport()
        {
            CityWin = new InfoCity();
            CityDefeated = new InfoCity();
        }
        public InfoCity CityWin { get; set; }
        public InfoCity CityDefeated { get; set; }
    }
}
