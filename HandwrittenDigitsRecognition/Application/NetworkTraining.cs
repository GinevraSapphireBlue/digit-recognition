using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandwrittenDigitsRecognition.NeuralNetwork;
using System.IO;

namespace HandwrittenDigitsRecognition.Application
{
    class NetworkTraining
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

        private int[][] trainingData;
        public int[][] TrainingData
        {
            get
            {
                if (!DataReadIn)
                    ReadTrainingData();
                return trainingData;
            }
            private set { trainingData = value; }
        }

        private int[] expectedResults;
        public int[] ExpectedResults
        {
            get
            {
                if (!DataReadIn)
                    ReadTrainingData();
                return expectedResults;
            }
            private set { expectedResults = value; }
        }

        private int numberOfExamples;
        public int NumerOfExamples { get; protected set; }

        /* CONSTRUCTOR */
        public NetworkTraining(Network trainNetwork, string file, double learnCoef)
        {
            DigitNetwork = trainNetwork;
            FileLocation = file;
            LearningCoef = learnCoef;
            DataReadIn = false;
            ReadTrainingData();
            NumerOfExamples = ExpectedResults.Length;
        }

        /* TRAIN NETWORK */
        public double TrainNetwork()
        {
            for (int i = 0; i < NumerOfExamples; i++)
            {
                /* Add inputs to network and calculate outputs */
                DigitNetwork.ResetAll();
                DigitNetwork.SetInputValues(TrainingData[i]);
                DigitNetwork.FeedResultsForward();

                /* Prepare array with expected values - 0s for all but the correct digit and 1 for the correct digit */
                int expectedDigit = ExpectedResults[i];
                int[] expectedValues = new int[10];
                for (int j = 0; j < 10; j++)
                {
                    if (j == expectedDigit)
                        expectedValues[j] = 1;
                    else
                        expectedValues[j] = 0;
                }

                /* Calculate errors and propagate them back through the network */
                DigitNetwork.PropagateErrorsBack(LearningCoef, expectedValues);

                /*  */
            }
        }

        private void ReadTrainingData()
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
                        trainingData[i][j] = int.Parse(items[j]);
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
