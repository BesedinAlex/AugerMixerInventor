using Inventor;

namespace AugerMixer.Model.Parts
{
    static class MainBody
    {
        public static double H = 1635, Degree = 18, D = 1530, B = 100, A = 15, T = 40, Rb = 730, Rm = 24, Ds = 250;
        public static void Build(InventorAPI api, string formName)
        {
            var oParameters = api.GetCompDef().Parameters;
            PlanarSketch[] sketch = new PlanarSketch[2];
            Profile[] profile = new Profile[2];
            SketchPoint[] point = new SketchPoint[8];
            SketchLine[] line = new SketchLine[7];
            SketchCircle[] circle = new SketchCircle[1];
            // Создание конической основы
            sketch[0] = api.Sketch(api.GetCompDef().WorkPlanes[3]);
            point[0] = api.Point(sketch[0], 0.1, 0.1);
            point[1] = api.Point(sketch[0], 0.1, 0.2);
            line[0] = api.Line(sketch[0], point[0], point[1]);
            point[2] = api.Point(sketch[0], D / 2 - B, H);
            point[3] = api.Point(sketch[0], D / 2, H);
            point[4] = api.Point(sketch[0], D / 2, H - T);
            point[5] = api.Point(sketch[0], D / 2 - B + 1, H - T);
            point[6] = api.Point(sketch[0], Ds + 1, 0);
            point[7] = api.Point(sketch[0], Ds, 0);
            line[1] = api.Line(sketch[0], point[2], point[3]);
            line[2] = api.Line(sketch[0], point[3], point[4]);
            line[3] = api.Line(sketch[0], point[4], point[5]);
            line[4] = api.Line(sketch[0], point[5], point[6]);
            line[5] = api.Line(sketch[0], point[6], point[7]);
            line[6] = api.Line(sketch[0], point[7], point[2]);
            var SketchSize = api.GetTransGeom().CreatePoint2d(-1, -1); // Место для выноса размеров
            sketch[0].GeometricConstraints.AddVertical((SketchEntity)line[0]);
            sketch[0].GeometricConstraints.AddHorizontal((SketchEntity)line[1]);
            sketch[0].GeometricConstraints.AddVertical((SketchEntity)line[2]);
            sketch[0].GeometricConstraints.AddHorizontal((SketchEntity)line[3]);
            sketch[0].GeometricConstraints.AddHorizontal((SketchEntity)line[5]);
            sketch[0].GeometricConstraints.AddHorizontalAlign(point[0], point[7]);
            sketch[0].GeometricConstraints.AddParallel((SketchEntity)line[4], (SketchEntity)line[6]);
            sketch[0].DimensionConstraints.AddTwoPointDistance(point[0], point[7], DimensionOrientationEnum.kHorizontalDim, SketchSize); // Ds
            sketch[0].DimensionConstraints.AddTwoPointDistance(point[0], point[3], DimensionOrientationEnum.kVerticalDim, SketchSize); // H
            sketch[0].DimensionConstraints.AddTwoPointDistance(point[0], point[3], DimensionOrientationEnum.kHorizontalDim, SketchSize); // D
            sketch[0].DimensionConstraints.AddTwoPointDistance(point[2], point[3], DimensionOrientationEnum.kHorizontalDim, SketchSize); // B
            sketch[0].DimensionConstraints.AddOffset(line[4], (SketchEntity)line[6], SketchSize, false); // A
            sketch[0].DimensionConstraints.AddTwoPointDistance(point[3], point[4], DimensionOrientationEnum.kVerticalDim, SketchSize); // T
            oParameters["d0"].Expression = Ds / 2 + " mm";
            oParameters["d1"].Expression = H + " mm";
            oParameters["d2"].Expression = D / 2 + " mm";
            oParameters["d3"].Expression = B + " mm";
            oParameters["d4"].Expression = A + " mm";
            oParameters["d5"].Expression = T + " mm";
            point[0].MoveTo(api.GetTransGeom().CreatePoint2d(0, 0)); // Выравнивание осевой линии центра
            sketch[0].DimensionConstraints.AddTwoLineAngle(line[6], line[0], api.GetTransGeom().CreatePoint2d(10, 40), true);
            Degree = oParameters["d6"]._Value * (180 / System.Math.PI);
            profile[0] = api.Profile(sketch[0]);
            api.Revolve(profile[0], line[0], 0);
            // Отверстия
            sketch[1] = api.Sketch(api.GetCompDef().WorkPlanes[2]);
            point[0] = api.Point(sketch[1], 0, Rb / 10);
            circle[0] = api.Circle(sketch[1], point[0], Rm / 10 / 2);
            profile[1] = api.Profile(sketch[1]);
            var extrude = api.Extrude(profile[1], H / 10, 0, 1);
            var objCollection2 = api.ObjectCollection();
            objCollection2.Add(extrude);
            api.GetCompDef().Features.CircularPatternFeatures.Add(objCollection2, api.GetCompDef().WorkAxes[2], true, 3, "360 degree", true, PatternComputeTypeEnum.kIdenticalCompute);
            System.Windows.Forms.MessageBox.Show(formName + " завершено.", formName);
        }
    }
}
