using ChatBot.ndfdXML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ChatBot;

namespace UnitTests
{
    [TestFixture]
    public class ExerciseServices
    {
        [Test]
        public void Something() {
            ndfdXMLPortTypeClient client = new ndfdXMLPortTypeClient();
            string value= client.LatLonListZipCode("20912");
            Console.WriteLine(value);
        }

        [Test]
        public void SomethingWeather()
        {
            Weather client = new Weather();
            client.ParseWeather();
        }
        
    }
}
