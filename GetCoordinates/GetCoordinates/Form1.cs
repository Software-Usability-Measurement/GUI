using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetCoordinates
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<string> item_name = new List<string>();
        private void Form1_Load(object sender, EventArgs e)
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
            label1.Text = item_name[ 0];
            label2.Text = item_name[ 1];
            label3.Text = item_name[ 2];
            label4.Text = item_name[ 3];
            label5.Text = item_name[ 4];
            label6.Text = item_name[ 5];
            pictureBox1.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[ 0] + "\\image_00" + 10.ToString() + ".jpg");
            pictureBox2.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[ 1] + "\\image_00" + 10.ToString() + ".jpg");
            pictureBox3.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[ 2] + "\\image_00" + 10.ToString() + ".jpg");
            pictureBox4.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[ 3] + "\\image_00" + 10.ToString() + ".jpg");
            pictureBox5.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[ 4] + "\\image_00" + 10.ToString() + ".jpg");
            pictureBox6.Image = Image.FromFile("K:\\resized_640_480\\" + item_name[ 5] + "\\image_00" + 10.ToString() + ".jpg");

        }
        

        private void MainPage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                System.Windows.Forms.Application.Exit();
            }
        }

        private void pictureBox1_MouseClick_1(object sender, MouseEventArgs e)
        {
                Console.WriteLine(MousePosition.X + "  " + MousePosition.Y);
        }
    }
}
