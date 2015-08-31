using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandwrittenDigitsRecognition.NeuralNetwork
{
    class SigmoidNeuron : Neuron
    {
        private double lambda;
        protected double Lambda
        {
            set { lambda = value; }
            get { return lambda; }
        }

        /* CONSTRUCTOR */
        public SigmoidNeuron(int lambdaValue = 1) : base()
        {
            Lambda = lambdaValue;
        }

        /* IMPLEMENTATIONS OF ACTIVATION FUNCTION AND ITS DERIVATIVE */
        protected double ActivationFunction(double input)
        {
            /* Sigmoid activation function */
            return 1 / (Math.Exp(-Lambda * input) + 1);
        }

        protected double DerivativeOfActivationFunction(double weight)
        {
            double fx = ActivationFunction(weight);
            return Lambda * fx * (1 - fx);
        }
    }
}
