using HandwrittenDigitsRecognition.NeuralNetwork.Neurons;
using System;
using System.Collections.Generic;

namespace HandwrittenDigitsRecognition.NeuralNetwork.Layers
{
    class SigmoidNeuronLayer : Layer
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

        /* SET UP CONNECTIONS TO PREVIOUS LAYER */
        public void LinkBackToLayer(Layer layer)
        {
            foreach (Neuron neuron in Neurons)
            {
                neuron.AddInput(layer.GetElements());
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
                //Console.Write("{0} ", n.GetOutput());
            }
            //Console.WriteLine();
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
                //Console.WriteLine("Output layer:");
                for (int i = 0; i < Neurons.Count; i++)
                {
                    Neurons[i].CalculateErrorCoef(expectedValues[i]);
                }
            }
            else /* Hidden layer */
            {
                //Console.WriteLine("Hidden layer:");
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
        public override List<Node> GetElements()
        {
            List<Node> elements = new List<Node>();
            elements.AddRange(Neurons);
            return elements;
        }
    }
}
