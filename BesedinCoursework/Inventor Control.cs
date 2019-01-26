using System;
using System.Windows.Forms;
namespace BesedinCoursework
{
    public partial class InventorControl : Form
    {
        private Inventor.Application ThisApplication = null; // Проверка активности Inventor
        public InventorControl()
        {
            InitializeComponent();
            try
            {
                ThisApplication = (Inventor.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Inventor.Application");
                if (ThisApplication != null)
                    label1.Text = "Inventor запущен.";
                InventorVersions.Enabled = false;
                InventorLaunch.Enabled = false;
            }
            catch
            {
                label1.Text = "Запустите Inventor!";
                InventorVersions.Enabled = true;
                InventorLaunch.Enabled = true;
            }
            InventorVersions.Items.AddRange(new object[] { "2015", "2016", "2017", "2018", "2019" });
            InventorVersions.Text = "2017";
        }
        
        private void InventorLaunch_Click(object sender, EventArgs e) // Кнопка запуска Inventor
        {
            try
            {
                System.Diagnostics.Process.Start("C://Program Files/Autodesk/Inventor " + InventorVersions.Text.ToString() + "/Bin/Inventor.exe");
                Close();
            }
            catch
            {
                try
                {
                    System.Diagnostics.Process.Start("D://Program Files/Autodesk/Inventor " + InventorVersions.Text.ToString() + "/Bin/Inventor.exe");
                    Close();
                }
                catch
                {
                    try
                    {
                        System.Diagnostics.Process.Start("E://Program Files/Autodesk/Inventor " + InventorVersions.Text.ToString() + "/Bin/Inventor.exe");
                        Close();
                    }
                    catch
                    {
                        MessageBox.Show("Данная версия Inventor не была найдена на дисках C, D, E.", "Запуск Inventor");
                    }
                }
            }
        }
    }
}
