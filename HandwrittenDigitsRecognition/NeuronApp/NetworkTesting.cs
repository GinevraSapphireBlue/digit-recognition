using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandwrittenDigitsRecognition.NeuralNetwork;
using System.IO;

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
        private string fileLocation;
        public string FileLocation { get; set; }

        private double learningCoef;
        public double LearningCoef { get; set; }

        private bool dataReadIn;
        private bool DataReadIn { get; set; }

        private int[][] testingData;
        public int[][] TestingData
        {
            get
            {
                if (!DataReadIn)
                    ReadTestingData();
                return testingData;
            }
            private set { testingData = value; }
        }

        private int[] expectedResults;
        public int[] ExpectedResults
        {
            get
            {
                if (!DataReadIn)
                    ReadTestingData();
                return expectedResults;
            }
            private set { expectedResults = value; }
        }

        private int numberOfExamples;
        public int NumerOfExamples { get; protected set; }

        private int countRecognized;
        public int CountRecognized { get; protected set; }

        private int countUnrecognized;
        public int CountUnrecognized { get; protected set; }

        private double averageError;
        public double AverageError { get; protected set; }

        public NetworkTesting(Network testNetwork, string file, double learnCoef)
        {
            DigitNetwork = testNetwork;
            FileLocation = file;
            LearningCoef = learnCoef;
            DataReadIn = false;
            ReadTestingData();
            if(ExpectedResults != null)
                NumerOfExamples = ExpectedResults.Length;
            else
            {
                NumerOfExamples = 0;
                Console.WriteLine("No testing examples read!!!");
            }
        }

        /* TEST NETWORK */
        public double TestNetwork()
        {
            AverageError = 0;
            CountRecognized = 0;
            CountUnrecognized = 0;

            for (int i = 0; i < NumerOfExamples; i++)
            {
                /* Add inputs to network and calculate outputs */
                DigitNetwork.ResetAll();
                DigitNetwork.SetInputValues(TestingData[i]);
                DigitNetwork.FeedResultsForward();

                AverageError += DigitNetwork.GetResult() - ExpectedResults[i];
                if (DigitNetwork.GetResult() == ExpectedResults[i])
                    CountRecognized++;
                else
                    CountUnrecognized++;
            }
            AverageError /= NumerOfExamples;
            return AverageError;
        }

        public void ReadTestingData()
        {
            StreamReader reader;

            try
            {
                reader = File.OpenText(FileLocation);
                string line;
                string[] items;
                int i = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    items = line.Split(',');
                    for (int j = 0; j < DigitNetwork.InputSize; j++)
                        testingData[i][j] = int.Parse(items[j]);
                    expectedResults[i] = int.Parse(items[DigitNetwork.InputSize]);
                    i++;
                }
                reader.Close();
                DataReadIn = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                DataReadIn = false;
            }
        }
    }
}
