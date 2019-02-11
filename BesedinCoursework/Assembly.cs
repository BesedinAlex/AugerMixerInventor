using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Inventor;

namespace BesedinCoursework
{
    public partial class Assembly : Form
    {
        private Inventor.Application ThisApplication = null; // Проверка активности Inventor
        private Dictionary<string, string> oFileName = new Dictionary<string, string>(); // Имена документов деталей
        private Dictionary<string, PartDocument> oPartDoc = new Dictionary<string, PartDocument>(); // Ссылки на документы деталей
        private AssemblyComponentDefinition oAssCompDef;
        public Assembly()
        {
            InitializeComponent();
        }
        private void Plane(int OccurrenceOne, int PartPlaneOne, int OccurrenceTwo, int PartPlaneTwo, string Offset = "0", bool MateOrFlush = false) // Совмещение базовых плоскостей
        {
            ComponentOccurrence oOcc1 = oAssCompDef.Occurrences[OccurrenceOne];
            ComponentOccurrence oOcc2 = oAssCompDef.Occurrences[OccurrenceTwo];
            PartComponentDefinition oPartComDef = (PartComponentDefinition)oOcc1.Definition;
            WorkPlane oPartPlane1 = oPartComDef.WorkPlanes[PartPlaneOne];
            oPartComDef = (PartComponentDefinition)oOcc2.Definition;
            WorkPlane oPartPlane2 = oPartComDef.WorkPlanes[PartPlaneTwo];
            oOcc1.CreateGeometryProxy(oPartPlane1, out object oAsmPlane1Obj);
            WorkPlaneProxy oAsmPlane1 = (WorkPlaneProxy)oAsmPlane1Obj;
            oOcc2.CreateGeometryProxy(oPartPlane2, out object oAsmPlane2Obj);
            WorkPlaneProxy oAsmPlane2 = (WorkPlaneProxy)oAsmPlane2Obj;
            if (MateOrFlush)
                oAssCompDef.Constraints.AddMateConstraint(oAsmPlane1, oAsmPlane2, Offset);
            else
                oAssCompDef.Constraints.AddFlushConstraint(oAsmPlane1, oAsmPlane2, Offset);
        }
        private void Axis(int OccurrenceOne, int PartAxisOne, int OccurrenceTwo, int PartAxisTwo, string Offset = "0") // Совмещение базовых осей
        {
            ComponentOccurrence oOcc1 = oAssCompDef.Occurrences[OccurrenceOne];
            ComponentOccurrence oOcc2 = oAssCompDef.Occurrences[OccurrenceTwo];
            PartComponentDefinition oPartComDef = (PartComponentDefinition)oOcc1.Definition;
            WorkAxis oPartAxis1 = oPartComDef.WorkAxes[PartAxisOne];
            oPartComDef = (PartComponentDefinition)oOcc2.Definition;
            WorkAxis oPartAxis2 = oPartComDef.WorkAxes[PartAxisTwo];
            oOcc1.CreateGeometryProxy(oPartAxis1, out object oAsmAxis1Obj);
            WorkAxisProxy oAsmAxis1 = (WorkAxisProxy)oAsmAxis1Obj;
            oOcc2.CreateGeometryProxy(oPartAxis2, out object oAsmAxis2Obj);
            WorkAxisProxy oAsmAxis2 = (WorkAxisProxy)oAsmAxis2Obj;
            oAssCompDef.Constraints.AddMateConstraint(oAsmAxis1, oAsmAxis2, Offset);
        }
        private void PlaneAngle(int OccurrenceOne, int PartPlaneOne, int OccurrenceTwo, int PartPlaneTwo, string Offset = "0") // Угол между базовыми плоскостями
        {
            ComponentOccurrence oOcc1 = oAssCompDef.Occurrences[OccurrenceOne];
            ComponentOccurrence oOcc2 = oAssCompDef.Occurrences[OccurrenceTwo];
            PartComponentDefinition oPartComDef = (PartComponentDefinition)oOcc1.Definition;
            WorkPlane oPartPlane1 = oPartComDef.WorkPlanes[PartPlaneOne];
            oPartComDef = (PartComponentDefinition)oOcc2.Definition;
            WorkPlane oPartPlane2 = oPartComDef.WorkPlanes[PartPlaneTwo];
            oOcc1.CreateGeometryProxy(oPartPlane1, out object oAsmPlane1Obj);
            WorkPlaneProxy oAsmPlane1 = (WorkPlaneProxy)oAsmPlane1Obj;
            oOcc2.CreateGeometryProxy(oPartPlane2, out object oAsmPlane2Obj);
            WorkPlaneProxy oAsmPlane2 = (WorkPlaneProxy)oAsmPlane2Obj;
            oAssCompDef.Constraints.AddAngleConstraint(oAsmPlane1, oAsmPlane2, Offset);
        }
        private void Surface(int OccurrenceOne, int PartFaceOne, int OccurrenceTwo, int PartFaceTwo, double Offset = 0, bool MateOrFlush = true) // Совмещение поверхностей
        {
            ComponentOccurrence oOcc1 = oAssCompDef.Occurrences[OccurrenceOne];
            ComponentOccurrence oOcc2 = oAssCompDef.Occurrences[OccurrenceTwo];
            Face oFace1 = oOcc1.SurfaceBodies[1].Faces[PartFaceOne];
            Face oFace2 = oOcc2.SurfaceBodies[1].Faces[PartFaceTwo];
            if (MateOrFlush)
                oAssCompDef.Constraints.AddMateConstraint(oFace1, oFace2, Offset);
            else
                oAssCompDef.Constraints.AddFlushConstraint(oFace1, oFace2, Offset);
        }
        private void InventorControl_Click(object sender, EventArgs e)
        {
            InventorControl IC = new InventorControl();
            IC.ShowDialog();
        }
        private void MainBody_Click(object sender, EventArgs e)
        {
            MainBody MB = new MainBody();
            MB.ShowDialog();
        }
        private void Top_Click(object sender, EventArgs e)
        {
            Top T = new Top();
            T.ShowDialog();
        }
        private void Screw_Click(object sender, EventArgs e)
        {
            Screw W = new Screw();
            W.ShowDialog();
        }
        private void Bottom_Click(object sender, EventArgs e)
        {
            Bottom B = new Bottom();
            B.ShowDialog();
        }
        private void Hold_Click(object sender, EventArgs e)
        {
            Hold H = new Hold();
            H.ShowDialog();
        }
        private void Build_Click(object sender, EventArgs e)
        {
            try
            {
                ThisApplication = (Inventor.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Inventor.Application");
            }
            catch
            {
                InventorControl IC = new InventorControl();
                IC.ShowDialog();
                return;
            }
            string[] PartIndex = { "КК", "К", "Шнек", "КВ", "БО" };
            string[] PartName = { "Конический корпус", "Крышка", "Шнек", "Корпус для выгрузки материалов", "Боковые опоры" };
            for (int I = 0; I < PartIndex.Length; I++)
            {
                openFileDialog1.Filter = "Inventor Part Document|*.ipt";
                openFileDialog1.Title = "Открыть файл \"" + PartName[I] + "\"";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (!string.IsNullOrWhiteSpace(openFileDialog1.FileName))
                    {
                        oPartDoc[PartIndex[I]] = (PartDocument)ThisApplication.Documents.Open(openFileDialog1.FileName, true);
                        oPartDoc[PartIndex[I]].DisplayName = "" + PartName[I] + "";
                        oFileName[PartIndex[I]] = openFileDialog1.FileName;
                    }
                }
                else
                {
                    MessageBox.Show("Сборка была прервана по причине того, \nчто не все детали были открыты приложением.", "Планетарно-шнековой смеситель в Inventor");
                    return;
                }
            }
            // Инициализация сборки
            AssemblyDocument oAssDoc = (AssemblyDocument)ThisApplication.Documents.Add(DocumentTypeEnum.kAssemblyDocumentObject, ThisApplication.FileManager.GetTemplateFile(DocumentTypeEnum.kAssemblyDocumentObject));
            oAssCompDef = oAssDoc.ComponentDefinition;
            oAssDoc.DisplayName = "Планетарно-шнековый смеситель";
            for (int I = 0; I < PartIndex.Length; I++)
                oAssDoc.ComponentDefinition.Occurrences.Add(oFileName[PartIndex[I]], ThisApplication.TransientGeometry.CreateMatrix());
            // Присоединение крышки
            Surface(1, 1, 2, 19);
            Surface(1, 2, 2, 20);
            try
            {
                Surface(1, 9, 2, 114);
            }
            catch
            {
                Surface(1, 9, 2, 113);
            }
            // Присоединение шнека
            Axis(1, 3, 3, 3);
            Plane(1, 3, 3, 3);
            PlaneAngle(1, 1, 3, 1, Convert.ToString(BesedinCoursework.MainBody.Degree));
            // Присоединение корпуса для выгрузки материалов
            Axis(1, 2, 4, 2);
            Surface(1, 9, 4, 28, -BesedinCoursework.MainBody.H / 10, false);
            Plane(1, 3, 4, 3);
            // Присоединение боковых опор
            Plane(1, 3, 5, 3);
            Plane(1, 2, 5, 2, Convert.ToString(BesedinCoursework.MainBody.H - BesedinCoursework.MainBody.H / 4));
            Plane(1, 1, 5, 1, Convert.ToString((BesedinCoursework.MainBody.H - (BesedinCoursework.MainBody.H - BesedinCoursework.MainBody.H / 4)) * 1.32));
            ObjectCollection oB = ThisApplication.TransientObjects.CreateObjectCollection();
            oB.Add(oAssCompDef.Occurrences[5]);
            oAssCompDef.OccurrencePatterns.AddCircularPattern(oB, oAssCompDef.WorkAxes[2], true, "90 degree", 4);
            MessageBox.Show("Сборка планетарно-шнекового смесителя завершена.", "Планетарно-шнековый смеситель в Inventor");
        }
    }
}