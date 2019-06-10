using LogicDemoWar;
using System;
using System.Threading;

namespace DemoWar
{
    class Program
    {
        static void Main(string[] args)
        {
            var citiesService = new CitiesService();

            for (int i = 0; i < 50; i++)
            {
                var init = true;
                while (true)
                {
                    var cities = citiesService.DoAttack(init);
                    init = false;
                    if (String.IsNullOrEmpty(cities.CityDefeated.Name))
                    {
                        Console.WriteLine("La ciudad " + cities.CityWin.Name + " ha vencido");
                        break;
                    }

                    var result = "La ciudad " + cities.CityWin.Name + " ha conquistado " + cities.CityDefeated.Name;

                    if (!string.IsNullOrEmpty(cities.CityDefeated.Conquered))
                    {
                        result += ", previamente conquistada por " + cities.CityDefeated.Conquered;
                    }

                    Console.WriteLine(result);
                    Thread.Sleep(1000);
                }


                Console.WriteLine("----------------------");
                Console.WriteLine("----------------------");
                Thread.Sleep(3000);
            }
        }
    }
}
