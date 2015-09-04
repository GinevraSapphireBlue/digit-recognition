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

        private Dictionary<int, List<int>> testingData;
        public Dictionary<int, List<int>> TestingData
        {
            get { return testingData; }
            set { testingData = value; }
        }

        private List<int> expectedTestingResults;
        public List<int> ExpectedTestingResults
        {
            get { return expectedTestingResults; }
            set { expectedTestingResults = value; }
        }

        public App(int numOfLayers = 2, int numOfNeuronsInLayer = 10, double learnCoef = 1, int numberOfEpochs = 10)
        {
            DigitNetwork = new Network(64, numOfLayers, numOfNeuronsInLayer);
            LearningCoef = learnCoef;
            string fileTrain = "NeuronApp\\optdigits.tra";
            string fileTest = "NeuronApp\\optdigits.tes";

            TrainingData = new Dictionary<int, List<int>>();
            TestingData = new Dictionary<int, List<int>>();
            ExpectedTrainingResults = new List<int>();
            ExpectedTestingResults = new List<int>();

            Training = new NetworkTraining(DigitNetwork, fileTrain, LearningCoef, TrainingData, ExpectedTrainingResults);
            Training.TrainNetwork(numberOfEpochs);
            Console.WriteLine("Correct = {0}, Incorrect = {1}", Training.CountCorrect, Training.CountIncorrect);
            Console.WriteLine("Percent correct = {0}", 100 * ((double)Training.CountCorrect) / (Training.CountCorrect + Training.CountIncorrect));

            Testing = new NetworkTesting(DigitNetwork, fileTest, LearningCoef, TestingData, ExpectedTestingResults);
            Testing.TestNetwork();
            Console.WriteLine("Correct = {0}, Incorrect = {1}", Testing.CountCorrect, Testing.CountIncorrect);
            Console.WriteLine("Percent correct = {0}", 100 * ((double)Testing.CountCorrect) / (Testing.CountCorrect + Testing.CountIncorrect));
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
