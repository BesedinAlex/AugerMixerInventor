using Inventor;

namespace AugerMixer.Model.Parts
{
    static class Hold
    {
        public static double B = 100, A1 = 65, H = 120, S1 = 4, A = 45, K = 10, K1 = 25, B1 = 50, B2 = 45, C = 40, D6 = 12, A2 = 40, H1 = 8;
        public static void Build(InventorAPI api, string formName)
        {
            PlanarSketch[] sketch = new PlanarSketch[4];
            Profile[] profile = new Profile[4];
            SketchPoint[] point = new SketchPoint[5];
            SketchLine[] line = new SketchLine[5];
            SketchCircle[] circle = new SketchCircle[1];
            ExtrudeFeature[] extrude = new ExtrudeFeature[4];
            // Нижняя основа
            sketch[0] = api.Sketch(api.GetCompDef().WorkPlanes[3]);
            point[0] = api.Point(sketch[0], 0, 0);
            point[1] = api.Point(sketch[0], B / 10, 0);
            point[2] = api.Point(sketch[0], B / 10, -S1 / 10);
            point[3] = api.Point(sketch[0], 0, -S1 / 10);
            line[0] = api.Line(sketch[0], point[0], point[1]);
            line[1] = api.Line(sketch[0], point[1], point[2]);
            line[2] = api.Line(sketch[0], point[2], point[3]);
            line[3] = api.Line(sketch[0], point[3], point[0]);
            profile[0] = api.Profile(sketch[0]);
            extrude[0] = api.Extrude(profile[0], A1 / 10, 2, 0);
            // Боковые крепления
            var oWorkPlane1 = api.GetCompDef().WorkPlanes.AddByPlaneAndOffset(api.GetCompDef().WorkPlanes[3], A / 2 / 10);
            oWorkPlane1.Visible = false;
            sketch[1] = api.Sketch(oWorkPlane1);
            point[0] = api.Point(sketch[1], 0, 0);
            point[1] = api.Point(sketch[1], (B * System.Math.Sin(Parts.MainBody.Degree / 180 * System.Math.PI)) / 10, H * System.Math.Cos(Parts.MainBody.Degree / 180 * System.Math.PI) / 10 - S1 / 10);
            point[2] = api.Point(sketch[1], point[1].Geometry.X + K / 10, point[1].Geometry.Y);
            point[3] = api.Point(sketch[1], B / 10, K1 / 10);
            point[4] = api.Point(sketch[1], B / 10, 0);
            line[0] = api.Line(sketch[1], point[0], point[1]);
            line[1] = api.Line(sketch[1], point[1], point[2]);
            line[2] = api.Line(sketch[1], point[2], point[3]);
            line[3] = api.Line(sketch[1], point[3], point[4]);
            line[4] = api.Line(sketch[1], point[4], point[0]);
            profile[1] = api.Profile(sketch[1]);
            extrude[1] = api.Extrude(profile[1], S1 / 10, 0, 0);
            var objCollection1 = api.ObjectCollection();
            objCollection1.Add(extrude[1]);
            api.GetCompDef().Features.MirrorFeatures.AddByDefinition(api.GetCompDef().Features.MirrorFeatures.CreateDefinition(objCollection1, api.GetCompDef().WorkPlanes[3], PatternComputeTypeEnum.kIdenticalCompute));
            // Болтовое отверстие
            sketch[2] = api.Sketch(api.GetCompDef().WorkPlanes[3]);
            point[0] = api.Point(sketch[2], B / 10 - B1 / 10, -S1 / 10);
            point[1] = api.Point(sketch[2], point[0].Geometry.X + B2 / 10, point[0].Geometry.Y);
            point[2] = api.Point(sketch[2], point[1].Geometry.X, point[0].Geometry.Y - H1 / 10);
            point[3] = api.Point(sketch[2], point[0].Geometry.X, point[2].Geometry.Y);
            line[0] = api.Line(sketch[2], point[0], point[1]);
            line[1] = api.Line(sketch[2], point[1], point[2]);
            line[2] = api.Line(sketch[2], point[2], point[3]);
            line[3] = api.Line(sketch[2], point[3], point[0]);
            profile[2] = api.Profile(sketch[2]);
            extrude[2] = api.Extrude(profile[2], A2 / 10, 2, 0);
            sketch[3] = api.Sketch(api.GetCompDef().WorkPlanes[2]);
            circle[0] = api.Circle(sketch[3], api.Point(sketch[3], -(B / 10 - C / 10), 0), D6 / 2 / 10);
            profile[3] = api.Profile(sketch[3]);
            extrude[3] = api.Extrude(profile[3], H, 2, 1);
            // Резьба
            var EdgeCollection1 = api.EdgeCollection();
            EdgeCollection1.Add(extrude[3].SideFaces[1].Edges[1]);
            var ThreadFeatures1 = api.ThreadFeatures();
            var stInfo1 = ThreadFeatures1.CreateStandardThreadInfo(false, true, "ISO Metric profile", "M" + D6 + "x1.5", "6g");
            ThreadFeatures1.Add(extrude[3].SideFaces[1], extrude[3].SideFaces[1].Edges[2], (ThreadInfo)stInfo1, false, true, 0);
            System.Windows.Forms.MessageBox.Show(formName + " завершено.", formName);
        }
        public static void ChooseGOSTValues(int index = 0)
        {
            double[,] standardValues = {
                { 100, 65, 120, 4, 45, 10, 25, 50, 45, 40, 12, 40, 8 },
                { 160, 95, 190, 5, 75, 15, 40, 70, 65, 50, 12, 60, 10 },
                { 255, 155, 310, 8, 125, 25, 65, 120, 115, 90, 20, 100, 16 }
            };
            B = standardValues[index, 0];
            A1 = standardValues[index, 1];
            H = standardValues[index, 2];
            S1 = standardValues[index, 3];
            A = standardValues[index, 4];
            K = standardValues[index, 5];
            K1 = standardValues[index, 6];
            B1 = standardValues[index, 7];
            B2 = standardValues[index, 8];
            C = standardValues[index, 9];
            D6 = standardValues[index, 10];
            A2 = standardValues[index, 11];
            H1 = standardValues[index, 12];
        }
    }
}
