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
    public class Exchange
    {
        public string QueryType;//Question, command, observation
        public string IsWeatherRelated;
        public string WantOpinion;

        private string content;
        private DateTime timestamp;

        public Exchange()
        {
            this.content = "";
            this.timestamp = DateTime.UtcNow;
        }

        public Exchange(string content)
        {
            this.content = content;
            timestamp = DateTime.UtcNow;
        }
    }
}
