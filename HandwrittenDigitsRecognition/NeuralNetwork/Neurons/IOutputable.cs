using System.Collections.Generic;

namespace HandwrittenDigitsRecognition.NeuralNetwork
{
    interface IOutputable
    {
        public double GetOutput();
        public void AddConsumers(List<Neuron> consumers);
    }
}
