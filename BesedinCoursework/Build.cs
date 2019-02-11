using Inventor;

namespace BesedinCoursework
{
    /// <summary>
    /// Contains functions to make parts and assemblies.
    /// Can be extended in future.
    /// </summary>
    public class Build
    {
        public static void assembly(Application app, System.Windows.Forms.OpenFileDialog openFileDialog, string formName)
        {
            System.Collections.Generic.Dictionary<string, string> fileName = new System.Collections.Generic.Dictionary<string, string>();
            System.Collections.Generic.Dictionary<string, PartDocument> partDocument = new System.Collections.Generic.Dictionary<string, PartDocument>();
            string[] partIndex = { "КК", "К", "Шнек", "КВ", "БО" };
            string[] partName = { "Конический корпус", "Крышка", "Шнек", "Корпус для выгрузки материалов", "Боковые опоры" };
            for (int i = 0; i < partIndex.Length; i++)
            {
                openFileDialog.Filter = "Inventor Part Document|*.ipt";
                openFileDialog.Title = "Открыть файл " + partName[i] + ".";
                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (!string.IsNullOrWhiteSpace(openFileDialog.FileName))
                    {
                        partDocument[partIndex[i]] = (PartDocument)app.Documents.Open(openFileDialog.FileName, true);
                        partDocument[partIndex[i]].DisplayName = partName[i];
                        fileName[partIndex[i]] = openFileDialog.FileName;
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Сборка была прервана по причине того, \nчто не все детали были открыты приложением.", formName);
                    return;
                }
            }
            // Инициализация сборки
            AssemblyDocument assemblyDocument = (AssemblyDocument)app.Documents.Add(DocumentTypeEnum.kAssemblyDocumentObject, app.FileManager.GetTemplateFile(DocumentTypeEnum.kAssemblyDocumentObject));
            AssemblyComponentDefinition assemblyComponentDefinition = assemblyDocument.ComponentDefinition;
            InventorAPI api = new InventorAPI(assemblyComponentDefinition);
            assemblyDocument.DisplayName = "Планетарно-шнековый смеситель";
            for (int i = 0; i < partIndex.Length; i++)
                assemblyDocument.ComponentDefinition.Occurrences.Add(fileName[partIndex[i]], app.TransientGeometry.CreateMatrix());
            // Присоединение крышки
            api.Surface(1, 1, 2, 19);
            api.Surface(1, 2, 2, 20);
            try
            {
                api.Surface(1, 9, 2, 114);
            }
            catch
            {
                api.Surface(1, 9, 2, 113);
            }
            // Присоединение шнека
            api.Axis(1, 3, 3, 3);
            api.Plane(1, 3, 3, 3);
            api.PlaneAngle(1, 1, 3, 1, System.Convert.ToString(MainBody.Degree));
            // Присоединение корпуса для выгрузки материалов
            api.Axis(1, 2, 4, 2);
            api.Surface(1, 9, 4, 28, -MainBody.H / 10, false);
            api.Plane(1, 3, 4, 3);
            // Присоединение боковых опор
            api.Plane(1, 3, 5, 3);
            api.Plane(1, 2, 5, 2, System.Convert.ToString(MainBody.H - MainBody.H / 4));
            api.Plane(1, 1, 5, 1, System.Convert.ToString((MainBody.H - (MainBody.H - MainBody.H / 4)) * 1.32));
            ObjectCollection oB = app.TransientObjects.CreateObjectCollection();
            oB.Add(assemblyComponentDefinition.Occurrences[5]);
            assemblyComponentDefinition.OccurrencePatterns.AddCircularPattern(oB, assemblyComponentDefinition.WorkAxes[2], true, "90 degree", 4);
            System.Windows.Forms.MessageBox.Show("Сборка планетарно-шнекового смесителя завершена.", formName);
        }
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
            RevolveFeature revolvefeature = api.revolve(profile[10], line[9], 0);
            System.Windows.Forms.MessageBox.Show(formName + " завершено.", formName);
        }
        public static void hold(InventorAPI api, string formName)
        {
            PlanarSketch[] sketch = new PlanarSketch[4];
            Profile[] profile = new Profile[4];
            SketchPoint[] point = new SketchPoint[5];
            SketchLine[] line = new SketchLine[5];
            SketchCircle[] circle = new SketchCircle[1];
            ExtrudeFeature[] extrude = new ExtrudeFeature[4];
            // Нижняя основа
            sketch[0] = api.sketch(api.getCompDef().WorkPlanes[3]);
            point[0] = api.point(sketch[0], 0, 0);
            point[1] = api.point(sketch[0], Hold.b / 10, 0);
            point[2] = api.point(sketch[0], Hold.b / 10, -Hold.S1 / 10);
            point[3] = api.point(sketch[0], 0, -Hold.S1 / 10);
            line[0] = api.line(sketch[0], point[0], point[1]);
            line[1] = api.line(sketch[0], point[1], point[2]);
            line[2] = api.line(sketch[0], point[2], point[3]);
            line[3] = api.line(sketch[0], point[3], point[0]);
            profile[0] = api.profile(sketch[0]);
            extrude[0] = api.extrude(profile[0], Hold.a1 / 10, 2, 0);
            // Боковые крепления
            WorkPlane oWorkPlane1 = api.getCompDef().WorkPlanes.AddByPlaneAndOffset(api.getCompDef().WorkPlanes[3], Hold.a / 2 / 10);
            oWorkPlane1.Visible = false;
            sketch[1] = api.sketch(oWorkPlane1);
            point[0] = api.point(sketch[1], 0, 0);
            point[1] = api.point(sketch[1], (Hold.b * System.Math.Sin(MainBody.Degree / 180 * System.Math.PI)) / 10, Hold.h * System.Math.Cos(MainBody.Degree / 180 * System.Math.PI) / 10 - Hold.S1 / 10);
            point[2] = api.point(sketch[1], point[1].Geometry.X + Hold.K / 10, point[1].Geometry.Y);
            point[3] = api.point(sketch[1], Hold.b / 10, Hold.K1 / 10);
            point[4] = api.point(sketch[1], Hold.b / 10, 0);
            line[0] = api.line(sketch[1], point[0], point[1]);
            line[1] = api.line(sketch[1], point[1], point[2]);
            line[2] = api.line(sketch[1], point[2], point[3]);
            line[3] = api.line(sketch[1], point[3], point[4]);
            line[4] = api.line(sketch[1], point[4], point[0]);
            profile[1] = api.profile(sketch[1]);
            extrude[1] = api.extrude(profile[1], Hold.S1 / 10, 0, 0);
            ObjectCollection objCollection1 = api.objectCollection();
            objCollection1.Add(extrude[1]);
            api.getCompDef().Features.MirrorFeatures.AddByDefinition(api.getCompDef().Features.MirrorFeatures.CreateDefinition(objCollection1, api.getCompDef().WorkPlanes[3], PatternComputeTypeEnum.kIdenticalCompute));
            // Болтовое отверстие
            sketch[2] = api.sketch(api.getCompDef().WorkPlanes[3]);
            point[0] = api.point(sketch[2], Hold.b / 10 - Hold.b1 / 10, -Hold.S1 / 10);
            point[1] = api.point(sketch[2], point[0].Geometry.X + Hold.b2 / 10, point[0].Geometry.Y);
            point[2] = api.point(sketch[2], point[1].Geometry.X, point[0].Geometry.Y - Hold.h1 / 10);
            point[3] = api.point(sketch[2], point[0].Geometry.X, point[2].Geometry.Y);
            line[0] = api.line(sketch[2], point[0], point[1]);
            line[1] = api.line(sketch[2], point[1], point[2]);
            line[2] = api.line(sketch[2], point[2], point[3]);
            line[3] = api.line(sketch[2], point[3], point[0]);
            profile[2] = api.profile(sketch[2]);
            extrude[2] = api.extrude(profile[2], Hold.a2 / 10, 2, 0);
            sketch[3] = api.sketch(api.getCompDef().WorkPlanes[2]);
            circle[0] = api.circle(sketch[3], api.point(sketch[3], -(Hold.b / 10 - Hold.c / 10), 0), Hold.d6 / 2 / 10);
            profile[3] = api.profile(sketch[3]);
            extrude[3] = api.extrude(profile[3], Hold.h, 2, 1);
            // Резьба
            EdgeCollection EdgeCollection1 = api.edgeCollection();
            EdgeCollection1.Add(extrude[3].SideFaces[1].Edges[1]);
            ThreadFeatures ThreadFeatures1 = api.threadFeatures();
            StandardThreadInfo stInfo1 = ThreadFeatures1.CreateStandardThreadInfo(false, true, "ISO Metric profile", "M" + Hold.d6 + "x1.5", "6g");
            ThreadFeatures1.Add(extrude[3].SideFaces[1], extrude[3].SideFaces[1].Edges[2], (ThreadInfo)stInfo1, false, true, 0);
            System.Windows.Forms.MessageBox.Show(formName + " завершено.", formName);
        }
        public static void mainBody(InventorAPI api, string formName)
        {
            Parameters oParameters = api.getCompDef().Parameters;
            PlanarSketch[] sketch = new PlanarSketch[2];
            Profile[] profile = new Profile[2];
            SketchPoint[] point = new SketchPoint[8];
            SketchLine[] line = new SketchLine[7];
            SketchCircle[] circle = new SketchCircle[1];
            // Создание конической основы
            sketch[0] = api.sketch(api.getCompDef().WorkPlanes[3]);
            point[0] = api.point(sketch[0], 0.1, 0.1);
            point[1] = api.point(sketch[0], 0.1, 0.2);
            line[0] = api.line(sketch[0], point[0], point[1]);
            point[2] = api.point(sketch[0], MainBody.D / 2 - MainBody.B, MainBody.H);
            point[3] = api.point(sketch[0], MainBody.D / 2, MainBody.H);
            point[4] = api.point(sketch[0], MainBody.D / 2, MainBody.H - MainBody.T);
            point[5] = api.point(sketch[0], MainBody.D / 2 - MainBody.B + 1, MainBody.H - MainBody.T);
            point[6] = api.point(sketch[0], MainBody.Ds + 1, 0);
            point[7] = api.point(sketch[0], MainBody.Ds, 0);
            line[1] = api.line(sketch[0], point[2], point[3]);
            line[2] = api.line(sketch[0], point[3], point[4]);
            line[3] = api.line(sketch[0], point[4], point[5]);
            line[4] = api.line(sketch[0], point[5], point[6]);
            line[5] = api.line(sketch[0], point[6], point[7]);
            line[6] = api.line(sketch[0], point[7], point[2]);
            Point2d SketchSize = api.getTransGeom().CreatePoint2d(-1, -1); // Место для выноса размеров
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
            oParameters["d0"].Expression = MainBody.Ds / 2 + " mm";
            oParameters["d1"].Expression = MainBody.H + " mm";
            oParameters["d2"].Expression = MainBody.D / 2 + " mm";
            oParameters["d3"].Expression = MainBody.B + " mm";
            oParameters["d4"].Expression = MainBody.A + " mm";
            oParameters["d5"].Expression = MainBody.T + " mm";
            point[0].MoveTo(api.getTransGeom().CreatePoint2d(0, 0)); // Выравнивание осевой линии центра
            sketch[0].DimensionConstraints.AddTwoLineAngle(line[6], line[0], api.getTransGeom().CreatePoint2d(10, 40), true);
            MainBody.Degree = oParameters["d6"]._Value * (180 / System.Math.PI);
            profile[0] = api.profile(sketch[0]);
            RevolveFeature revolve = api.revolve(profile[0], line[0], 0);
            // Отверстия
            sketch[1] = api.sketch(api.getCompDef().WorkPlanes[2]);
            point[0] = api.point(sketch[1], 0, MainBody.Rb / 10);
            circle[0] = api.circle(sketch[1], point[0], MainBody.Rm / 10 / 2);
            profile[1] = api.profile(sketch[1]);
            ExtrudeFeature extrude = api.extrude(profile[1], MainBody.H / 10, 0, 1);
            ObjectCollection objCollection2 = api.objectCollection();
            objCollection2.Add(extrude);
            CircularPatternFeature CircularPatternFeature2 = api.getCompDef().Features.CircularPatternFeatures.Add(objCollection2, api.getCompDef().WorkAxes[2], true, 3, "360 degree", true, PatternComputeTypeEnum.kIdenticalCompute);
            System.Windows.Forms.MessageBox.Show(formName + " завершено.", formName);
        }
        public static void screw(InventorAPI api, string formName)
        {
            PlanarSketch[] sketch = new PlanarSketch[4];
            Profile[] profile = new Profile[4];
            SketchPoint[] point = new SketchPoint[4];
            SketchLine[] line = new SketchLine[4];
            RevolveFeature[] revolve = new RevolveFeature[4];
            // Создание цилиндра
            sketch[0] = api.sketch(api.getCompDef().WorkPlanes[3]);
            point[0] = api.point(sketch[0], 0, 0);
            point[1] = api.point(sketch[0], 0, Screw.H / 10);
            point[2] = api.point(sketch[0], Screw.D1 / 10 / 2, Screw.H / 10);
            point[3] = api.point(sketch[0], Screw.D1 / 10 / 2, 0);
            line[0] = api.line(sketch[0], point[0], point[1]);
            line[1] = api.line(sketch[0], point[1], point[2]);
            line[2] = api.line(sketch[0], point[2], point[3]);
            line[3] = api.line(sketch[0], point[3], point[0]);
            profile[0] = api.profile(sketch[0]);
            revolve[0] = api.revolve(profile[0], line[0], 0);
            // Создание пружины
            sketch[1] = api.sketch(api.getCompDef().WorkPlanes[3]);
            point[0] = api.point(sketch[1], Screw.D1 / 10 / 2, 0);
            point[1] = api.point(sketch[1], Screw.D / 10 / 2, 0);
            point[3] = api.point(sketch[1], Screw.D1 / 10 / 2, Screw.T / 10);
            line[0] = api.line(sketch[1], point[0], point[1]);
            line[1] = api.line(sketch[1], point[1], point[3]);
            line[2] = api.line(sketch[1], point[3], point[0]);
            profile[1] = api.profile(sketch[1]);
            CoilFeature coil = api.getCompDef().Features.CoilFeatures.AddByPitchAndHeight(profile[1], api.getCompDef().WorkAxes[2], Screw.H1 / 10, Screw.H2 / 10, PartFeatureOperationEnum.kJoinOperation, false, false, 0, false, 0, 0, true);
            // Верхняя граница пружины
            sketch[2] = api.sketch(api.getCompDef().WorkPlanes[3]);
            point[0] = api.point(sketch[2], 0, Screw.H2 / 10);
            point[1] = api.point(sketch[2], 0, Screw.H2 / 10 + Screw.T / 10);
            point[2] = api.point(sketch[2], Screw.D / 10 / 2, Screw.H2 / 10 + Screw.T / 10);
            point[3] = api.point(sketch[2], Screw.D / 10 / 2, Screw.H2 / 10);
            line[0] = api.line(sketch[2], point[0], point[1]);
            line[1] = api.line(sketch[2], point[1], point[2]);
            line[2] = api.line(sketch[2], point[2], point[3]);
            line[3] = api.line(sketch[2], point[3], point[0]);
            profile[2] = api.profile(sketch[2]);
            revolve[2] = api.revolve(profile[2], line[0], 0);
            // Крепление
            sketch[3] = api.sketch(api.getCompDef().WorkPlanes[3]);
            point[0] = api.point(sketch[3], 0, Screw.H3 / 10);
            point[1] = api.point(sketch[3], 0, Screw.H3 / 10 + Screw.A / 10);
            point[2] = api.point(sketch[3], Screw.D / 10 / 2, Screw.H3 / 10 + Screw.A / 10);
            point[3] = api.point(sketch[3], Screw.D / 10 / 2, Screw.H3 / 10);
            line[0] = api.line(sketch[3], point[0], point[1]);
            line[1] = api.line(sketch[3], point[1], point[2]);
            line[2] = api.line(sketch[3], point[2], point[3]);
            line[3] = api.line(sketch[3], point[3], point[0]);
            profile[3] = api.profile(sketch[3]);
            revolve[3] = api.revolve(profile[3], line[0], 0);
            System.Windows.Forms.MessageBox.Show(formName + " завершено.", formName);
        }
        public static void top(InventorAPI api, string formName)
        {
            Parameters oParameters = api.getCompDef().Parameters;
            PlanarSketch[] sketch = new PlanarSketch[12];
            Profile[] profile = new Profile[12];
            SketchPoint[] point = new SketchPoint[8];
            SketchLine[] line = new SketchLine[8];
            SketchCircle[] circle = new SketchCircle[1];
            ExtrudeFeature[] extrude = new ExtrudeFeature[12];
            // Основа корпуса
            sketch[0] = api.sketch(api.getCompDef().WorkPlanes[3]);
            point[0] = api.point(sketch[0], 0, Top.H / 10 - Top.T / 10);
            point[1] = api.point(sketch[0], 0, Top.H / 10);
            point[2] = api.point(sketch[0], Top.D1 / 10 / 2, Top.H / 10);
            point[3] = api.point(sketch[0], Top.D1 / 10 / 2, Top.T / 10);
            point[4] = api.point(sketch[0], Top.D / 10 / 2, Top.T / 10);
            point[5] = api.point(sketch[0], Top.D / 10 / 2, 0);
            point[6] = api.point(sketch[0], Top.D1 / 10 / 2 - Top.T / 10, 0);
            point[7] = api.point(sketch[0], Top.D1 / 10 / 2 - Top.T / 10, Top.H / 10 - Top.T / 10);
            line[0] = api.line(sketch[0], point[0], point[1]);
            line[1] = api.line(sketch[0], point[1], point[2]);
            line[2] = api.line(sketch[0], point[2], point[3]);
            line[3] = api.line(sketch[0], point[3], point[4]);
            line[4] = api.line(sketch[0], point[4], point[5]);
            line[5] = api.line(sketch[0], point[5], point[6]);
            line[6] = api.line(sketch[0], point[6], point[7]);
            line[7] = api.line(sketch[0], point[7], point[0]);
            profile[0] = api.profile(sketch[0]);
            RevolveFeature revolveFeature = api.revolve(profile[0], line[0], 0);
            // Ребра жесткости
            sketch[1] = api.sketch(api.getCompDef().WorkPlanes[3]);
            point[0] = api.point(sketch[1], Top.D1 / 10 / 2 - Top.T / 10, Top.H / 10);
            point[1] = api.point(sketch[1], point[0].Geometry.X + Top.T / 10 * 2, Top.H / 10);
            point[2] = api.point(sketch[1], Top.D / 10 / 2, Top.T / 10 * 2);
            point[3] = api.point(sketch[1], Top.D / 10 / 2, Top.T / 10);
            point[4] = api.point(sketch[1], point[0].Geometry.X, Top.T / 10);
            line[0] = api.line(sketch[1], point[0], point[1]);
            line[0] = api.line(sketch[1], point[1], point[2]);
            line[0] = api.line(sketch[1], point[2], point[3]);
            line[0] = api.line(sketch[1], point[3], point[4]);
            line[0] = api.line(sketch[1], point[4], point[0]);
            profile[1] = api.profile(sketch[1]);
            extrude[1] = api.extrude(profile[1], Top.A / 10, 2, 0);
            ObjectCollection objCollection1 = api.objectCollection();
            objCollection1.Add(extrude[1]);
            CircularPatternFeature CircularPatternFeature2 = api.getCompDef().Features.CircularPatternFeatures.Add(objCollection1, api.getCompDef().WorkAxes[2], true, Top.ACount, "360 degree", true, PatternComputeTypeEnum.kIdenticalCompute);
            // Резервные проходы (2 штуки)
            WorkPlane oWorkPlane2 = api.getCompDef().WorkPlanes.AddByPlaneAndOffset(api.getCompDef().WorkPlanes[2], Top.HR / 10);
            oWorkPlane2.Visible = false;
            sketch[2] = api.sketch(oWorkPlane2);
            circle[0] = api.circle(sketch[2], api.point(sketch[2], Top.RR / 10, 0), Top.DR / 10 / 2);
            profile[2] = api.profile(sketch[2]);
            extrude[2] = api.extrude(profile[2], Top.hR / 10, 2, 0);
            sketch[3] = api.sketch(oWorkPlane2);
            circle[0] = api.circle(sketch[3], api.point(sketch[3], Top.RR / 10, 0), Top.dR / 10 / 2);
            profile[3] = api.profile(sketch[3]);
            extrude[3] = api.extrude(profile[3], Top.HR / 10 - Top.H / 10, 1, 0);
            sketch[4] = api.sketch(oWorkPlane2);
            circle[0] = api.circle(sketch[4], api.point(sketch[4], Top.RR / 10, 0), Top.TR / 10 / 2);
            profile[4] = api.profile(sketch[4]);
            extrude[4] = api.extrude(profile[4], 10000, 2, 1);
            PlanarSketch oSketch5 = api.getCompDef().Sketches.Add(oWorkPlane2);
            sketch[5] = api.sketch(oWorkPlane2);
            circle[0] = api.circle(sketch[5], api.point(sketch[5], Top.RR / 10, Top.OR / 10), Top.oR / 10 / 2);
            profile[5] = api.profile(sketch[5]);
            extrude[5] = api.extrude(profile[5], Top.hR / 10, 2, 1);
            WorkAxis Axis5 = api.getCompDef().WorkAxes.AddByRevolvedFace(extrude[4].Faces[1]);
            Axis5.Visible = false;
            ObjectCollection objCollection5 = api.objectCollection();
            objCollection5.Add(extrude[5]);
            CircularPatternFeature CircularPatternFeature5 = api.getCompDef().Features.CircularPatternFeatures.Add(objCollection5, Axis5, true, 8, "360 degree", true, PatternComputeTypeEnum.kIdenticalCompute);
            ObjectCollection objCollection6 = api.objectCollection();
            for (int i = 2; i < 6; i++)
                objCollection6.Add(extrude[i]);
            objCollection6.Add(CircularPatternFeature5);
            CircularPatternFeature CircularPatternFeature6 = api.getCompDef().Features.CircularPatternFeatures.Add(objCollection6, api.getCompDef().WorkAxes[2], true, 2, "150 degree", true, PatternComputeTypeEnum.kIdenticalCompute);
            // Отверстия под болты
            sketch[7] = api.sketch(api.getCompDef().WorkPlanes[2]);
            point[0] = api.point(sketch[7], 0, 0);
            point[1] = api.point(sketch[7], 0, Top.MBRb / 10);
            point[2] = api.point(sketch[7], Top.MBRb / 10, 0);
            line[0] = api.line(sketch[7], point[1], point[0]);
            line[1] = api.line(sketch[7], point[0], point[2]);
            sketch[7].DimensionConstraints.AddTwoLineAngle(line[0], line[1], api.getTransGeom().CreatePoint2d(1, 1));
            sketch[7].GeometricConstraints.AddHorizontal((SketchEntity)line[1]);
            circle[0] = api.circle(sketch[7], point[1], Top.MBRm / 10 / 2);
            oParameters["d21"].Expression = "82.5 degree";
            sketch[7].GeometricConstraints.AddCoincident((SketchEntity)circle[0].CenterSketchPoint, (SketchEntity)point[1]);
            profile[7] = api.profile(sketch[7]);
            extrude[7] = api.extrude(profile[7], 10000, 2, 1);
            ObjectCollection objCollection7 = api.objectCollection();
            objCollection7.Add(extrude[7]);
            CircularPatternFeature CircularPatternFeature7 = api.getCompDef().Features.CircularPatternFeatures.Add(objCollection7, api.getCompDef().WorkAxes[2], true, 3, "360 degree", true, PatternComputeTypeEnum.kIdenticalCompute);
            // Загрузка сыпучих материалов
            sketch[8] = api.sketch(oWorkPlane2);
            point[0] = api.point(sketch[8], 0, 0);
            point[1] = api.point(sketch[8], 0, -Top.R1R / 10);
            point[2] = api.point(sketch[8], -Top.R1R / 10, 0);
            line[0] = api.line(sketch[8], point[0], point[1]);
            line[1] = api.line(sketch[8], point[0], point[2]);
            sketch[8].DimensionConstraints.AddTwoLineAngle(line[0], line[1], api.getTransGeom().CreatePoint2d(1, 1));
            sketch[8].GeometricConstraints.AddVertical((SketchEntity)line[0]);
            circle[0] = api.circle(sketch[8], api.point(sketch[8], -Top.R1R / 10, -1), Top.D1R / 10 / 2);
            oParameters["d27"].Expression = "75 degree";
            sketch[8].GeometricConstraints.AddCoincident((SketchEntity)circle[0].CenterSketchPoint, (SketchEntity)point[2]);
            profile[8] = api.profile(sketch[8]);
            extrude[8] = api.extrude(profile[8], Top.hR / 10, 2, 0);
            sketch[9] = api.sketch(extrude[8].Faces[1]);
            circle[0] = api.circle(sketch[9], api.point(sketch[9], 0, 0), Top.d1R / 10 / 2);
            profile[9] = api.profile(sketch[9]);
            extrude[9] = api.extrude(profile[9], Top.HR / 10 - Top.H / 10, 0, 0);
            sketch[10] = api.sketch(extrude[8].Faces[1]);
            circle[0] = api.circle(sketch[10], api.point(sketch[10], 0, 0), Top.T1R / 10 / 2);
            profile[10] = api.profile(sketch[10]);
            extrude[10] = api.extrude(profile[10], 10000, 2, 1);
            sketch[11] = api.sketch(extrude[8].Faces[1]);
            circle[0] = api.circle(sketch[11], api.point(sketch[11], 0, Top.O1R / 10), Top.o1R / 10 / 2);
            profile[11] = api.profile(sketch[11]);
            extrude[11] = api.extrude(profile[11], Top.hR / 10, 1, 1);
            WorkAxis Axis11 = api.getCompDef().WorkAxes.AddByRevolvedFace(extrude[10].Faces[1]);
            Axis11.Visible = false;
            ObjectCollection objCollection11 = api.objectCollection();
            objCollection11.Add(extrude[11]);
            CircularPatternFeature CircularPatternFeature11 = api.getCompDef().Features.CircularPatternFeatures.Add(objCollection11, Axis11, true, 12, "360 degree", true, PatternComputeTypeEnum.kIdenticalCompute);
            System.Windows.Forms.MessageBox.Show(formName + " завершено.", formName);
        }
    }
}