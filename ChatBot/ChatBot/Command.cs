using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot
{

    public class Command
    {
        public Command()
        {

        }
        public string Type { get; set; }
        public int HowMany { get; set; }

        public int Foobar { get; set; }

        //public static Exchange Convert(Command c)
        //{
        //    return new Exchange();
        //}
        static public explicit operator Exchange(Command command)
        {
            //string foo = "a" + "b" + "c";
            StringBuilder sb = new StringBuilder();
            sb.Append("SAY WHAT ");
            sb.Append(command.HowMany);
            if (command.Type == "Query")
            {
                sb.Append("?");
            }
            return new Exchange();
        }
    }
}
