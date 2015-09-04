using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandwrittenDigitsRecognition.NeuralNetwork;
using System.IO;
using HandwrittenDigitsRecognition.NeuralNetwork.Neurons;

namespace HandwrittenDigitsRecognition.NeuronApp
{
    class NetworkTesting
    {
        private Network digitNetwork;
        public Network DigitNetwork
        {
            get { return digitNetwork; }
            private set { digitNetwork = value; }
        }
        private string fileName;
        public string FileName { get; set; }

        private double learningCoef;
        public double LearningCoef { get; set; }

        private Dictionary<int, List<int>> testingData;
        public Dictionary<int, List<int>> TestingData
        {
            get
            {
                return testingData;
            }
            private set { testingData = value; }
        }

        private List<int> expectedResults;
        public List<int> ExpectedResults
        {
            get
            {
                return expectedResults;
            }
            private set { expectedResults = value; }
        }

        private int numberOfExamples;
        public int NumberOfExamples { get; protected set; }

        private int countCorrect;
        public int CountCorrect { get; protected set; }

        private int countIncorrect;
        public int CountIncorrect { get; protected set; }

        private double averageError;
        public double AverageError { get; protected set; }

        public NetworkTesting(Network testNetwork, string file, double learnCoef, Dictionary<int, List<int>> testingData, List<int> expectedValues)
        {
            DigitNetwork = testNetwork;
            FileName = file;
            LearningCoef = learnCoef;
            TestingData = testingData;
            ExpectedResults = expectedValues;
            ReadTestingData();
            if(ExpectedResults != null)
                NumberOfExamples = ExpectedResults.Count;
            else
            {
                NumberOfExamples = 0;
                Console.WriteLine("No testing examples read!!!");
            }
            CountCorrect = 0;
            CountIncorrect = 0;
        }

        /* TEST NETWORK */
        public double TestNetwork()
        {
            AverageError = 0;
            Console.WriteLine("Start testing");

            for (int i = 0; i < NumberOfExamples; i++)
            {
                //Console.WriteLine("Input number {0}", i + 1);
                /* Add inputs to network and calculate outputs */
                DigitNetwork.ResetAll();
                DigitNetwork.SetInputValues(TestingData[i].ToArray<int>());
                DigitNetwork.FeedResultsForward();

                /*
                Console.WriteLine("Network result: {0}; expected result: {1}", DigitNetwork.GetResult(), ExpectedResults[i]);
                Console.WriteLine("Output neurons:");
                foreach (Neuron n in DigitNetwork.OutputLayer.Neurons)
                    Console.Write("{0} ", n.GetOutput());
                Console.WriteLine();
                */
                if (DigitNetwork.GetResult() == ExpectedResults[i])
                {
                    CountCorrect++;
                    //Console.WriteLine("Correct identification, Correct = {0}", CountCorrect);
                }
                else
                {
                    CountIncorrect++;
                    //Console.WriteLine("Wrong identification, Wrong = {0}", CountIncorrect);
                }

                AverageError += DigitNetwork.GetResult() - ExpectedResults[i];
            }
            AverageError /= NumberOfExamples;
            Console.WriteLine("Average error: {0}", AverageError);
            return AverageError;
        }

        /* READ IN TESTING DATA */
        public void ReadTestingData()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), FileName);

            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                    ProcessInput(line);
                Console.WriteLine("Finished reading in testing data, read in {0} lines", TestingData.Count);
                reader.Close();
            }
        }

        public void ProcessInput(string line)
        {
            string[] items;

            int i = TestingData.Count;
            TestingData.Add(i, new List<int>(64));
            items = line.Split(',');
            for (int j = 0; j < 64; j++)
                TestingData[i].Add(int.Parse(items[j]));
            ExpectedResults.Add(int.Parse(items[64]));
            //Console.WriteLine(line);
        }
    }
}
