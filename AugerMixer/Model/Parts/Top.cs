using Inventor;

namespace AugerMixer.Model.Parts
{
    static class Top
    {
        public static double D = 1530, D1 = 1400, T = 40, H = 200, A = 40, ACount = 8; // Основа крышки
        public static double HR = 305, Hr = 30, RR = 580, DR = 180, Dr = 120, OR = 75, Or = 18, TR = 100; // Резервные проходы
        public static double MBRb = 730, MBRm = 24; // Отверстия под болты
        public static double D1R = 380, D1r = 260, T1R = 240, O1r = 22, O1R = 168, R1R = 540; // Загрузка сыпучих материалов
        public static void Build(InventorAPI api, string formName)
        {
            var oParameters = api.GetCompDef().Parameters;
            PlanarSketch[] sketch = new PlanarSketch[12];
            Profile[] profile = new Profile[12];
            SketchPoint[] point = new SketchPoint[8];
            SketchLine[] line = new SketchLine[8];
            SketchCircle[] circle = new SketchCircle[1];
            ExtrudeFeature[] extrude = new ExtrudeFeature[12];
            // Основа корпуса
            sketch[0] = api.Sketch(api.GetCompDef().WorkPlanes[3]);
            point[0] = api.Point(sketch[0], 0, H / 10 - T / 10);
            point[1] = api.Point(sketch[0], 0, H / 10);
            point[2] = api.Point(sketch[0], D1 / 10 / 2, H / 10);
            point[3] = api.Point(sketch[0], D1 / 10 / 2, T / 10);
            point[4] = api.Point(sketch[0], D / 10 / 2, T / 10);
            point[5] = api.Point(sketch[0], D / 10 / 2, 0);
            point[6] = api.Point(sketch[0], D1 / 10 / 2 - T / 10, 0);
            point[7] = api.Point(sketch[0], D1 / 10 / 2 - T / 10, H / 10 - T / 10);
            line[0] = api.Line(sketch[0], point[0], point[1]);
            line[1] = api.Line(sketch[0], point[1], point[2]);
            line[2] = api.Line(sketch[0], point[2], point[3]);
            line[3] = api.Line(sketch[0], point[3], point[4]);
            line[4] = api.Line(sketch[0], point[4], point[5]);
            line[5] = api.Line(sketch[0], point[5], point[6]);
            line[6] = api.Line(sketch[0], point[6], point[7]);
            line[7] = api.Line(sketch[0], point[7], point[0]);
            profile[0] = api.Profile(sketch[0]);
            api.Revolve(profile[0], line[0], 0);
            // Ребра жесткости
            sketch[1] = api.Sketch(api.GetCompDef().WorkPlanes[3]);
            point[0] = api.Point(sketch[1], D1 / 10 / 2 - T / 10, H / 10);
            point[1] = api.Point(sketch[1], point[0].Geometry.X + T / 10 * 2, H / 10);
            point[2] = api.Point(sketch[1], D / 10 / 2, T / 10 * 2);
            point[3] = api.Point(sketch[1], D / 10 / 2, T / 10);
            point[4] = api.Point(sketch[1], point[0].Geometry.X, T / 10);
            line[0] = api.Line(sketch[1], point[0], point[1]);
            line[0] = api.Line(sketch[1], point[1], point[2]);
            line[0] = api.Line(sketch[1], point[2], point[3]);
            line[0] = api.Line(sketch[1], point[3], point[4]);
            line[0] = api.Line(sketch[1], point[4], point[0]);
            profile[1] = api.Profile(sketch[1]);
            extrude[1] = api.Extrude(profile[1], A / 10, 2, 0);
            var objCollection1 = api.ObjectCollection();
            objCollection1.Add(extrude[1]);
            api.GetCompDef().Features.CircularPatternFeatures.Add(objCollection1, api.GetCompDef().WorkAxes[2], true, ACount, "360 degree", true, PatternComputeTypeEnum.kIdenticalCompute);
            // Резервные проходы (2 штуки)
            var oWorkPlane2 = api.GetCompDef().WorkPlanes.AddByPlaneAndOffset(api.GetCompDef().WorkPlanes[2], HR / 10);
            oWorkPlane2.Visible = false;
            sketch[2] = api.Sketch(oWorkPlane2);
            circle[0] = api.Circle(sketch[2], api.Point(sketch[2], RR / 10, 0), DR / 10 / 2);
            profile[2] = api.Profile(sketch[2]);
            extrude[2] = api.Extrude(profile[2], Hr / 10, 2, 0);
            sketch[3] = api.Sketch(oWorkPlane2);
            circle[0] = api.Circle(sketch[3], api.Point(sketch[3], RR / 10, 0), Dr / 10 / 2);
            profile[3] = api.Profile(sketch[3]);
            extrude[3] = api.Extrude(profile[3], HR / 10 - H / 10, 1, 0);
            sketch[4] = api.Sketch(oWorkPlane2);
            circle[0] = api.Circle(sketch[4], api.Point(sketch[4], RR / 10, 0), TR / 10 / 2);
            profile[4] = api.Profile(sketch[4]);
            extrude[4] = api.Extrude(profile[4], 10000, 2, 1);
            sketch[5] = api.GetCompDef().Sketches.Add(oWorkPlane2);
            sketch[5] = api.Sketch(oWorkPlane2);
            circle[0] = api.Circle(sketch[5], api.Point(sketch[5], RR / 10, OR / 10), Or / 10 / 2);
            profile[5] = api.Profile(sketch[5]);
            extrude[5] = api.Extrude(profile[5], Hr / 10, 2, 1);
            var Axis5 = api.GetCompDef().WorkAxes.AddByRevolvedFace(extrude[4].Faces[1]);
            Axis5.Visible = false;
            var objCollection5 = api.ObjectCollection();
            objCollection5.Add(extrude[5]);
            var CircularPatternFeature5 = api.GetCompDef().Features.CircularPatternFeatures.Add(objCollection5, Axis5, true, 8, "360 degree", true, PatternComputeTypeEnum.kIdenticalCompute);
            var objCollection6 = api.ObjectCollection();
            for (int i = 2; i < 6; i++)
                objCollection6.Add(extrude[i]);
            objCollection6.Add(CircularPatternFeature5);
            api.GetCompDef().Features.CircularPatternFeatures.Add(objCollection6, api.GetCompDef().WorkAxes[2], true, 2, "150 degree", true, PatternComputeTypeEnum.kIdenticalCompute);
            // Отверстия под болты
            sketch[7] = api.Sketch(api.GetCompDef().WorkPlanes[2]);
            point[0] = api.Point(sketch[7], 0, 0);
            point[1] = api.Point(sketch[7], 0, MBRb / 10);
            point[2] = api.Point(sketch[7], MBRb / 10, 0);
            line[0] = api.Line(sketch[7], point[1], point[0]);
            line[1] = api.Line(sketch[7], point[0], point[2]);
            sketch[7].DimensionConstraints.AddTwoLineAngle(line[0], line[1], api.GetTransGeom().CreatePoint2d(1, 1));
            sketch[7].GeometricConstraints.AddHorizontal((SketchEntity)line[1]);
            circle[0] = api.Circle(sketch[7], point[1], MBRm / 10 / 2);
            oParameters["d21"].Expression = "82.5 degree";
            sketch[7].GeometricConstraints.AddCoincident((SketchEntity)circle[0].CenterSketchPoint, (SketchEntity)point[1]);
            profile[7] = api.Profile(sketch[7]);
            extrude[7] = api.Extrude(profile[7], 10000, 2, 1);
            var objCollection7 = api.ObjectCollection();
            objCollection7.Add(extrude[7]);
            api.GetCompDef().Features.CircularPatternFeatures.Add(objCollection7, api.GetCompDef().WorkAxes[2], true, 3, "360 degree", true, PatternComputeTypeEnum.kIdenticalCompute);
            // Загрузка сыпучих материалов
            sketch[8] = api.Sketch(oWorkPlane2);
            point[0] = api.Point(sketch[8], 0, 0);
            point[1] = api.Point(sketch[8], 0, -R1R / 10);
            point[2] = api.Point(sketch[8], -R1R / 10, 0);
            line[0] = api.Line(sketch[8], point[0], point[1]);
            line[1] = api.Line(sketch[8], point[0], point[2]);
            sketch[8].DimensionConstraints.AddTwoLineAngle(line[0], line[1], api.GetTransGeom().CreatePoint2d(1, 1));
            sketch[8].GeometricConstraints.AddVertical((SketchEntity)line[0]);
            circle[0] = api.Circle(sketch[8], api.Point(sketch[8], -R1R / 10, -1), D1R / 10 / 2);
            oParameters["d27"].Expression = "75 degree";
            sketch[8].GeometricConstraints.AddCoincident((SketchEntity)circle[0].CenterSketchPoint, (SketchEntity)point[2]);
            profile[8] = api.Profile(sketch[8]);
            extrude[8] = api.Extrude(profile[8], Hr / 10, 2, 0);
            sketch[9] = api.Sketch(extrude[8].Faces[1]);
            circle[0] = api.Circle(sketch[9], api.Point(sketch[9], 0, 0), D1r / 10 / 2);
            profile[9] = api.Profile(sketch[9]);
            extrude[9] = api.Extrude(profile[9], HR / 10 - H / 10, 0, 0);
            sketch[10] = api.Sketch(extrude[8].Faces[1]);
            circle[0] = api.Circle(sketch[10], api.Point(sketch[10], 0, 0), T1R / 10 / 2);
            profile[10] = api.Profile(sketch[10]);
            extrude[10] = api.Extrude(profile[10], 10000, 2, 1);
            sketch[11] = api.Sketch(extrude[8].Faces[1]);
            circle[0] = api.Circle(sketch[11], api.Point(sketch[11], 0, O1R / 10), O1r / 10 / 2);
            profile[11] = api.Profile(sketch[11]);
            extrude[11] = api.Extrude(profile[11], Hr / 10, 1, 1);
            var Axis11 = api.GetCompDef().WorkAxes.AddByRevolvedFace(extrude[10].Faces[1]);
            Axis11.Visible = false;
            var objCollection11 = api.ObjectCollection();
            objCollection11.Add(extrude[11]);
            api.GetCompDef().Features.CircularPatternFeatures.Add(objCollection11, Axis11, true, 12, "360 degree", true, PatternComputeTypeEnum.kIdenticalCompute);
            System.Windows.Forms.MessageBox.Show(formName + " завершено.", formName);
        }
    }
}
