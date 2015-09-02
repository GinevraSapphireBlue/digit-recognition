using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandwrittenDigitsRecognition.NeuralNetwork.Neurons
{
    abstract class Node
    {
        public abstract double GetOutput();
        public abstract void AddConsumers(List<Neuron> consumers);
    }
}
