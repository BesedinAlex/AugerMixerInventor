using System;
using System.Windows.Forms;

namespace AugerMixer
{
    /// <summary>
    /// Contains functions that are used in all forms.
    /// </summary>
    public static class Auto
    {
        /// <summary>
        /// Returns value entered to textBox, if it's double.
        /// </summary>
        /// <param name="textBox">
        /// Where it checks for possible mistakes.
        /// </param>
        /// <param name="defaultDouble">
        /// Returns it, if you make mistake.
        /// </param>
        public static double CheckTextBoxChange(TextBox textBox, double defaultDouble)
        {
            try
            {
                return Convert.ToDouble(textBox.Text);
            }
            catch
            {
                MessageBox.Show("При вводе числа была допущена ошибка (например, была введена буква). Вернулось первоначальное значение '" + defaultDouble + "'.", "Ошибка при заполнении поля");
                textBox.Text = Convert.ToString(defaultDouble);
                return defaultDouble;
            }
        }
        /// <summary>
        /// Checks if Inventor is active and connects to it.
        /// </summary>
        /// <returns>
        /// Returns connection to Inventor. If there's no connection, returns null.
        /// </returns>
        public static Inventor.Application AppActivity(Inventor.Application app)
        {
            try
            {
                return (Inventor.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Inventor.Application");
            }
            catch
            {
                InventorControl IC = new InventorControl();
                IC.ShowDialog();
                return null;
            }
        }
        /// <summary>
        /// Saves the .ipt.
        /// </summary>
        /// <param name="app">
        /// Links Inventor.
        /// </param>
        public static void SavePart(Inventor.Application app, SaveFileDialog saveFileDialog, Form form, InventorAPI api)
        {
            app = AppActivity(app);
            if (app == null)
                return;
            try
            {
                saveFileDialog.Filter = "Inventor Part Document|*.ipt";
                saveFileDialog.Title = api.getLongName();
                saveFileDialog.FileName = api.getPartDoc().DisplayName;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    if (!string.IsNullOrWhiteSpace(saveFileDialog.FileName))
                    {
                        api.getPartDoc().SaveAs(saveFileDialog.FileName, false);
                        api.setFileName(saveFileDialog.FileName);
                    }
                form.Close();
            }
            catch
            {
                MessageBox.Show("Деталь ещё не создана, либо программа не видит созданную деталь.", form.Text);
            }
        }
    }
}
