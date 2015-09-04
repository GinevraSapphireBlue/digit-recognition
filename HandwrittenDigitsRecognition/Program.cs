using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using HandwrittenDigitsRecognition.NeuralNetwork;
using HandwrittenDigitsRecognition.NeuronApp;
using System.IO;

namespace HandwrittenDigitsRecognition
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        /*
        private bool dataReadIn;
        private bool DataReadIn { get; set; }

        
        private Dictionary<int, List<int>> trainingData;
        public Dictionary<int, List<int>> TrainingData
        {
            static get
            {
                if (trainingData == null)
                    trainingData = new Dictionary<int, List<int>>();
                return trainingData;
            }
            private static set { trainingData = value; }
        }

        private List<int> expectedResults;
        public List<int> ExpectedResults
        {
            static get
            {
                if (expectedResults == null)
                    expectedResults = new List<int>();
                return expectedResults;
            }
            private static set { expectedResults = value; }
        }
        */
         
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            Dictionary<int, List<int>> trainingData = new Dictionary<int, List<int>>();
            List<int> expectedResults = new List<int>();
            //ReadTrainingData(trainingData, expectedResults);
            NeuronApp.App myApp = new App();
        }

        static void ReadTrainingData(Dictionary<int, List<int>> trainingData, List<int> expectedResults)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "NeuronApp\\optdigits.tra");

            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                    ProcessInput(line, trainingData, expectedResults);
                Console.WriteLine("Finished reading in training data, read in {0} lines", trainingData.Count);
                reader.Close();
            }
        }

        static void ProcessInput(string line, Dictionary<int, List<int>> trainingData, List<int> expectedResults)
        {
            string[] items;
            
            int i = trainingData.Count;
            trainingData.Add(i, new List<int>(64));
            items = line.Split(',');
            for (int j = 0; j < 64; j++)
                trainingData[i].Add(int.Parse(items[j]));
            expectedResults.Add(int.Parse(items[64]));
            Console.WriteLine(line);
        }
    }
}
