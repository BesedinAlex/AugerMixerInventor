using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Inventor;

namespace BesedinCoursework
{
    public partial class MainBody : Form
    {
        private Inventor.Application ThisApplication = null;
        private Dictionary<string, PartDocument> oPartDoc = new Dictionary<string, PartDocument>();
        private Dictionary<string, string> oFileName = new Dictionary<string, string>();
        private Dictionary<string, PartComponentDefinition> oCompDef = new Dictionary<string, PartComponentDefinition>();
        private Dictionary<string, TransientGeometry> oTransGeom = new Dictionary<string, TransientGeometry>();
        private Dictionary<string, Transaction> oTrans = new Dictionary<string, Transaction>();
        public static double H = 1635, Degree = 18;
        double D = 1530, B = 100, A = 15, T = 40, Rb = 730, Rm = 24, Ds = 250;
        public MainBody()
        {
            InitializeComponent();
            textBox1.Text = Convert.ToString(H);
            textBox2.Text = Convert.ToString(D);
            textBox3.Text = Convert.ToString(B);
            textBox4.Text = Convert.ToString(A);
            textBox5.Text = Convert.ToString(T);
            textBox7.Text = Convert.ToString(Rb);
            textBox8.Text = Convert.ToString(Rm);
            textBox10.Text = Convert.ToString(Ds);
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
            oPartDoc["КК"] = (PartDocument)ThisApplication.Documents.Add(DocumentTypeEnum.kPartDocumentObject, ThisApplication.FileManager.GetTemplateFile(DocumentTypeEnum.kPartDocumentObject));
            oCompDef["КК"] = oPartDoc["КК"].ComponentDefinition;
            oTransGeom["КК"] = ThisApplication.TransientGeometry;
            oFileName["КК"] = null;
            oPartDoc["КК"].DisplayName = "Конический корпус";
            Parameters oParameters = oCompDef["КК"].Parameters; // Хранит параметры детали
            SketchPoint[] point = new SketchPoint[8];
            SketchLine[] line = new SketchLine[7];
            SketchCircle[] circle = new SketchCircle[1];
            // Создание конической основы
            PlanarSketch oSketch = oCompDef["КК"].Sketches.Add(oCompDef["КК"].WorkPlanes[3]);
            point[0] = oSketch.SketchPoints.Add(oTransGeom["КК"].CreatePoint2d(0.1, 0.1), false);
            point[1] = oSketch.SketchPoints.Add(oTransGeom["КК"].CreatePoint2d(0.1, 0.2), false);
            line[0] = oSketch.SketchLines.AddByTwoPoints(point[0], point[1]);
            point[2] = oSketch.SketchPoints.Add(oTransGeom["КК"].CreatePoint2d(D / 2 - B, H), false);
            point[3] = oSketch.SketchPoints.Add(oTransGeom["КК"].CreatePoint2d(D / 2, H), false);
            point[4] = oSketch.SketchPoints.Add(oTransGeom["КК"].CreatePoint2d(D / 2, H - T), false);
            point[5] = oSketch.SketchPoints.Add(oTransGeom["КК"].CreatePoint2d(D / 2 - B + 1, H - T), false);
            point[6] = oSketch.SketchPoints.Add(oTransGeom["КК"].CreatePoint2d(Ds + 1, 0), false);
            point[7] = oSketch.SketchPoints.Add(oTransGeom["КК"].CreatePoint2d(Ds, 0), false);
            line[1] = oSketch.SketchLines.AddByTwoPoints(point[2], point[3]);
            line[2] = oSketch.SketchLines.AddByTwoPoints(point[3], point[4]);
            line[3] = oSketch.SketchLines.AddByTwoPoints(point[4], point[5]);
            line[4] = oSketch.SketchLines.AddByTwoPoints(point[5], point[6]);
            line[5] = oSketch.SketchLines.AddByTwoPoints(point[6], point[7]);
            line[6] = oSketch.SketchLines.AddByTwoPoints(point[7], point[2]);
            Point2d SketchSize = oTransGeom["КК"].CreatePoint2d(-1, -1); // Место для выноса размеров
            oSketch.GeometricConstraints.AddVertical((SketchEntity)line[0]); // Выравнивание линии по вертикали
            oSketch.GeometricConstraints.AddHorizontal((SketchEntity)line[1]); // Выравнивание линии по горизонтали
            oSketch.GeometricConstraints.AddVertical((SketchEntity)line[2]);
            oSketch.GeometricConstraints.AddHorizontal((SketchEntity)line[3]);
            oSketch.GeometricConstraints.AddHorizontal((SketchEntity)line[5]);
            oSketch.GeometricConstraints.AddHorizontalAlign(point[0], point[7]); // Выравнивает точки по горизонтали
            oSketch.GeometricConstraints.AddParallel((SketchEntity)line[4], (SketchEntity)line[6]); // Добавление параллельности линиям
            oSketch.DimensionConstraints.AddTwoPointDistance(point[0], point[7], DimensionOrientationEnum.kHorizontalDim, SketchSize); // Ds
            oSketch.DimensionConstraints.AddTwoPointDistance(point[0], point[3], DimensionOrientationEnum.kVerticalDim, SketchSize); // H (по вертикали)
            oSketch.DimensionConstraints.AddTwoPointDistance(point[0], point[3], DimensionOrientationEnum.kHorizontalDim, SketchSize); // D (по горизонтали)
            oSketch.DimensionConstraints.AddTwoPointDistance(point[2], point[3], DimensionOrientationEnum.kHorizontalDim, SketchSize); // B (по горизонтали)
            oSketch.DimensionConstraints.AddOffset(line[4], (SketchEntity)line[6], SketchSize, false); // A
            oSketch.DimensionConstraints.AddTwoPointDistance(point[3], point[4], DimensionOrientationEnum.kVerticalDim, SketchSize); // T
            oParameters["d0"].Expression = "" + Ds / 2 + " mm";
            oParameters["d1"].Expression = "" + H + " mm";
            oParameters["d2"].Expression = "" + D / 2 + " mm";
            oParameters["d3"].Expression = "" + B + " mm";
            oParameters["d4"].Expression = "" + A + " mm";
            oParameters["d5"].Expression = "" + T + " mm";
            point[0].MoveTo(oTransGeom["КК"].CreatePoint2d(0, 0)); // Выравнивание осевой линии центра
            oSketch.DimensionConstraints.AddTwoLineAngle(line[6], line[0], oTransGeom["КК"].CreatePoint2d(10, 40),true);
            Degree = oParameters["d6"]._Value * (180 / Math.PI);
            Profile oProfile = (Profile)oSketch.Profiles.AddForSolid();
            RevolveFeature revolvefeature = oCompDef["КК"].Features.RevolveFeatures.AddFull(oProfile, line[0], PartFeatureOperationEnum.kJoinOperation);
            // Отверстия
            PlanarSketch oSketch2 = oCompDef["КК"].Sketches.Add(oCompDef["КК"].WorkPlanes[2]);
            point[0] = oSketch2.SketchPoints.Add(oTransGeom["КК"].CreatePoint2d(0, Rb / 10), false);
            circle[0] = oSketch2.SketchCircles.AddByCenterRadius(point[0], Rm / 10 / 2);
            Profile oProfile2 = (Profile)oSketch2.Profiles.AddForSolid();
            ExtrudeFeature oExtrudeDef2 = oCompDef["КК"].Features.ExtrudeFeatures.AddByDistanceExtent(oProfile2, H / 10, PartFeatureExtentDirectionEnum.kPositiveExtentDirection, PartFeatureOperationEnum.kCutOperation, oProfile2);
            ObjectCollection objCollection2 = ThisApplication.TransientObjects.CreateObjectCollection();
            objCollection2.Add(oExtrudeDef2);
            CircularPatternFeature CircularPatternFeature2 = oCompDef["КК"].Features.CircularPatternFeatures.Add(objCollection2, oCompDef["КК"].WorkAxes[2], true, 3, "360 degree", true, PatternComputeTypeEnum.kIdenticalCompute);
            MessageBox.Show("Создание конического корпуса завершено.", "Построение конического корпуса");
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
                saveFileDialog1.Title = "Сохранение конического корпуса";
                saveFileDialog1.FileName = oPartDoc["КК"].DisplayName;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    if (!string.IsNullOrWhiteSpace(saveFileDialog1.FileName))
                    {
                        oPartDoc["КК"].SaveAs(saveFileDialog1.FileName, false);
                        oFileName["КК"] = saveFileDialog1.FileName;
                    }
                Close();
            }
            catch
            {
                MessageBox.Show("Деталь ещё не создана, либо программа не видит созданную деталь.", "Построение конического корпуса");
            }
        }
        private void error_check(System.Windows.Forms.TextBox textBox, double Number) // Проверка ошибки в textBox
        {
            try
            {
                if (Convert.ToDouble(textBox.Text) <= 0)
                {
                    MessageBox.Show("Введите значение больше нуля!", "Построение конического корпуса");
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
            error_check(textBox1, 1635);
            H = Convert.ToDouble(textBox1.Text);
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            error_check(textBox2, 1530);
            D = Convert.ToDouble(textBox2.Text);
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDouble(textBox3.Text) <= 0 | Convert.ToDouble(textBox3.Text) > D / 2) 
                {
                    MessageBox.Show("Введите значение больше нуля и меньше " + Convert.ToString(D / 2) + "!", "Построение конического корпуса");
                    B = D / 2 / 2;
                    textBox3.Text = Convert.ToString(B);
                }
                else
                    B = Convert.ToDouble(textBox3.Text);
            }
            catch
            {
                B = D / 2 / 2;
                textBox3.Text = Convert.ToString(B);
                return;
            }
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            error_check(textBox4, 15);
            A = Convert.ToDouble(textBox4.Text);
        }
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            error_check(textBox5, 40);
            T = Convert.ToDouble(textBox5.Text);
        }
        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDouble(textBox7.Text) < D / 2 - B - Rm | Convert.ToDouble(textBox7.Text) >= D / 2 + Rm)
                {
                    MessageBox.Show("Введите значение от " + Convert.ToString(D / 2 - B) + " до " + Convert.ToString(D / 2) + "!", "Построение конического корпуса");
                    Rb = 730;
                    textBox7.Text = Convert.ToString(Rb);
                }
                else
                    Rb = Convert.ToDouble(textBox7.Text);
            }
            catch
            {
                Rb = 730;
                textBox7.Text = Convert.ToString(Rb);
                return;
            }
        }
        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            error_check(textBox8, 24);
            Rm = Convert.ToDouble(textBox8.Text);
        }
        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDouble(textBox10.Text) > D / 2 - B | Convert.ToDouble(textBox10.Text) == 0)
                {
                    MessageBox.Show("Введите значение от нуля до " + (D / 2 - B) + "!", "Построение конического корпуса");
                    Ds = (D / 2 - B) / 2;
                    textBox10.Text = Convert.ToString(Ds);
                }
                else
                    Ds = Convert.ToDouble(textBox10.Text);
            }
            catch
            {
                Ds = (D / 2 - B) / 2;
                textBox10.Text = Convert.ToString(Ds);
                return;
            }
        }
    }
}