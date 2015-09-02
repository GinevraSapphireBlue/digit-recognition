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
        private string fileLocation;
        public string FileLocation { get; set; }

        private double learningCoef;
        public double LearningCoef { get; set; }

        private bool dataReadIn;
        private bool DataReadIn { get; set; }

        private Dictionary<int, List<int>> trainingData;
        public Dictionary<int, List<int>> TrainingData
        {
            get
            {
                if (!DataReadIn)
                    ReadTrainingData();
                return trainingData;
            }
            private set { trainingData = value; }
        }

        private List<int> expectedResults;
        public List<int> ExpectedResults
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

            for (int i = 0; i < NumerOfExamples; i++)
            {
                /* Add inputs to network and calculate outputs */
                DigitNetwork.ResetAll();
                DigitNetwork.SetInputValues(TrainingData[i].ToArray<int>());
                DigitNetwork.FeedResultsForward();

                averageError += DigitNetwork.GetResult() - ExpectedResults[i];

                /* Calculate errors and propagate them back through the network, then adjust weights */
                DigitNetwork.PropagateErrorsBack(LearningCoef, ExpectedResults[i]);
            }
            averageError /= NumerOfExamples;
            return averageError;
        }

        private void ReadTrainingData()
        {
            StreamReader reader;
            TrainingData = new Dictionary<int, List<int>>();
            ExpectedResults = new List<int>();
            try
            {
                reader = File.OpenText(FileLocation);
                string line;
                string[] items;
                int i = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    TrainingData.Add(i, new List<int>(64));
                    items = line.Split(',');
                    for (int j = 0; j < DigitNetwork.InputSize; j++)
                        TrainingData[i][j] = int.Parse(items[j]);
                    ExpectedResults.Add(int.Parse(items[DigitNetwork.InputSize]));
                    i++;
                }
                reader.Close();
                DataReadIn = true;
                Console.WriteLine("Training data read in");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                DataReadIn = false;
            }
        }
    }
}
