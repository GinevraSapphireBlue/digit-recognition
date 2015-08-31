using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandwrittenDigitsRecognition.NeuralNetwork
{
    class SigmoidNeuronLayer : ILayerable
    {
        private List<Neuron> neurons;
        public List<Neuron> Neurons
        {
            set { neurons = value; }
            get { return neurons; }
        }

        /* CONSTRUCTOR */
        public SigmoidNeuronLayer(int count)
        {
            Neurons = new List<Neuron>();
            for (int i = 0; i < count; i++)
            {
                Neurons.Add(new SigmoidNeuron());
            }
        }

        /* ILayerable implementation */
        public List<Neuron> GetElements()
        {
            return Neurons;
        }

        /* SET UP CONNECTIONS TO PREVIOUS LAYER */
        public void LinkBackToLayer(ILayerable layer)
        {
            foreach (Neuron neuron in Neurons)
            {
                neuron.AddInputs(layer.GetElements());
            }
            foreach (IOutputable element in layer.GetElements())
            {
                element.AddConsumers(Neurons);
            }
        }

        /* OPERATE ON ALL NEURONS IN LAYER */
        public void FireAll()
        {
            foreach (Neuron n in Neurons)
            {
                n.Fire();
            }
        }
        public void ResetAll()
        {
            foreach (Neuron n in Neurons)
            {
                n.Reset();
            }
        }
        public void UpdateAllWeights(double learningCoef)
        {
            foreach (Neuron n in Neurons)
            {
                n.UpdateWeights(learningCoef);
            }
        }
    }
}
