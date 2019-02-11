using Inventor;

namespace BesedinCoursework
{
    public class Build
    {
        public static void bottom(InventorAPI api, string formName)
        {
            Parameters oParameters = api.getCompDef().Parameters;
            PlanarSketch[] sketch = new PlanarSketch[11];
            Profile[] profile = new Profile[11];
            SketchPoint[] point = new SketchPoint[10];
            SketchLine[] line = new SketchLine[10];
            SketchCircle[] circle = new SketchCircle[2];
            ExtrudeFeature[] extrude = new ExtrudeFeature[10];
            // Основание
            sketch[0] = api.sketch(api.getCompDef().WorkPlanes[2]);
            circle[0] = api.circle(sketch[0], api.point(sketch[0], 0, 0), Bottom.D1 / 10 / 2);
            profile[0] = api.profile(sketch[0]);
            extrude[0] = api.extrude(profile[0], Bottom.H1 / 10, 0, 0);
            sketch[1] = api.sketch(extrude[0].Faces[3]);
            circle[0] = api.circle(sketch[1], api.point(sketch[1], 0, 0), Bottom.D2 / 10 / 2);
            profile[1] = api.profile(sketch[1]);
            extrude[1] = api.extrude(profile[1], Bottom.H2 / 10, 0, 0);
            // Кубическая часть корпуса
            sketch[2] = api.sketch(extrude[1].Faces[2]);
            point[0] = api.point(sketch[2], Bottom.L2 / 10 / 2, Bottom.L1 / 10 / 2);
            point[1] = api.point(sketch[2], point[0].Geometry.X, -point[0].Geometry.Y);
            point[2] = api.point(sketch[2], -point[0].Geometry.X, -point[0].Geometry.Y);
            point[3] = api.point(sketch[2], -point[0].Geometry.X, point[0].Geometry.Y);
            line[0] = api.line(sketch[2], point[0], point[1]);
            line[1] = api.line(sketch[2], point[1], point[2]);
            line[2] = api.line(sketch[2], point[2], point[3]);
            line[3] = api.line(sketch[2], point[3], point[0]);
            profile[2] = api.profile(sketch[2]);
            extrude[2] = api.extrude(profile[2], Bottom.T / 10, 0, 0);
            sketch[3] = api.sketch(extrude[2].Faces[6]);
            point[0] = api.point(sketch[3], Bottom.L2 / 10 / 2, Bottom.L1 / 10 / 2);
            point[1] = api.point(sketch[3], point[0].Geometry.X, -point[0].Geometry.Y);
            point[2] = api.point(sketch[3], -point[0].Geometry.X, -point[0].Geometry.Y);
            point[3] = api.point(sketch[3], -point[0].Geometry.X, point[0].Geometry.Y);
            line[0] = api.line(sketch[3], point[0], point[1]);
            line[1] = api.line(sketch[3], point[1], point[2]);
            line[2] = api.line(sketch[3], point[2], point[3]);
            line[3] = api.line(sketch[3], point[3], point[0]);
            point[4] = api.point(sketch[3], point[0].Geometry.X - Bottom.T / 10, point[0].Geometry.Y - Bottom.T / 10);
            point[5] = api.point(sketch[3], point[0].Geometry.X - Bottom.T / 10, -point[0].Geometry.Y + Bottom.T / 10);
            point[6] = api.point(sketch[3], -point[0].Geometry.X + Bottom.T / 10, -point[0].Geometry.Y + Bottom.T / 10);
            point[7] = api.point(sketch[3], -point[0].Geometry.X + Bottom.T / 10, point[0].Geometry.Y - Bottom.T / 10);
            line[4] = api.line(sketch[3], point[4], point[5]);
            line[5] = api.line(sketch[3], point[5], point[6]);
            line[6] = api.line(sketch[3], point[6], point[7]);
            line[7] = api.line(sketch[3], point[7], point[4]);
            profile[3] = api.profile(sketch[3]);
            extrude[3] = api.extrude(profile[3], (Bottom.HB - Bottom.T * 2) / 10, 0, 0);
            WorkPlane oWorkPlane4 = api.getCompDef().WorkPlanes.AddByPlaneAndOffset(api.getCompDef().WorkPlanes[2], (Bottom.H1 + Bottom.H2 + Bottom.HB - Bottom.T) / 10);
            oWorkPlane4.Visible = false;
            sketch[4] = api.sketch(oWorkPlane4);
            point[0] = api.point(sketch[4], Bottom.L2 / 10 / 2, Bottom.L1 / 10 / 2);
            point[1] = api.point(sketch[4], point[0].Geometry.X, -point[0].Geometry.Y);
            point[2] = api.point(sketch[4], -point[0].Geometry.X, -point[0].Geometry.Y);
            point[3] = api.point(sketch[4], -point[0].Geometry.X, point[0].Geometry.Y);
            line[0] = api.line(sketch[4], point[0], point[1]);
            line[1] = api.line(sketch[4], point[1], point[2]);
            line[2] = api.line(sketch[4], point[2], point[3]);
            line[3] = api.line(sketch[4], point[3], point[0]);
            circle[0] = api.circle(sketch[4], api.point(sketch[4], 0, 0), Bottom.D3 / 2 / 10);
            profile[4] = api.profile(sketch[4]);
            extrude[4] = api.extrude(profile[4], Bottom.T / 10, 0, 0);
            // Переход к коническому корпусу
            sketch[5] = api.sketch(extrude[4].Faces[3]);
            circle[0] = api.circle(sketch[5], api.point(sketch[5], 0, 0), Bottom.D3 / 2 / 10);
            circle[1] = api.circle(sketch[5], api.point(sketch[5], 0, 0), (Bottom.D3 / 2 + Bottom.T) / 10);
            profile[5] = api.profile(sketch[5]);
            extrude[5] = api.extrude(profile[5], Bottom.H3 / 10, 0, 0);
            // Отверстия у основания
            sketch[6] = api.sketch(api.getCompDef().WorkPlanes[2]);
            point[0] = api.point(sketch[6], 0, 0);
            point[1] = api.point(sketch[6], 0, Bottom.DR / 2 / 10);
            point[2] = api.point(sketch[6], Bottom.DR / 2 / 10, 0);
            line[0] = api.line(sketch[6], point[0], point[1]);
            line[1] = api.line(sketch[6], point[0], point[2]);
            sketch[6].DimensionConstraints.AddTwoLineAngle(line[0], line[1], api.getTransGeom().CreatePoint2d(1, 1));
            sketch[6].GeometricConstraints.AddHorizontal((SketchEntity)line[1]);
            circle[0] = api.circle(sketch[6], api.point(sketch[6], Bottom.DR / 10, -1), Bottom.oR / 10);
            oParameters["d13"].Expression = "105 degree";
            sketch[6].GeometricConstraints.AddCoincident((SketchEntity)circle[0].CenterSketchPoint, (SketchEntity)point[1]);
            profile[6] = api.profile(sketch[6]);
            extrude[6] = api.extrude(profile[6], Bottom.H1 / 10, 0, 1);
            ObjectCollection objCollection = api.objectCollection();
            objCollection.Add(extrude[6]);
            CircularPatternFeature CircularPatternFeature6 = api.getCompDef().Features.CircularPatternFeatures.Add(objCollection, api.getCompDef().WorkAxes[2], true, 12, "360 degree", true, PatternComputeTypeEnum.kIdenticalCompute);
            // Трубки
            sketch[7] = api.sketch(api.getCompDef().WorkPlanes[1]);
            circle[0] = api.circle(sketch[7], api.point(sketch[7], Bottom.H4 / 10, 0), Bottom.D4D / 2 / 10);
            profile[7] = api.profile(sketch[7]);
            extrude[7] = api.extrude(profile[7], Bottom.D3 / 10 + Bottom.T * 2 / 10 + Bottom.A * 2 / 10, 2, 0);
            sketch[8] = api.sketch(api.getCompDef().WorkPlanes[1]);
            circle[0] = api.circle(sketch[8], api.point(sketch[8], Bottom.H4 / 10, 0), Bottom.D4d / 2 / 10);
            profile[8] = api.profile(sketch[8]);
            extrude[8] = api.extrude(profile[8], Bottom.D3 / 10 + Bottom.T * 2 / 10 + Bottom.A * 2 / 10, 2, 1);
            sketch[9] = api.sketch(extrude[4].Faces[3]);
            circle[0] = api.circle(sketch[9], api.point(sketch[9], 0, 0), Bottom.D3 / 2 / 10);
            profile[9] = api.profile(sketch[9]);
            extrude[9] = api.extrude(profile[9], Bottom.H3, 0, 1);
            // Крышка
            sketch[10] = api.sketch(api.getCompDef().WorkPlanes[3]);
            point[0] = api.point(sketch[10], Bottom.L2 / 2 / 10, Bottom.H5 / 10);
            point[1] = api.point(sketch[10], point[0].Geometry.X, point[0].Geometry.Y + Bottom.D51 / 2 / 10);
            point[2] = api.point(sketch[10], point[1].Geometry.X + Bottom.L51 / 10, point[1].Geometry.Y);
            point[3] = api.point(sketch[10], point[2].Geometry.X, point[0].Geometry.Y + Bottom.D52 / 2 / 10);
            point[4] = api.point(sketch[10], point[3].Geometry.X + Bottom.L52 / 10, point[3].Geometry.Y);
            point[5] = api.point(sketch[10], point[4].Geometry.X, point[0].Geometry.Y + Bottom.D53 / 2 / 10);
            point[6] = api.point(sketch[10], point[5].Geometry.X + Bottom.L53 / 10, point[5].Geometry.Y);
            point[7] = api.point(sketch[10], point[6].Geometry.X, point[0].Geometry.Y + Bottom.D54 / 2 / 10);
            point[8] = api.point(sketch[10], point[7].Geometry.X + Bottom.L54 / 10, point[7].Geometry.Y);
            point[9] = api.point(sketch[10], point[8].Geometry.X, point[0].Geometry.Y);
            line[0] = api.line(sketch[10], point[0], point[1]);
            line[1] = api.line(sketch[10], point[1], point[2]);
            line[2] = api.line(sketch[10], point[2], point[3]);
            line[3] = api.line(sketch[10], point[3], point[4]);
            line[4] = api.line(sketch[10], point[4], point[5]);
            line[5] = api.line(sketch[10], point[5], point[6]);
            line[6] = api.line(sketch[10], point[6], point[7]);
            line[7] = api.line(sketch[10], point[7], point[8]);
            line[8] = api.line(sketch[10], point[8], point[9]);
            line[9] = api.line(sketch[10], point[9], point[0]);
            profile[10] = api.profile(sketch[10]);
            RevolveFeature revolvefeature10 = api.revolve(profile[10], line[9], 0);
            System.Windows.Forms.MessageBox.Show("Создание корпуса для выгрузки материалов завершено.", formName);
        }
    }
}
