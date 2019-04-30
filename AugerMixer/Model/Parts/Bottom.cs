using Inventor;

namespace AugerMixer.Model.Parts
{
    static class Bottom
    {
        public static double D1 = 440, H1 = 30, D2 = 350, H2 = 50, DR = 395, Or = 11; // Основание
        public static double L1 = 480, L2 = 440, T = 15, HB = 250, D3 = 250, H3 = 90; // "Коробка"
        public static double H4 = 400, D4D = 30, D4d = 20, A = 40; // Трубки
        public static double H5 = 205, D51 = 200, L51 = 20, D52 = 245, L52 = 45, D53 = 100, L53 = 22.5, D54 = 122.5, L54 = 35; // Крышка
        public static void Build(InventorAPI api, string formName)
        {
            var oParameters = api.GetCompDef().Parameters;
            PlanarSketch[] sketch = new PlanarSketch[11];
            Profile[] profile = new Profile[11];
            SketchPoint[] point = new SketchPoint[10];
            SketchLine[] line = new SketchLine[10];
            SketchCircle[] circle = new SketchCircle[2];
            ExtrudeFeature[] extrude = new ExtrudeFeature[10];
            // Основание
            sketch[0] = api.Sketch(api.GetCompDef().WorkPlanes[2]);
            circle[0] = api.Circle(sketch[0], api.Point(sketch[0], 0, 0), D1 / 10 / 2);
            profile[0] = api.Profile(sketch[0]);
            extrude[0] = api.Extrude(profile[0], H1 / 10, 0, 0);
            sketch[1] = api.Sketch(extrude[0].Faces[3]);
            circle[0] = api.Circle(sketch[1], api.Point(sketch[1], 0, 0), D2 / 10 / 2);
            profile[1] = api.Profile(sketch[1]);
            extrude[1] = api.Extrude(profile[1], H2 / 10, 0, 0);
            // Кубическая часть корпуса
            sketch[2] = api.Sketch(extrude[1].Faces[2]);
            point[0] = api.Point(sketch[2], L2 / 10 / 2, L1 / 10 / 2);
            point[1] = api.Point(sketch[2], point[0].Geometry.X, -point[0].Geometry.Y);
            point[2] = api.Point(sketch[2], -point[0].Geometry.X, -point[0].Geometry.Y);
            point[3] = api.Point(sketch[2], -point[0].Geometry.X, point[0].Geometry.Y);
            line[0] = api.Line(sketch[2], point[0], point[1]);
            line[1] = api.Line(sketch[2], point[1], point[2]);
            line[2] = api.Line(sketch[2], point[2], point[3]);
            line[3] = api.Line(sketch[2], point[3], point[0]);
            profile[2] = api.Profile(sketch[2]);
            extrude[2] = api.Extrude(profile[2], T / 10, 0, 0);
            sketch[3] = api.Sketch(extrude[2].Faces[6]);
            point[0] = api.Point(sketch[3], L2 / 10 / 2, L1 / 10 / 2);
            point[1] = api.Point(sketch[3], point[0].Geometry.X, -point[0].Geometry.Y);
            point[2] = api.Point(sketch[3], -point[0].Geometry.X, -point[0].Geometry.Y);
            point[3] = api.Point(sketch[3], -point[0].Geometry.X, point[0].Geometry.Y);
            line[0] = api.Line(sketch[3], point[0], point[1]);
            line[1] = api.Line(sketch[3], point[1], point[2]);
            line[2] = api.Line(sketch[3], point[2], point[3]);
            line[3] = api.Line(sketch[3], point[3], point[0]);
            point[4] = api.Point(sketch[3], point[0].Geometry.X - T / 10, point[0].Geometry.Y - T / 10);
            point[5] = api.Point(sketch[3], point[0].Geometry.X - T / 10, -point[0].Geometry.Y + T / 10);
            point[6] = api.Point(sketch[3], -point[0].Geometry.X + T / 10, -point[0].Geometry.Y + T / 10);
            point[7] = api.Point(sketch[3], -point[0].Geometry.X + T / 10, point[0].Geometry.Y - T / 10);
            line[4] = api.Line(sketch[3], point[4], point[5]);
            line[5] = api.Line(sketch[3], point[5], point[6]);
            line[6] = api.Line(sketch[3], point[6], point[7]);
            line[7] = api.Line(sketch[3], point[7], point[4]);
            profile[3] = api.Profile(sketch[3]);
            extrude[3] = api.Extrude(profile[3], (HB - T * 2) / 10, 0, 0);
            var oWorkPlane4 = api.GetCompDef().WorkPlanes.AddByPlaneAndOffset(api.GetCompDef().WorkPlanes[2], (H1 + H2 + HB - T) / 10);
            oWorkPlane4.Visible = false;
            sketch[4] = api.Sketch(oWorkPlane4);
            point[0] = api.Point(sketch[4], L2 / 10 / 2, L1 / 10 / 2);
            point[1] = api.Point(sketch[4], point[0].Geometry.X, -point[0].Geometry.Y);
            point[2] = api.Point(sketch[4], -point[0].Geometry.X, -point[0].Geometry.Y);
            point[3] = api.Point(sketch[4], -point[0].Geometry.X, point[0].Geometry.Y);
            line[0] = api.Line(sketch[4], point[0], point[1]);
            line[1] = api.Line(sketch[4], point[1], point[2]);
            line[2] = api.Line(sketch[4], point[2], point[3]);
            line[3] = api.Line(sketch[4], point[3], point[0]);
            circle[0] = api.Circle(sketch[4], api.Point(sketch[4], 0, 0), D3 / 2 / 10);
            profile[4] = api.Profile(sketch[4]);
            extrude[4] = api.Extrude(profile[4], T / 10, 0, 0);
            // Переход к коническому корпусу
            sketch[5] = api.Sketch(extrude[4].Faces[3]);
            circle[0] = api.Circle(sketch[5], api.Point(sketch[5], 0, 0), D3 / 2 / 10);
            circle[1] = api.Circle(sketch[5], api.Point(sketch[5], 0, 0), (D3 / 2 + T) / 10);
            profile[5] = api.Profile(sketch[5]);
            extrude[5] = api.Extrude(profile[5], H3 / 10, 0, 0);
            // Отверстия у основания
            sketch[6] = api.Sketch(api.GetCompDef().WorkPlanes[2]);
            point[0] = api.Point(sketch[6], 0, 0);
            point[1] = api.Point(sketch[6], 0, DR / 2 / 10);
            point[2] = api.Point(sketch[6], DR / 2 / 10, 0);
            line[0] = api.Line(sketch[6], point[0], point[1]);
            line[1] = api.Line(sketch[6], point[0], point[2]);
            sketch[6].DimensionConstraints.AddTwoLineAngle(line[0], line[1], api.GetTransGeom().CreatePoint2d(1, 1));
            sketch[6].GeometricConstraints.AddHorizontal((SketchEntity)line[1]);
            circle[0] = api.Circle(sketch[6], api.Point(sketch[6], DR / 10, -1), Or / 10);
            oParameters["d13"].Expression = "105 degree";
            sketch[6].GeometricConstraints.AddCoincident((SketchEntity)circle[0].CenterSketchPoint, (SketchEntity)point[1]);
            profile[6] = api.Profile(sketch[6]);
            extrude[6] = api.Extrude(profile[6], H1 / 10, 0, 1);
            var objCollection = api.ObjectCollection();
            objCollection.Add(extrude[6]);
            api.GetCompDef().Features.CircularPatternFeatures.Add(objCollection, api.GetCompDef().WorkAxes[2], true, 12, "360 degree", true, PatternComputeTypeEnum.kIdenticalCompute);
            // Трубки
            sketch[7] = api.Sketch(api.GetCompDef().WorkPlanes[1]);
            circle[0] = api.Circle(sketch[7], api.Point(sketch[7], H4 / 10, 0), D4D / 2 / 10);
            profile[7] = api.Profile(sketch[7]);
            extrude[7] = api.Extrude(profile[7], D3 / 10 + T * 2 / 10 + A * 2 / 10, 2, 0);
            sketch[8] = api.Sketch(api.GetCompDef().WorkPlanes[1]);
            circle[0] = api.Circle(sketch[8], api.Point(sketch[8], H4 / 10, 0), D4d / 2 / 10);
            profile[8] = api.Profile(sketch[8]);
            extrude[8] = api.Extrude(profile[8], D3 / 10 + T * 2 / 10 + A * 2 / 10, 2, 1);
            sketch[9] = api.Sketch(extrude[4].Faces[3]);
            circle[0] = api.Circle(sketch[9], api.Point(sketch[9], 0, 0), D3 / 2 / 10);
            profile[9] = api.Profile(sketch[9]);
            extrude[9] = api.Extrude(profile[9], H3, 0, 1);
            // Крышка
            sketch[10] = api.Sketch(api.GetCompDef().WorkPlanes[3]);
            point[0] = api.Point(sketch[10], L2 / 2 / 10, H5 / 10);
            point[1] = api.Point(sketch[10], point[0].Geometry.X, point[0].Geometry.Y + D51 / 2 / 10);
            point[2] = api.Point(sketch[10], point[1].Geometry.X + L51 / 10, point[1].Geometry.Y);
            point[3] = api.Point(sketch[10], point[2].Geometry.X, point[0].Geometry.Y + D52 / 2 / 10);
            point[4] = api.Point(sketch[10], point[3].Geometry.X + L52 / 10, point[3].Geometry.Y);
            point[5] = api.Point(sketch[10], point[4].Geometry.X, point[0].Geometry.Y + D53 / 2 / 10);
            point[6] = api.Point(sketch[10], point[5].Geometry.X + L53 / 10, point[5].Geometry.Y);
            point[7] = api.Point(sketch[10], point[6].Geometry.X, point[0].Geometry.Y + D54 / 2 / 10);
            point[8] = api.Point(sketch[10], point[7].Geometry.X + L54 / 10, point[7].Geometry.Y);
            point[9] = api.Point(sketch[10], point[8].Geometry.X, point[0].Geometry.Y);
            line[0] = api.Line(sketch[10], point[0], point[1]);
            line[1] = api.Line(sketch[10], point[1], point[2]);
            line[2] = api.Line(sketch[10], point[2], point[3]);
            line[3] = api.Line(sketch[10], point[3], point[4]);
            line[4] = api.Line(sketch[10], point[4], point[5]);
            line[5] = api.Line(sketch[10], point[5], point[6]);
            line[6] = api.Line(sketch[10], point[6], point[7]);
            line[7] = api.Line(sketch[10], point[7], point[8]);
            line[8] = api.Line(sketch[10], point[8], point[9]);
            line[9] = api.Line(sketch[10], point[9], point[0]);
            profile[10] = api.Profile(sketch[10]);
            api.Revolve(profile[10], line[9], 0);
            System.Windows.Forms.MessageBox.Show(formName + " завершено.", formName);
        }
    }
}
