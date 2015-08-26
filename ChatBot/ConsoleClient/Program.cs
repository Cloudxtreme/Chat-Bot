using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using ChatBot;
using ChatBot.Wcf;

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
