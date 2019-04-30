using System;
using System.Windows.Forms;

namespace AugerMixer.Forms
{
    public partial class Assembly : Form
    {
        private Inventor.Application app = null;
        public Assembly() =>
            InitializeComponent();
        private void InventorControl_Click(object sender, EventArgs e) =>
            new InventorControl().ShowDialog();
        private void MainBody_Click(object sender, EventArgs e) =>
            new MainBody().ShowDialog();
        private void Top_Click(object sender, EventArgs e) =>
            new Top().ShowDialog();
        private void Auger_Click(object sender, EventArgs e) =>
            new Auger().ShowDialog();
        private void Bottom_Click(object sender, EventArgs e) =>
            new Bottom().ShowDialog();
        private void Hold_Click(object sender, EventArgs e) =>
            new Hold().ShowDialog();
        private void Build_Click(object sender, EventArgs e)
        {
            app = Auto.AppActivity(app);
            if (app == null)
                return;
            Model.Assembly.Build(app, openFileDialog1, Text);
        }
    }
}