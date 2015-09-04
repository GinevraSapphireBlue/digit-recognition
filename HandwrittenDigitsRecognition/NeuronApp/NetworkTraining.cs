using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandwrittenDigitsRecognition.NeuralNetwork;
using System.IO;

namespace HandwrittenDigitsRecognition.NeuronApp
{
    class NetworkTraining
    {
        private Network digitNetwork;
        public Network DigitNetwork
        {
            get { return digitNetwork; }
            private set { digitNetwork = value; }     
        }
        private string fileName;
        public string FileName { get; private set; }

        private double learningCoef;
        public double LearningCoef { get; set; }

        private Dictionary<int, List<int>> trainingData;
        public Dictionary<int, List<int>> TrainingData
        {
            get { return trainingData; }
            set { trainingData = value; }
        }

        private List<int> expectedResults;
        public List<int> ExpectedResults
        {
            get { return expectedResults; }
            set { expectedResults = value; }
        }

        private int numberOfExamples;
        public int NumerOfExamples { get; private set; }

        /* CONSTRUCTOR */
        public NetworkTraining(Network trainNetwork, string file, double learnCoef, Dictionary<int, List<int>> trainingData, List<int> expectedValues)
        {
            DigitNetwork = trainNetwork;
            FileName = file;
            LearningCoef = learnCoef;
            TrainingData = trainingData;
            ExpectedResults = expectedValues;
            ReadTrainingData();
            if (ExpectedResults != null)
                NumerOfExamples = ExpectedResults.Count;
            else
            {
                NumerOfExamples = 0;
                Console.WriteLine("No training examples read!!!");
            }
        }

        /* TRAIN NETWORK */
        public double TrainNetwork()
        {
            double averageError = 0;

            Console.WriteLine("Start training");

            for (int i = 0; i < NumerOfExamples; i++)
            {
                Console.WriteLine("Input number {0}", i+1);
                /* Add inputs to network and calculate outputs */
                DigitNetwork.ResetAll();
                DigitNetwork.SetInputValues(TrainingData[i].ToArray<int>());
                DigitNetwork.FeedResultsForward();

                Console.WriteLine("Network result: {0}; expected result: {1}", DigitNetwork.GetResult(), ExpectedResults[i]);

                averageError += DigitNetwork.GetResult() - ExpectedResults[i];

                /* Calculate errors and propagate them back through the network, then adjust weights */
                DigitNetwork.PropagateErrorsBack(LearningCoef, ExpectedResults[i]);
            }
            averageError /= NumerOfExamples;
            Console.WriteLine("Average error: {0}", averageError);
            return averageError;
        }

        /* READ IN TRAINING DATA */
        public void ReadTrainingData()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), FileName);

            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                    ProcessInput(line);
                Console.WriteLine("Finished reading in training data, read in {0} lines", trainingData.Count);
                reader.Close();
            }
        }

        public void ProcessInput(string line)
        {
            string[] items;

            int i = TrainingData.Count;
            TrainingData.Add(i, new List<int>(64));
            items = line.Split(',');
            for (int j = 0; j < 64; j++)
                TrainingData[i].Add(int.Parse(items[j]));
            ExpectedResults.Add(int.Parse(items[64]));
            //Console.WriteLine(line);
        }
    }
}
