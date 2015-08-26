using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatBot;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {

            Weather client = new Weather();
            client.ForecastWeather();
        }
    }
}
