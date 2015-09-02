using HandwrittenDigitsRecognition.NeuralNetwork.Neurons;
using System.Collections.Generic;

namespace HandwrittenDigitsRecognition.NeuralNetwork.Layers
{
    class SigmoidNeuronLayer : Layer
    {
        private List<Node> neurons;
        public List<Node> Neurons
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

        /* SET UP CONNECTIONS TO PREVIOUS LAYER */
        public void LinkBackToLayer(Layer layer)
        {
            foreach (Neuron neuron in Neurons)
            {
                neuron.AddInputs(layer.GetElements());
            }
            foreach (Node element in layer.GetElements())
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
        public void CalculateAllErrors(int[] expectedValues = null)
        {
            if (expectedValues != null) /* Output layer */
            {
                for (int i = 0; i < Neurons.Count; i++)
                {
                    Neurons[i].CalculateErrorCoef(expectedValues[i]);
                }
            }
            else /* Hidden layer */
            {
                for (int i = 0; i < Neurons.Count; i++)
                {
                    Neurons[i].CalculateErrorCoef();
                }
            }
        }

        public void UpdateAllWeights(double learningCoef)
        {
            foreach (Neuron n in Neurons)
            {
                n.UpdateWeights(learningCoef);
            }
        }
        public List<Node> GetElements()
        {
            return Neurons;
        }
    }
}
