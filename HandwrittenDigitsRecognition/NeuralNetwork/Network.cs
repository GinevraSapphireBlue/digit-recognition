using HandwrittenDigitsRecognition.NeuralNetwork.Layers;
using System.Collections.Generic;

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

        private List<Layer> layers;
        public List<Layer> Layers
        {
            set { layers = value; }
            get { return layers; }
        }

        public SigmoidNeuronLayer OutputLayer
        {
            get { return (SigmoidNeuronLayer)Layers[LayerCount]; }
        }

        private int inputSize;
        public int InputSize { get; protected set; }

        public Network(int numOfInputs, int numOfLayers, int numOfNeurons)
        {
            InputSize = numOfInputs;
            /* numOfLayers - number of hidden layers;
             * LayerCount - number of hidden layers + output layer */
            LayerCount = numOfLayers + 1;
            Layers = new List<Layer>();
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
        public int GetResult()
        {
            int chosenDigit = 0;
            double largestOutput = OutputLayer.Neurons[0].GetOutput();
            for (int i = 1; i < 10; i++)
            {
                double currentNeuronOutput = OutputLayer.Neurons[i].GetOutput();
                if (currentNeuronOutput > largestOutput)
                {
                    chosenDigit = i;
                    largestOutput = currentNeuronOutput;
                }
            }
            return chosenDigit;
        }
        public void PropagateErrorsBack(double learningCoef, int expectedDigit)
        {
            /* Prepare array with expected values - 0s for all but the correct digit and 1 for the correct digit */
            int[] expectedValues = new int[10];
            for (int j = 0; j < 10; j++)
            {
                if (j == expectedDigit)
                    expectedValues[j] = 1;
                else
                    expectedValues[j] = 0;
            }

            /* Calculate errors for output layer */
            ((SigmoidNeuronLayer)Layers[LayerCount]).CalculateAllErrors(expectedValues);
            /* Calculate errors for hidden layers */
            for (int i = LayerCount - 1; i >= 0; i--)
                ((SigmoidNeuronLayer)Layers[i]).CalculateAllErrors();

            /* Update new weights for all layers, from last to first */
            for (int i = LayerCount; i >= 0; i--)
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
