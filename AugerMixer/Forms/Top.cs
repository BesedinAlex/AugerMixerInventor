using System;
using System.Windows.Forms;

namespace AugerMixer.Forms
{
    public partial class Top : Form
    {
        private Model.InventorAPI api;
        private Inventor.Application app = null;
        
        public Top()
        {
            InitializeComponent();
            textBox1.Text = Convert.ToString(Model.Parts.Top.D);
            textBox2.Text = Convert.ToString(Model.Parts.Top.D1);
            textBox3.Text = Convert.ToString(Model.Parts.Top.H);
            textBox4.Text = Convert.ToString(Model.Parts.Top.T);
            textBox5.Text = Convert.ToString(Model.Parts.Top.A);
            textBox6.Text = Convert.ToString(Model.Parts.Top.ACount);
            textBox7.Text = Convert.ToString(Model.Parts.Top.MBRb);
            textBox8.Text = Convert.ToString(Model.Parts.Top.MBRm);
            textBox9.Text = Convert.ToString(Model.Parts.Top.RR);
            textBox10.Text = Convert.ToString(Model.Parts.Top.HR);
            textBox11.Text = Convert.ToString(Model.Parts.Top.DR);
            textBox12.Text = Convert.ToString(Model.Parts.Top.Hr);
            textBox13.Text = Convert.ToString(Model.Parts.Top.Dr);
            textBox14.Text = Convert.ToString(Model.Parts.Top.TR);
            textBox15.Text = Convert.ToString(Model.Parts.Top.OR);
            textBox16.Text = Convert.ToString(Model.Parts.Top.Or);
            textBox17.Text = Convert.ToString(Model.Parts.Top.R1R);
            textBox18.Text = Convert.ToString(Model.Parts.Top.D1R);
            textBox19.Text = Convert.ToString(Model.Parts.Top.D1r);
            textBox20.Text = Convert.ToString(Model.Parts.Top.T1R);
            textBox21.Text = Convert.ToString(Model.Parts.Top.O1R);
            textBox22.Text = Convert.ToString(Model.Parts.Top.O1r);
        }
        private void Build_Click(object sender, EventArgs e)
        {
            app = Auto.AppActivity(app);
            if (app == null)
                return;
            api = new Model.InventorAPI(app, "К", "Крышка");
            Model.Parts.Top.Build(api, Text);
        }
        private void Save_Click(object sender, EventArgs e) =>
            Auto.SavePart(app, saveFileDialog1, this, api);
        private void TextBox1_TextChanged(object sender, EventArgs e) =>
            Model.Parts.Top.D = Auto.CheckTextBoxChange(textBox1, 1530);
        private void TextBox2_TextChanged(object sender, EventArgs e) =>
            Model.Parts.Top.D1 = Auto.CheckTextBoxChange(textBox2, 1340);
        private void TextBox3_TextChanged(object sender, EventArgs e) =>
            Model.Parts.Top.H = Auto.CheckTextBoxChange(textBox3, 200);
        private void TextBox4_TextChanged(object sender, EventArgs e) =>
            Model.Parts.Top.T = Auto.CheckTextBoxChange(textBox4, 40);
        private void TextBox5_TextChanged(object sender, EventArgs e) =>
            Model.Parts.Top.A = Auto.CheckTextBoxChange(textBox5, 40);
        private void TextBox6_TextChanged(object sender, EventArgs e) =>
            Model.Parts.Top.ACount = Auto.CheckTextBoxChange(textBox6, 8);
        private void TextBox7_TextChanged(object sender, EventArgs e) =>
            Model.Parts.Top.MBRb = Auto.CheckTextBoxChange(textBox7, 730);
        private void TextBox8_TextChanged(object sender, EventArgs e) =>
            Model.Parts.Top.MBRm = Auto.CheckTextBoxChange(textBox8, 24);
        private void TextBox9_TextChanged(object sender, EventArgs e) =>
            Model.Parts.Top.RR = Auto.CheckTextBoxChange(textBox9, 580);
        private void TextBox10_TextChanged(object sender, EventArgs e) =>
            Model.Parts.Top.HR = Auto.CheckTextBoxChange(textBox10, 305);
        private void TextBox11_TextChanged(object sender, EventArgs e) =>
            Model.Parts.Top.DR = Auto.CheckTextBoxChange(textBox11, 180);
        private void TextBox12_TextChanged(object sender, EventArgs e) =>
            Model.Parts.Top.Hr = Auto.CheckTextBoxChange(textBox12, 30);
        private void TextBox13_TextChanged(object sender, EventArgs e) =>
            Model.Parts.Top.Dr = Auto.CheckTextBoxChange(textBox13, 120);
        private void TextBox14_TextChanged(object sender, EventArgs e) =>
            Model.Parts.Top.TR = Auto.CheckTextBoxChange(textBox14, 100);
        private void TextBox15_TextChanged(object sender, EventArgs e) =>
            Model.Parts.Top.OR = Auto.CheckTextBoxChange(textBox15, 75);
        private void TextBox16_TextChanged(object sender, EventArgs e) =>
            Model.Parts.Top.Or = Auto.CheckTextBoxChange(textBox16, 18);
        private void TextBox17_TextChanged(object sender, EventArgs e) =>
            Model.Parts.Top.R1R = Auto.CheckTextBoxChange(textBox17, 540);
        private void TextBox18_TextChanged(object sender, EventArgs e) =>
            Model.Parts.Top.D1R = Auto.CheckTextBoxChange(textBox18, 380);
        private void TextBox19_TextChanged(object sender, EventArgs e) =>
            Model.Parts.Top.D1r = Auto.CheckTextBoxChange(textBox19, 260);
        private void TextBox20_TextChanged(object sender, EventArgs e) =>
            Model.Parts.Top.T1R = Auto.CheckTextBoxChange(textBox20, 240);
        private void TextBox21_TextChanged(object sender, EventArgs e) =>
            Model.Parts.Top.O1R = Auto.CheckTextBoxChange(textBox21, 168);
        private void TextBox22_TextChanged(object sender, EventArgs e) =>
            Model.Parts.Top.O1r = Auto.CheckTextBoxChange(textBox22, 22);
    }
}