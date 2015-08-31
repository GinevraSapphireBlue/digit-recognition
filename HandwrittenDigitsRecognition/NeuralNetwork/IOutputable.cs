using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandwrittenDigitsRecognition.NeuralNetwork
{
    interface IOutputable
    {
        public double GetOutput();
        public void AddConsumers(List<Neuron> consumers);
    }
}
