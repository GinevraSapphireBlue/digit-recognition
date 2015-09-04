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
            double correctPercent = 0;

            NeuronApp.App myApp;
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;

            worker.DoWork += new DoWorkEventHandler(
                delegate(object o, DoWorkEventArgs args)
                {
                    BackgroundWorker bw = o as BackgroundWorker;

                    //Report that neural network started working
                    bw.ReportProgress(1);

                    //Start the network and do the real work
                    myApp = new App(numberOfLayers, numberOfNeurons, learningCoefficient, numberOfEpochs);
                    correctPercent = (100 * ((double)myApp.Testing.CountCorrect) / (myApp.Testing.CountCorrect + myApp.Testing.CountIncorrect));
                });

            worker.ProgressChanged += new ProgressChangedEventHandler(
                delegate(object o, ProgressChangedEventArgs args)
                {
                    if (args.ProgressPercentage == 1)
                    {
                        ResultLabel.Text = "Proszę o cierpliwość. Próbuję rozszyfrować ręczne pismo";
                        UseWaitCursor = true;
                    }
                });

            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
                delegate(object o, RunWorkerCompletedEventArgs args)
                {
                    StringBuilder sb = new StringBuilder("Udało mi się rozszyfrować ");
                    sb.Append(((int)correctPercent).ToString());
                    sb.Append("% cyfr.");
                    ResultLabel.Text = sb.ToString();
                    button1.Text = "Sprawdź mnie ponownie";
                    UseWaitCursor = false;
                });

            worker.RunWorkerAsync();
                    
            //NeuronApp.App myApp = new App(numberOfLayers, numberOfNeurons, learningCoefficient, numberOfEpochs);            
        }
    }
}
