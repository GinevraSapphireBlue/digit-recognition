using HandwrittenDigitsRecognition.NeuronApp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HandwrittenDigitsRecognition
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int numberOfLayers = int.Parse(textBox1.Text);
            int numberOfNeurons = int.Parse(textBox2.Text);
            double learningCoefficient = double.Parse(textBox3.Text);
            int numberOfEpochs = int.Parse(textBox4.Text);
            NeuronApp.App myApp = new App(numberOfLayers, numberOfNeurons, learningCoefficient, numberOfEpochs);
            ResultLabel.Text = (100 * ((double)myApp.Testing.CountCorrect) / (myApp.Testing.CountCorrect + myApp.Testing.CountIncorrect)).ToString();
        }
    }
}
