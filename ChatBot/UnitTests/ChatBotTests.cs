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
    public class ChatBotTests
    {
        [Test]
        public void FirstTest()
        {
            Bot bot = new Bot();
            Exchange ex =bot.ProcessCommand(new Exchange("Hello Bot!"));
            Assert.AreEqual("Ok.", ex.ToString());
        }

        [Test]
        public void SecondTest()
        {
            Bot bot = new Bot();
            Exchange ex = bot.ProcessCommand(new Exchange("Wow, it is hot!"));
            Assert.AreEqual("No it is not.", ex.ToString());
        }

        [Test]
        public void ThirdTest()
        {
            //Need to implement Command "message"
            List<Exchange> ex;
            Bot bot = new Bot();
            //Command someCommand = new Command()
            //{
            //    Type = "Recall",
            //    HowMany = 6
            //};

            //Command someCommand2 = new Command();
            //someCommand.Type = "Recall";
            //someCommand.HowMany = 6;

            ex = bot.ProcessCommand(new Command()
            {
                Type = "Recall",
                HowMany = 6
            });
            Assert.AreEqual(6, ex.Count());
        }
    }

}
