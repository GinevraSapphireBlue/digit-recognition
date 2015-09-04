using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandwrittenDigitsRecognition.NeuralNetwork;
using System.IO;

namespace HandwrittenDigitsRecognition.NeuronApp
{
    class App
    {
        private Network digitNetwork;
        public Network DigitNetwork
        {
            get { return digitNetwork; }
            private set { digitNetwork = value; }
        }

        private double learningCoef;
        public double LearningCoef { get; set; }

        private NetworkTraining training;
        public NetworkTraining Training { get; set; }

        private NetworkTesting testing;
        public NetworkTesting Testing { get; set; }

        private Dictionary<int, List<int>> trainingData;
        public Dictionary<int, List<int>> TrainingData
        {
            get { return trainingData; }
            set { trainingData = value; }
        }

        private List<int> expectedTrainingResults;
        public List<int> ExpectedTrainingResults
        {
            get { return expectedTrainingResults; }
            set { expectedTrainingResults = value; }
        }

        public App(int numOfLayers = 2, int numOfNeuronsInLayer = 10, double learnCoef = 1)
        {
            DigitNetwork = new Network(64, numOfLayers, numOfNeuronsInLayer);
            LearningCoef = learnCoef;
            string fileTrain = "NeuronApp\\optdigits.tra";
            string pathTest = Path.Combine(Directory.GetCurrentDirectory(), "NeuronApp\\optdigits.tes");

            TrainingData = new Dictionary<int, List<int>>();
            ExpectedTrainingResults = new List<int>();
            //ReadTrainingData(trainingData, expectedResults);

            Training = new NetworkTraining(DigitNetwork, fileTrain, LearningCoef, TrainingData, ExpectedTrainingResults);
            Training.TrainNetwork();
            //Testing = new NetworkTesting(DigitNetwork, pathTest, LearningCoef);
        }

        public void ReadTrainingData(Dictionary<int, List<int>> trainingData, List<int> expectedResults)
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

        public void ProcessInput(string line, Dictionary<int, List<int>> trainingData, List<int> expectedResults)
        {
            string[] items;

            int i = trainingData.Count;
            trainingData.Add(i, new List<int>(64));
            items = line.Split(',');
            for (int j = 0; j < 64; j++)
            {
                trainingData[i].Add(int.Parse(items[j]));
                Console.Write(trainingData[i][j]);
                Console.Write(" ");
            }
            Console.WriteLine();
            expectedResults.Add(int.Parse(items[64]));
            //Console.WriteLine(line);
        }

        public void Train()
        {
            double oldAverageError = 10;
            double newAverageError = 9;
            int i = 1;
            while(newAverageError < oldAverageError){
                Console.WriteLine("Training round number {0}", i);
                oldAverageError = newAverageError;
                newAverageError = Training.TrainNetwork();
                Console.WriteLine("Average error = {0}", newAverageError);
                i++;
            }
        }
    }
}
