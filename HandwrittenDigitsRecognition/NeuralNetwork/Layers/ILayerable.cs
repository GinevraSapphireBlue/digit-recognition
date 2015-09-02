using System.Collections.Generic;

namespace HandwrittenDigitsRecognition.NeuralNetwork
{
    interface ILayerable
    {
        public List<IOutputable> GetElements();
    }
}
