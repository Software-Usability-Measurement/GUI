using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tobii.EyeX.Framework;
using EyeXFramework;
using EyeXFramework.Forms;

namespace CSCI298
{
    public partial class StartPage : Form
    {
        Random random = new Random();
        private string filename;
        public string path;
        public StartPage()
        {
            InitializeComponent();
            Program._eyeHost.Start();
            Program.EyeXHost.Connect(behaviorMap1);
            behaviorMap1.Add(dog, new GazeAwareBehavior(onGazeLabel));
            behaviorMap1.Add(cat, new GazeAwareBehavior(onGazeLabel));
            Program.EyeXHost.Connect(behaviorMap2);
            behaviorMap2.Add(pictureBox1, new GazeAwareBehavior(onGazepictureBox));
            behaviorMap2.Add(pictureBox2, new GazeAwareBehavior(onGazepictureBox));
            
        }


        private void onGazeLabel(object sender, GazeAwareEventArgs e)
        {

            var label = sender as Label;
            if (label != null)
            {
                label.Font = (e.HasGaze) ? new Font(label.Font, FontStyle.Underline) : new Font(label.Font, FontStyle.Bold);
            }
        }

        private void onGazepictureBox(object sender, GazeAwareEventArgs e)
        {
            var pic = sender as PictureBox;
            if (pic != null)
            {
                pic.BackColor = (e.HasGaze) ? Color.Black : Color.DarkGray;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
//            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
        }

        /*
        private void dog_MouseEnter(object sender, EventArgs e)
        {
            dog.Font = new Font(dog.Font, FontStyle.Underline);
        }

        private void cat_MouseEnter(object sender, EventArgs e)
        {
            cat.Font = new Font(cat.Font, FontStyle.Underline);
        }

        private void animals_MouseLeave(object sender, EventArgs e)
        {
            cat.Font = new Font(cat.Font, FontStyle.Bold);
            dog.Font = new Font(dog.Font, FontStyle.Bold);
          
        }


        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.Black;
        }
        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.Black;
        }

        private void pictureBoxs_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.DarkGray;
            pictureBox2.BackColor = Color.DarkGray;
        }
        */

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime localDate = DateTime.Now;
            string theText = textBox1.Text;
            if(theText.Length == 0)
            {
                MessageBox.Show("Please Type your Name");
            }
            else
            {
                try
                {
                    //filename = theText + random.Next(1000, 9999).ToString() + random.Next(1000, 9999).ToString();
                    filename = theText + localDate.ToString("MM-dd-yyyy_hh-mm-ss-tt");
                    using (StreamWriter sw = File.AppendText(@"J:\\Rishabh\\Experiment_Results\\filename.txt"))
                    {
                        sw.WriteLine(filename);
                    }
                }
                catch (ArgumentOutOfRangeException exp)
                {
                    filename = theText + random.Next(1000, 9999).ToString() + random.Next(1000, 9999).ToString();
                    using (StreamWriter sw = File.AppendText(@"J:\\Rishabh\\Experiment_Results\\filename.txt"))
                    {
                        sw.WriteLine(filename);
                    }
                    Console.WriteLine(exp);
                }
                
                MessageBox.Show("Hello: - " + theText + "!!");
                path  = @"J:\\Rishabh\\Experiment_Results\\"+filename + ".txt";
                var myFile = File.Create(path);
                myFile.Close();
                Program._eyeHost.Dispose();
                MainPage mainPage = new MainPage();
                mainPage.MyFilePath = path;
                mainPage.Show();
                this.Hide();

            }
        }

    }
}
