using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot
{
    public class WeatherSensor
    {
        public WeatherSensor()
        {
            Temperature = 72;
        }
        public int Temperature { get; set; }

        public bool IsHot()
        {
              return Temperature > 75;
        }
    }

    //public class BotMemory : List<Exchange>
    //{
    //}

    public class BotMemory
    {
        public List<Exchange> Memory { get; }

        public BotMemory(List<Exchange> memory)
        {
            this.Memory = memory;
        }
    }

    public class Bot
    {
        private BotMemory memory;
        private WeatherSensor sensor;
        public Bot()
        {
            memory = new BotMemory(new List<Exchange>());
            sensor = new WeatherSensor();
        }

        public Bot(List<Exchange> memory, WeatherSensor sensor)
        {
            this.memory = new BotMemory(memory);
            this.sensor = sensor;
        }
        public Exchange ProcessCommand(Exchange exchange)
        {
            memory.Memory.Add(exchange);

            if (exchange.ToString().StartsWith("SAY WHAT"))
            {
                return memory.Memory[Int32.Parse(exchange.ToString().Substring(9))];
            }

            switch (exchange.ToString())
            {
                case "IS HOT?":
                    if (sensor.IsHot())
                    {
                        return new Exchange("YES");
                    }
                    return new Exchange("NO");
                default:
                    return new Exchange("OK");
            }
        }

        public List<Exchange> ProcessCommand(Command command)
        {
            List<Exchange> e = new List<Exchange>();
            var item = (Exchange) command;
            e.Add(item);
            return e;
        }
    }
}
