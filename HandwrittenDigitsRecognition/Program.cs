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
         
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            Dictionary<int, List<int>> trainingData = new Dictionary<int, List<int>>();
            List<int> expectedResults = new List<int>();
            //ReadTrainingData(trainingData, expectedResults);
            NeuronApp.App myApp = new App(2, 10, 0.03, 100);
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
