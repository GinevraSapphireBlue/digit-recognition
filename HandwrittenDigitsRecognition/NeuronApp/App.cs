using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandwrittenDigitsRecognition.NeuralNetwork;

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

        public App(int numOfLayers = 2, int numOfNeuronsInLayer = 10, double learnCoef = 1)
        {
            DigitNetwork = new Network(64, numOfLayers, numOfNeuronsInLayer);
            LearningCoef = learnCoef;

            Training = new NetworkTraining(DigitNetwork, "Data/optdigits.tra", LearningCoef);
            Testing = new NetworkTesting(DigitNetwork, "Data/optdigits.tes", LearningCoef);
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
