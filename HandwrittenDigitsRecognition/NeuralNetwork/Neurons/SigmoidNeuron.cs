﻿using System;

namespace HandwrittenDigitsRecognition.NeuralNetwork.Neurons
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
        protected override double ActivationFunction(double input)
        {
            /* Sigmoid activation function */
            return 1 / (Math.Exp(-Lambda * input) + 1);
        }

        protected override double DerivativeOfActivationFunction(double input)
        {
            double fx = ActivationFunction(input);
            return Lambda * fx * (1 - fx);
        }
    }
}
