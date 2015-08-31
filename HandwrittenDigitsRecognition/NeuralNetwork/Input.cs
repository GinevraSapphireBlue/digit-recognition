using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandwrittenDigitsRecognition.NeuralNetwork
{
    class Input : IOutputable
    {
        /**
          * Represents single input to neural network.
        **/ 
        private int inputValue;
        public int InputValue
        {
            get { return inputValue; }
            set { inputValue = value; }
        }
        private List<Neuron> consumers;
        public List<Neuron> Consumers
        {
            set { consumers = value; }
            get { return consumers; }
        }

        public Input(int input){
            InputValue = input;
            Consumers = new List<Neuron>();
        }

        public void AddConsumers(List<Neuron> neurons)
        {
            Consumers.AddRange(neurons);
        }

        /* IOutputable implementation */
        public double GetOutput()
        {
            return (double)InputValue;
        }
    }
}
