using System;
using System.Windows.Forms;
using Inventor;

namespace BesedinCoursework
{
    /// <summary>
    /// Some functions to clear controller code.
    /// </summary>
    public class Auto
    {
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
        /// Saves the .ipt.
        /// </summary>
        /// <param name="app">
        /// Links the app.
        /// </param>
        public static void saveFunction(Inventor.Application app, SaveFileDialog saveFileDialog, Form form, InventorAPI api)
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
        public static void buildBottom(Inventor.Application app)
        {
            InventorAPI api = Bottom.api;
            Parameters oParameters = api.getCompDef().Parameters;
            PlanarSketch[] sketch = new PlanarSketch[11];
            Profile[] profile = new Profile[11];
            SketchPoint[] point = new SketchPoint[10];
            SketchLine[] line = new SketchLine[10];
            SketchCircle[] circle = new SketchCircle[2];
            // Основание
            sketch[0] = api.sketch(api.getCompDef().WorkPlanes[2]);
            circle[0] = api.circle(sketch[0], api.point(sketch[0], 0, 0), Bottom.D1 / 10 / 2);
            profile[0] = api.profile(sketch[0]);
            ExtrudeFeature oExtrudeDef = api.getCompDef().Features.ExtrudeFeatures.AddByDistanceExtent(profile[0], Bottom.H1 / 10, PartFeatureExtentDirectionEnum.kPositiveExtentDirection, PartFeatureOperationEnum.kJoinOperation, profile[0]);
            sketch[1] = api.sketch(oExtrudeDef.Faces[3]);
            circle[0] = api.circle(sketch[1], api.point(sketch[1], 0, 0), Bottom.D2 / 10 / 2);
            profile[1] = api.profile(sketch[1]);
            ExtrudeFeature oExtrudeDef1 = api.getCompDef().Features.ExtrudeFeatures.AddByDistanceExtent(profile[1], Bottom.H2 / 10, PartFeatureExtentDirectionEnum.kPositiveExtentDirection, PartFeatureOperationEnum.kJoinOperation, profile[1]);
            // Кубическая часть корпуса
            sketch[2] = api.sketch(oExtrudeDef1.Faces[2]);
            point[0] = api.point(sketch[2], L2 / 10 / 2, L1 / 10 / 2);
            point[1] = api.point(sketch[2], point[0].Geometry.X, -point[0].Geometry.Y);
            point[2] = api.point(sketch[2], -point[0].Geometry.X, -point[0].Geometry.Y);
            point[3] = api.point(sketch[2], -point[0].Geometry.X, point[0].Geometry.Y);
            line[0] = api.line(sketch[2], point[0], point[1]);
            line[1] = api.line(sketch[2], point[1], point[2]);
            line[2] = api.line(sketch[2], point[2], point[3]);
            line[3] = api.line(sketch[2], point[3], point[0]);
            profile[2] = api.profile(sketch[2]);
            ExtrudeFeature oExtrudeDef2 = api.getCompDef().Features.ExtrudeFeatures.AddByDistanceExtent(profile[2], T / 10, PartFeatureExtentDirectionEnum.kPositiveExtentDirection, PartFeatureOperationEnum.kJoinOperation, profile[2]);
            PlanarSketch oSketch3 = api.getCompDef().Sketches.Add(oExtrudeDef2.Faces[6], false);
            point[0] = oSketch3.SketchPoints.Add(api.getTransGeom().CreatePoint2d(L2 / 10 / 2, L1 / 10 / 2), false);
            point[1] = oSketch3.SketchPoints.Add(api.getTransGeom().CreatePoint2d(point[0].Geometry.X, -point[0].Geometry.Y), false);
            point[2] = oSketch3.SketchPoints.Add(api.getTransGeom().CreatePoint2d(-point[0].Geometry.X, -point[0].Geometry.Y), false);
            point[3] = oSketch3.SketchPoints.Add(api.getTransGeom().CreatePoint2d(-point[0].Geometry.X, point[0].Geometry.Y), false);
            line[0] = oSketch3.SketchLines.AddByTwoPoints(point[0], point[1]);
            line[1] = oSketch3.SketchLines.AddByTwoPoints(point[1], point[2]);
            line[2] = oSketch3.SketchLines.AddByTwoPoints(point[2], point[3]);
            line[3] = oSketch3.SketchLines.AddByTwoPoints(point[3], point[0]);
            point[4] = oSketch3.SketchPoints.Add(api.getTransGeom().CreatePoint2d(point[0].Geometry.X - T / 10, point[0].Geometry.Y - T / 10), false);
            point[5] = oSketch3.SketchPoints.Add(api.getTransGeom().CreatePoint2d(point[0].Geometry.X - T / 10, -point[0].Geometry.Y + T / 10), false);
            point[6] = oSketch3.SketchPoints.Add(api.getTransGeom().CreatePoint2d(-point[0].Geometry.X + T / 10, -point[0].Geometry.Y + T / 10), false);
            point[7] = oSketch3.SketchPoints.Add(api.getTransGeom().CreatePoint2d(-point[0].Geometry.X + T / 10, point[0].Geometry.Y - T / 10), false);
            line[4] = oSketch3.SketchLines.AddByTwoPoints(point[4], point[5]);
            line[5] = oSketch3.SketchLines.AddByTwoPoints(point[5], point[6]);
            line[6] = oSketch3.SketchLines.AddByTwoPoints(point[6], point[7]);
            line[7] = oSketch3.SketchLines.AddByTwoPoints(point[7], point[4]);
            Profile oProfile3 = (Profile)oSketch3.Profiles.AddForSolid();
            ExtrudeFeature oExtrudeDef3 = api.getCompDef().Features.ExtrudeFeatures.AddByDistanceExtent(oProfile3, (HB - T * 2) / 10, PartFeatureExtentDirectionEnum.kPositiveExtentDirection, PartFeatureOperationEnum.kJoinOperation, oProfile3);
            WorkPlane oWorkPlane4 = api.getCompDef().WorkPlanes.AddByPlaneAndOffset(api.getCompDef().WorkPlanes[2], (H1 + H2 + HB - T) / 10, false);
            oWorkPlane4.Visible = false;
            PlanarSketch oSketch4 = api.getCompDef().Sketches.Add(oWorkPlane4);
            point[0] = oSketch4.SketchPoints.Add(api.getTransGeom().CreatePoint2d(L2 / 10 / 2, L1 / 10 / 2), false);
            point[1] = oSketch4.SketchPoints.Add(api.getTransGeom().CreatePoint2d(point[0].Geometry.X, -point[0].Geometry.Y), false);
            point[2] = oSketch4.SketchPoints.Add(api.getTransGeom().CreatePoint2d(-point[0].Geometry.X, -point[0].Geometry.Y), false);
            point[3] = oSketch4.SketchPoints.Add(api.getTransGeom().CreatePoint2d(-point[0].Geometry.X, point[0].Geometry.Y), false);
            line[0] = oSketch4.SketchLines.AddByTwoPoints(point[0], point[1]);
            line[1] = oSketch4.SketchLines.AddByTwoPoints(point[1], point[2]);
            line[2] = oSketch4.SketchLines.AddByTwoPoints(point[2], point[3]);
            line[3] = oSketch4.SketchLines.AddByTwoPoints(point[3], point[0]);
            circle[0] = oSketch4.SketchCircles.AddByCenterRadius(api.getTransGeom().CreatePoint2d(0, 0), D3 / 2 / 10);
            Profile oProfile4 = (Profile)oSketch4.Profiles.AddForSolid();
            ExtrudeFeature oExtrudeDef4 = api.getCompDef().Features.ExtrudeFeatures.AddByDistanceExtent(oProfile4, T / 10, PartFeatureExtentDirectionEnum.kPositiveExtentDirection, PartFeatureOperationEnum.kJoinOperation, oProfile4);
            // Переход к коническому корпусу
            PlanarSketch oSketch5 = api.getCompDef().Sketches.Add(oExtrudeDef4.Faces[3], false);
            circle[0] = oSketch5.SketchCircles.AddByCenterRadius(api.getTransGeom().CreatePoint2d(0, 0), D3 / 2 / 10);
            circle[1] = oSketch5.SketchCircles.AddByCenterRadius(api.getTransGeom().CreatePoint2d(0, 0), (D3 / 2 + T) / 10);
            Profile oProfile5 = (Profile)oSketch5.Profiles.AddForSolid();
            ExtrudeFeature oExtrudeDef5 = api.getCompDef().Features.ExtrudeFeatures.AddByDistanceExtent(oProfile5, H3 / 10, PartFeatureExtentDirectionEnum.kPositiveExtentDirection, PartFeatureOperationEnum.kJoinOperation, oProfile5);
            // Отверстия у основания
            PlanarSketch oSketch6 = api.getCompDef().Sketches.Add(api.getCompDef().WorkPlanes[2]);
            point[0] = oSketch6.SketchPoints.Add(api.getTransGeom().CreatePoint2d(0, 0), false);
            point[1] = oSketch6.SketchPoints.Add(api.getTransGeom().CreatePoint2d(0, DR / 2 / 10), false);
            point[2] = oSketch6.SketchPoints.Add(api.getTransGeom().CreatePoint2d(DR / 2 / 10, 0), false);
            line[0] = oSketch6.SketchLines.AddByTwoPoints(point[0], point[1]);
            line[1] = oSketch6.SketchLines.AddByTwoPoints(point[0], point[2]);
            oSketch6.DimensionConstraints.AddTwoLineAngle(line[0], line[1], api.getTransGeom().CreatePoint2d(1, 1));
            oSketch6.GeometricConstraints.AddHorizontal((SketchEntity)line[1]);
            circle[0] = oSketch6.SketchCircles.AddByCenterRadius(api.getTransGeom().CreatePoint2d(DR / 10, -1), oR / 10);
            oParameters["form.D13"].Expression = "105 degree";
            oSketch6.GeometricConstraints.AddCoincident((SketchEntity)circle[0].CenterSketchPoint, (SketchEntity)point[1]);
            Profile oProfile6 = (Profile)oSketch6.Profiles.AddForSolid();
            ExtrudeFeature oExtrudeDef6 = api.getCompDef().Features.ExtrudeFeatures.AddByDistanceExtent(oProfile6, H1 / 10, PartFeatureExtentDirectionEnum.kPositiveExtentDirection, PartFeatureOperationEnum.kCutOperation, oProfile6);
            ObjectCollection objCollection6 = app.TransientObjects.CreateObjectCollection();
            objCollection6.Add(oExtrudeDef6);
            CircularPatternFeature CircularPatternFeature6 = api.getCompDef().Features.CircularPatternFeatures.Add(objCollection6, api.getCompDef().WorkAxes[2], true, 12, "360 degree", true, PatternComputeTypeEnum.kIdenticalCompute);
            // Трубки
            PlanarSketch oSketch7 = api.getCompDef().Sketches.Add(api.getCompDef().WorkPlanes[1]);
            circle[0] = oSketch7.SketchCircles.AddByCenterRadius(api.getTransGeom().CreatePoint2d(H4 / 10, 0), D4D / 2 / 10);
            Profile oProfile7 = (Profile)oSketch7.Profiles.AddForSolid();
            ExtrudeFeature oExtrudeDef7 = api.getCompDef().Features.ExtrudeFeatures.AddByDistanceExtent(oProfile7, D3 / 10 + T * 2 / 10 + A * 2 / 10, PartFeatureExtentDirectionEnum.kSymmetricExtentDirection, PartFeatureOperationEnum.kJoinOperation, oProfile7);
            PlanarSketch oSketch8 = api.getCompDef().Sketches.Add(api.getCompDef().WorkPlanes[1]);
            circle[0] = oSketch8.SketchCircles.AddByCenterRadius(api.getTransGeom().CreatePoint2d(H4 / 10, 0), D4d / 2 / 10);
            Profile oProfile8 = (Profile)oSketch8.Profiles.AddForSolid();
            ExtrudeFeature oExtrudeDef8 = api.getCompDef().Features.ExtrudeFeatures.AddByDistanceExtent(oProfile8, D3 / 10 + T * 2 / 10 + A * 2 / 10, PartFeatureExtentDirectionEnum.kSymmetricExtentDirection, PartFeatureOperationEnum.kCutOperation, oProfile8);
            PlanarSketch oSketch9 = api.getCompDef().Sketches.Add(oExtrudeDef4.Faces[3], false);
            circle[0] = oSketch9.SketchCircles.AddByCenterRadius(api.getTransGeom().CreatePoint2d(0, 0), D3 / 2 / 10);
            Profile oProfile9 = (Profile)oSketch9.Profiles.AddForSolid();
            ExtrudeFeature oExtrudeDef9 = api.getCompDef().Features.ExtrudeFeatures.AddByDistanceExtent(oProfile9, H3 / 10, PartFeatureExtentDirectionEnum.kPositiveExtentDirection, PartFeatureOperationEnum.kCutOperation, oProfile9);
            // Крышка
            PlanarSketch oSketch10 = api.getCompDef().Sketches.Add(api.getCompDef().WorkPlanes[3]);
            point[0] = oSketch10.SketchPoints.Add(api.getTransGeom().CreatePoint2d(L2 / 2 / 10, H5 / 10), false);
            point[1] = oSketch10.SketchPoints.Add(api.getTransGeom().CreatePoint2d(point[0].Geometry.X, point[0].Geometry.Y + D51 / 2 / 10), false);
            point[2] = oSketch10.SketchPoints.Add(api.getTransGeom().CreatePoint2d(point[1].Geometry.X + L51 / 10, point[1].Geometry.Y), false);
            point[3] = oSketch10.SketchPoints.Add(api.getTransGeom().CreatePoint2d(point[2].Geometry.X, point[0].Geometry.Y + D52 / 2 / 10), false);
            point[4] = oSketch10.SketchPoints.Add(api.getTransGeom().CreatePoint2d(point[3].Geometry.X + L52 / 10, point[3].Geometry.Y), false);
            point[5] = oSketch10.SketchPoints.Add(api.getTransGeom().CreatePoint2d(point[4].Geometry.X, point[0].Geometry.Y + D53 / 2 / 10), false);
            point[6] = oSketch10.SketchPoints.Add(api.getTransGeom().CreatePoint2d(point[5].Geometry.X + L53 / 10, point[5].Geometry.Y), false);
            point[7] = oSketch10.SketchPoints.Add(api.getTransGeom().CreatePoint2d(point[6].Geometry.X, point[0].Geometry.Y + D54 / 2 / 10), false);
            point[8] = oSketch10.SketchPoints.Add(api.getTransGeom().CreatePoint2d(point[7].Geometry.X + L54 / 10, point[7].Geometry.Y), false);
            point[9] = oSketch10.SketchPoints.Add(api.getTransGeom().CreatePoint2d(point[8].Geometry.X, point[0].Geometry.Y), false);
            line[0] = oSketch10.SketchLines.AddByTwoPoints(point[0], point[1]);
            line[1] = oSketch10.SketchLines.AddByTwoPoints(point[1], point[2]);
            line[2] = oSketch10.SketchLines.AddByTwoPoints(point[2], point[3]);
            line[3] = oSketch10.SketchLines.AddByTwoPoints(point[3], point[4]);
            line[4] = oSketch10.SketchLines.AddByTwoPoints(point[4], point[5]);
            line[5] = oSketch10.SketchLines.AddByTwoPoints(point[5], point[6]);
            line[6] = oSketch10.SketchLines.AddByTwoPoints(point[6], point[7]);
            line[7] = oSketch10.SketchLines.AddByTwoPoints(point[7], point[8]);
            line[8] = oSketch10.SketchLines.AddByTwoPoints(point[8], point[9]);
            line[9] = oSketch10.SketchLines.AddByTwoPoints(point[9], point[0]);
            Profile oProfile10 = (Profile)oSketch10.Profiles.AddForSolid();
            RevolveFeature revolvefeature10 = api.getCompDef().Features.RevolveFeatures.AddFull(oProfile10, line[9], PartFeatureOperationEnum.kJoinOperation);
            MessageBox.Show("Создание корпуса для выгрузки материалов завершено.", Text);
        }
    }
}
