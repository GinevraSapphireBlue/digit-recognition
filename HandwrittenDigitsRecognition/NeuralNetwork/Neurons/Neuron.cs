using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandwrittenDigitsRecognition.NeuralNetwork
{
    abstract class Neuron : IOutputable
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

        private Dictionary<IOutputable, double> inputs;
        public Dictionary<IOutputable, double> Inputs
        {
            get { return inputs; }
            set { inputs = value; }
        }

        private List<IOutputable> consumers;
        public List<IOutputable> Consumers
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
                    CalculateErrorCoef(ExpectedValue);
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
            Bias = 0;
            Inputs = new Dictionary<IOutputable, double>();
            Consumers = new List<IOutputable>();
            ExpectedValue = -1;
            Fired = false;
            ErrorCalculated = false;
        }

        /* SETTING INPUTS AND CONSUMERS */
        public void AddInput(IOutputable input)
        {
            Inputs.Add(input, Rand.NextDouble());
        }
        public void AddInputs(ICollection<IOutputable> newInputs)
        {
            foreach (IOutputable input in newInputs)
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
            Output = ActivationFunction(SumInputs());
            Fired = true;
        }

        public void UpdateWeights(double learningCoef, int expected = -1)
        {
            ExpectedValue = expected;
            foreach (IOutputable input in Inputs.Keys)
            {
                Inputs[input] *= 1.00 + learningCoef * ErrorCoef;
            }
            Bias *= 1.00 + learningCoef * ErrorCoef;
        }

        /* Setting neuron to not yet fired */
        public void Reset()
        {
            Fired = false;
            ErrorCalculated = false;
        }

        /* HELPERS */
        protected double SumInputs()
        {
            double weightedInput = 0;
            foreach (Neuron n in Inputs.Keys)
            {
                weightedInput += n.Output * Inputs[n];
            }
            weightedInput += Bias;
            return weightedInput;
        }
        protected double SumWeights()
        {
            double sum = 0;
            foreach (Neuron n in inputs.Keys)
                sum += inputs[n];
            return sum;
        }
        protected double GetWeight(IOutputable input)
        {
            return Inputs[input];
        }
        protected void CalculateErrorCoef(int expected = -1)
        {
            double totalWeight = SumWeights();
            double difference;

            if (expected == -1) /* Valid for hidden layers' neurons */
            {
                double totalErrorFromUp = 0;
                foreach (Neuron n in Consumers)
                {
                    totalErrorFromUp += n.ErrorCoef * n.GetWeight(this);
                }
                difference = totalErrorFromUp;
            }
            else /* Valid for output layer neurons */
            {
                difference = expected - Output;
            }

            ErrorCoef = DerivativeOfActivationFunction(totalWeight) * difference;
            ErrorCalculated = true;
        }

        /* DEPENDENT ON TYPE OF NEURON - NOT IMPLEMENTED IN ABSTRACT NEURON */
        protected double ActivationFunction(double input);
        protected double DerivativeOfActivationFunction(double x);

    }
}
