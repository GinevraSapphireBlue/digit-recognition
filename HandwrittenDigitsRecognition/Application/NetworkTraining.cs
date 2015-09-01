using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandwrittenDigitsRecognition.NeuralNetwork;

namespace HandwrittenDigitsRecognition.Application
{
    class NetworkTraining
    {
        private Network digitNetwork;
        public Network DigitNetwork
        {
            get { return digitNetwork; }
            private set { digitNetwork = value; }     
        }
        private string fileLocation;
        public string FileLocation { get; set; }

        public NetworkTraining(Network trainNetwork, string file)
        {
            DigitNetwork = trainNetwork;
            FileLocation = file;
        }
    }
}
