using HandwrittenDigitsRecognition.NeuralNetwork.Neurons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandwrittenDigitsRecognition.NeuralNetwork.Layers
{
    abstract class Layer
    {
        public abstract List<Node> GetElements();
    }
}
