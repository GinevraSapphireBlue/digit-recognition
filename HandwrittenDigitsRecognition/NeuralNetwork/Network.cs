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

        private int inputSize;
        public int InputSize { get; protected set; }

        public Network(int numOfInputs, int numOfLayers, int numOfNeurons)
        {
            InputSize = numOfInputs;
            /* numOfLayers - number of hidden layers;
             * LayerCount - number of hidden layers + output layer */
            LayerCount = numOfLayers + 1;
            Layers = new List<ILayerable>();
            //Add input layer containing numOfInputs inputs, all set initially to 0
            Layers.Add(new InputLayer(InputSize));
            //Add neuron layers and link each layer to the previous one
            for (int i = 1; i < LayerCount; i++)
            {
                Layers.Add(new SigmoidNeuronLayer(numOfNeurons));
                ((SigmoidNeuronLayer)Layers[i]).LinkBackToLayer(Layers[i - 1]);
            }
            //Add final (output) neuron layer - 10 neurons for 10 digits
            Layers.Add(new SigmoidNeuronLayer(10));
            ((SigmoidNeuronLayer)Layers[LayerCount]).LinkBackToLayer(Layers[LayerCount - 1]);
        }

        /* LEARNING ELEMENTS */
        public void SetInputValues(int[] inputs)
        {
            ((InputLayer)Layers[0]).SetInputs(inputs);
        }
        public void FeedResultsForward()
        {
            /* Fire all neuron layers in order */
            for (int i = 1; i <= LayerCount; i++)
            {
                ((SigmoidNeuronLayer)Layers[i]).FireAll();
            }
        }
        public void PropagateErrorsBack(double learningCoef, int[] expectedValues)
        {
            /* Calculate errors and new weights for output layer */
            ((SigmoidNeuronLayer)Layers[LayerCount]).UpdateAllWeights(learningCoef, expectedValues);
            /* Calculate errors and new weights for hidden layers, from last to first */
            for (int i = LayerCount - 1; i >= 0; i--)
            {
                ((SigmoidNeuronLayer)Layers[i]).UpdateAllWeights(learningCoef);
            }
        }
        public void ResetAll()
        {
            for (int i = 1; i <= LayerCount; i++)
            {
                ((SigmoidNeuronLayer)Layers[i]).ResetAll();
            }
        }
    }
}
