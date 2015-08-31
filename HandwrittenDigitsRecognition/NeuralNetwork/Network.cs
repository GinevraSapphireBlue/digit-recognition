using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandwrittenDigitsRecognition.NeuralNetwork
{
    class Network
    {
        private int layerCount;
        public int LayerCount
        {
            set { layerCount = value; }
            get { return layerCount; }
        }
        private List<ILayerable> layers;
        public List<ILayerable> Layers
        {
            set { layers = value; }
            get { return layers; }
        }

        public Network(int[] inputs, int numOfLayers, int numOfNeurons)
        {
            LayerCount = numOfLayers;
            Layers = new List<ILayerable>();
            //Add input layer
            Layers.Add(new InputLayer(inputs));
            //Add neuron layers and link each to previous one
            for (int i = 1; i < LayerCount; i++)
            {
                Layers.Add(new SigmoidNeuronLayer(numOfNeurons));
                ((SigmoidNeuronLayer)Layers[i]).LinkBackToLayer(Layers[i - 1]);
            }
            //Add final (output) neuron layer - 10 neurons for 10 digits
            Layers.Add(new SigmoidNeuronLayer(10));
            ((SigmoidNeuronLayer)Layers[layerCount]).LinkBackToLayer(Layers[layerCount - 1]);
        }

        //TO DO - ADD LEARNING ELEMENTS TO NETWORK LEVEL
    }
}
