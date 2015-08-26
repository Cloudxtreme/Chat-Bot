using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Recognition;


namespace ChatBot
{
    /// <summary>
    /// What someone said.
    /// </summary>
    public class Translation
    {
        public SpeechRecognizer recognizer;
        public Choices grammarChoices;
        public GrammarBuilder builder;
        public Grammar grammar;

        public Translation()
        {
            this.recognizer = new SpeechRecognizer();
            this.grammarChoices = new Choices();
            this.builder = new GrammarBuilder();
        }

        public Translation(Choices manualChoices)
        {
            this.recognizer = new SpeechRecognizer();
            this.grammarChoices = manualChoices;
            this.builder = new GrammarBuilder();
        }

        public void LoadGrammar(Grammar grammarToLoad)
        {
            recognizer.LoadGrammar(grammarToLoad);
        }

        public Choices AddGrammar(string providedGrammar)
        {
            Choices grammarToLoad = new Choices();

            grammarToLoad.Add(providedGrammar);

            return grammarToLoad;
        }

        public GrammarBuilder AppendGrammar(Choices grammarToLoad)
        {
            GrammarBuilder grammarBuilder = new GrammarBuilder();

            grammarBuilder.Append(grammarToLoad);

            return grammarBuilder;
        }

        // Create a simple handler for the SpeechRecognized event
        private void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            System.Console.Out.WriteLine("Speech recognized: " + e.Result.Text);
        }
    }
}
