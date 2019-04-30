using System;
using System.Windows.Forms;

namespace AugerMixer
{
    /// <summary>
    /// Common used methods.
    /// </summary>
    public static class Auto
    {
        /// <summary>
        /// Returns value entered to textBox, if itsn't wrong.
        /// Return defaul value otherwise.
        /// </summary>
        public static double CheckTextBoxChange(TextBox textBox, double defaultValue)
        {
            try
            {
                return Convert.ToDouble(textBox.Text);
            }
            catch
            {
                MessageBox.Show("При вводе числа была допущена ошибка (например, была введена буква). Вернулось первоначальное значение '" + defaultValue + "'.", "Ошибка при заполнении поля");
                textBox.Text = Convert.ToString(defaultValue);
                return defaultValue;
            }
        }
        /// <summary>
        /// Checks if Inventor is active and connects to it.
        /// </summary>
        /// <returns>
        /// Returns connection to Inventor. Otherwise returns null.
        /// </returns>
        public static Inventor.Application AppActivity(Inventor.Application app)
        {
            try
            {
                return (Inventor.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Inventor.Application");
            }
            catch
            {
                InventorControlForm IC = new InventorControlForm();
                IC.ShowDialog();
                return null;
            }
        }
        /// <summary>
        /// Saves the .ipt.
        /// </summary>
        /// <param name="app">
        /// Link to Inventor.
        /// </param>
        public static void SavePart(Inventor.Application app, SaveFileDialog saveFileDialog, Form form, Model.InventorAPI api)
        {
            app = AppActivity(app);
            if (app == null)
                return;
            try
            {
                saveFileDialog.Filter = "Inventor Part Document|*.ipt";
                saveFileDialog.Title = api.GetLongName();
                saveFileDialog.FileName = api.GetPartDoc().DisplayName;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    if (!string.IsNullOrWhiteSpace(saveFileDialog.FileName))
                    {
                        api.GetPartDoc().SaveAs(saveFileDialog.FileName, false);
                        api.SetFileName(saveFileDialog.FileName);
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
