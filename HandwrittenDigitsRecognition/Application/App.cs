using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandwrittenDigitsRecognition.NeuralNetwork;

namespace HandwrittenDigitsRecognition.Application
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

        public App(double learnCoef = 1)
        {
            DigitNetwork = new Network(16, 2, 10);
            LearningCoef = learnCoef;
        }
    }
}
