using System;
using System.Windows.Forms;

namespace BesedinCoursework
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
            app = Auto.appActivity(app);
            if (app == null)
                return;
            api = new InventorAPI(app, "КВ", "Корпус для выгрузки материалов");
            Build.bottom(api, Text);
        }
        private void Save_Click(object sender, EventArgs e) =>
            Auto.savePartFunction(app, saveFileDialog1, this, api);
        private void textBox2_TextChanged(object sender, EventArgs e) =>
            D1 = Auto.checkTextBoxChange(textBox2, 440);
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            H1 = Auto.checkTextBoxChange(textBox3, 30);
            textBox1.Text = Convert.ToString(H1 + H2 + HB + H3);
        }
        private void textBox4_TextChanged(object sender, EventArgs e) =>
            DR = Auto.checkTextBoxChange(textBox4, 395);
        private void textBox5_TextChanged(object sender, EventArgs e) =>
            oR = Auto.checkTextBoxChange(textBox5, 11);
        private void textBox6_TextChanged(object sender, EventArgs e) =>
            D2 = Auto.checkTextBoxChange(textBox6, 350);
        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            H2 = Auto.checkTextBoxChange(textBox7, 50);
            textBox1.Text = Convert.ToString(H1 + H2 + HB + H3);
        }
        private void textBox8_TextChanged(object sender, EventArgs e) =>
            L1 = Auto.checkTextBoxChange(textBox8, 480);
        private void textBox9_TextChanged(object sender, EventArgs e) =>
            L2 = Auto.checkTextBoxChange(textBox9, 440);
        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            HB = Auto.checkTextBoxChange(textBox10, 250);
            textBox1.Text = Convert.ToString(H1 + H2 + HB + H3);
        }
        private void textBox11_TextChanged(object sender, EventArgs e) =>
            T = Auto.checkTextBoxChange(textBox11, 15);
        private void textBox12_TextChanged(object sender, EventArgs e) =>
            D3 = Auto.checkTextBoxChange(textBox12, 250);
        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            H3 = Auto.checkTextBoxChange(textBox13, 90);
            textBox1.Text = Convert.ToString(H1 + H2 + HB + H3);
        }
        private void textBox14_TextChanged(object sender, EventArgs e) =>
            H4 = Auto.checkTextBoxChange(textBox14, 400);
        private void textBox15_TextChanged(object sender, EventArgs e) =>
            D4D = Auto.checkTextBoxChange(textBox15, 30);
        private void textBox16_TextChanged(object sender, EventArgs e) =>
            D4d = Auto.checkTextBoxChange(textBox16, 20);
        private void textBox17_TextChanged(object sender, EventArgs e) =>
            A = Auto.checkTextBoxChange(textBox17, 40);
        private void textBox18_TextChanged(object sender, EventArgs e) =>
            H5 = Auto.checkTextBoxChange(textBox18, 205);
        private void textBox19_TextChanged(object sender, EventArgs e) =>
            D51 = Auto.checkTextBoxChange(textBox19, 200);
        private void textBox20_TextChanged(object sender, EventArgs e) =>
            L51 = Auto.checkTextBoxChange(textBox20, 20);
        private void textBox21_TextChanged(object sender, EventArgs e) =>
            D52 = Auto.checkTextBoxChange(textBox21, 245);
        private void textBox22_TextChanged(object sender, EventArgs e) =>
            L52 = Auto.checkTextBoxChange(textBox22, 45);
        private void textBox23_TextChanged(object sender, EventArgs e) =>
            D53 = Auto.checkTextBoxChange(textBox23, 100);
        private void textBox24_TextChanged(object sender, EventArgs e) =>
            L53 = Auto.checkTextBoxChange(textBox24, 22.5);
        private void textBox25_TextChanged(object sender, EventArgs e) =>
            D54 = Auto.checkTextBoxChange(textBox25, 122.5);
        private void textBox26_TextChanged(object sender, EventArgs e) =>
            L54 = Auto.checkTextBoxChange(textBox26, 35);
    }
}