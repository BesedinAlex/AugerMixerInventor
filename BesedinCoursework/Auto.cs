using System;
using System.Windows.Forms;
using Inventor;

namespace BesedinCoursework
{
    public class Auto
    {
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
    }
}
