using System;
using System.Windows.Forms;
namespace BesedinCoursework
{
    public partial class InventorControl : Form
    {
        private Inventor.Application thisApplication = null;
        public InventorControl()
        {
            InitializeComponent();
            try
            {
                thisApplication = (Inventor.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Inventor.Application");
                if (thisApplication != null)
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
            inventorVersions.Items.AddRange(new object[] { "2015", "2016", "2017", "2018", "2019" });
            inventorVersions.Text = "2017";
        }
        private void launchTry(char drive)
        {
            System.Diagnostics.Process.Start(drive + "://Program Files/Autodesk/Inventor " + inventorVersions.Text.ToString() + "/Bin/Inventor.exe");
            Close();
        }
        private void InventorLaunch_Click(object sender, EventArgs e) // Кнопка запуска Inventor
        {
            try
            { 
                launchTry('C');
            }
            catch
            {
                try
                {
                    launchTry('D');
                }
                catch
                {
                    MessageBox.Show("Данная версия Inventor не была найдена на дисках C или D.", Text);
                }
            }
        }
    }
}
