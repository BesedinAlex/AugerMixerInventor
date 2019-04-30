using System;
using System.Windows.Forms;

namespace AugerMixer.Forms
{
    public partial class Bottom : Form
    {
        private Model.InventorAPI api;
        private Inventor.Application app = null;
        public Bottom()
        {
            InitializeComponent();
            textBox1.Text = Convert.ToString(Model.Parts.Bottom.H1 + Model.Parts.Bottom.H2 + Model.Parts.Bottom.HB + Model.Parts.Bottom.H3);
            textBox2.Text = Convert.ToString(Model.Parts.Bottom.D1);
            textBox3.Text = Convert.ToString(Model.Parts.Bottom.H1);
            textBox4.Text = Convert.ToString(Model.Parts.Bottom.DR);
            textBox5.Text = Convert.ToString(Model.Parts.Bottom.Or);
            textBox6.Text = Convert.ToString(Model.Parts.Bottom.D2);
            textBox7.Text = Convert.ToString(Model.Parts.Bottom.H2);
            textBox8.Text = Convert.ToString(Model.Parts.Bottom.L1);
            textBox9.Text = Convert.ToString(Model.Parts.Bottom.L2);
            textBox10.Text = Convert.ToString(Model.Parts.Bottom.HB);
            textBox11.Text = Convert.ToString(Model.Parts.Bottom.T);
            textBox12.Text = Convert.ToString(Model.Parts.Bottom.D3);
            textBox13.Text = Convert.ToString(Model.Parts.Bottom.H3);
            textBox14.Text = Convert.ToString(Model.Parts.Bottom.H4);
            textBox15.Text = Convert.ToString(Model.Parts.Bottom.D4D);
            textBox16.Text = Convert.ToString(Model.Parts.Bottom.D4d);
            textBox17.Text = Convert.ToString(Model.Parts.Bottom.A);
            textBox18.Text = Convert.ToString(Model.Parts.Bottom.H5);
            textBox19.Text = Convert.ToString(Model.Parts.Bottom.D51);
            textBox20.Text = Convert.ToString(Model.Parts.Bottom.L51);
            textBox21.Text = Convert.ToString(Model.Parts.Bottom.D52);
            textBox22.Text = Convert.ToString(Model.Parts.Bottom.L52);
            textBox23.Text = Convert.ToString(Model.Parts.Bottom.D53);
            textBox24.Text = Convert.ToString(Model.Parts.Bottom.L53);
            textBox25.Text = Convert.ToString(Model.Parts.Bottom.D54);
            textBox26.Text = Convert.ToString(Model.Parts.Bottom.L54);
        }
        private void Build_Click(object sender, EventArgs e)
        {
            app = Auto.AppActivity(app);
            if (app == null)
                return;
            api = new Model.InventorAPI(app, "КВ", "Корпус для выгрузки материалов");
            Model.Parts.Bottom.Build(api, Text);
        }
        private void Save_Click(object sender, EventArgs e) =>
            Auto.SavePart(app, saveFileDialog1, this, api);
        private void TextBox2_TextChanged(object sender, EventArgs e) =>
            Model.Parts.Bottom.D1 = Auto.CheckTextBoxChange(textBox2, 440);
        private void TextBox3_TextChanged(object sender, EventArgs e)
        {
            Model.Parts.Bottom.H1 = Auto.CheckTextBoxChange(textBox3, 30);
            textBox1.Text = Convert.ToString(Model.Parts.Bottom.H1 + Model.Parts.Bottom.H2 + Model.Parts.Bottom.HB + Model.Parts.Bottom.H3);
        }
        private void TextBox4_TextChanged(object sender, EventArgs e) =>
            Model.Parts.Bottom.DR = Auto.CheckTextBoxChange(textBox4, 395);
        private void TextBox5_TextChanged(object sender, EventArgs e) =>
            Model.Parts.Bottom.Or = Auto.CheckTextBoxChange(textBox5, 11);
        private void TextBox6_TextChanged(object sender, EventArgs e) =>
            Model.Parts.Bottom.D2 = Auto.CheckTextBoxChange(textBox6, 350);
        private void TextBox7_TextChanged(object sender, EventArgs e)
        {
            Model.Parts.Bottom.H2 = Auto.CheckTextBoxChange(textBox7, 50);
            textBox1.Text = Convert.ToString(Model.Parts.Bottom.H1 + Model.Parts.Bottom.H2 + Model.Parts.Bottom.HB + Model.Parts.Bottom.H3);
        }
        private void TextBox8_TextChanged(object sender, EventArgs e) =>
            Model.Parts.Bottom.L1 = Auto.CheckTextBoxChange(textBox8, 480);
        private void TextBox9_TextChanged(object sender, EventArgs e) =>
            Model.Parts.Bottom.L2 = Auto.CheckTextBoxChange(textBox9, 440);
        private void TextBox10_TextChanged(object sender, EventArgs e)
        {
            Model.Parts.Bottom.HB = Auto.CheckTextBoxChange(textBox10, 250);
            textBox1.Text = Convert.ToString(Model.Parts.Bottom.H1 + Model.Parts.Bottom.H2 + Model.Parts.Bottom.HB + Model.Parts.Bottom.H3);
        }
        private void TextBox11_TextChanged(object sender, EventArgs e) =>
            Model.Parts.Bottom.T = Auto.CheckTextBoxChange(textBox11, 15);
        private void TextBox12_TextChanged(object sender, EventArgs e) =>
            Model.Parts.Bottom.D3 = Auto.CheckTextBoxChange(textBox12, 250);
        private void TextBox13_TextChanged(object sender, EventArgs e)
        {
            Model.Parts.Bottom.H3 = Auto.CheckTextBoxChange(textBox13, 90);
            textBox1.Text = Convert.ToString(Model.Parts.Bottom.H1 + Model.Parts.Bottom.H2 + Model.Parts.Bottom.HB + Model.Parts.Bottom.H3);
        }
        private void TextBox14_TextChanged(object sender, EventArgs e) =>
            Model.Parts.Bottom.H4 = Auto.CheckTextBoxChange(textBox14, 400);
        private void TextBox15_TextChanged(object sender, EventArgs e) =>
            Model.Parts.Bottom.D4D = Auto.CheckTextBoxChange(textBox15, 30);
        private void TextBox16_TextChanged(object sender, EventArgs e) =>
            Model.Parts.Bottom.D4d = Auto.CheckTextBoxChange(textBox16, 20);
        private void TextBox17_TextChanged(object sender, EventArgs e) =>
            Model.Parts.Bottom.A = Auto.CheckTextBoxChange(textBox17, 40);
        private void TextBox18_TextChanged(object sender, EventArgs e) =>
            Model.Parts.Bottom.H5 = Auto.CheckTextBoxChange(textBox18, 205);
        private void TextBox19_TextChanged(object sender, EventArgs e) =>
            Model.Parts.Bottom.D51 = Auto.CheckTextBoxChange(textBox19, 200);
        private void TextBox20_TextChanged(object sender, EventArgs e) =>
            Model.Parts.Bottom.L51 = Auto.CheckTextBoxChange(textBox20, 20);
        private void TextBox21_TextChanged(object sender, EventArgs e) =>
            Model.Parts.Bottom.D52 = Auto.CheckTextBoxChange(textBox21, 245);
        private void TextBox22_TextChanged(object sender, EventArgs e) =>
            Model.Parts.Bottom.L52 = Auto.CheckTextBoxChange(textBox22, 45);
        private void TextBox23_TextChanged(object sender, EventArgs e) =>
            Model.Parts.Bottom.D53 = Auto.CheckTextBoxChange(textBox23, 100);
        private void TextBox24_TextChanged(object sender, EventArgs e) =>
            Model.Parts.Bottom.L53 = Auto.CheckTextBoxChange(textBox24, 22.5);
        private void TextBox25_TextChanged(object sender, EventArgs e) =>
            Model.Parts.Bottom.D54 = Auto.CheckTextBoxChange(textBox25, 122.5);
        private void TextBox26_TextChanged(object sender, EventArgs e) =>
            Model.Parts.Bottom.L54 = Auto.CheckTextBoxChange(textBox26, 35);
    }
}