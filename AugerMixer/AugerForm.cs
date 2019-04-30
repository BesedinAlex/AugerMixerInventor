using System;
using System.Windows.Forms;
using AugerMixer.Model.Parts;

namespace AugerMixer
{
    public partial class AugerForm : Form
    {
        private Model.InventorAPI api;
        private Inventor.Application app = null;
        public AugerForm()
        {
            InitializeComponent();
            textBox1.Text = Convert.ToString(Auger.D);
            textBox2.Text = Convert.ToString(Auger.D1);
            textBox3.Text = Convert.ToString(Auger.H);
            textBox4.Text = Convert.ToString(Auger.H1);
            textBox5.Text = Convert.ToString(Auger.H2);
            textBox6.Text = Convert.ToString(Auger.T);
            textBox7.Text = Convert.ToString(Auger.H3);
            textBox8.Text = Convert.ToString(Auger.A);
        }
        private void Build_Click(object sender, EventArgs e)
        {
            app = Auto.AppActivity(app);
            if (app == null)
                return;
            api = new Model.InventorAPI(app, "Шнек", "Шнек");
            Auger.Build(api, Text);
        }
        private void Save_Click(object sender, EventArgs e) =>
            Auto.SavePart(app, saveFileDialog1, this, api);
        private void TextBox1_TextChanged(object sender, EventArgs e) =>
            Auger.D = Auto.CheckTextBoxChange(textBox1, 220);
        private void TextBox2_TextChanged(object sender, EventArgs e) =>
            Auger.D1 = Auto.CheckTextBoxChange(textBox2, 100);
        private void TextBox3_TextChanged(object sender, EventArgs e) =>
            Auger.H = Auto.CheckTextBoxChange(textBox3, 1600);
        private void TextBox4_TextChanged(object sender, EventArgs e) =>
            Auger.H1 = Auto.CheckTextBoxChange(textBox4, 120);
        private void TextBox5_TextChanged(object sender, EventArgs e) =>
            Auger.H2 = Auto.CheckTextBoxChange(textBox5, 1300);
        private void TextBox6_TextChanged(object sender, EventArgs e) =>
            Auger.T = Auto.CheckTextBoxChange(textBox6, 25);
        private void TextBox7_TextChanged(object sender, EventArgs e) =>
            Auger.H3 = Auto.CheckTextBoxChange(textBox7, 1500);
        private void TextBox8_TextChanged(object sender, EventArgs e) =>
            Auger.A = Auto.CheckTextBoxChange(textBox8, 40);
    }
}