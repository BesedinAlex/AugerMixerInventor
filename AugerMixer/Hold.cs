using System;
using System.Windows.Forms;

namespace AugerMixer
{
    public partial class Hold : Form
    {
        private InventorAPI api;
        private Inventor.Application app = null;
        public static double b = 100, a1 = 65, h = 120, S1 = 4, a = 45, K = 10, K1 = 25, b1 = 50, b2 = 45, c = 40, d6 = 12, a2 = 40, h1 = 8;
        public Hold()
        {
            InitializeComponent();
            comboBox1.Items.AddRange(new object[] { 100, 160, 255 });
            comboBox1.SelectedIndex = 0;
        }
        private void Build_Click(object sender, EventArgs e)
        {
            app = Auto.AppActivity(app);
            if (app == null)
                return;
            api = new InventorAPI(app, "БО", "Боковые опоры");
            Build.Hold(api, Text);
        }
        private void Save_Click(object sender, EventArgs e) =>
            Auto.SavePart(app, saveFileDialog1, this, api);
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    b = 100;
                    a1 = 65;
                    h = 120;
                    S1 = 4;
                    a = 45;
                    K = 10;
                    K1 = 25;
                    b1 = 50;
                    b2 = 45;
                    c = 40;
                    d6 = 12;
                    a2 = 40;
                    h1 = 8;
                    break;
                case 1:
                    b = 160;
                    a1 = 95;
                    h = 190;
                    S1 = 5;
                    a = 75;
                    K = 15;
                    K1 = 40;
                    b1 = 70;
                    b2 = 65;
                    c = 50;
                    d6 = 12;
                    a2 = 60;
                    h1 = 10;
                    break;
                case 2:
                    b = 255;
                    a1 = 155;
                    h = 310;
                    S1 = 8;
                    a = 125;
                    K = 25;
                    K1 = 65;
                    b1 = 120;
                    b2 = 115;
                    c = 90;
                    d6 = 20;
                    a2 = 100;
                    h1 = 16;
                    break;
            }
            textBox1.Text = Convert.ToString(a1);
            textBox2.Text = Convert.ToString(h);
            textBox3.Text = Convert.ToString(S1);
            textBox4.Text = Convert.ToString(a);
            textBox5.Text = Convert.ToString(K);
            textBox6.Text = Convert.ToString(K1);
            textBox7.Text = Convert.ToString(b1);
            textBox8.Text = Convert.ToString(b2);
            textBox9.Text = Convert.ToString(c);
            textBox10.Text = Convert.ToString(d6);
            textBox11.Text = Convert.ToString(a2);
            textBox12.Text = Convert.ToString(h1);
        }
    }
}