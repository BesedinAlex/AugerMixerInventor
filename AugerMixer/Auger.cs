using System;
using System.Windows.Forms;

namespace AugerMixer
{
    public partial class Auger : Form
    {
        private InventorAPI api;
        private Inventor.Application app = null;
        public static double H = 1700, D = 220, D1 = 100, H1 = 120, H2 = 1300, T = 25, H3 = 1500, A = 40;
        public Auger()
        {
            InitializeComponent();
            textBox1.Text = Convert.ToString(D);
            textBox2.Text = Convert.ToString(D1);
            textBox3.Text = Convert.ToString(H);
            textBox4.Text = Convert.ToString(H1);
            textBox5.Text = Convert.ToString(H2);
            textBox6.Text = Convert.ToString(T);
            textBox7.Text = Convert.ToString(H3);
            textBox8.Text = Convert.ToString(A);
        }
        private void Build_Click(object sender, EventArgs e)
        {
            app = Auto.AppActivity(app);
            if (app == null)
                return;
            api = new InventorAPI(app, "Шнек", "Шнек");
            Build.Screw(api, Text);
        }
        private void Save_Click(object sender, EventArgs e) =>
            Auto.SavePart(app, saveFileDialog1, this, api);
        private void textBox1_TextChanged(object sender, EventArgs e) =>
            D = Auto.CheckTextBoxChange(textBox1, 220);
        private void textBox2_TextChanged(object sender, EventArgs e) =>
            D1 = Auto.CheckTextBoxChange(textBox2, 100);
        private void textBox3_TextChanged(object sender, EventArgs e) =>
            H = Auto.CheckTextBoxChange(textBox3, 1600);
        private void textBox4_TextChanged(object sender, EventArgs e) =>
            H1 = Auto.CheckTextBoxChange(textBox4, 120);
        private void textBox5_TextChanged(object sender, EventArgs e) =>
            H2 = Auto.CheckTextBoxChange(textBox5, 1300);
        private void textBox6_TextChanged(object sender, EventArgs e) =>
            T = Auto.CheckTextBoxChange(textBox6, 25);
        private void textBox7_TextChanged(object sender, EventArgs e) =>
            H3 = Auto.CheckTextBoxChange(textBox7, 1500);
        private void textBox8_TextChanged(object sender, EventArgs e) =>
            A = Auto.CheckTextBoxChange(textBox8, 40);
    }
}