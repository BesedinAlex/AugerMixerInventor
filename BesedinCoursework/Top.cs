using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Inventor;

namespace BesedinCoursework
{
    public partial class Top : Form
    {
        private Inventor.Application ThisApplication = null;
        private Dictionary<string, PartDocument> oPartDoc = new Dictionary<string, PartDocument>();
        private Dictionary<string, string> oFileName = new Dictionary<string, string>();
        private Dictionary<string, PartComponentDefinition> oCompDef = new Dictionary<string, PartComponentDefinition>();
        private Dictionary<string, TransientGeometry> oTransGeom = new Dictionary<string, TransientGeometry>();
        double D = 1530, D1 = 1400, T = 40, H = 200, A = 40, ACount = 8; // Основа крышки
        double HR = 305, hR = 30, RR = 580, DR = 180, dR = 120, OR = 75, oR = 18, TR = 100; // Резервные проходы
        double MBRb = 730, MBRm = 24; // Отверстия под болты
        double D1R = 380, d1R = 260, T1R = 240, o1R = 22, O1R = 168, R1R = 540; // Загрузка сыпучих материалов
        public Top()
        {
            InitializeComponent();
            textBox1.Text = Convert.ToString(D);
            textBox2.Text = Convert.ToString(D1);
            textBox3.Text = Convert.ToString(H);
            textBox4.Text = Convert.ToString(T);
            textBox5.Text = Convert.ToString(A);
            textBox6.Text = Convert.ToString(ACount);
            textBox7.Text = Convert.ToString(MBRb);
            textBox8.Text = Convert.ToString(MBRm);
            textBox9.Text = Convert.ToString(RR);
            textBox10.Text = Convert.ToString(HR);
            textBox11.Text = Convert.ToString(DR);
            textBox12.Text = Convert.ToString(hR);
            textBox13.Text = Convert.ToString(dR);
            textBox14.Text = Convert.ToString(TR);
            textBox15.Text = Convert.ToString(OR);
            textBox16.Text = Convert.ToString(oR);
            textBox17.Text = Convert.ToString(R1R);
            textBox18.Text = Convert.ToString(D1R);
            textBox19.Text = Convert.ToString(d1R);
            textBox20.Text = Convert.ToString(T1R);
            textBox21.Text = Convert.ToString(O1R);
            textBox22.Text = Convert.ToString(o1R);
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
            D /= 10;
            D1 /= 10;
            T /= 10;
            H /= 10;
            oPartDoc["К"] = (PartDocument)ThisApplication.Documents.Add(DocumentTypeEnum.kPartDocumentObject, ThisApplication.FileManager.GetTemplateFile(DocumentTypeEnum.kPartDocumentObject));
            oCompDef["К"] = oPartDoc["К"].ComponentDefinition;
            oTransGeom["К"] = ThisApplication.TransientGeometry;
            oFileName["К"] = null;
            oPartDoc["К"].DisplayName = "Крышка";
            Parameters oParameters = oCompDef["К"].Parameters;
            SketchPoint[] point = new SketchPoint[8];
            SketchLine[] line = new SketchLine[8];
            SketchCircle[] circle = new SketchCircle[1];
            // Основа корпуса
            PlanarSketch oSketch = oCompDef["К"].Sketches.Add(oCompDef["К"].WorkPlanes[3]);
            point[0] = oSketch.SketchPoints.Add(oTransGeom["К"].CreatePoint2d(0, H - T), false);
            point[1] = oSketch.SketchPoints.Add(oTransGeom["К"].CreatePoint2d(0, H), false);
            point[2] = oSketch.SketchPoints.Add(oTransGeom["К"].CreatePoint2d(D1 / 2, H), false);
            point[3] = oSketch.SketchPoints.Add(oTransGeom["К"].CreatePoint2d(D1 / 2, T), false);
            point[4] = oSketch.SketchPoints.Add(oTransGeom["К"].CreatePoint2d(D / 2, T), false);
            point[5] = oSketch.SketchPoints.Add(oTransGeom["К"].CreatePoint2d(D / 2, 0), false);
            point[6] = oSketch.SketchPoints.Add(oTransGeom["К"].CreatePoint2d(D1 / 2 - T, 0), false);
            point[7] = oSketch.SketchPoints.Add(oTransGeom["К"].CreatePoint2d(D1 / 2 - T, H - T), false);
            line[0] = oSketch.SketchLines.AddByTwoPoints(point[0], point[1]);
            line[1] = oSketch.SketchLines.AddByTwoPoints(point[1], point[2]);
            line[2] = oSketch.SketchLines.AddByTwoPoints(point[2], point[3]);
            line[3] = oSketch.SketchLines.AddByTwoPoints(point[3], point[4]);
            line[4] = oSketch.SketchLines.AddByTwoPoints(point[4], point[5]);
            line[5] = oSketch.SketchLines.AddByTwoPoints(point[5], point[6]);
            line[6] = oSketch.SketchLines.AddByTwoPoints(point[6], point[7]);
            line[7] = oSketch.SketchLines.AddByTwoPoints(point[7], point[0]);
            Profile oProfile = default(Profile);
            oProfile = (Profile)oSketch.Profiles.AddForSolid();
            RevolveFeature revolvefeature = default(RevolveFeature);
            revolvefeature = oCompDef["К"].Features.RevolveFeatures.AddFull(oProfile, line[0], PartFeatureOperationEnum.kJoinOperation);
            // Ребра жесткости
            PlanarSketch oSketch1 = oCompDef["К"].Sketches.Add(oCompDef["К"].WorkPlanes[3]);
            point[0] = oSketch1.SketchPoints.Add(oTransGeom["К"].CreatePoint2d(D1 / 2 - T, H), false);
            point[1] = oSketch1.SketchPoints.Add(oTransGeom["К"].CreatePoint2d(point[0].Geometry.X + T * 2, H), false);
            point[2] = oSketch1.SketchPoints.Add(oTransGeom["К"].CreatePoint2d(D / 2, T * 2), false);
            point[3] = oSketch1.SketchPoints.Add(oTransGeom["К"].CreatePoint2d(D / 2, T), false);
            point[4] = oSketch1.SketchPoints.Add(oTransGeom["К"].CreatePoint2d(point[0].Geometry.X, T), false);
            line[0] = oSketch1.SketchLines.AddByTwoPoints(point[0], point[1]);
            line[0] = oSketch1.SketchLines.AddByTwoPoints(point[1], point[2]);
            line[0] = oSketch1.SketchLines.AddByTwoPoints(point[2], point[3]);
            line[0] = oSketch1.SketchLines.AddByTwoPoints(point[3], point[4]);
            line[0] = oSketch1.SketchLines.AddByTwoPoints(point[4], point[0]);
            Profile oProfile1 = (Profile)oSketch1.Profiles.AddForSolid();
            ExtrudeFeature oExtrudeDef1 = oCompDef["К"].Features.ExtrudeFeatures.AddByDistanceExtent(oProfile1, A / 10, PartFeatureExtentDirectionEnum.kSymmetricExtentDirection, PartFeatureOperationEnum.kJoinOperation, oProfile1);
            ObjectCollection objCollection1 = ThisApplication.TransientObjects.CreateObjectCollection();
            objCollection1.Add(oExtrudeDef1);
            CircularPatternFeature CircularPatternFeature2 = oCompDef["К"].Features.CircularPatternFeatures.Add(objCollection1, oCompDef["К"].WorkAxes[2], true, ACount, "360 degree", true, PatternComputeTypeEnum.kIdenticalCompute);
            // Резервные проходы (2 штуки)
            WorkPlane oWorkPlane2 = oCompDef["К"].WorkPlanes.AddByPlaneAndOffset(oCompDef["К"].WorkPlanes[2], HR / 10, false);
            oWorkPlane2.Visible = false;
            PlanarSketch oSketch2 = oCompDef["К"].Sketches.Add(oWorkPlane2, false);
            circle[0] = oSketch2.SketchCircles.AddByCenterRadius(oTransGeom["К"].CreatePoint2d(RR / 10, 0), DR / 10 / 2);
            Profile oProfile2 = (Profile)oSketch2.Profiles.AddForSolid();
            ExtrudeFeature oExtrudeDef2 = oCompDef["К"].Features.ExtrudeFeatures.AddByDistanceExtent(oProfile2, hR / 10, PartFeatureExtentDirectionEnum.kSymmetricExtentDirection, PartFeatureOperationEnum.kJoinOperation, oProfile2);
            PlanarSketch oSketch3 = oCompDef["К"].Sketches.Add(oWorkPlane2, false);
            circle[0] = oSketch3.SketchCircles.AddByCenterRadius(oTransGeom["К"].CreatePoint2d(RR / 10, 0), dR / 10 / 2);
            Profile oProfile3 = (Profile)oSketch3.Profiles.AddForSolid();
            ExtrudeFeature oExtrudeDef3 = oCompDef["К"].Features.ExtrudeFeatures.AddByDistanceExtent(oProfile3, HR / 10 - H, PartFeatureExtentDirectionEnum.kNegativeExtentDirection, PartFeatureOperationEnum.kJoinOperation, oProfile3);
            PlanarSketch oSketch4 = oCompDef["К"].Sketches.Add(oWorkPlane2, false);
            circle[0] = oSketch4.SketchCircles.AddByCenterRadius(oTransGeom["К"].CreatePoint2d(RR / 10, 0), TR / 10 / 2);
            Profile oProfile4 = (Profile)oSketch4.Profiles.AddForSolid();
            ExtrudeFeature oExtrudeDef4 = oCompDef["К"].Features.ExtrudeFeatures.AddByDistanceExtent(oProfile4, 1000, PartFeatureExtentDirectionEnum.kSymmetricExtentDirection, PartFeatureOperationEnum.kCutOperation, oProfile4);
            PlanarSketch oSketch5 = oCompDef["К"].Sketches.Add(oWorkPlane2, false);
            circle[0] = oSketch5.SketchCircles.AddByCenterRadius(oTransGeom["К"].CreatePoint2d(RR / 10, OR / 10), oR / 10 / 2);
            Profile oProfile5 = (Profile)oSketch5.Profiles.AddForSolid();
            ExtrudeFeature oExtrudeDef5 = oCompDef["К"].Features.ExtrudeFeatures.AddByDistanceExtent(oProfile5, hR / 10, PartFeatureExtentDirectionEnum.kSymmetricExtentDirection, PartFeatureOperationEnum.kCutOperation, oProfile5);
            WorkAxis Axis5 = oCompDef["К"].WorkAxes.AddByRevolvedFace(oExtrudeDef4.Faces[1]);
            Axis5.Visible = false;
            ObjectCollection objCollection5 = ThisApplication.TransientObjects.CreateObjectCollection();
            objCollection5.Add(oExtrudeDef5);
            CircularPatternFeature CircularPatternFeature5 = oCompDef["К"].Features.CircularPatternFeatures.Add(objCollection5, Axis5, true, 8, "360 degree", true, PatternComputeTypeEnum.kIdenticalCompute);
            ObjectCollection objCollection6 = ThisApplication.TransientObjects.CreateObjectCollection();
            objCollection6.Add(oExtrudeDef2);
            objCollection6.Add(oExtrudeDef3);
            objCollection6.Add(oExtrudeDef4);
            objCollection6.Add(oExtrudeDef5);
            objCollection6.Add(CircularPatternFeature5);
            CircularPatternFeature CircularPatternFeature6 = oCompDef["К"].Features.CircularPatternFeatures.Add(objCollection6, oCompDef["К"].WorkAxes[2], true, 2, "150 degree", true, PatternComputeTypeEnum.kIdenticalCompute);
            // Отверстия под болты
            PlanarSketch oSketch7 = oCompDef["К"].Sketches.Add(oCompDef["К"].WorkPlanes[2]);
            point[0] = oSketch7.SketchPoints.Add(oTransGeom["К"].CreatePoint2d(0, 0), false);
            point[1] = oSketch7.SketchPoints.Add(oTransGeom["К"].CreatePoint2d(0, MBRb / 10), false);
            point[2] = oSketch7.SketchPoints.Add(oTransGeom["К"].CreatePoint2d(MBRb / 10, 0), false);
            line[0] = oSketch7.SketchLines.AddByTwoPoints(point[1], point[0]);
            line[1] = oSketch7.SketchLines.AddByTwoPoints(point[0], point[2]);
            oSketch7.DimensionConstraints.AddTwoLineAngle(line[0], line[1], oTransGeom["К"].CreatePoint2d(1, 1));
            oSketch7.GeometricConstraints.AddHorizontal((SketchEntity)line[1]);
            circle[0] = oSketch7.SketchCircles.AddByCenterRadius(point[1], MBRm / 10 / 2);
            oParameters["d21"].Expression = "82.5 degree";
            oSketch7.GeometricConstraints.AddCoincident((SketchEntity)circle[0].CenterSketchPoint, (SketchEntity)point[1]);
            Profile oProfile7 = (Profile)oSketch7.Profiles.AddForSolid();
            ExtrudeFeature oExtrudeDef7 = oCompDef["К"].Features.ExtrudeFeatures.AddByDistanceExtent(oProfile7, 1000, PartFeatureExtentDirectionEnum.kSymmetricExtentDirection, PartFeatureOperationEnum.kCutOperation, oProfile7);
            ObjectCollection objCollection7 = ThisApplication.TransientObjects.CreateObjectCollection();
            objCollection7.Add(oExtrudeDef7);
            CircularPatternFeature CircularPatternFeature7 = oCompDef["К"].Features.CircularPatternFeatures.Add(objCollection7, oCompDef["К"].WorkAxes[2], true, 3, "360 degree", true, PatternComputeTypeEnum.kIdenticalCompute);
            // Загрузка сыпучих материалов
            PlanarSketch oSketch8 = oCompDef["К"].Sketches.Add(oWorkPlane2, false);
            point[0] = oSketch8.SketchPoints.Add(oTransGeom["К"].CreatePoint2d(0, 0), false);
            point[1] = oSketch8.SketchPoints.Add(oTransGeom["К"].CreatePoint2d(0, -R1R / 10), false);
            point[2] = oSketch8.SketchPoints.Add(oTransGeom["К"].CreatePoint2d(-R1R / 10, 0), false);
            line[0] = oSketch8.SketchLines.AddByTwoPoints(point[0], point[1]);
            line[1] = oSketch8.SketchLines.AddByTwoPoints(point[0], point[2]);
            oSketch8.DimensionConstraints.AddTwoLineAngle(line[0], line[1], oTransGeom["К"].CreatePoint2d(1, 1));
            oSketch8.GeometricConstraints.AddVertical((SketchEntity)line[0]);
            circle[0] = oSketch8.SketchCircles.AddByCenterRadius(oTransGeom["К"].CreatePoint2d(-R1R / 10, -1), D1R / 10 / 2);
            oParameters["d27"].Expression = "75 degree";
            oSketch8.GeometricConstraints.AddCoincident((SketchEntity)circle[0].CenterSketchPoint, (SketchEntity)point[2]);
            Profile oProfile8 = (Profile)oSketch8.Profiles.AddForSolid();
            ExtrudeFeature oExtrudeDef8 = oCompDef["К"].Features.ExtrudeFeatures.AddByDistanceExtent(oProfile8, hR / 10, PartFeatureExtentDirectionEnum.kSymmetricExtentDirection, PartFeatureOperationEnum.kJoinOperation, oProfile8);
            PlanarSketch oSketch9 = oCompDef["К"].Sketches.Add(oExtrudeDef8.Faces[1], false);
            circle[0] = oSketch9.SketchCircles.AddByCenterRadius(oTransGeom["К"].CreatePoint2d(0, 0), d1R / 10 / 2);
            Profile oProfile9 = (Profile)oSketch9.Profiles.AddForSolid();
            ExtrudeFeature oExtrudeDef9 = oCompDef["К"].Features.ExtrudeFeatures.AddByDistanceExtent(oProfile9, HR / 10 - H, PartFeatureExtentDirectionEnum.kPositiveExtentDirection, PartFeatureOperationEnum.kJoinOperation, oProfile9);
            PlanarSketch oSketch10 = oCompDef["К"].Sketches.Add(oExtrudeDef8.Faces[1], false);
            circle[0] = oSketch10.SketchCircles.AddByCenterRadius(oTransGeom["К"].CreatePoint2d(0, 0), T1R / 10 / 2);
            Profile oProfile10 = (Profile)oSketch10.Profiles.AddForSolid();
            ExtrudeFeature oExtrudeDef10 = oCompDef["К"].Features.ExtrudeFeatures.AddByDistanceExtent(oProfile10, 1000, PartFeatureExtentDirectionEnum.kSymmetricExtentDirection, PartFeatureOperationEnum.kCutOperation, oProfile10);
            PlanarSketch oSketch11 = oCompDef["К"].Sketches.Add(oExtrudeDef8.Faces[1], false);
            circle[0] = oSketch11.SketchCircles.AddByCenterRadius(oTransGeom["К"].CreatePoint2d(0, O1R / 10), o1R / 10 / 2);
            Profile oProfile11 = (Profile)oSketch11.Profiles.AddForSolid();
            ExtrudeFeature oExtrudeDef11 = oCompDef["К"].Features.ExtrudeFeatures.AddByDistanceExtent(oProfile11, hR / 10, PartFeatureExtentDirectionEnum.kNegativeExtentDirection, PartFeatureOperationEnum.kCutOperation, oProfile5);
            WorkAxis Axis11 = oCompDef["К"].WorkAxes.AddByRevolvedFace(oExtrudeDef10.Faces[1]);
            Axis11.Visible = false;
            ObjectCollection objCollection11 = ThisApplication.TransientObjects.CreateObjectCollection();
            objCollection11.Add(oExtrudeDef11);
            CircularPatternFeature CircularPatternFeature11 = oCompDef["К"].Features.CircularPatternFeatures.Add(objCollection11, Axis11, true, 12, "360 degree", true, PatternComputeTypeEnum.kIdenticalCompute);
            D *= 10;
            D1 *= 10;
            T *= 10;
            H *= 10;
            MessageBox.Show("Создание крышки завершено.", "Построение крышки");
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
                saveFileDialog1.Title = "Сохранение крышки";
                saveFileDialog1.FileName = oPartDoc["К"].DisplayName;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (!string.IsNullOrWhiteSpace(saveFileDialog1.FileName))
                    {
                        oPartDoc["К"].SaveAs(saveFileDialog1.FileName, false);
                        oFileName["К"] = saveFileDialog1.FileName;
                    }
                }
                Close();
            }
            catch
            {
                MessageBox.Show("Деталь ещё не создана, либо программа не видит созданную деталь.", "Построение крышки");
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e) =>
            D = Auto.checkTextBoxChange(textBox1, 1530);
        private void textBox2_TextChanged(object sender, EventArgs e) =>
            D1 = Auto.checkTextBoxChange(textBox2, 1340);
        private void textBox3_TextChanged(object sender, EventArgs e) =>
            H = Auto.checkTextBoxChange(textBox3, 200);
        private void textBox4_TextChanged(object sender, EventArgs e) =>
            T = Auto.checkTextBoxChange(textBox4, 40);
        private void textBox5_TextChanged(object sender, EventArgs e) =>
            A = Auto.checkTextBoxChange(textBox5, 40);
        private void textBox6_TextChanged(object sender, EventArgs e) =>
            ACount = Auto.checkTextBoxChange(textBox6, 8);
        private void textBox7_TextChanged(object sender, EventArgs e) =>
            MBRb = Auto.checkTextBoxChange(textBox7, 730);
        private void textBox8_TextChanged(object sender, EventArgs e) =>
            MBRm = Auto.checkTextBoxChange(textBox8, 24);
        private void textBox9_TextChanged(object sender, EventArgs e) =>
            RR = Auto.checkTextBoxChange(textBox9, 580);
        private void textBox10_TextChanged(object sender, EventArgs e) =>
            HR = Auto.checkTextBoxChange(textBox10, 305);
        private void textBox11_TextChanged(object sender, EventArgs e) =>
            DR = Auto.checkTextBoxChange(textBox11, 180);
        private void textBox12_TextChanged(object sender, EventArgs e) =>
            hR = Auto.checkTextBoxChange(textBox12, 30);
        private void textBox13_TextChanged(object sender, EventArgs e) =>
            dR = Auto.checkTextBoxChange(textBox13, 120);
        private void textBox14_TextChanged(object sender, EventArgs e) =>
            TR = Auto.checkTextBoxChange(textBox14, 100);
        private void textBox15_TextChanged(object sender, EventArgs e) =>
            OR = Auto.checkTextBoxChange(textBox15, 75);
        private void textBox16_TextChanged(object sender, EventArgs e) =>
            oR = Auto.checkTextBoxChange(textBox16, 18);
        private void textBox17_TextChanged(object sender, EventArgs e) =>
            R1R = Auto.checkTextBoxChange(textBox17, 540);
        private void textBox18_TextChanged(object sender, EventArgs e) =>
            D1R = Auto.checkTextBoxChange(textBox18, 380);
        private void textBox19_TextChanged(object sender, EventArgs e) =>
            d1R = Auto.checkTextBoxChange(textBox19, 260);
        private void textBox20_TextChanged(object sender, EventArgs e) =>
            T1R = Auto.checkTextBoxChange(textBox20, 240);
        private void textBox21_TextChanged(object sender, EventArgs e) =>
            O1R = Auto.checkTextBoxChange(textBox21, 168);
        private void textBox22_TextChanged(object sender, EventArgs e) =>
            o1R = Auto.checkTextBoxChange(textBox22, 22);
    }
}