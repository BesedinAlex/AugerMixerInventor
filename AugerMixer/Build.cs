using Inventor;

namespace AugerMixer
{
    /// <summary>
    /// Contains functions to make parts and assemblies.
    /// Can be extended in future.
    /// </summary>
    public static class Build
    {
        public static void Assembly(Application app, System.Windows.Forms.OpenFileDialog openFileDialog, string formName)
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
            api.PlaneAngle(1, 1, 3, 1, System.Convert.ToString(AugerMixer.MainBody.Degree));
            // Присоединение корпуса для выгрузки материалов
            api.Axis(1, 2, 4, 2);
            api.Surface(1, 9, 4, 28, -AugerMixer.MainBody.H / 10, false);
            api.Plane(1, 3, 4, 3);
            // Присоединение боковых опор
            api.Plane(1, 3, 5, 3);
            api.Plane(1, 2, 5, 2, System.Convert.ToString(AugerMixer.MainBody.H - AugerMixer.MainBody.H / 4));
            api.Plane(1, 1, 5, 1, System.Convert.ToString((AugerMixer.MainBody.H - (AugerMixer.MainBody.H - AugerMixer.MainBody.H / 4)) * 1.32));
            ObjectCollection oB = app.TransientObjects.CreateObjectCollection();
            oB.Add(assemblyComponentDefinition.Occurrences[5]);
            assemblyComponentDefinition.OccurrencePatterns.AddCircularPattern(oB, assemblyComponentDefinition.WorkAxes[2], true, "90 degree", 4);
            System.Windows.Forms.MessageBox.Show("Сборка планетарно-шнекового смесителя завершена.", formName);
        }
        public static void Bottom(InventorAPI api, string formName)
        {
            Parameters oParameters = api.GetCompDef().Parameters;
            PlanarSketch[] sketch = new PlanarSketch[11];
            Profile[] profile = new Profile[11];
            SketchPoint[] point = new SketchPoint[10];
            SketchLine[] line = new SketchLine[10];
            SketchCircle[] circle = new SketchCircle[2];
            ExtrudeFeature[] extrude = new ExtrudeFeature[10];
            // Основание
            sketch[0] = api.Sketch(api.GetCompDef().WorkPlanes[2]);
            circle[0] = api.Circle(sketch[0], api.Point(sketch[0], 0, 0), AugerMixer.Bottom.D1 / 10 / 2);
            profile[0] = api.Profile(sketch[0]);
            extrude[0] = api.Extrude(profile[0], AugerMixer.Bottom.H1 / 10, 0, 0);
            sketch[1] = api.Sketch(extrude[0].Faces[3]);
            circle[0] = api.Circle(sketch[1], api.Point(sketch[1], 0, 0), AugerMixer.Bottom.D2 / 10 / 2);
            profile[1] = api.Profile(sketch[1]);
            extrude[1] = api.Extrude(profile[1], AugerMixer.Bottom.H2 / 10, 0, 0);
            // Кубическая часть корпуса
            sketch[2] = api.Sketch(extrude[1].Faces[2]);
            point[0] = api.Point(sketch[2], AugerMixer.Bottom.L2 / 10 / 2, AugerMixer.Bottom.L1 / 10 / 2);
            point[1] = api.Point(sketch[2], point[0].Geometry.X, -point[0].Geometry.Y);
            point[2] = api.Point(sketch[2], -point[0].Geometry.X, -point[0].Geometry.Y);
            point[3] = api.Point(sketch[2], -point[0].Geometry.X, point[0].Geometry.Y);
            line[0] = api.Line(sketch[2], point[0], point[1]);
            line[1] = api.Line(sketch[2], point[1], point[2]);
            line[2] = api.Line(sketch[2], point[2], point[3]);
            line[3] = api.Line(sketch[2], point[3], point[0]);
            profile[2] = api.Profile(sketch[2]);
            extrude[2] = api.Extrude(profile[2], AugerMixer.Bottom.T / 10, 0, 0);
            sketch[3] = api.Sketch(extrude[2].Faces[6]);
            point[0] = api.Point(sketch[3], AugerMixer.Bottom.L2 / 10 / 2, AugerMixer.Bottom.L1 / 10 / 2);
            point[1] = api.Point(sketch[3], point[0].Geometry.X, -point[0].Geometry.Y);
            point[2] = api.Point(sketch[3], -point[0].Geometry.X, -point[0].Geometry.Y);
            point[3] = api.Point(sketch[3], -point[0].Geometry.X, point[0].Geometry.Y);
            line[0] = api.Line(sketch[3], point[0], point[1]);
            line[1] = api.Line(sketch[3], point[1], point[2]);
            line[2] = api.Line(sketch[3], point[2], point[3]);
            line[3] = api.Line(sketch[3], point[3], point[0]);
            point[4] = api.Point(sketch[3], point[0].Geometry.X - AugerMixer.Bottom.T / 10, point[0].Geometry.Y - AugerMixer.Bottom.T / 10);
            point[5] = api.Point(sketch[3], point[0].Geometry.X - AugerMixer.Bottom.T / 10, -point[0].Geometry.Y + AugerMixer.Bottom.T / 10);
            point[6] = api.Point(sketch[3], -point[0].Geometry.X + AugerMixer.Bottom.T / 10, -point[0].Geometry.Y + AugerMixer.Bottom.T / 10);
            point[7] = api.Point(sketch[3], -point[0].Geometry.X + AugerMixer.Bottom.T / 10, point[0].Geometry.Y - AugerMixer.Bottom.T / 10);
            line[4] = api.Line(sketch[3], point[4], point[5]);
            line[5] = api.Line(sketch[3], point[5], point[6]);
            line[6] = api.Line(sketch[3], point[6], point[7]);
            line[7] = api.Line(sketch[3], point[7], point[4]);
            profile[3] = api.Profile(sketch[3]);
            extrude[3] = api.Extrude(profile[3], (AugerMixer.Bottom.HB - AugerMixer.Bottom.T * 2) / 10, 0, 0);
            WorkPlane oWorkPlane4 = api.GetCompDef().WorkPlanes.AddByPlaneAndOffset(api.GetCompDef().WorkPlanes[2], (AugerMixer.Bottom.H1 + AugerMixer.Bottom.H2 + AugerMixer.Bottom.HB - AugerMixer.Bottom.T) / 10);
            oWorkPlane4.Visible = false;
            sketch[4] = api.Sketch(oWorkPlane4);
            point[0] = api.Point(sketch[4], AugerMixer.Bottom.L2 / 10 / 2, AugerMixer.Bottom.L1 / 10 / 2);
            point[1] = api.Point(sketch[4], point[0].Geometry.X, -point[0].Geometry.Y);
            point[2] = api.Point(sketch[4], -point[0].Geometry.X, -point[0].Geometry.Y);
            point[3] = api.Point(sketch[4], -point[0].Geometry.X, point[0].Geometry.Y);
            line[0] = api.Line(sketch[4], point[0], point[1]);
            line[1] = api.Line(sketch[4], point[1], point[2]);
            line[2] = api.Line(sketch[4], point[2], point[3]);
            line[3] = api.Line(sketch[4], point[3], point[0]);
            circle[0] = api.Circle(sketch[4], api.Point(sketch[4], 0, 0), AugerMixer.Bottom.D3 / 2 / 10);
            profile[4] = api.Profile(sketch[4]);
            extrude[4] = api.Extrude(profile[4], AugerMixer.Bottom.T / 10, 0, 0);
            // Переход к коническому корпусу
            sketch[5] = api.Sketch(extrude[4].Faces[3]);
            circle[0] = api.Circle(sketch[5], api.Point(sketch[5], 0, 0), AugerMixer.Bottom.D3 / 2 / 10);
            circle[1] = api.Circle(sketch[5], api.Point(sketch[5], 0, 0), (AugerMixer.Bottom.D3 / 2 + AugerMixer.Bottom.T) / 10);
            profile[5] = api.Profile(sketch[5]);
            extrude[5] = api.Extrude(profile[5], AugerMixer.Bottom.H3 / 10, 0, 0);
            // Отверстия у основания
            sketch[6] = api.Sketch(api.GetCompDef().WorkPlanes[2]);
            point[0] = api.Point(sketch[6], 0, 0);
            point[1] = api.Point(sketch[6], 0, AugerMixer.Bottom.DR / 2 / 10);
            point[2] = api.Point(sketch[6], AugerMixer.Bottom.DR / 2 / 10, 0);
            line[0] = api.Line(sketch[6], point[0], point[1]);
            line[1] = api.Line(sketch[6], point[0], point[2]);
            sketch[6].DimensionConstraints.AddTwoLineAngle(line[0], line[1], api.GetTransGeom().CreatePoint2d(1, 1));
            sketch[6].GeometricConstraints.AddHorizontal((SketchEntity)line[1]);
            circle[0] = api.Circle(sketch[6], api.Point(sketch[6], AugerMixer.Bottom.DR / 10, -1), AugerMixer.Bottom.oR / 10);
            oParameters["d13"].Expression = "105 degree";
            sketch[6].GeometricConstraints.AddCoincident((SketchEntity)circle[0].CenterSketchPoint, (SketchEntity)point[1]);
            profile[6] = api.Profile(sketch[6]);
            extrude[6] = api.Extrude(profile[6], AugerMixer.Bottom.H1 / 10, 0, 1);
            ObjectCollection objCollection = api.ObjectCollection();
            objCollection.Add(extrude[6]);
            CircularPatternFeature CircularPatternFeature6 = api.GetCompDef().Features.CircularPatternFeatures.Add(objCollection, api.GetCompDef().WorkAxes[2], true, 12, "360 degree", true, PatternComputeTypeEnum.kIdenticalCompute);
            // Трубки
            sketch[7] = api.Sketch(api.GetCompDef().WorkPlanes[1]);
            circle[0] = api.Circle(sketch[7], api.Point(sketch[7], AugerMixer.Bottom.H4 / 10, 0), AugerMixer.Bottom.D4D / 2 / 10);
            profile[7] = api.Profile(sketch[7]);
            extrude[7] = api.Extrude(profile[7], AugerMixer.Bottom.D3 / 10 + AugerMixer.Bottom.T * 2 / 10 + AugerMixer.Bottom.A * 2 / 10, 2, 0);
            sketch[8] = api.Sketch(api.GetCompDef().WorkPlanes[1]);
            circle[0] = api.Circle(sketch[8], api.Point(sketch[8], AugerMixer.Bottom.H4 / 10, 0), AugerMixer.Bottom.D4d / 2 / 10);
            profile[8] = api.Profile(sketch[8]);
            extrude[8] = api.Extrude(profile[8], AugerMixer.Bottom.D3 / 10 + AugerMixer.Bottom.T * 2 / 10 + AugerMixer.Bottom.A * 2 / 10, 2, 1);
            sketch[9] = api.Sketch(extrude[4].Faces[3]);
            circle[0] = api.Circle(sketch[9], api.Point(sketch[9], 0, 0), AugerMixer.Bottom.D3 / 2 / 10);
            profile[9] = api.Profile(sketch[9]);
            extrude[9] = api.Extrude(profile[9], AugerMixer.Bottom.H3, 0, 1);
            // Крышка
            sketch[10] = api.Sketch(api.GetCompDef().WorkPlanes[3]);
            point[0] = api.Point(sketch[10], AugerMixer.Bottom.L2 / 2 / 10, AugerMixer.Bottom.H5 / 10);
            point[1] = api.Point(sketch[10], point[0].Geometry.X, point[0].Geometry.Y + AugerMixer.Bottom.D51 / 2 / 10);
            point[2] = api.Point(sketch[10], point[1].Geometry.X + AugerMixer.Bottom.L51 / 10, point[1].Geometry.Y);
            point[3] = api.Point(sketch[10], point[2].Geometry.X, point[0].Geometry.Y + AugerMixer.Bottom.D52 / 2 / 10);
            point[4] = api.Point(sketch[10], point[3].Geometry.X + AugerMixer.Bottom.L52 / 10, point[3].Geometry.Y);
            point[5] = api.Point(sketch[10], point[4].Geometry.X, point[0].Geometry.Y + AugerMixer.Bottom.D53 / 2 / 10);
            point[6] = api.Point(sketch[10], point[5].Geometry.X + AugerMixer.Bottom.L53 / 10, point[5].Geometry.Y);
            point[7] = api.Point(sketch[10], point[6].Geometry.X, point[0].Geometry.Y + AugerMixer.Bottom.D54 / 2 / 10);
            point[8] = api.Point(sketch[10], point[7].Geometry.X + AugerMixer.Bottom.L54 / 10, point[7].Geometry.Y);
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
            RevolveFeature revolvefeature = api.Revolve(profile[10], line[9], 0);
            System.Windows.Forms.MessageBox.Show(formName + " завершено.", formName);
        }
        public static void Hold(InventorAPI api, string formName)
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
            point[1] = api.Point(sketch[0], AugerMixer.Hold.b / 10, 0);
            point[2] = api.Point(sketch[0], AugerMixer.Hold.b / 10, -AugerMixer.Hold.S1 / 10);
            point[3] = api.Point(sketch[0], 0, -AugerMixer.Hold.S1 / 10);
            line[0] = api.Line(sketch[0], point[0], point[1]);
            line[1] = api.Line(sketch[0], point[1], point[2]);
            line[2] = api.Line(sketch[0], point[2], point[3]);
            line[3] = api.Line(sketch[0], point[3], point[0]);
            profile[0] = api.Profile(sketch[0]);
            extrude[0] = api.Extrude(profile[0], AugerMixer.Hold.a1 / 10, 2, 0);
            // Боковые крепления
            WorkPlane oWorkPlane1 = api.GetCompDef().WorkPlanes.AddByPlaneAndOffset(api.GetCompDef().WorkPlanes[3], AugerMixer.Hold.a / 2 / 10);
            oWorkPlane1.Visible = false;
            sketch[1] = api.Sketch(oWorkPlane1);
            point[0] = api.Point(sketch[1], 0, 0);
            point[1] = api.Point(sketch[1], (AugerMixer.Hold.b * System.Math.Sin(AugerMixer.MainBody.Degree / 180 * System.Math.PI)) / 10, AugerMixer.Hold.h * System.Math.Cos(AugerMixer.MainBody.Degree / 180 * System.Math.PI) / 10 - AugerMixer.Hold.S1 / 10);
            point[2] = api.Point(sketch[1], point[1].Geometry.X + AugerMixer.Hold.K / 10, point[1].Geometry.Y);
            point[3] = api.Point(sketch[1], AugerMixer.Hold.b / 10, AugerMixer.Hold.K1 / 10);
            point[4] = api.Point(sketch[1], AugerMixer.Hold.b / 10, 0);
            line[0] = api.Line(sketch[1], point[0], point[1]);
            line[1] = api.Line(sketch[1], point[1], point[2]);
            line[2] = api.Line(sketch[1], point[2], point[3]);
            line[3] = api.Line(sketch[1], point[3], point[4]);
            line[4] = api.Line(sketch[1], point[4], point[0]);
            profile[1] = api.Profile(sketch[1]);
            extrude[1] = api.Extrude(profile[1], AugerMixer.Hold.S1 / 10, 0, 0);
            ObjectCollection objCollection1 = api.ObjectCollection();
            objCollection1.Add(extrude[1]);
            api.GetCompDef().Features.MirrorFeatures.AddByDefinition(api.GetCompDef().Features.MirrorFeatures.CreateDefinition(objCollection1, api.GetCompDef().WorkPlanes[3], PatternComputeTypeEnum.kIdenticalCompute));
            // Болтовое отверстие
            sketch[2] = api.Sketch(api.GetCompDef().WorkPlanes[3]);
            point[0] = api.Point(sketch[2], AugerMixer.Hold.b / 10 - AugerMixer.Hold.b1 / 10, -AugerMixer.Hold.S1 / 10);
            point[1] = api.Point(sketch[2], point[0].Geometry.X + AugerMixer.Hold.b2 / 10, point[0].Geometry.Y);
            point[2] = api.Point(sketch[2], point[1].Geometry.X, point[0].Geometry.Y - AugerMixer.Hold.h1 / 10);
            point[3] = api.Point(sketch[2], point[0].Geometry.X, point[2].Geometry.Y);
            line[0] = api.Line(sketch[2], point[0], point[1]);
            line[1] = api.Line(sketch[2], point[1], point[2]);
            line[2] = api.Line(sketch[2], point[2], point[3]);
            line[3] = api.Line(sketch[2], point[3], point[0]);
            profile[2] = api.Profile(sketch[2]);
            extrude[2] = api.Extrude(profile[2], AugerMixer.Hold.a2 / 10, 2, 0);
            sketch[3] = api.Sketch(api.GetCompDef().WorkPlanes[2]);
            circle[0] = api.Circle(sketch[3], api.Point(sketch[3], -(AugerMixer.Hold.b / 10 - AugerMixer.Hold.c / 10), 0), AugerMixer.Hold.d6 / 2 / 10);
            profile[3] = api.Profile(sketch[3]);
            extrude[3] = api.Extrude(profile[3], AugerMixer.Hold.h, 2, 1);
            // Резьба
            EdgeCollection EdgeCollection1 = api.EdgeCollection();
            EdgeCollection1.Add(extrude[3].SideFaces[1].Edges[1]);
            ThreadFeatures ThreadFeatures1 = api.ThreadFeatures();
            StandardThreadInfo stInfo1 = ThreadFeatures1.CreateStandardThreadInfo(false, true, "ISO Metric profile", "M" + AugerMixer.Hold.d6 + "x1.5", "6g");
            ThreadFeatures1.Add(extrude[3].SideFaces[1], extrude[3].SideFaces[1].Edges[2], (ThreadInfo)stInfo1, false, true, 0);
            System.Windows.Forms.MessageBox.Show(formName + " завершено.", formName);
        }
        public static void MainBody(InventorAPI api, string formName)
        {
            Parameters oParameters = api.GetCompDef().Parameters;
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
            point[2] = api.Point(sketch[0], AugerMixer.MainBody.D / 2 - AugerMixer.MainBody.B, AugerMixer.MainBody.H);
            point[3] = api.Point(sketch[0], AugerMixer.MainBody.D / 2, AugerMixer.MainBody.H);
            point[4] = api.Point(sketch[0], AugerMixer.MainBody.D / 2, AugerMixer.MainBody.H - AugerMixer.MainBody.T);
            point[5] = api.Point(sketch[0], AugerMixer.MainBody.D / 2 - AugerMixer.MainBody.B + 1, AugerMixer.MainBody.H - AugerMixer.MainBody.T);
            point[6] = api.Point(sketch[0], AugerMixer.MainBody.Ds + 1, 0);
            point[7] = api.Point(sketch[0], AugerMixer.MainBody.Ds, 0);
            line[1] = api.Line(sketch[0], point[2], point[3]);
            line[2] = api.Line(sketch[0], point[3], point[4]);
            line[3] = api.Line(sketch[0], point[4], point[5]);
            line[4] = api.Line(sketch[0], point[5], point[6]);
            line[5] = api.Line(sketch[0], point[6], point[7]);
            line[6] = api.Line(sketch[0], point[7], point[2]);
            Point2d SketchSize = api.GetTransGeom().CreatePoint2d(-1, -1); // Место для выноса размеров
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
            oParameters["d0"].Expression = AugerMixer.MainBody.Ds / 2 + " mm";
            oParameters["d1"].Expression = AugerMixer.MainBody.H + " mm";
            oParameters["d2"].Expression = AugerMixer.MainBody.D / 2 + " mm";
            oParameters["d3"].Expression = AugerMixer.MainBody.B + " mm";
            oParameters["d4"].Expression = AugerMixer.MainBody.A + " mm";
            oParameters["d5"].Expression = AugerMixer.MainBody.T + " mm";
            point[0].MoveTo(api.GetTransGeom().CreatePoint2d(0, 0)); // Выравнивание осевой линии центра
            sketch[0].DimensionConstraints.AddTwoLineAngle(line[6], line[0], api.GetTransGeom().CreatePoint2d(10, 40), true);
            AugerMixer.MainBody.Degree = oParameters["d6"]._Value * (180 / System.Math.PI);
            profile[0] = api.Profile(sketch[0]);
            RevolveFeature revolve = api.Revolve(profile[0], line[0], 0);
            // Отверстия
            sketch[1] = api.Sketch(api.GetCompDef().WorkPlanes[2]);
            point[0] = api.Point(sketch[1], 0, AugerMixer.MainBody.Rb / 10);
            circle[0] = api.Circle(sketch[1], point[0], AugerMixer.MainBody.Rm / 10 / 2);
            profile[1] = api.Profile(sketch[1]);
            ExtrudeFeature extrude = api.Extrude(profile[1], AugerMixer.MainBody.H / 10, 0, 1);
            ObjectCollection objCollection2 = api.ObjectCollection();
            objCollection2.Add(extrude);
            CircularPatternFeature CircularPatternFeature2 = api.GetCompDef().Features.CircularPatternFeatures.Add(objCollection2, api.GetCompDef().WorkAxes[2], true, 3, "360 degree", true, PatternComputeTypeEnum.kIdenticalCompute);
            System.Windows.Forms.MessageBox.Show(formName + " завершено.", formName);
        }
        public static void Screw(InventorAPI api, string formName)
        {
            PlanarSketch[] sketch = new PlanarSketch[4];
            Profile[] profile = new Profile[4];
            SketchPoint[] point = new SketchPoint[4];
            SketchLine[] line = new SketchLine[4];
            RevolveFeature[] revolve = new RevolveFeature[4];
            // Создание цилиндра
            sketch[0] = api.Sketch(api.GetCompDef().WorkPlanes[3]);
            point[0] = api.Point(sketch[0], 0, 0);
            point[1] = api.Point(sketch[0], 0, Auger.H / 10);
            point[2] = api.Point(sketch[0], Auger.D1 / 10 / 2, Auger.H / 10);
            point[3] = api.Point(sketch[0], Auger.D1 / 10 / 2, 0);
            line[0] = api.Line(sketch[0], point[0], point[1]);
            line[1] = api.Line(sketch[0], point[1], point[2]);
            line[2] = api.Line(sketch[0], point[2], point[3]);
            line[3] = api.Line(sketch[0], point[3], point[0]);
            profile[0] = api.Profile(sketch[0]);
            revolve[0] = api.Revolve(profile[0], line[0], 0);
            // Создание пружины
            sketch[1] = api.Sketch(api.GetCompDef().WorkPlanes[3]);
            point[0] = api.Point(sketch[1], Auger.D1 / 10 / 2, 0);
            point[1] = api.Point(sketch[1], Auger.D / 10 / 2, 0);
            point[3] = api.Point(sketch[1], Auger.D1 / 10 / 2, Auger.T / 10);
            line[0] = api.Line(sketch[1], point[0], point[1]);
            line[1] = api.Line(sketch[1], point[1], point[3]);
            line[2] = api.Line(sketch[1], point[3], point[0]);
            profile[1] = api.Profile(sketch[1]);
            CoilFeature coil = api.GetCompDef().Features.CoilFeatures.AddByPitchAndHeight(profile[1], api.GetCompDef().WorkAxes[2], Auger.H1 / 10, Auger.H2 / 10, PartFeatureOperationEnum.kJoinOperation, false, false, 0, false, 0, 0, true);
            // Верхняя граница пружины
            sketch[2] = api.Sketch(api.GetCompDef().WorkPlanes[3]);
            point[0] = api.Point(sketch[2], 0, Auger.H2 / 10);
            point[1] = api.Point(sketch[2], 0, Auger.H2 / 10 + Auger.T / 10);
            point[2] = api.Point(sketch[2], Auger.D / 10 / 2, Auger.H2 / 10 + Auger.T / 10);
            point[3] = api.Point(sketch[2], Auger.D / 10 / 2, Auger.H2 / 10);
            line[0] = api.Line(sketch[2], point[0], point[1]);
            line[1] = api.Line(sketch[2], point[1], point[2]);
            line[2] = api.Line(sketch[2], point[2], point[3]);
            line[3] = api.Line(sketch[2], point[3], point[0]);
            profile[2] = api.Profile(sketch[2]);
            revolve[2] = api.Revolve(profile[2], line[0], 0);
            // Крепление
            sketch[3] = api.Sketch(api.GetCompDef().WorkPlanes[3]);
            point[0] = api.Point(sketch[3], 0, Auger.H3 / 10);
            point[1] = api.Point(sketch[3], 0, Auger.H3 / 10 + Auger.A / 10);
            point[2] = api.Point(sketch[3], Auger.D / 10 / 2, Auger.H3 / 10 + Auger.A / 10);
            point[3] = api.Point(sketch[3], Auger.D / 10 / 2, Auger.H3 / 10);
            line[0] = api.Line(sketch[3], point[0], point[1]);
            line[1] = api.Line(sketch[3], point[1], point[2]);
            line[2] = api.Line(sketch[3], point[2], point[3]);
            line[3] = api.Line(sketch[3], point[3], point[0]);
            profile[3] = api.Profile(sketch[3]);
            revolve[3] = api.Revolve(profile[3], line[0], 0);
            System.Windows.Forms.MessageBox.Show(formName + " завершено.", formName);
        }
        public static void Top(InventorAPI api, string formName)
        {
            Parameters oParameters = api.GetCompDef().Parameters;
            PlanarSketch[] sketch = new PlanarSketch[12];
            Profile[] profile = new Profile[12];
            SketchPoint[] point = new SketchPoint[8];
            SketchLine[] line = new SketchLine[8];
            SketchCircle[] circle = new SketchCircle[1];
            ExtrudeFeature[] extrude = new ExtrudeFeature[12];
            // Основа корпуса
            sketch[0] = api.Sketch(api.GetCompDef().WorkPlanes[3]);
            point[0] = api.Point(sketch[0], 0, AugerMixer.Top.H / 10 - AugerMixer.Top.T / 10);
            point[1] = api.Point(sketch[0], 0, AugerMixer.Top.H / 10);
            point[2] = api.Point(sketch[0], AugerMixer.Top.D1 / 10 / 2, AugerMixer.Top.H / 10);
            point[3] = api.Point(sketch[0], AugerMixer.Top.D1 / 10 / 2, AugerMixer.Top.T / 10);
            point[4] = api.Point(sketch[0], AugerMixer.Top.D / 10 / 2, AugerMixer.Top.T / 10);
            point[5] = api.Point(sketch[0], AugerMixer.Top.D / 10 / 2, 0);
            point[6] = api.Point(sketch[0], AugerMixer.Top.D1 / 10 / 2 - AugerMixer.Top.T / 10, 0);
            point[7] = api.Point(sketch[0], AugerMixer.Top.D1 / 10 / 2 - AugerMixer.Top.T / 10, AugerMixer.Top.H / 10 - AugerMixer.Top.T / 10);
            line[0] = api.Line(sketch[0], point[0], point[1]);
            line[1] = api.Line(sketch[0], point[1], point[2]);
            line[2] = api.Line(sketch[0], point[2], point[3]);
            line[3] = api.Line(sketch[0], point[3], point[4]);
            line[4] = api.Line(sketch[0], point[4], point[5]);
            line[5] = api.Line(sketch[0], point[5], point[6]);
            line[6] = api.Line(sketch[0], point[6], point[7]);
            line[7] = api.Line(sketch[0], point[7], point[0]);
            profile[0] = api.Profile(sketch[0]);
            RevolveFeature revolveFeature = api.Revolve(profile[0], line[0], 0);
            // Ребра жесткости
            sketch[1] = api.Sketch(api.GetCompDef().WorkPlanes[3]);
            point[0] = api.Point(sketch[1], AugerMixer.Top.D1 / 10 / 2 - AugerMixer.Top.T / 10, AugerMixer.Top.H / 10);
            point[1] = api.Point(sketch[1], point[0].Geometry.X + AugerMixer.Top.T / 10 * 2, AugerMixer.Top.H / 10);
            point[2] = api.Point(sketch[1], AugerMixer.Top.D / 10 / 2, AugerMixer.Top.T / 10 * 2);
            point[3] = api.Point(sketch[1], AugerMixer.Top.D / 10 / 2, AugerMixer.Top.T / 10);
            point[4] = api.Point(sketch[1], point[0].Geometry.X, AugerMixer.Top.T / 10);
            line[0] = api.Line(sketch[1], point[0], point[1]);
            line[0] = api.Line(sketch[1], point[1], point[2]);
            line[0] = api.Line(sketch[1], point[2], point[3]);
            line[0] = api.Line(sketch[1], point[3], point[4]);
            line[0] = api.Line(sketch[1], point[4], point[0]);
            profile[1] = api.Profile(sketch[1]);
            extrude[1] = api.Extrude(profile[1], AugerMixer.Top.A / 10, 2, 0);
            ObjectCollection objCollection1 = api.ObjectCollection();
            objCollection1.Add(extrude[1]);
            CircularPatternFeature CircularPatternFeature2 = api.GetCompDef().Features.CircularPatternFeatures.Add(objCollection1, api.GetCompDef().WorkAxes[2], true, AugerMixer.Top.ACount, "360 degree", true, PatternComputeTypeEnum.kIdenticalCompute);
            // Резервные проходы (2 штуки)
            WorkPlane oWorkPlane2 = api.GetCompDef().WorkPlanes.AddByPlaneAndOffset(api.GetCompDef().WorkPlanes[2], AugerMixer.Top.HR / 10);
            oWorkPlane2.Visible = false;
            sketch[2] = api.Sketch(oWorkPlane2);
            circle[0] = api.Circle(sketch[2], api.Point(sketch[2], AugerMixer.Top.RR / 10, 0), AugerMixer.Top.DR / 10 / 2);
            profile[2] = api.Profile(sketch[2]);
            extrude[2] = api.Extrude(profile[2], AugerMixer.Top.hR / 10, 2, 0);
            sketch[3] = api.Sketch(oWorkPlane2);
            circle[0] = api.Circle(sketch[3], api.Point(sketch[3], AugerMixer.Top.RR / 10, 0), AugerMixer.Top.dR / 10 / 2);
            profile[3] = api.Profile(sketch[3]);
            extrude[3] = api.Extrude(profile[3], AugerMixer.Top.HR / 10 - AugerMixer.Top.H / 10, 1, 0);
            sketch[4] = api.Sketch(oWorkPlane2);
            circle[0] = api.Circle(sketch[4], api.Point(sketch[4], AugerMixer.Top.RR / 10, 0), AugerMixer.Top.TR / 10 / 2);
            profile[4] = api.Profile(sketch[4]);
            extrude[4] = api.Extrude(profile[4], 10000, 2, 1);
            PlanarSketch oSketch5 = api.GetCompDef().Sketches.Add(oWorkPlane2);
            sketch[5] = api.Sketch(oWorkPlane2);
            circle[0] = api.Circle(sketch[5], api.Point(sketch[5], AugerMixer.Top.RR / 10, AugerMixer.Top.OR / 10), AugerMixer.Top.oR / 10 / 2);
            profile[5] = api.Profile(sketch[5]);
            extrude[5] = api.Extrude(profile[5], AugerMixer.Top.hR / 10, 2, 1);
            WorkAxis Axis5 = api.GetCompDef().WorkAxes.AddByRevolvedFace(extrude[4].Faces[1]);
            Axis5.Visible = false;
            ObjectCollection objCollection5 = api.ObjectCollection();
            objCollection5.Add(extrude[5]);
            CircularPatternFeature CircularPatternFeature5 = api.GetCompDef().Features.CircularPatternFeatures.Add(objCollection5, Axis5, true, 8, "360 degree", true, PatternComputeTypeEnum.kIdenticalCompute);
            ObjectCollection objCollection6 = api.ObjectCollection();
            for (int i = 2; i < 6; i++)
                objCollection6.Add(extrude[i]);
            objCollection6.Add(CircularPatternFeature5);
            CircularPatternFeature CircularPatternFeature6 = api.GetCompDef().Features.CircularPatternFeatures.Add(objCollection6, api.GetCompDef().WorkAxes[2], true, 2, "150 degree", true, PatternComputeTypeEnum.kIdenticalCompute);
            // Отверстия под болты
            sketch[7] = api.Sketch(api.GetCompDef().WorkPlanes[2]);
            point[0] = api.Point(sketch[7], 0, 0);
            point[1] = api.Point(sketch[7], 0, AugerMixer.Top.MBRb / 10);
            point[2] = api.Point(sketch[7], AugerMixer.Top.MBRb / 10, 0);
            line[0] = api.Line(sketch[7], point[1], point[0]);
            line[1] = api.Line(sketch[7], point[0], point[2]);
            sketch[7].DimensionConstraints.AddTwoLineAngle(line[0], line[1], api.GetTransGeom().CreatePoint2d(1, 1));
            sketch[7].GeometricConstraints.AddHorizontal((SketchEntity)line[1]);
            circle[0] = api.Circle(sketch[7], point[1], AugerMixer.Top.MBRm / 10 / 2);
            oParameters["d21"].Expression = "82.5 degree";
            sketch[7].GeometricConstraints.AddCoincident((SketchEntity)circle[0].CenterSketchPoint, (SketchEntity)point[1]);
            profile[7] = api.Profile(sketch[7]);
            extrude[7] = api.Extrude(profile[7], 10000, 2, 1);
            ObjectCollection objCollection7 = api.ObjectCollection();
            objCollection7.Add(extrude[7]);
            CircularPatternFeature CircularPatternFeature7 = api.GetCompDef().Features.CircularPatternFeatures.Add(objCollection7, api.GetCompDef().WorkAxes[2], true, 3, "360 degree", true, PatternComputeTypeEnum.kIdenticalCompute);
            // Загрузка сыпучих материалов
            sketch[8] = api.Sketch(oWorkPlane2);
            point[0] = api.Point(sketch[8], 0, 0);
            point[1] = api.Point(sketch[8], 0, -AugerMixer.Top.R1R / 10);
            point[2] = api.Point(sketch[8], -AugerMixer.Top.R1R / 10, 0);
            line[0] = api.Line(sketch[8], point[0], point[1]);
            line[1] = api.Line(sketch[8], point[0], point[2]);
            sketch[8].DimensionConstraints.AddTwoLineAngle(line[0], line[1], api.GetTransGeom().CreatePoint2d(1, 1));
            sketch[8].GeometricConstraints.AddVertical((SketchEntity)line[0]);
            circle[0] = api.Circle(sketch[8], api.Point(sketch[8], -AugerMixer.Top.R1R / 10, -1), AugerMixer.Top.D1R / 10 / 2);
            oParameters["d27"].Expression = "75 degree";
            sketch[8].GeometricConstraints.AddCoincident((SketchEntity)circle[0].CenterSketchPoint, (SketchEntity)point[2]);
            profile[8] = api.Profile(sketch[8]);
            extrude[8] = api.Extrude(profile[8], AugerMixer.Top.hR / 10, 2, 0);
            sketch[9] = api.Sketch(extrude[8].Faces[1]);
            circle[0] = api.Circle(sketch[9], api.Point(sketch[9], 0, 0), AugerMixer.Top.d1R / 10 / 2);
            profile[9] = api.Profile(sketch[9]);
            extrude[9] = api.Extrude(profile[9], AugerMixer.Top.HR / 10 - AugerMixer.Top.H / 10, 0, 0);
            sketch[10] = api.Sketch(extrude[8].Faces[1]);
            circle[0] = api.Circle(sketch[10], api.Point(sketch[10], 0, 0), AugerMixer.Top.T1R / 10 / 2);
            profile[10] = api.Profile(sketch[10]);
            extrude[10] = api.Extrude(profile[10], 10000, 2, 1);
            sketch[11] = api.Sketch(extrude[8].Faces[1]);
            circle[0] = api.Circle(sketch[11], api.Point(sketch[11], 0, AugerMixer.Top.O1R / 10), AugerMixer.Top.o1R / 10 / 2);
            profile[11] = api.Profile(sketch[11]);
            extrude[11] = api.Extrude(profile[11], AugerMixer.Top.hR / 10, 1, 1);
            WorkAxis Axis11 = api.GetCompDef().WorkAxes.AddByRevolvedFace(extrude[10].Faces[1]);
            Axis11.Visible = false;
            ObjectCollection objCollection11 = api.ObjectCollection();
            objCollection11.Add(extrude[11]);
            CircularPatternFeature CircularPatternFeature11 = api.GetCompDef().Features.CircularPatternFeatures.Add(objCollection11, Axis11, true, 12, "360 degree", true, PatternComputeTypeEnum.kIdenticalCompute);
            System.Windows.Forms.MessageBox.Show(formName + " завершено.", formName);
        }
    }
}
