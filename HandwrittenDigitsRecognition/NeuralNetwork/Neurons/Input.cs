using HandwrittenDigitsRecognition.NeuralNetwork.Neurons;
using System.Collections.Generic;

namespace HandwrittenDigitsRecognition.NeuralNetwork.Neurons
{
    class Input : Node
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
        public void SetInputValue(int input)
        {
            InputValue = input;
        }

        /* IOutputable implementation */
        public double GetOutput()
        {
            return (double)InputValue;
        }
    }
}
