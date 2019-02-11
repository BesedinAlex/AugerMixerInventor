using System;
using System.Windows.Forms;

namespace BesedinCoursework
{
    /// <summary>
    /// Contains functions that are used in all forms.
    /// </summary>
    public class Auto
    {
        /// <summary>
        /// Returns value entered to textBox, if it's double.
        /// </summary>
        /// <param name="textBox">
        /// The one you checks for possible mistakes.
        /// </param>
        /// <param name="defaultDouble">
        /// Returns it, if you try to enter none.
        /// </param>
        public static double checkTextBoxChange(System.Windows.Forms.TextBox textBox, double defaultDouble)
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
        /// Checks if app is active. Returns null if it isn't.
        /// </summary>
        public static Inventor.Application appActivity(Inventor.Application app)
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
        /// Links the app.
        /// </param>
        public static void savePartFunction(Inventor.Application app, SaveFileDialog saveFileDialog, Form form, InventorAPI api)
        {
            app = appActivity(app);
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