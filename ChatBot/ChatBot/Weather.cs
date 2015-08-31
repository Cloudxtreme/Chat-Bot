using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;
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
        public Seasons currentSeason;
        public Dictionary<string, int> layoutDictionary; 
        public Dictionary<string, double> weatherDictionary;
        
        /// START: Copied from http://cladosnippet.blogspot.com/2012/04/c-get-season-according-to-given-date.html

        /// <summary>
        /// Astronomic Season
        /// </summary>
        public enum Seasons
        {
            /// <summary>
            /// Spring begins on the 80th day, on a leap year it begins one the 81
            /// </summary>
            Spring,

            /// <summary>
            /// Summer begins on the 172nd day, on a leap year it begins one the 173
            /// </summary>
            Summer,

            /// <summary>
            /// Autumn the 266th, on a leap year it begins one the 267
            /// </summary>
            Autumn,

            /// <summary>
            /// Winter the 355th, on a leap year it begins one the 356
            /// </summary>
            Winter
        }

        /// <summary>
        /// Get the current season return the day <see cref="Enumerations.Seasons">
        /// </see></summary>
        /// <param name="date">

        /// <returns></returns>
        public static Seasons CurrentSeason(DateTime date)
        {
            /* Astronomically Spring begins on March 21st, the 80th day of the year. 
             * Summer begins on the 172nd day, Autumn, the 266th and Winter the 355th.
             * Of course, on a leap year add one day to each, 81, 173, 267 and 356.*/

            int doy = date.DayOfYear - Convert.ToInt32((DateTime.IsLeapYear(date.Year)) && date.DayOfYear > 59);

            if (doy < 80 || doy >= 355) return Seasons.Winter;

            if (doy >= 80 && doy < 172) return Seasons.Spring;

            if (doy >= 172 && doy < 266) return Seasons.Summer;

            return Seasons.Autumn;
        }

        /// END: Copied from http://cladosnippet.blogspot.com/2012/04/c-get-season-according-to-given-date.html

        public Weather()
        {
            // Position of Alexandria, VA on the map -- should be extensible to any "home" location of the application user
            longitude = 38.8047m; // 38.8047 degrees N (N is positive)
            latitude = -77.0472m; // 77.0472 degrees W (W is negative)
            weatherForecastProductType = productType.timeseries;
            startTime = DateTime.UtcNow; // Weather forecast begins at the current time
            endTime = DateTime.UtcNow.AddDays(1); // Weather forecast ends a day after the current time -- Daily weather forecast
            weatherUnit = unitType.m; // Weather forecast generated in metric (SI) units

            currentSeason = CurrentSeason(startTime);
            forecastParametersType = new weatherParametersType();
            forecastParametersType.maxt = true; // Maximum temperature
            forecastParametersType.mint = true; // Minimum temperature
            forecastParametersType.appt = true; // Apparent temperature
            forecastParametersType.maxrh = true; // Maximum relative humidity
            forecastParametersType.minrh = true; // Minimum relative humidity
            forecastParametersType.pop12 = true; // 12 hour probability of precipitation
            if (Equals(currentSeason, Seasons.Winter)) // Snowfall is almost certainly zero unless the query is made in the winter
            {
                forecastParametersType.snow = true; // Snowfall amount
            }
            forecastParametersType.wspd = true; // Wind speed
            forecastParametersType.wdir = true; // Wind direction

            weatherDictionary = new Dictionary<string, double>();
            weatherDictionary.Add("Maximum Temperature", 0.0);
            weatherDictionary.Add("Minimum Temperature", 0.0);
            weatherDictionary.Add("Apparent Temperature", 0.0);
            weatherDictionary.Add("12 Hour Probability of Precipitation", 0.0);
            weatherDictionary.Add("Wind Speed", 0.0);
            weatherDictionary.Add("Wind Direction", 0.0);
            weatherDictionary.Add("Maximum Relative Humidity", 0.0);
            weatherDictionary.Add("Minimum Relative Humidity", 0.0);
            weatherDictionary.Add("Snowfall Amount", 0.0); // Need to fix this for winter... will not work
        }

        // string ftpNWS = "ftp://tgftp.nws.noaa.gov/SL.us008001/ST.expr/DF.gr2/DC.ndfd/AR.neast";
        public string ForecastWeather()
        {
            ndfdXMLPortTypeClient ndfdXmlClient = new ndfdXMLPortTypeClient();
            HttpTransportBindingElement transportBinding = new HttpTransportBindingElement();

            //https://stackoverflow.com/questions/7033442/using-iso-8859-1-encoding-between-wcf-and-oracle-linux
            CustomBinding binding = new CustomBinding(new CustomTextMessageBindingElement("iso-8859-1", "text/xml", MessageVersion.Soap11), transportBinding);
            ndfdXmlClient.Endpoint.Binding = binding;

            string client = ndfdXmlClient.NDFDgen(longitude, latitude, weatherForecastProductType, startTime, endTime, weatherUnit,
                forecastParametersType);

            return client;
        }

        public void ParseWeather()
        {
            string client = ForecastWeather();

            // Console.Out.WriteLine(client);

            XDocument xmlDocument = XDocument.Parse(client);

            var parent = xmlDocument.Descendants("parameters");
            var child = parent.Descendants();
            var descendants = child.Elements();

            /* Logical Approach
            1. Find all descendants of 'parameters' (i.e., temperature, humidity, wind speed, wind direction, etc.)
            2. Find all descendants of children of 'parameters' (i.e., name and collection of values)
            3. Pair descendants of 'parameters' and children of 'parameters' together
            4. Store list of children of 'parameters' into their respective variables
            */

            foreach (XElement element in child)
            {
                foreach (XAttribute attribute in element.Attributes())
                {
                    if (attribute.Name.Equals("time-layout"))
                    {
                        
                        Console.Out.WriteLine("Value: {0}", attribute.Value);
                    }
                }
            }

            foreach (XElement element in descendants)
            {
                Console.Out.WriteLine("Name: {0} Value: {1}", element.Name, element.Value);
            }

            /* 
            TODO:
            Store elements in separate variables
            Present contents of each variable by some means to the application user
            */
        }
    }
}
