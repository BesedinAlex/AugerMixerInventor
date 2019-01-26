using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Inventor;

namespace BesedinCoursework
{
    public partial class Screw : Form
    {
        private Inventor.Application ThisApplication = null;
        private Dictionary<string, PartDocument> oPartDoc = new Dictionary<string, PartDocument>();
        private Dictionary<string, string> oFileName = new Dictionary<string, string>();
        private Dictionary<string, PartComponentDefinition> oCompDef = new Dictionary<string, PartComponentDefinition>();
        private Dictionary<string, TransientGeometry> oTransGeom = new Dictionary<string, TransientGeometry>();
        double H = 1700, D = 220, D1 = 100, H1 = 120, H2 = 1300, T = 25, H3 = 1500, A = 40;
        public Screw()
        {
            InitializeComponent();
            textBox1.Text = Convert.ToString(D);
            textBox2.Text = Convert.ToString(D1);
            textBox3.Text = Convert.ToString(H);
            textBox4.Text = Convert.ToString(H1);
            textBox5.Text = Convert.ToString(H2);
            textBox6.Text = Convert.ToString(T);
            textBox7.Text = Convert.ToString(H3);
            textBox8.Text = Convert.ToString(A);
        }
        private void Build_Click(object sender, EventArgs e)
        {
            try
            {
                ThisApplication = (Inventor.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Inventor.Application");
            }
            catch
            {
                InventorControl IC = new InventorControl();
                IC.ShowDialog();
                return;
            }
            H /= 10;
            D /= 10;
            D1 /= 10;
            H1 /= 10;
            H2 /= 10;
            T /= 10;
            H3 /= 10;
            A /= 10;
            oPartDoc["Шнек"] = (PartDocument)ThisApplication.Documents.Add(DocumentTypeEnum.kPartDocumentObject, ThisApplication.FileManager.GetTemplateFile(DocumentTypeEnum.kPartDocumentObject));
            oCompDef["Шнек"] = oPartDoc["Шнек"].ComponentDefinition;
            oTransGeom["Шнек"] = ThisApplication.TransientGeometry;
            oFileName["Шнек"] = null;
            oPartDoc["Шнек"].DisplayName = "Шнек";
            SketchPoint[] point = new SketchPoint[4];
            SketchLine[] line = new SketchLine[4];
            // Создание цилиндра
            PlanarSketch oSketch = oCompDef["Шнек"].Sketches.Add(oCompDef["Шнек"].WorkPlanes[3]);
            point[0] = oSketch.SketchPoints.Add(oTransGeom["Шнек"].CreatePoint2d(0, 0), false);
            point[1] = oSketch.SketchPoints.Add(oTransGeom["Шнек"].CreatePoint2d(0, H), false);
            point[2] = oSketch.SketchPoints.Add(oTransGeom["Шнек"].CreatePoint2d(D1 / 2, H), false);
            point[3] = oSketch.SketchPoints.Add(oTransGeom["Шнек"].CreatePoint2d(D1 / 2, 0), false);
            line[0] = oSketch.SketchLines.AddByTwoPoints(point[0], point[1]);
            line[1] = oSketch.SketchLines.AddByTwoPoints(point[1], point[2]);
            line[2] = oSketch.SketchLines.AddByTwoPoints(point[2], point[3]);
            line[3] = oSketch.SketchLines.AddByTwoPoints(point[3], point[0]);
            Profile oProfile = (Profile)oSketch.Profiles.AddForSolid();
            RevolveFeature revolvefeature = oCompDef["Шнек"].Features.RevolveFeatures.AddFull(oProfile, line[0], PartFeatureOperationEnum.kJoinOperation);
            // Создание пружины
            PlanarSketch oSketch1 = oCompDef["Шнек"].Sketches.Add(oCompDef["Шнек"].WorkPlanes[3]);
            point[0] = oSketch1.SketchPoints.Add(oTransGeom["Шнек"].CreatePoint2d(D1 / 2, 0), false);
            point[1] = oSketch1.SketchPoints.Add(oTransGeom["Шнек"].CreatePoint2d(D / 2, 0), false);
            point[3] = oSketch1.SketchPoints.Add(oTransGeom["Шнек"].CreatePoint2d(D1 / 2, T), false);
            line[0] = oSketch1.SketchLines.AddByTwoPoints(point[0], point[1]);
            line[1] = oSketch1.SketchLines.AddByTwoPoints(point[1], point[3]);
            line[2] = oSketch1.SketchLines.AddByTwoPoints(point[3], point[0]);
            Profile oProfile1 = (Profile)oSketch1.Profiles.AddForSolid();
            CoilFeature coil = oCompDef["Шнек"].Features.CoilFeatures.AddByPitchAndHeight(oProfile1, oCompDef["Шнек"].WorkAxes[2], H1, H2, PartFeatureOperationEnum.kJoinOperation, false, false, 0, false, 0, 0, true);
            // Верхняя граница пружины
            PlanarSketch oSketch2 = oCompDef["Шнек"].Sketches.Add(oCompDef["Шнек"].WorkPlanes[3]);
            point[0] = oSketch2.SketchPoints.Add(oTransGeom["Шнек"].CreatePoint2d(0, H2), false);
            point[1] = oSketch2.SketchPoints.Add(oTransGeom["Шнек"].CreatePoint2d(0, H2 + T), false);
            point[2] = oSketch2.SketchPoints.Add(oTransGeom["Шнек"].CreatePoint2d(D / 2, H2 + T), false);
            point[3] = oSketch2.SketchPoints.Add(oTransGeom["Шнек"].CreatePoint2d(D / 2, H2), false);
            line[0] = oSketch2.SketchLines.AddByTwoPoints(point[0], point[1]);
            line[1] = oSketch2.SketchLines.AddByTwoPoints(point[1], point[2]);
            line[2] = oSketch2.SketchLines.AddByTwoPoints(point[2], point[3]);
            line[3] = oSketch2.SketchLines.AddByTwoPoints(point[3], point[0]);
            Profile oProfile2 = (Profile)oSketch2.Profiles.AddForSolid();
            RevolveFeature revolvefeature2 = oCompDef["Шнек"].Features.RevolveFeatures.AddFull(oProfile2, line[0], PartFeatureOperationEnum.kJoinOperation);
            // Крепление
            PlanarSketch oSketch3 = oCompDef["Шнек"].Sketches.Add(oCompDef["Шнек"].WorkPlanes[3]);
            point[0] = oSketch3.SketchPoints.Add(oTransGeom["Шнек"].CreatePoint2d(0, H3), false);
            point[1] = oSketch3.SketchPoints.Add(oTransGeom["Шнек"].CreatePoint2d(0, H3 + A), false);
            point[2] = oSketch3.SketchPoints.Add(oTransGeom["Шнек"].CreatePoint2d(D / 2, H3 + A), false);
            point[3] = oSketch3.SketchPoints.Add(oTransGeom["Шнек"].CreatePoint2d(D / 2, H3), false);
            line[0] = oSketch3.SketchLines.AddByTwoPoints(point[0], point[1]);
            line[1] = oSketch3.SketchLines.AddByTwoPoints(point[1], point[2]);
            line[2] = oSketch3.SketchLines.AddByTwoPoints(point[2], point[3]);
            line[3] = oSketch3.SketchLines.AddByTwoPoints(point[3], point[0]);
            Profile oProfile3 = (Profile)oSketch3.Profiles.AddForSolid();
            RevolveFeature revolvefeature3 = oCompDef["Шнек"].Features.RevolveFeatures.AddFull(oProfile3, line[0], PartFeatureOperationEnum.kJoinOperation);
            H *= 10;
            D *= 10;
            D1 *= 10;
            H1 *= 10;
            H2 *= 10;
            T *= 10;
            H3 *= 10;
            A *= 10;
            MessageBox.Show("Создание шнека завершено.", "Построение шнека");
        }
        private void Save_Click(object sender, EventArgs e)
        {
            try
            {
                ThisApplication = (Inventor.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Inventor.Application");
            }
            catch
            {
                InventorControl IC = new InventorControl();
                IC.ShowDialog();
                return;
            }
            try
            {
                saveFileDialog1.Filter = "Inventor Part Document|*.ipt";
                saveFileDialog1.Title = "Сохранение шнека";
                saveFileDialog1.FileName = oPartDoc["Шнек"].DisplayName;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    if (!string.IsNullOrWhiteSpace(saveFileDialog1.FileName))
                    {
                        oPartDoc["Шнек"].SaveAs(saveFileDialog1.FileName, false);
                        oFileName["Шнек"] = saveFileDialog1.FileName;
                    }
                Close();
            }
            catch
            {
                MessageBox.Show("Деталь ещё не создана, либо программа не видит созданную деталь.", "Построение шнека");
            }
        }
        private void error_check(System.Windows.Forms.TextBox textBox, double Number) // Проверка ошибки в textBox
        {
            try
            {
                if (Convert.ToDouble(textBox.Text) <= 0)
                {
                    MessageBox.Show("Введите значение больше нуля!", "Построение корпуса для выгрузки материалов");
                    textBox.Text = Convert.ToString(Number);
                }
            }
            catch
            {
                textBox.Text = Convert.ToString(Number);
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            error_check(textBox1, 220);
            D = Convert.ToDouble(textBox1.Text);
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            error_check(textBox2, 100);
            D1 = Convert.ToDouble(textBox2.Text);
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            error_check(textBox3, 1600);
            H = Convert.ToDouble(textBox3.Text);
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            error_check(textBox4, 120);
            H1 = Convert.ToDouble(textBox4.Text);
        }
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            error_check(textBox5, 1300);
            H2 = Convert.ToDouble(textBox5.Text);
        }
        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            error_check(textBox6, 25);
            T = Convert.ToDouble(textBox6.Text);
        }
        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            error_check(textBox7, 1500);
            H3 = Convert.ToDouble(textBox7.Text);
        }
        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            error_check(textBox8, 40);
            A = Convert.ToDouble(textBox8.Text);
        }
    }
}
