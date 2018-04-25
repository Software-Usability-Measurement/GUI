using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tobii.EyeX.Framework;
using EyeXFramework;
using EyeXFramework.Forms;

namespace CSCI298
{


    public partial class MainPage : Form
    {
        // countervar to keep track of the time
        bool flag = false;
        int countervar;
        // appcounter will close the application after 20 runs
        int appcounter = 40;
        List<int> counterList = new List<int>();
        // list contaning all the items
        List<string> item_name = new List<string>();
        // Random initializer
        Random r = new Random();
        List<int> unqList;
        List<string> copytofile = new List<string>();
        string pathname;
        int turn;
        int turn_code;
        System.IO.StreamWriter write;
        public string MyFilePath { get; set; }
        
        public MainPage()
        {
            InitializeComponent();
            Program.EyeXHost.Connect(behaviorMap1);
            behaviorMap1.Add(label1, new GazeAwareBehavior(onGazeLabel) );
            behaviorMap1.Add(label2, new GazeAwareBehavior(onGazeLabel) );
            behaviorMap1.Add(label3, new GazeAwareBehavior(onGazeLabel) );
            behaviorMap1.Add(label4, new GazeAwareBehavior(onGazeLabel) );
            behaviorMap1.Add(label5, new GazeAwareBehavior(onGazeLabel) );
            behaviorMap1.Add(label6, new GazeAwareBehavior(onGazeLabel) );
            Program.EyeXHost.Connect(behaviorMap2);
            behaviorMap2.Add(pictureBox1, new GazeAwareBehavior(onGazepictureBox) );
            behaviorMap2.Add(pictureBox2, new GazeAwareBehavior(onGazepictureBox) );
            behaviorMap2.Add(pictureBox3, new GazeAwareBehavior(onGazepictureBox) );
            behaviorMap2.Add(pictureBox4, new GazeAwareBehavior(onGazepictureBox) );
            behaviorMap2.Add(pictureBox5, new GazeAwareBehavior(onGazepictureBox) );
            behaviorMap2.Add(pictureBox6, new GazeAwareBehavior(onGazepictureBox) );

        }
        private void onGazeLabel(object sender, GazeAwareEventArgs e)
        {
            
            var label = sender as Label;
            if (label != null)
            {
                // label.Font = (e.HasGaze)? new Font(label.Font, FontStyle.Underline) : new Font(label.Font, FontStyle.Bold);
            }
        }

        private void onGazepictureBox(object sender, GazeAwareEventArgs e)
        {
            var pic = sender as PictureBox;
            if (pic != null)
            {
                // pic.BackColor = (e.HasGaze) ? Color.Black : Color.DarkGray;
            }
        }
        

        private void MainPage_Load(object sender, EventArgs e)
        {
            item_name.Add("airplanes");
            item_name.Add("anchor");
            item_name.Add("ant");
            item_name.Add("buddha");
            item_name.Add("butterfly");
            item_name.Add("camera");
            item_name.Add("cars");
            item_name.Add("cat");
            item_name.Add("cellphone");
            item_name.Add("chair");
            item_name.Add("crocodile");
            item_name.Add("cup");
            item_name.Add("dog");
            item_name.Add("dollar_bill");
            item_name.Add("dolphin");
            item_name.Add("elephant");
            item_name.Add("headphone");
            item_name.Add("helicopter");
            item_name.Add("kangaroo");
            item_name.Add("lamp");
            item_name.Add("laptop");
            item_name.Add("lotus");
            item_name.Add("Motorbikes");
            item_name.Add("person");
            item_name.Add("pigeon");
            item_name.Add("pizza");
            item_name.Add("pyramid");
            item_name.Add("rhino");
            item_name.Add("scorpion");
            item_name.Add("soccer_ball");
            item_name.Add("starfish");
            item_name.Add("stop_sign");
            item_name.Add("sunflower");
            item_name.Add("watch");
            counterList = UniqueRandomNumber(40, 40);
//            counterList.ForEach(x => Console.Write(x + " "));
//            Console.WriteLine(counterList.Count);
            pathname = this.MyFilePath;
//            Console.WriteLine("The Path is: - " + pathname);
            write = new System.IO.StreamWriter(pathname, true);
            copytofile.Add("Start Recording Data");
            turn = counterList.IndexOf(appcounter - 1);
            flag = false;
            countervar = 4;
            setDefaultValues();
           
        }


        GazePointDataStream lightlyFilteredGazeDataStream = Program._eyeHost.CreateGazePointDataStream(GazePointDataMode.LightlyFiltered);

        private void setDefaultValues() 
        {

            if (appcounter > 0)
            {

                try
                {
                    DialogResult dlgResult = MessageBox.Show("Hi, get ready for the Trial Number: " + (40 - appcounter) + "/40", "Trial Number: - " + (40 - appcounter), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Console.WriteLine(dlgResult);
                    countervar = 4;
                    timer1.Start();
                    Program._eyeHost.Start();
                    unqList = UniqueRandomNumber(30, 6);
                    //int turn = counterList.IndexOf(appcounter-1);
                    // Write the data to the list
                    lightlyFilteredGazeDataStream.Next += (s, eye) => copytofile.Add((40 - appcounter).ToString() + "  " + turn + "  " + eye.X.ToString() + "  " + eye.Y.ToString() + "  " + eye.Timestamp.ToString());
                    appcounter = appcounter - 1;
                    if (turn < 20)
                    {
                        // display start screen with trial number
                        displayCorrectValues();
                        // display end screen
                    }
                    else if(turn >= 20 && turn < 28)
                    {
                        // display start screen with trial number
                        displayonePermute();
                        // display end screen
                    }
                    else if(turn >=28 && turn < 34)
                    {
                        // display start screen with trial number
                        displayShuffleValues();
                        // display end screen
                    }
                    else
                    {
                        // display start screen with trial number
                        displayWrongValues();
                        // display end screen
                    }
                    unqList.Clear();
                }
                catch (Exception e)
                {
                    DialogResult dlgResult = MessageBox.Show("Hi, get ready for the Trial Number: " + (40 - appcounter) + "/40", "Trial Number: - " + (40 - appcounter), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Console.WriteLine(dlgResult);
                    countervar = 4;
                    timer1.Start();
                    Program._eyeHost.Start();
                    unqList = UniqueRandomNumber(30, 6);
                    //int turn = counterList.IndexOf(appcounter-1);
                    // Write the data to the list
                    lightlyFilteredGazeDataStream.Next += (s, eye) => copytofile.Add((40 - appcounter).ToString() + "  " + turn + "  " + eye.X.ToString() + "  " + eye.Y.ToString() + "  " + eye.Timestamp.ToString());
                    appcounter = appcounter - 1;
                    if (turn < 20)
                    {
                        // display start screen with trial number
                        displayCorrectValues();
                        // display end screen
                    }
                    else if (turn >= 20 && turn < 28)
                    {
                        // display start screen with trial number
                        displayonePermute();
                        // display end screen
                    }
                    else if (turn >= 28 && turn < 34)
                    {
                        // display start screen with trial number
                        displayShuffleValues();
                        // display end screen
                    }
                    else
                    {
                        // display start screen with trial number
                        displayWrongValues();
                        // display end screen
                    }
                    unqList.Clear();
                    Console.WriteLine("Exception" + e.ToString() + "occured in iteration number: - " + appcounter + "in setDefaultValues function");
                }
            }
            else
            {
                System.Windows.Forms.Application.Exit();
                storeInFile();
            }
        }

        // fiunction to display the unique number list
        private List<int> UniqueRandomNumber(int maxRange, int totalRandomNumber)
        {
            List<int> noList = new List<int>();
            int count = 0;

            List<int> listRange = new List<int>();
            for (int i = 0; i < totalRandomNumber; i++)
            {
                listRange.Add(i);
            }
            while (listRange.Count > 0)
            {
                int item = r.Next(0, maxRange);
                if (!noList.Contains(item) && listRange.Count > 0)
                {
                    noList.Add(item);
                    listRange.Remove(count);
                    count++;
                }
            }
            return noList;
        }

        // display the correct values for the labels
        private void displayCorrectValues()
        {
            int imgNumber = r.Next(10, 34);
            try
            {
                label1.Text = item_name[unqList[0]];
                label2.Text = item_name[unqList[1]];
                label3.Text = item_name[unqList[2]];
                label4.Text = item_name[unqList[3]];
                label5.Text = item_name[unqList[4]];
                label6.Text = item_name[unqList[5]];
                pictureBox1.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[unqList[0]] + "\\image_00" + imgNumber.ToString() + ".jpg");
                pictureBox2.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[unqList[1]] + "\\image_00" + imgNumber.ToString() + ".jpg");
                pictureBox3.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[unqList[2]] + "\\image_00" + imgNumber.ToString() + ".jpg");
                pictureBox4.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[unqList[3]] + "\\image_00" + imgNumber.ToString() + ".jpg");
                pictureBox5.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[unqList[4]] + "\\image_00" + imgNumber.ToString() + ".jpg");
                pictureBox6.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[unqList[5]] + "\\image_00" + imgNumber.ToString() + ".jpg");
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                pictureBox1.Visible = true;
                pictureBox2.Visible = true;
                pictureBox3.Visible = true;
                pictureBox4.Visible = true;
                pictureBox5.Visible = true;
                pictureBox6.Visible = true;
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                imgNumber = r.Next(10, 34);
                label1.Text = item_name[0];
                label2.Text = item_name[1];
                label3.Text = item_name[2];
                label4.Text = item_name[3];
                label5.Text = item_name[4];
                label6.Text = item_name[5];
                pictureBox1.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[0] + "\\image_00" + imgNumber.ToString() + ".jpg");
                pictureBox2.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[1] + "\\image_00" + imgNumber.ToString() + ".jpg");
                pictureBox3.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[2] + "\\image_00" + imgNumber.ToString() + ".jpg");
                pictureBox4.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[3] + "\\image_00" + imgNumber.ToString() + ".jpg");
                pictureBox5.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[4] + "\\image_00" + imgNumber.ToString() + ".jpg");
                pictureBox6.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[5] + "\\image_0004.jpg");
                Console.WriteLine("Exception" + e.ToString() + "occured in iteration number: - " + appcounter);
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                pictureBox1.Visible = true;
                pictureBox2.Visible = true;
                pictureBox3.Visible = true;
                pictureBox4.Visible = true;
                pictureBox5.Visible = true;
                pictureBox6.Visible = true;
            }
            copytofile.Add("start");
        }

        private List<int> Shuffle(List<int> unqList)
        {
            int n = unqList.Count;
            while (n > 1)
            {
                int k = (r.Next(0, n) % n);
                n--;
                int value = unqList[k];
                unqList[k] = unqList[n];
                unqList[n] = value;
            }
            return unqList;
        }


        // display shuffeled values for the labels
        private void displayShuffleValues()
        {
            int imgNumber = r.Next(10, 34);
            try
            {
                label1.Text = item_name[unqList[0]];
                label2.Text = item_name[unqList[1]];
                label3.Text = item_name[unqList[2]];
                label4.Text = item_name[unqList[3]];
                label5.Text = item_name[unqList[4]];
                label6.Text = item_name[unqList[5]];
                unqList = Shuffle(unqList);
                pictureBox1.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[unqList[0]] + "\\image_00" + imgNumber.ToString() + ".jpg");
                pictureBox2.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[unqList[1]] + "\\image_00" + imgNumber.ToString() + ".jpg");
                pictureBox3.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[unqList[2]] + "\\image_00" + imgNumber.ToString() + ".jpg");
                pictureBox4.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[unqList[3]] + "\\image_00" + imgNumber.ToString() + ".jpg");
                pictureBox5.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[unqList[4]] + "\\image_00" + imgNumber.ToString() + ".jpg");
                pictureBox6.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[unqList[5]] + "\\image_00" + imgNumber.ToString() + ".jpg");
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                pictureBox1.Visible = true;
                pictureBox2.Visible = true;
                pictureBox3.Visible = true;
                pictureBox4.Visible = true;
                pictureBox5.Visible = true;
                pictureBox6.Visible = true;
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                imgNumber = r.Next(10, 34);
                label1.Text = item_name[5];
                label2.Text = item_name[4];
                label3.Text = item_name[3];
                label4.Text = item_name[2];
                label5.Text = item_name[1];
                label6.Text = item_name[0];
                unqList = Shuffle(unqList);
                pictureBox1.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[0] + "\\image_00" + imgNumber.ToString() + ".jpg");
                pictureBox2.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[1] + "\\image_00" + imgNumber.ToString() + ".jpg");
                pictureBox3.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[2] + "\\image_00" + imgNumber.ToString() + ".jpg");
                pictureBox4.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[3] + "\\image_00" + imgNumber.ToString() + ".jpg");
                pictureBox5.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[4] + "\\image_00" + imgNumber.ToString() + ".jpg");
                pictureBox6.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[5] + "\\image_00" + imgNumber.ToString() + ".jpg");
                Console.WriteLine("Exception" + e.ToString() + "occured in iteration number: - " + appcounter);
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                pictureBox1.Visible = true;
                pictureBox2.Visible = true;
                pictureBox3.Visible = true;
                pictureBox4.Visible = true;
                pictureBox5.Visible = true;
                pictureBox6.Visible = true;
            }
            copytofile.Add("start");
        }

        // display 1 novelty value
        private void displayWrongValues()
        {
            int imgNumber = r.Next(10, 34);
            try
            {
                label1.Text = item_name[unqList[0]];
                label2.Text = item_name[unqList[1]];
                label3.Text = item_name[unqList[2]];
                label4.Text = item_name[unqList[3]];
                label5.Text = item_name[unqList[4]];
                label6.Text = item_name[unqList[5]];
                unqList[unqList.FindIndex(ind => ind.Equals(unqList.Max()))] = r.Next(0, 6);
                pictureBox1.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[unqList[0]] + "\\image_00" + imgNumber.ToString() + ".jpg");
                pictureBox2.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[unqList[1]] + "\\image_00" + imgNumber.ToString() + ".jpg");
                pictureBox3.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[unqList[2]] + "\\image_00" + imgNumber.ToString() + ".jpg");
                pictureBox4.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[unqList[3]] + "\\image_00" + imgNumber.ToString() + ".jpg");
                pictureBox5.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[unqList[4]] + "\\image_00" + imgNumber.ToString() + ".jpg");
                pictureBox6.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[unqList[5]] + "\\image_00" + imgNumber.ToString() + ".jpg");
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                pictureBox1.Visible = true;
                pictureBox2.Visible = true;
                pictureBox3.Visible = true;
                pictureBox4.Visible = true;
                pictureBox5.Visible = true;
                pictureBox6.Visible = true;
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                imgNumber = r.Next(10, 34);
                label1.Text = item_name[0];
                label2.Text = item_name[1];
                label3.Text = item_name[2];
                label4.Text = item_name[3];
                label5.Text = item_name[4];
                label6.Text = item_name[5];
                unqList[unqList.FindIndex(ind => ind.Equals(unqList.Max()))] = r.Next(31, 34);
                pictureBox1.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[10] + "\\image_00" + imgNumber.ToString() + ".jpg");
                pictureBox2.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[11] + "\\image_00" + imgNumber.ToString() + ".jpg");
                pictureBox3.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[12] + "\\image_00" + imgNumber.ToString() + ".jpg");
                pictureBox4.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[13] + "\\image_00" + imgNumber.ToString() + ".jpg");
                pictureBox5.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[14] + "\\image_00" + imgNumber.ToString() + ".jpg");
                pictureBox6.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[15] + "\\image_00" + imgNumber.ToString() + ".jpg");
                Console.WriteLine("Exception" + e.ToString() + "occured in iteration number: - " + appcounter);
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                pictureBox1.Visible = true;
                pictureBox2.Visible = true;
                pictureBox3.Visible = true;
                pictureBox4.Visible = true;
                pictureBox5.Visible = true;
                pictureBox6.Visible = true;
            }
            copytofile.Add("start");
        }


        //display 1 permutation values
        private void displayonePermute()
        {
            int imgNumber = r.Next(10, 34);
            try
            {
                label1.Text = item_name[unqList[0]];
                label2.Text = item_name[unqList[1]];
                label3.Text = item_name[unqList[2]];
                label4.Text = item_name[unqList[3]];
                label5.Text = item_name[unqList[4]];
                label6.Text = item_name[unqList[5]];
                pictureBox1.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[unqList[0]] + "\\image_00" + imgNumber.ToString() + ".jpg");
                pictureBox2.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[unqList[1]] + "\\image_00" + imgNumber.ToString() + ".jpg");
                pictureBox3.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[unqList[2]] + "\\image_00" + imgNumber.ToString() + ".jpg");
                pictureBox4.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[unqList[3]] + "\\image_00" + imgNumber.ToString() + ".jpg");
                pictureBox5.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[unqList[5]] + "\\image_00" + imgNumber.ToString() + ".jpg");
                pictureBox6.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[unqList[4]] + "\\image_00" + imgNumber.ToString() + ".jpg");
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                pictureBox1.Visible = true;
                pictureBox2.Visible = true;
                pictureBox3.Visible = true;
                pictureBox4.Visible = true;
                pictureBox5.Visible = true;
                pictureBox6.Visible = true;
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                imgNumber = r.Next(10, 34);
                label1.Text = item_name[0];
                label2.Text = item_name[1];
                label3.Text = item_name[2];
                label4.Text = item_name[3];
                label5.Text = item_name[4];
                label6.Text = item_name[5];
                pictureBox1.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[0] + "\\image_00" + imgNumber.ToString() + ".jpg");
                pictureBox2.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[1] + "\\image_00" + imgNumber.ToString() + ".jpg");
                pictureBox3.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[2] + "\\image_00" + imgNumber.ToString() + ".jpg");
                pictureBox4.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[3] + "\\image_00" + imgNumber.ToString() + ".jpg");
                pictureBox5.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[5] + "\\image_00" + imgNumber.ToString() + ".jpg");
                pictureBox6.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[4] + "\\image_00" + imgNumber.ToString() + ".jpg");
                Console.WriteLine("Exception" + e.ToString() + "occured in iteration number: - " + appcounter);
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                pictureBox1.Visible = true;
                pictureBox2.Visible = true;
                pictureBox3.Visible = true;
                pictureBox4.Visible = true;
                pictureBox5.Visible = true;
                pictureBox6.Visible = true;
            }
            copytofile.Add("start");
        }


        private void storeInFile()
        {
            Program._eyeHost.Dispose();
            foreach (String s in copytofile)
            {
                write.WriteLine(s);
            }
            write.Close();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            if (countervar > 0)
            {
                // diplay the new timer by upgrading the countervar variable
                countervar = countervar - 1;
                timenumber.Text = countervar + "seconds";

                //turn_code = 0;
            }
            else
            {
                // if user ran out of time
                timer1.Stop();
                Program._eyeHost.Dispose();
                timenumber.Text = "Time's up!";
                //                MessageBox.Show("Time is Up");
                //                timer1.Stop();
                try
                {
                    label1.Visible = false;
                    label2.Visible = false;
                    label3.Visible = false;
                    label4.Visible = false;
                    label5.Visible = false;
                    label6.Visible = false;
                    pictureBox1.Visible = false;
                    pictureBox2.Visible = false;
                    pictureBox3.Visible = false;
                    pictureBox4.Visible = false;
                    pictureBox5.Visible = false;
                    pictureBox6.Visible = false;
                    DialogResult dlgResult = MessageBox.Show("Was the UI Congruent??", "!!Question!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    Console.WriteLine(dlgResult);
                    if((3-countervar) != -1)
                    {
                        copytofile.Add("Stop" + "  " + (3 - countervar).ToString() + "  " + dlgResult);
                    }
                    turn = counterList.IndexOf(appcounter - 1);
                    //turn_code = -1;
                    flag = true;
                    /*
                    label1.Visible = true;
                    label2.Visible = true;
                    label3.Visible = true;
                    label4.Visible = true;
                    label5.Visible = true;
                    label6.Visible = true;
                    pictureBox1.Visible = true;
                    pictureBox2.Visible = true;
                    pictureBox3.Visible = true;
                    pictureBox4.Visible = true;
                    pictureBox5.Visible = true;
                    pictureBox6.Visible = true;
                    */
                    setDefaultValues();

                }
                catch (Exception exp)
                {
                    label1.Visible = false;
                    label2.Visible = false;
                    label3.Visible = false;
                    label4.Visible = false;
                    label5.Visible = false;
                    label6.Visible = false;
                    pictureBox1.Visible = false;
                    pictureBox2.Visible = false;
                    pictureBox3.Visible = false;
                    pictureBox4.Visible = false;
                    pictureBox5.Visible = false;
                    pictureBox6.Visible = false;
                    DialogResult dlgResult = MessageBox.Show("Was the UI Congruent??", "!!Question!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    Console.WriteLine(dlgResult);
                    if ((3 - countervar) != -1)
                    {
                        copytofile.Add("Stop" + "  " + (3 - countervar).ToString() + "  " + dlgResult);
                    }
                    turn = counterList.IndexOf(appcounter - 1);
                    //turn_code = -1;
                    /*
                    label1.Visible = true;
                    label2.Visible = true;
                    label3.Visible = true;
                    label4.Visible = true;
                    label5.Visible = true;
                    label6.Visible = true;
                    pictureBox1.Visible = true;
                    pictureBox2.Visible = true;
                    pictureBox3.Visible = true;
                    pictureBox4.Visible = true;
                    pictureBox5.Visible = true;
                    pictureBox6.Visible = true;
                    */
                    setDefaultValues();


                }

            }
        }

        // space bar click to skip the current window 
        private void MainPage_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Space)
                {
                    timer1.Stop();
                    Program._eyeHost.Dispose();
                    timenumber.Text = "";
                    label1.Visible = false;
                    label2.Visible = false;
                    label3.Visible = false;
                    label4.Visible = false;
                    label5.Visible = false;
                    label6.Visible = false;
                    pictureBox1.Visible = false;
                    pictureBox2.Visible = false;
                    pictureBox3.Visible = false;
                    pictureBox4.Visible = false;
                    pictureBox5.Visible = false;
                    pictureBox6.Visible = false;
                    DialogResult dlgResult = MessageBox.Show("Was the UI Congruent??", "!!Question!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    Console.WriteLine(dlgResult);
                    if ((3 - countervar) != -1)
                    {
                        copytofile.Add("Stop" + "  " + (3 - countervar).ToString() + "  " + dlgResult);
                    }
                    turn = counterList.IndexOf(appcounter - 1);
                    flag = true;
                    /*

                    */
                    setDefaultValues();
                }
                if (e.KeyCode == Keys.Escape)
                {
                    System.Windows.Forms.Application.Exit();
                }

            }
            catch (Exception e12)
            {
                if (e.KeyCode == Keys.Space)
                {
                    timer1.Stop();
                    Program._eyeHost.Dispose();
                    timenumber.Text = "";
                    label1.Visible = false;
                    label2.Visible = false;
                    label3.Visible = false;
                    label4.Visible = false;
                    label5.Visible = false;
                    label6.Visible = false;
                    pictureBox1.Visible = false;
                    pictureBox2.Visible = false;
                    pictureBox3.Visible = false;
                    pictureBox4.Visible = false;
                    pictureBox5.Visible = false;
                    pictureBox6.Visible = false;
                    DialogResult dlgResult = MessageBox.Show("Was the UI Congruent??", "!!Question!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    Console.WriteLine(dlgResult);
                    if ((3 - countervar) != -1)
                    {
                        copytofile.Add("Stop" + "  " + (3 - countervar).ToString() + "  " + dlgResult);
                    }
                    turn = counterList.IndexOf(appcounter - 1);
                    flag = true;
                    /*
                    label1.Visible = true;
                    label2.Visible = true;
                    label3.Visible = true;
                    label4.Visible = true;
                    label5.Visible = true;
                    label6.Visible = true;
                    pictureBox1.Visible = true;
                    pictureBox2.Visible = true;
                    pictureBox3.Visible = true;
                    pictureBox4.Visible = true;
                    pictureBox5.Visible = true;
                    pictureBox6.Visible = true;
                    */
                    setDefaultValues();
                }
                if (e.KeyCode == Keys.Escape)
                {
                    System.Windows.Forms.Application.Exit();
                }
            }
        }

        /*

        // mouse movement on the labels
        private void label1_MouseEnter(object sender, EventArgs e)
        {
            label1.Font = new Font(label1.Font, FontStyle.Underline);
        }


        private void label2_MouseEnter(object sender, EventArgs e)
        {
            label2.Font = new Font(label1.Font, FontStyle.Underline);
        }

        private void label3_MouseEnter(object sender, EventArgs e)
        {
            label3.Font = new Font(label1.Font, FontStyle.Underline);
        }

        private void label4_MouseEnter(object sender, EventArgs e)
        {
            label4.Font = new Font(label1.Font, FontStyle.Underline);
        }

        private void label5_MouseEnter(object sender, EventArgs e)
        {
            label5.Font = new Font(label1.Font, FontStyle.Underline);
        }

        private void label6_MouseEnter(object sender, EventArgs e)
        {
            label6.Font = new Font(label1.Font, FontStyle.Underline);
        }

        private void labels_MouseLeave(object sender, EventArgs e)
        {
            label1.Font = new Font(label1.Font, FontStyle.Bold);
            label2.Font = new Font(label2.Font, FontStyle.Bold);
            label3.Font = new Font(label3.Font, FontStyle.Bold);
            label4.Font = new Font(label4.Font, FontStyle.Bold);
            label5.Font = new Font(label5.Font, FontStyle.Bold);
            label6.Font = new Font(label6.Font, FontStyle.Bold);
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.Black;
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.Black;
        }
        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.Black;
        }
        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            pictureBox4.BackColor = Color.Black;
        }
        private void pictureBox5_MouseEnter(object sender, EventArgs e)
        {
            pictureBox5.BackColor = Color.Black;
        }
        private void pictureBox6_MouseEnter(object sender, EventArgs e)
        {
            pictureBox6.BackColor = Color.Black;
        }

        private void pictureBoxs_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.DarkGray;
            pictureBox2.BackColor = Color.DarkGray;
            pictureBox3.BackColor = Color.DarkGray;
            pictureBox4.BackColor = Color.DarkGray;
            pictureBox5.BackColor = Color.DarkGray;
            pictureBox6.BackColor = Color.DarkGray;
        }
        */
        private void operation_MouseClick(object sender, MouseEventArgs e)
        {
            Console.WriteLine(MousePosition.X + "  " + MousePosition.Y);
        }


    }
}
