using System;
using System.Windows.Forms;

namespace AugerMixer
{
    public partial class AssemblyForm : Form
    {
        private Inventor.Application app = null;
        public AssemblyForm() =>
            InitializeComponent();
        private void InventorControl_Click(object sender, EventArgs e) =>
            new InventorControlForm().ShowDialog();
        private void MainBody_Click(object sender, EventArgs e) =>
            new MainBodyForm().ShowDialog();
        private void Top_Click(object sender, EventArgs e) =>
            new TopForm().ShowDialog();
        private void Auger_Click(object sender, EventArgs e) =>
            new AugerForm().ShowDialog();
        private void Bottom_Click(object sender, EventArgs e) =>
            new BottomForm().ShowDialog();
        private void Hold_Click(object sender, EventArgs e) =>
            new HoldForm().ShowDialog();
        private void Build_Click(object sender, EventArgs e)
        {
            app = Auto.AppActivity(app);
            if (app == null)
                return;
            Model.Assembly.Build(app, openFileDialog1, Text);
        }
    }
}