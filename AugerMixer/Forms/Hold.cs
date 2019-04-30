using System;
using System.Windows.Forms;

namespace AugerMixer.Forms
{
    public partial class Hold : Form
    {
        private Model.InventorAPI api;
        private Inventor.Application app = null;
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
            api = new Model.InventorAPI(app, "БО", "Боковые опоры");
            Model.Parts.Hold.Build(api, Text);
        }
        private void Save_Click(object sender, EventArgs e) =>
            Auto.SavePart(app, saveFileDialog1, this, api);
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Model.Parts.Hold.ChooseGOSTValues(comboBox1.SelectedIndex);
            textBox1.Text = Convert.ToString(Model.Parts.Hold.A1);
            textBox2.Text = Convert.ToString(Model.Parts.Hold.H);
            textBox3.Text = Convert.ToString(Model.Parts.Hold.S1);
            textBox4.Text = Convert.ToString(Model.Parts.Hold.A);
            textBox5.Text = Convert.ToString(Model.Parts.Hold.K);
            textBox6.Text = Convert.ToString(Model.Parts.Hold.K1);
            textBox7.Text = Convert.ToString(Model.Parts.Hold.B1);
            textBox8.Text = Convert.ToString(Model.Parts.Hold.B2);
            textBox9.Text = Convert.ToString(Model.Parts.Hold.C);
            textBox10.Text = Convert.ToString(Model.Parts.Hold.D6);
            textBox11.Text = Convert.ToString(Model.Parts.Hold.A2);
            textBox12.Text = Convert.ToString(Model.Parts.Hold.H1);
        }
    }
}