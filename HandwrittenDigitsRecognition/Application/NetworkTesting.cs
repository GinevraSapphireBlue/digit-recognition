using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandwrittenDigitsRecognition.NeuralNetwork;

namespace HandwrittenDigitsRecognition.Application
{
    class NetworkTesting
    {
        private Network digitNetwork;
        public Network DigitNetwork
        {
            get { return digitNetwork; }
            private set { digitNetwork = value; }
        }
        private string fileLocation;
        public string FileLocation { get; set; }

        public NetworkTesting(Network testNetwork, string file)
        {
            DigitNetwork = testNetwork;
            FileLocation = file;
        }
    }
}
