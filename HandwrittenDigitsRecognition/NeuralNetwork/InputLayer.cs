using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public InputLayer(int[] inputArray)
        {
            Inputs = new List<Input>();
            for(int i = 0; i < inputArray.Length; i++)
            {
                Inputs.Add(new Input(inputArray[i]));
            }
        }
        
        public List<Input> GetElements()
        {
            return Inputs;
        }
    }
}
