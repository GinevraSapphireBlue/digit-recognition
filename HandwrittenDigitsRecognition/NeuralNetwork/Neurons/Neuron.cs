using HandwrittenDigitsRecognition.NeuralNetwork.Neurons;
using System;
using System.Collections.Generic;

namespace HandwrittenDigitsRecognition.NeuralNetwork.Neurons
{
    abstract class Neuron : Node
    {
        /**
          * Represents an abstract neuron, with no activation function given
        **/
        private double bias;
        public double Bias
        {
            get { return bias; }
            set { bias = value; }
        }

        private Dictionary<Node, double> inputs;
        public Dictionary<Node, double> Inputs
        {
            get { return inputs; }
            set { inputs = value; }
        }

        private List<Node> consumers;
        public List<Node> Consumers
        {
            get { return consumers; }
            set { consumers = value; }
        }

        private bool fired;
        protected bool Fired
        {
            get { return fired; }
            set { fired = value; }
        }

        private int expectedValue;
        protected int ExpectedValue
        {
            get { return expectedValue; }
            set { expectedValue = value; }
        }

        private double output;
        public double Output
        {
            protected set { output = value; }
            get {
                if (!fired)
                    Fire();
                return output;
            }
        }

        private bool errorCalculated;
        protected bool ErrorCalculated
        {
            get { return errorCalculated; }
            set { errorCalculated = value; }
        }

        private double errorCoef;
        public double ErrorCoef
        {
            protected set { errorCoef = value; }
            get
            {
                if (!errorCalculated)
                    CalculateErrorCoef();
                return errorCoef;
            }
        }

        private Random rand;
        protected Random Rand
        {
            get
            {
                if (rand == null)
                    rand = new Random();
                return rand;
            }
        }

        /* CONSTRUCTOR STUB */
        public Neuron()
        {
            Bias = Rand.NextDouble()/4;
            Inputs = new Dictionary<Node, double>();
            Consumers = new List<Node>();
            ExpectedValue = -1;
            Fired = false;
            ErrorCalculated = false;
        }

        /* SETTING INPUTS AND CONSUMERS */
        public void AddInput(Node input)
        {
            Inputs.Add(input, Rand.NextDouble()/4);
        }
        public void AddInputs(ICollection<Node> newInputs)
        {
            foreach (Node input in newInputs)
            {
                AddInput(input);
            }
        }
        public void AddConsumer(Neuron neuron)
        {
            Consumers.Add(neuron);
        }
        public void AddConsumers(List<Neuron> neurons)
        {
            Consumers.AddRange(neurons);
        }

        /* IOutputable implementation */
        public double GetOutput()
        {
            return Output;
        }

        /* BASIC FUNCTIONALITY */
        public void Fire()
        {
            Output = ActivationFunction(SumWeightedInputs());
            Fired = true;
        }

        public void CalculateErrorCoef(int expected = -1)
        {
            ExpectedValue = expected;
            double totalWeight = SumWeightedInputs();

            if (ExpectedValue == -1) /* Valid for hidden layers' neurons */
            {
                double totalWeightedErrorFromUp = 0;
                foreach (Neuron n in Consumers)
                {
                    totalWeightedErrorFromUp += n.GetWeight(this) * n.ErrorCoef;
                }
                ErrorCoef = DerivativeOfActivationFunction(totalWeight) * totalWeightedErrorFromUp;
            }
            else /* Valid for output layer neurons */
            {
                ErrorCoef = DerivativeOfActivationFunction(totalWeight) * (ExpectedValue - Output);
            }

            ErrorCalculated = true;
        }

        public void UpdateWeights(double learningCoef)
        {
            foreach (Node input in Inputs.Keys)
            {
                Inputs[input] += learningCoef * ErrorCoef * input.GetOutput();
            }
            Bias += learningCoef * ErrorCoef * Bias;
        }

        /* Setting neuron to not yet fired */
        public void Reset()
        {
            Fired = false;
            ErrorCalculated = false;
        }

        /* HELPERS */
        protected double SumWeightedInputs()
        {
            double weightedInput = 0;
            foreach (Node n in Inputs.Keys)
            {
                weightedInput += n.GetOutput() * Inputs[n];
            }
            weightedInput += Bias;
            return weightedInput;
        }
        protected double GetWeight(Node input)
        {
            return Inputs[input];
        }

        /* DEPENDENT ON TYPE OF NEURON - NOT IMPLEMENTED IN ABSTRACT NEURON */
        protected abstract double ActivationFunction(double input);
        protected abstract double DerivativeOfActivationFunction(double x);

    }
}
