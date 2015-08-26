using ChatBot.ndfdXML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot
{
    
    public class ExerciseServices
    {
        public void Something() {
            ndfdXMLPortTypeClient client = new ndfdXMLPortTypeClient();
            string value= client.LatLonListZipCode("20912");

        }
    }
}
