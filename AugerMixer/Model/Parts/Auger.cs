using Inventor;

namespace AugerMixer.Model.Parts
{
    class Auger
    {
        public static double H = 1700, D = 220, D1 = 100, H1 = 120, H2 = 1300, T = 25, H3 = 1500, A = 40;
        public static void Build(InventorAPI api, string formName)
        {
            PlanarSketch[] sketch = new PlanarSketch[4];
            Profile[] profile = new Profile[4];
            SketchPoint[] point = new SketchPoint[4];
            SketchLine[] line = new SketchLine[4];
            RevolveFeature[] revolve = new RevolveFeature[4];
            // Создание цилиндра
            sketch[0] = api.Sketch(api.GetCompDef().WorkPlanes[3]);
            point[0] = api.Point(sketch[0], 0, 0);
            point[1] = api.Point(sketch[0], 0, H / 10);
            point[2] = api.Point(sketch[0], D1 / 10 / 2, H / 10);
            point[3] = api.Point(sketch[0], D1 / 10 / 2, 0);
            line[0] = api.Line(sketch[0], point[0], point[1]);
            line[1] = api.Line(sketch[0], point[1], point[2]);
            line[2] = api.Line(sketch[0], point[2], point[3]);
            line[3] = api.Line(sketch[0], point[3], point[0]);
            profile[0] = api.Profile(sketch[0]);
            revolve[0] = api.Revolve(profile[0], line[0], 0);
            // Создание пружины
            sketch[1] = api.Sketch(api.GetCompDef().WorkPlanes[3]);
            point[0] = api.Point(sketch[1], D1 / 10 / 2, 0);
            point[1] = api.Point(sketch[1], D / 10 / 2, 0);
            point[3] = api.Point(sketch[1], D1 / 10 / 2, T / 10);
            line[0] = api.Line(sketch[1], point[0], point[1]);
            line[1] = api.Line(sketch[1], point[1], point[3]);
            line[2] = api.Line(sketch[1], point[3], point[0]);
            profile[1] = api.Profile(sketch[1]);
            var coil = api.GetCompDef().Features.CoilFeatures.AddByPitchAndHeight(profile[1], api.GetCompDef().WorkAxes[2], H1 / 10, H2 / 10, PartFeatureOperationEnum.kJoinOperation, false, false, 0, false, 0, 0, true);
            // Верхняя граница пружины
            sketch[2] = api.Sketch(api.GetCompDef().WorkPlanes[3]);
            point[0] = api.Point(sketch[2], 0, H2 / 10);
            point[1] = api.Point(sketch[2], 0, H2 / 10 + T / 10);
            point[2] = api.Point(sketch[2], D / 10 / 2, H2 / 10 + T / 10);
            point[3] = api.Point(sketch[2], D / 10 / 2, H2 / 10);
            line[0] = api.Line(sketch[2], point[0], point[1]);
            line[1] = api.Line(sketch[2], point[1], point[2]);
            line[2] = api.Line(sketch[2], point[2], point[3]);
            line[3] = api.Line(sketch[2], point[3], point[0]);
            profile[2] = api.Profile(sketch[2]);
            revolve[2] = api.Revolve(profile[2], line[0], 0);
            // Крепление
            sketch[3] = api.Sketch(api.GetCompDef().WorkPlanes[3]);
            point[0] = api.Point(sketch[3], 0, H3 / 10);
            point[1] = api.Point(sketch[3], 0, H3 / 10 + A / 10);
            point[2] = api.Point(sketch[3], D / 10 / 2, H3 / 10 + A / 10);
            point[3] = api.Point(sketch[3], D / 10 / 2, H3 / 10);
            line[0] = api.Line(sketch[3], point[0], point[1]);
            line[1] = api.Line(sketch[3], point[1], point[2]);
            line[2] = api.Line(sketch[3], point[2], point[3]);
            line[3] = api.Line(sketch[3], point[3], point[0]);
            profile[3] = api.Profile(sketch[3]);
            revolve[3] = api.Revolve(profile[3], line[0], 0);
            System.Windows.Forms.MessageBox.Show(formName + " завершено.", formName);
        }
    }
}
