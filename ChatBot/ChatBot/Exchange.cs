using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot
{
    /// <summary>
    /// What someone said.
    /// </summary>
    public class Exchange: IFormattable, IFormatProvider, ICustomFormatter
    {
        public string QueryType;//Question, command, observation
        public string IsWeatherRelated;
        public string WantOpinion;


        private string content;
        private DateTime whenSpoken;
        public Exchange()
        {
            this.content = "";
        }

        public Exchange(string content)
        {
            this.content = content;
            whenSpoken = DateTime.UtcNow;
        }
        public override string ToString()
        {
            return content;
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            //TODO:
            throw new NotImplementedException();
        }

        public object GetFormat(Type formatType)
        {
            throw new NotImplementedException();
        }

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            throw new NotImplementedException();
        }

        
    }
}
