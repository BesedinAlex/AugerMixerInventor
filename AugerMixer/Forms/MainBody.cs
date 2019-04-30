using System;
using System.Windows.Forms;

namespace AugerMixer.Forms
{
    public partial class MainBody : Form
    {
        private Model.InventorAPI api;
        private Inventor.Application app = null;
        public MainBody()
        {
            InitializeComponent();
            textBox1.Text = Convert.ToString(Model.Parts.MainBody.H);
            textBox2.Text = Convert.ToString(Model.Parts.MainBody.D);
            textBox3.Text = Convert.ToString(Model.Parts.MainBody.B);
            textBox4.Text = Convert.ToString(Model.Parts.MainBody.A);
            textBox5.Text = Convert.ToString(Model.Parts.MainBody.T);
            textBox7.Text = Convert.ToString(Model.Parts.MainBody.Rb);
            textBox8.Text = Convert.ToString(Model.Parts.MainBody.Rm);
            textBox10.Text = Convert.ToString(Model.Parts.MainBody.Ds);
        }
        private void Build_Click(object sender, EventArgs e)
        {
            app = Auto.AppActivity(app);
            if (app == null)
                return;
            api = new Model.InventorAPI(app, "КК", "Конический корпус");
            Model.Parts.MainBody.Build(api, Text);
        }
        private void Save_Click(object sender, EventArgs e) =>
            Auto.SavePart(app, saveFileDialog1, this, api);
        private void TextBox1_TextChanged(object sender, EventArgs e) =>
            Model.Parts.MainBody.H = Auto.CheckTextBoxChange(textBox1, 1635);
        private void TextBox2_TextChanged(object sender, EventArgs e) =>
            Model.Parts.MainBody.D = Auto.CheckTextBoxChange(textBox2, 1530);
        private void TextBox3_TextChanged(object sender, EventArgs e) =>
            Model.Parts.MainBody.B = Auto.CheckTextBoxChange(textBox3, Model.Parts.MainBody.D / 2 / 2);
        private void TextBox4_TextChanged(object sender, EventArgs e) =>
            Model.Parts.MainBody.A = Auto.CheckTextBoxChange(textBox4, 15);
        private void TextBox5_TextChanged(object sender, EventArgs e) =>
            Model.Parts.MainBody.T = Auto.CheckTextBoxChange(textBox5, 40);
        private void TextBox7_TextChanged(object sender, EventArgs e) =>
            Model.Parts.MainBody.Rb = Auto.CheckTextBoxChange(textBox7, 730);
        private void TextBox8_TextChanged(object sender, EventArgs e) =>
            Model.Parts.MainBody.Rm = Auto.CheckTextBoxChange(textBox8, 24);
        private void TextBox10_TextChanged(object sender, EventArgs e) =>
            Model.Parts.MainBody.Ds = Auto.CheckTextBoxChange(textBox10, (Model.Parts.MainBody.D / 2 - Model.Parts.MainBody.B) / 2);
    }
}