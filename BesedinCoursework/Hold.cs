using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Inventor;

namespace BesedinCoursework
{
    public partial class Hold : Form
    {
        public Hold()
        {
            InitializeComponent();
            this.comboBox1.Items.AddRange(new object[] { 100, 160, 255 });
            comboBox1.SelectedIndex = 0;
        }
        private Inventor.Application ThisApplication = null;
        private Dictionary<string, PartDocument> oPartDoc = new Dictionary<string, PartDocument>();
        private Dictionary<string, string> oFileName = new Dictionary<string, string>();
        private Dictionary<string, PartComponentDefinition> oCompDef = new Dictionary<string, PartComponentDefinition>();
        private Dictionary<string, TransientGeometry> oTransGeom = new Dictionary<string, TransientGeometry>();
        double b = 100, a1 = 65, h = 120, S1 = 4, a = 45, K = 10, K1 = 25, b1 = 50, b2 = 45, c = 40, d6 = 12, a2 = 40, h1 = 8;
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
            oPartDoc["БО"] = (PartDocument)ThisApplication.Documents.Add(DocumentTypeEnum.kPartDocumentObject, ThisApplication.FileManager.GetTemplateFile(DocumentTypeEnum.kPartDocumentObject));
            oCompDef["БО"] = oPartDoc["БО"].ComponentDefinition;
            oTransGeom["БО"] = ThisApplication.TransientGeometry;
            oFileName["БО"] = null;
            oPartDoc["БО"].DisplayName = "Боковые опоры";
            Parameters oParameters = oCompDef["БО"].Parameters; // Хранит параметры детали
            SketchPoint[] point = new SketchPoint[5];
            SketchLine[] line = new SketchLine[5];
            SketchCircle[] circle = new SketchCircle[1];
            // Нижняя основа
            PlanarSketch oSketch = oCompDef["БО"].Sketches.Add(oCompDef["БО"].WorkPlanes[3]);
            point[0] = oSketch.SketchPoints.Add(oTransGeom["БО"].CreatePoint2d(0, 0), false);
            point[1] = oSketch.SketchPoints.Add(oTransGeom["БО"].CreatePoint2d(b / 10, 0), false);
            point[2] = oSketch.SketchPoints.Add(oTransGeom["БО"].CreatePoint2d(b / 10, -S1 / 10), false);
            point[3] = oSketch.SketchPoints.Add(oTransGeom["БО"].CreatePoint2d(0, -S1 / 10), false);
            line[0] = oSketch.SketchLines.AddByTwoPoints(point[0], point[1]);
            line[1] = oSketch.SketchLines.AddByTwoPoints(point[1], point[2]);
            line[2] = oSketch.SketchLines.AddByTwoPoints(point[2], point[3]);
            line[3] = oSketch.SketchLines.AddByTwoPoints(point[3], point[0]);
            Profile oProfile = (Profile)oSketch.Profiles.AddForSolid();
            ExtrudeFeature oExtrudeDef = oCompDef["БО"].Features.ExtrudeFeatures.AddByDistanceExtent(oProfile, a1 / 10, PartFeatureExtentDirectionEnum.kSymmetricExtentDirection, PartFeatureOperationEnum.kJoinOperation, oProfile);
            // Боковые крепления
            WorkPlane oWorkPlane1 = oCompDef["БО"].WorkPlanes.AddByPlaneAndOffset(oCompDef["БО"].WorkPlanes[3], a / 2 / 10, false);
            oWorkPlane1.Visible = false;
            PlanarSketch oSketch1 = oCompDef["БО"].Sketches.Add(oWorkPlane1);
            point[0] = oSketch1.SketchPoints.Add(oTransGeom["БО"].CreatePoint2d(0, 0), false);
            point[1] = oSketch1.SketchPoints.Add(oTransGeom["БО"].CreatePoint2d((b * Math.Sin(MainBody.Degree / 180 * Math.PI)) / 10, h * Math.Cos(MainBody.Degree / 180 * Math.PI) / 10 - S1 / 10), false);
            point[2] = oSketch1.SketchPoints.Add(oTransGeom["БО"].CreatePoint2d(point[1].Geometry.X + K / 10, point[1].Geometry.Y), false);
            point[3] = oSketch1.SketchPoints.Add(oTransGeom["БО"].CreatePoint2d(b / 10, K1 / 10), false);
            point[4] = oSketch1.SketchPoints.Add(oTransGeom["БО"].CreatePoint2d(b / 10, 0), false);
            line[0] = oSketch1.SketchLines.AddByTwoPoints(point[0], point[1]);
            line[1] = oSketch1.SketchLines.AddByTwoPoints(point[1], point[2]);
            line[2] = oSketch1.SketchLines.AddByTwoPoints(point[2], point[3]);
            line[3] = oSketch1.SketchLines.AddByTwoPoints(point[3], point[4]);
            line[4] = oSketch1.SketchLines.AddByTwoPoints(point[4], point[0]);
            Profile oProfile1 = (Profile)oSketch1.Profiles.AddForSolid();
            ExtrudeFeature oExtrudeDef1 = oCompDef["БО"].Features.ExtrudeFeatures.AddByDistanceExtent(oProfile1, S1 / 10, PartFeatureExtentDirectionEnum.kPositiveExtentDirection, PartFeatureOperationEnum.kJoinOperation, oProfile1);
            ObjectCollection objCollection1 = ThisApplication.TransientObjects.CreateObjectCollection();
            objCollection1.Add(oExtrudeDef1);
            oCompDef["БО"].Features.MirrorFeatures.AddByDefinition(oCompDef["БО"].Features.MirrorFeatures.CreateDefinition(objCollection1, oCompDef["БО"].WorkPlanes[3], PatternComputeTypeEnum.kIdenticalCompute));
            // Болтовое отверстие
            PlanarSketch oSketch2 = oCompDef["БО"].Sketches.Add(oCompDef["БО"].WorkPlanes[3]);
            point[0] = oSketch2.SketchPoints.Add(oTransGeom["БО"].CreatePoint2d(b / 10 - b1 / 10, -S1 / 10), false);
            point[1] = oSketch2.SketchPoints.Add(oTransGeom["БО"].CreatePoint2d(point[0].Geometry.X + b2 / 10, point[0].Geometry.Y), false);
            point[2] = oSketch2.SketchPoints.Add(oTransGeom["БО"].CreatePoint2d(point[1].Geometry.X, point[0].Geometry.Y - h1 / 10), false);
            point[3] = oSketch2.SketchPoints.Add(oTransGeom["БО"].CreatePoint2d(point[0].Geometry.X, point[2].Geometry.Y), false);
            line[0] = oSketch2.SketchLines.AddByTwoPoints(point[0], point[1]);
            line[1] = oSketch2.SketchLines.AddByTwoPoints(point[1], point[2]);
            line[2] = oSketch2.SketchLines.AddByTwoPoints(point[2], point[3]);
            line[3] = oSketch2.SketchLines.AddByTwoPoints(point[3], point[0]);
            Profile oProfile2 = (Profile)oSketch2.Profiles.AddForSolid();
            ExtrudeFeature oExtrudeDef2 = oCompDef["БО"].Features.ExtrudeFeatures.AddByDistanceExtent(oProfile2, a2 / 10, PartFeatureExtentDirectionEnum.kSymmetricExtentDirection, PartFeatureOperationEnum.kJoinOperation, oProfile2);
            PlanarSketch oSketch3 = oCompDef["БО"].Sketches.Add(oCompDef["БО"].WorkPlanes[2]);
            circle[0] = oSketch3.SketchCircles.AddByCenterRadius(oTransGeom["БО"].CreatePoint2d(-(b / 10 - c / 10), 0), d6 / 2 / 10);
            Profile oProfile3 = (Profile)oSketch3.Profiles.AddForSolid();
            ExtrudeFeature oExtrudeDef3 = oCompDef["БО"].Features.ExtrudeFeatures.AddByDistanceExtent(oProfile3, h, PartFeatureExtentDirectionEnum.kSymmetricExtentDirection, PartFeatureOperationEnum.kCutOperation, oProfile3);
            // Резьба
            EdgeCollection EdgeCollection1 = ThisApplication.TransientObjects.CreateEdgeCollection();
            EdgeCollection1.Add(oExtrudeDef3.SideFaces[1].Edges[1]);
            ThreadFeatures ThreadFeatures1 = oCompDef["БО"].Features.ThreadFeatures;
            StandardThreadInfo stInfo1 = ThreadFeatures1.CreateStandardThreadInfo(false, true, "ISO Metric profile", "M" + d6 + "x1.5", "6g");
            ThreadFeatures1.Add(oExtrudeDef3.SideFaces[1], oExtrudeDef3.SideFaces[1].Edges[2], (ThreadInfo)stInfo1, false, true, 0);
            MessageBox.Show("Создание боковых опор завершено.", "Построение боковых опор");
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
                saveFileDialog1.Title = "Сохранение боковых опор";
                saveFileDialog1.FileName = oPartDoc["БО"].DisplayName;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    if (!string.IsNullOrWhiteSpace(saveFileDialog1.FileName))
                    {
                        oPartDoc["БО"].SaveAs(saveFileDialog1.FileName, false);
                        oFileName["БО"] = saveFileDialog1.FileName;
                    }
                Close();
            }
            catch
            {
                MessageBox.Show("Деталь ещё не создана, либо программа не видит созданную деталь.", "Построение боковых опор");
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0: b = 100; a1 = 65; h = 120; S1 = 4; a = 45; K = 10; K1 = 25; b1 = 50; b2 = 45; c = 40; d6 = 12; a2 = 40; h1 = 8; break;
                case 1: b = 160; a1 = 95; h = 190; S1 = 5; a = 75; K = 15; K1 = 40; b1 = 70; b2 = 65; c = 50; d6 = 12; a2 = 60; h1 = 10; break;
                case 2: b = 255; a1 = 155; h = 310; S1 = 8; a = 125; K = 25; K1 = 65; b1 = 120; b2 = 115; c = 90; d6 = 20; a2 = 100; h1 = 16; break;
            }
            textBox1.Text = Convert.ToString(a1);
            textBox2.Text = Convert.ToString(h);
            textBox3.Text = Convert.ToString(S1);
            textBox4.Text = Convert.ToString(a);
            textBox5.Text = Convert.ToString(K);
            textBox6.Text = Convert.ToString(K1);
            textBox7.Text = Convert.ToString(b1);
            textBox8.Text = Convert.ToString(b2);
            textBox9.Text = Convert.ToString(c);
            textBox10.Text = Convert.ToString(d6);
            textBox11.Text = Convert.ToString(a2);
            textBox12.Text = Convert.ToString(h1);
        }
    }
}
