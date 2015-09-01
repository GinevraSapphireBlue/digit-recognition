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

        private int[,] trainingData;
        public int[,] TrainingData
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

        /* CONSTRUCTOR */
        public NetworkTraining(Network trainNetwork, string file, double learnCoef)
        {
            DigitNetwork = trainNetwork;
            FileLocation = file;
            LearningCoef = learnCoef;
            DataReadIn = false;
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
                        trainingData[i, j] = int.Parse(items[j]);
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
