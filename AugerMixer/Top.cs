using System;
using System.Windows.Forms;

namespace AugerMixer
{
    public partial class Top : Form
    {
        private InventorAPI api;
        private Inventor.Application app = null;
        public static double D = 1530, D1 = 1400, T = 40, H = 200, A = 40, ACount = 8; // Основа крышки
        public static double HR = 305, hR = 30, RR = 580, DR = 180, dR = 120, OR = 75, oR = 18, TR = 100; // Резервные проходы
        public static double MBRb = 730, MBRm = 24; // Отверстия под болты
        public static double D1R = 380, d1R = 260, T1R = 240, o1R = 22, O1R = 168, R1R = 540; // Загрузка сыпучих материалов
        public Top()
        {
            InitializeComponent();
            textBox1.Text = Convert.ToString(D);
            textBox2.Text = Convert.ToString(D1);
            textBox3.Text = Convert.ToString(H);
            textBox4.Text = Convert.ToString(T);
            textBox5.Text = Convert.ToString(A);
            textBox6.Text = Convert.ToString(ACount);
            textBox7.Text = Convert.ToString(MBRb);
            textBox8.Text = Convert.ToString(MBRm);
            textBox9.Text = Convert.ToString(RR);
            textBox10.Text = Convert.ToString(HR);
            textBox11.Text = Convert.ToString(DR);
            textBox12.Text = Convert.ToString(hR);
            textBox13.Text = Convert.ToString(dR);
            textBox14.Text = Convert.ToString(TR);
            textBox15.Text = Convert.ToString(OR);
            textBox16.Text = Convert.ToString(oR);
            textBox17.Text = Convert.ToString(R1R);
            textBox18.Text = Convert.ToString(D1R);
            textBox19.Text = Convert.ToString(d1R);
            textBox20.Text = Convert.ToString(T1R);
            textBox21.Text = Convert.ToString(O1R);
            textBox22.Text = Convert.ToString(o1R);
        }
        private void Build_Click(object sender, EventArgs e)
        {
            app = Auto.AppActivity(app);
            if (app == null)
                return;
            api = new InventorAPI(app, "К", "Крышка");
            Build.Top(api, Text);
        }
        private void Save_Click(object sender, EventArgs e) =>
            Auto.SavePart(app, saveFileDialog1, this, api);
        private void TextBox1_TextChanged(object sender, EventArgs e) =>
            D = Auto.CheckTextBoxChange(textBox1, 1530);
        private void TextBox2_TextChanged(object sender, EventArgs e) =>
            D1 = Auto.CheckTextBoxChange(textBox2, 1340);
        private void TextBox3_TextChanged(object sender, EventArgs e) =>
            H = Auto.CheckTextBoxChange(textBox3, 200);
        private void TextBox4_TextChanged(object sender, EventArgs e) =>
            T = Auto.CheckTextBoxChange(textBox4, 40);
        private void TextBox5_TextChanged(object sender, EventArgs e) =>
            A = Auto.CheckTextBoxChange(textBox5, 40);
        private void TextBox6_TextChanged(object sender, EventArgs e) =>
            ACount = Auto.CheckTextBoxChange(textBox6, 8);
        private void TextBox7_TextChanged(object sender, EventArgs e) =>
            MBRb = Auto.CheckTextBoxChange(textBox7, 730);
        private void TextBox8_TextChanged(object sender, EventArgs e) =>
            MBRm = Auto.CheckTextBoxChange(textBox8, 24);
        private void TextBox9_TextChanged(object sender, EventArgs e) =>
            RR = Auto.CheckTextBoxChange(textBox9, 580);
        private void TextBox10_TextChanged(object sender, EventArgs e) =>
            HR = Auto.CheckTextBoxChange(textBox10, 305);
        private void TextBox11_TextChanged(object sender, EventArgs e) =>
            DR = Auto.CheckTextBoxChange(textBox11, 180);
        private void TextBox12_TextChanged(object sender, EventArgs e) =>
            hR = Auto.CheckTextBoxChange(textBox12, 30);
        private void TextBox13_TextChanged(object sender, EventArgs e) =>
            dR = Auto.CheckTextBoxChange(textBox13, 120);
        private void TextBox14_TextChanged(object sender, EventArgs e) =>
            TR = Auto.CheckTextBoxChange(textBox14, 100);
        private void TextBox15_TextChanged(object sender, EventArgs e) =>
            OR = Auto.CheckTextBoxChange(textBox15, 75);
        private void TextBox16_TextChanged(object sender, EventArgs e) =>
            oR = Auto.CheckTextBoxChange(textBox16, 18);
        private void TextBox17_TextChanged(object sender, EventArgs e) =>
            R1R = Auto.CheckTextBoxChange(textBox17, 540);
        private void TextBox18_TextChanged(object sender, EventArgs e) =>
            D1R = Auto.CheckTextBoxChange(textBox18, 380);
        private void TextBox19_TextChanged(object sender, EventArgs e) =>
            d1R = Auto.CheckTextBoxChange(textBox19, 260);
        private void TextBox20_TextChanged(object sender, EventArgs e) =>
            T1R = Auto.CheckTextBoxChange(textBox20, 240);
        private void TextBox21_TextChanged(object sender, EventArgs e) =>
            O1R = Auto.CheckTextBoxChange(textBox21, 168);
        private void TextBox22_TextChanged(object sender, EventArgs e) =>
            o1R = Auto.CheckTextBoxChange(textBox22, 22);
    }
}