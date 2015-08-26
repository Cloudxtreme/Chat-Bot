using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using ChatBot.ndfdXML;
using ChatBot.Wcf;

namespace ChatBot
{
    public class Weather
    {
        // Input variables to generate XML file containing all weather data
        public decimal longitude;
        public decimal latitude;
        public productType weatherForecastProductType;
        public DateTime startTime;
        public DateTime endTime;
        public unitType weatherUnit;
        public weatherParametersType forecastParametersType;

        // Output variables to store all weather data found in the generated XML file
        public double maxTempertaure;
        public double minTemperature;
        public double apparentTemperature;
        public double maxHumidity;
        public double minHumidity;
        public double snow;
        public string chanceOfPrecipitation;
        public double windSpeed;
        public string windDirection;
        public string weatherInfo;        

        public Weather()
        {
            // Position of Alexandria, VA on the map -- should be extensible to any "home" location of the application user
            longitude = 38.8047m; // 38.8047 degrees N (N is positive)
            latitude = -77.0472m; // 77.0472 degrees W (W is negative)
            weatherForecastProductType = productType.timeseries;
            startTime = DateTime.UtcNow; // Weather forecast begins at the current time
            endTime = DateTime.UtcNow.AddDays(1); // Weather forecast ends a day after the current time -- Daily weather forecast
            weatherUnit = unitType.m; // Weather forecast generated in metric (SI) units
            forecastParametersType = new weatherParametersType();
            forecastParametersType.maxt = true; // Maximum temperature
            forecastParametersType.mint = true; // Minimum temperature
            forecastParametersType.maxrh = true; // Maximum relative humidity
            forecastParametersType.minrh = true; // Minimum relative humidity
            forecastParametersType.pop12 = true; // 12 hour probability of precipitation
            forecastParametersType.snow = true; // Snowfall amount
            forecastParametersType.wspd = true; // Wind speed
            forecastParametersType.wdir = true; // Wind direction
            forecastParametersType.wx = true;   // Weather -- Not sure what this means, want to find out
        }

        // string ftpNWS = "ftp://tgftp.nws.noaa.gov/SL.us008001/ST.expr/DF.gr2/DC.ndfd/AR.neast";
        public void ForecastWeather()
        {
            ndfdXMLPortTypeClient ndfdXmlClient = new ndfdXMLPortTypeClient();

            HttpTransportBindingElement transportBinding = new HttpTransportBindingElement();

            //https://stackoverflow.com/questions/7033442/using-iso-8859-1-encoding-between-wcf-and-oracle-linux
            CustomBinding binding = new CustomBinding(new CustomTextMessageBindingElement("iso-8859-1", "text/xml", MessageVersion.Soap11), transportBinding);
            ndfdXmlClient.Endpoint.Binding = binding;

            string client = ndfdXmlClient.NDFDgen(longitude, latitude, weatherForecastProductType, startTime, endTime, weatherUnit,
                forecastParametersType);

            Console.Out.WriteLine(client); // I presume that the results are contained in the return of NDFDgen()

            forecastParametersType.ToString(); // I want to determine what the default setting is, possibly some of these are set to TRUE by default already
        }

        public void PresentWeather()
        {
            /* 
            TODO:
            Store elements in separate variables
            Present contents of each variable by some means to the application user
            */
        }
    }
}
