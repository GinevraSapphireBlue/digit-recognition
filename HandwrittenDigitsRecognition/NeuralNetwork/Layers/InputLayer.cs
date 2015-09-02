using System.Collections.Generic;

namespace HandwrittenDigitsRecognition.NeuralNetwork
{
    class InputLayer : ILayerable
    {
        private List<Input> inputs;
        public List<Input> Inputs
        {
            set { inputs = value; }
            get { return inputs; }
        }

        public InputLayer(int numOfInputs)
        {
            Inputs = new List<Input>();
            /* Create numOfInputs inputs and set them initially to 0 */
            for(int i = 0; i < numOfInputs; i++)
            {
                Inputs.Add(new Input(0));
            }
        }

        public void SetInputs(int[] inputArray)
        {
            for (int i = 0; i < Inputs.Count; i++)
            {
                Inputs[i].SetInputValue(inputArray[i]);
            }
        }

        public List<Input> GetElements()
        {
            return Inputs;
        }
    }
}
