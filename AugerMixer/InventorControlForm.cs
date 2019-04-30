using System;
using System.Windows.Forms;

namespace AugerMixer
{
    public partial class InventorControlForm : Form
    {
        private Inventor.Application app = null;
        public InventorControlForm()
        {
            InitializeComponent();
            try
            {
                app = (Inventor.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Inventor.Application");
                if (app != null)
                    label1.Text = "Inventor запущен.";
                inventorVersions.Enabled = false;
                InventorLaunch.Enabled = false;
            }
            catch
            {
                label1.Text = "Запустите Inventor!";
                inventorVersions.Enabled = true;
                InventorLaunch.Enabled = true;
            }
            inventorVersions.Items.AddRange(new object[] { "2015", "2016", "2017", "2018", "2019", "2020" });
            inventorVersions.Text = "2020";
        }
        private void LaunchTry(char drive)
        {
            System.Diagnostics.Process.Start(drive + "://Program Files/Autodesk/Inventor " + inventorVersions.Text.ToString() + "/Bin/Inventor.exe");
            Close();
        }
        private void InventorLaunch_Click(object sender, EventArgs e) // Кнопка запуска Inventor
        {
            try
            { 
                LaunchTry('C');
            }
            catch
            {
                try
                {
                    LaunchTry('D');
                }
                catch
                {
                    MessageBox.Show("Данная версия Inventor не была найдена на дисках C или D.", Text);
                }
            }
        }
    }
}