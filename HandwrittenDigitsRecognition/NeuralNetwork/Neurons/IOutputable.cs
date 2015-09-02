using System.Collections.Generic;

namespace HandwrittenDigitsRecognition.NeuralNetwork
{
    interface IOutputable
    {
        double GetOutput();
        void AddConsumers(List<Neuron> consumers);
    }
}
