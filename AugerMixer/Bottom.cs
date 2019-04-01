using System;
using System.Windows.Forms;

namespace AugerMixer
{
    public partial class Bottom : Form
    {
        private InventorAPI api;
        private Inventor.Application app = null;
        public static double D1 = 440, H1 = 30, D2 = 350, H2 = 50, DR = 395, oR = 11; // Основание
        public static double L1 = 480, L2 = 440, T = 15, HB = 250, D3 = 250, H3 = 90; // "Коробка"
        public static double H4 = 400, D4D = 30, D4d = 20, A = 40; // Трубки
        public static double H5 = 205, D51 = 200, L51 = 20, D52 = 245, L52 = 45, D53 = 100, L53 = 22.5, D54 = 122.5, L54 = 35; // Крышка
        public Bottom()
        {
            InitializeComponent();
            textBox1.Text = Convert.ToString(H1 + H2 + HB + H3);
            textBox2.Text = Convert.ToString(D1);
            textBox3.Text = Convert.ToString(H1);
            textBox4.Text = Convert.ToString(DR);
            textBox5.Text = Convert.ToString(oR);
            textBox6.Text = Convert.ToString(D2);
            textBox7.Text = Convert.ToString(H2);
            textBox8.Text = Convert.ToString(L1);
            textBox9.Text = Convert.ToString(L2);
            textBox10.Text = Convert.ToString(HB);
            textBox11.Text = Convert.ToString(T);
            textBox12.Text = Convert.ToString(D3);
            textBox13.Text = Convert.ToString(H3);
            textBox14.Text = Convert.ToString(H4);
            textBox15.Text = Convert.ToString(D4D);
            textBox16.Text = Convert.ToString(D4d);
            textBox17.Text = Convert.ToString(A);
            textBox18.Text = Convert.ToString(H5);
            textBox19.Text = Convert.ToString(D51);
            textBox20.Text = Convert.ToString(L51);
            textBox21.Text = Convert.ToString(D52);
            textBox22.Text = Convert.ToString(L52);
            textBox23.Text = Convert.ToString(D53);
            textBox24.Text = Convert.ToString(L53);
            textBox25.Text = Convert.ToString(D54);
            textBox26.Text = Convert.ToString(L54);
        }
        private void Build_Click(object sender, EventArgs e)
        {
            app = Auto.AppActivity(app);
            if (app == null)
                return;
            api = new InventorAPI(app, "КВ", "Корпус для выгрузки материалов");
            Build.Bottom(api, Text);
        }
        private void Save_Click(object sender, EventArgs e) =>
            Auto.SavePart(app, saveFileDialog1, this, api);
        private void TextBox2_TextChanged(object sender, EventArgs e) =>
            D1 = Auto.CheckTextBoxChange(textBox2, 440);
        private void TextBox3_TextChanged(object sender, EventArgs e)
        {
            H1 = Auto.CheckTextBoxChange(textBox3, 30);
            textBox1.Text = Convert.ToString(H1 + H2 + HB + H3);
        }
        private void TextBox4_TextChanged(object sender, EventArgs e) =>
            DR = Auto.CheckTextBoxChange(textBox4, 395);
        private void TextBox5_TextChanged(object sender, EventArgs e) =>
            oR = Auto.CheckTextBoxChange(textBox5, 11);
        private void TextBox6_TextChanged(object sender, EventArgs e) =>
            D2 = Auto.CheckTextBoxChange(textBox6, 350);
        private void TextBox7_TextChanged(object sender, EventArgs e)
        {
            H2 = Auto.CheckTextBoxChange(textBox7, 50);
            textBox1.Text = Convert.ToString(H1 + H2 + HB + H3);
        }
        private void TextBox8_TextChanged(object sender, EventArgs e) =>
            L1 = Auto.CheckTextBoxChange(textBox8, 480);
        private void TextBox9_TextChanged(object sender, EventArgs e) =>
            L2 = Auto.CheckTextBoxChange(textBox9, 440);
        private void TextBox10_TextChanged(object sender, EventArgs e)
        {
            HB = Auto.CheckTextBoxChange(textBox10, 250);
            textBox1.Text = Convert.ToString(H1 + H2 + HB + H3);
        }
        private void TextBox11_TextChanged(object sender, EventArgs e) =>
            T = Auto.CheckTextBoxChange(textBox11, 15);
        private void TextBox12_TextChanged(object sender, EventArgs e) =>
            D3 = Auto.CheckTextBoxChange(textBox12, 250);
        private void TextBox13_TextChanged(object sender, EventArgs e)
        {
            H3 = Auto.CheckTextBoxChange(textBox13, 90);
            textBox1.Text = Convert.ToString(H1 + H2 + HB + H3);
        }
        private void TextBox14_TextChanged(object sender, EventArgs e) =>
            H4 = Auto.CheckTextBoxChange(textBox14, 400);
        private void TextBox15_TextChanged(object sender, EventArgs e) =>
            D4D = Auto.CheckTextBoxChange(textBox15, 30);
        private void TextBox16_TextChanged(object sender, EventArgs e) =>
            D4d = Auto.CheckTextBoxChange(textBox16, 20);
        private void TextBox17_TextChanged(object sender, EventArgs e) =>
            A = Auto.CheckTextBoxChange(textBox17, 40);
        private void TextBox18_TextChanged(object sender, EventArgs e) =>
            H5 = Auto.CheckTextBoxChange(textBox18, 205);
        private void TextBox19_TextChanged(object sender, EventArgs e) =>
            D51 = Auto.CheckTextBoxChange(textBox19, 200);
        private void TextBox20_TextChanged(object sender, EventArgs e) =>
            L51 = Auto.CheckTextBoxChange(textBox20, 20);
        private void TextBox21_TextChanged(object sender, EventArgs e) =>
            D52 = Auto.CheckTextBoxChange(textBox21, 245);
        private void TextBox22_TextChanged(object sender, EventArgs e) =>
            L52 = Auto.CheckTextBoxChange(textBox22, 45);
        private void TextBox23_TextChanged(object sender, EventArgs e) =>
            D53 = Auto.CheckTextBoxChange(textBox23, 100);
        private void TextBox24_TextChanged(object sender, EventArgs e) =>
            L53 = Auto.CheckTextBoxChange(textBox24, 22.5);
        private void TextBox25_TextChanged(object sender, EventArgs e) =>
            D54 = Auto.CheckTextBoxChange(textBox25, 122.5);
        private void TextBox26_TextChanged(object sender, EventArgs e) =>
            L54 = Auto.CheckTextBoxChange(textBox26, 35);
    }
}