using Inventor;

namespace AugerMixer.Model
{
    class Assembly
    {
        public static void Build(Application app, System.Windows.Forms.OpenFileDialog openFileDialog, string formName)
        {
            var fileName = new System.Collections.Generic.Dictionary<string, string>();
            var partDocument = new System.Collections.Generic.Dictionary<string, PartDocument>();
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
            var assemblyDocument = (AssemblyDocument)app.Documents.Add(DocumentTypeEnum.kAssemblyDocumentObject, app.FileManager.GetTemplateFile(DocumentTypeEnum.kAssemblyDocumentObject));
            var assemblyComponentDefinition = assemblyDocument.ComponentDefinition;
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
            api.PlaneAngle(1, 1, 3, 1, System.Convert.ToString(Parts.MainBody.Degree));
            // Присоединение корпуса для выгрузки материалов
            api.Axis(1, 2, 4, 2);
            api.Surface(1, 9, 4, 28, -Parts.MainBody.H / 10, false);
            api.Plane(1, 3, 4, 3);
            // Присоединение боковых опор
            api.Plane(1, 3, 5, 3);
            api.Plane(1, 2, 5, 2, System.Convert.ToString(Parts.MainBody.H - Parts.MainBody.H / 4));
            api.Plane(1, 1, 5, 1, System.Convert.ToString((Parts.MainBody.H - (Parts.MainBody.H - Parts.MainBody.H / 4)) * 1.32));
            var oB = app.TransientObjects.CreateObjectCollection();
            oB.Add(assemblyComponentDefinition.Occurrences[5]);
            assemblyComponentDefinition.OccurrencePatterns.AddCircularPattern(oB, assemblyComponentDefinition.WorkAxes[2], true, "90 degree", 4);
            System.Windows.Forms.MessageBox.Show("Сборка планетарно-шнекового смесителя завершена.", formName);
        }
    }
}
