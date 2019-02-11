using System.Collections.Generic;
using Inventor;

namespace BesedinCoursework
{
    /// <summary>
    /// Contains frequently used API to work with Inventor faster.
    /// </summary>
    public class InventorAPI
    {
        private string shortName;
        private Application app = null;
        private static Dictionary<string, string> fileName = new Dictionary<string, string>();
        private static Dictionary<string, PartDocument> partDocument = new Dictionary<string, PartDocument>();
        private static Dictionary<string, PartComponentDefinition> partComponentDefinition = new Dictionary<string, PartComponentDefinition>();
        private static Dictionary<string, TransientGeometry> transientGeometry = new Dictionary<string, TransientGeometry>();
        private AssemblyComponentDefinition assemblyComponentDefinition;
        /// <summary>
        /// To build parts.
        /// </summary>
        /// <param name="app">
        /// Links the app.
        /// </param>
        public InventorAPI(Application app, string shortName, string longName)
        {
            this.shortName = shortName;
            this.app = app;
            partDocument[shortName] = (PartDocument)app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, app.FileManager.GetTemplateFile(DocumentTypeEnum.kPartDocumentObject));
            partComponentDefinition[shortName] = partDocument[shortName].ComponentDefinition;
            transientGeometry[shortName] = app.TransientGeometry;
            fileName[shortName] = null;
            partDocument[shortName].DisplayName = longName;
        }
        /// <summary>
        /// To assemble parts.
        /// </summary>
        public InventorAPI(AssemblyComponentDefinition assemblyComponentDefinition) =>
            this.assemblyComponentDefinition = assemblyComponentDefinition;
        // Accessing dictionaries
        public string getShortName() =>
            shortName;
        public string getLongName() =>
            partDocument[shortName].DisplayName;
        public void setFileName(string setName) =>
            fileName[shortName] = setName;
        public string getFileName() =>
            fileName[shortName];
        public PartDocument getPartDoc() =>
            partDocument[shortName];
        public PartComponentDefinition getCompDef() =>
            partComponentDefinition[shortName];
        public TransientGeometry getTransGeom() =>
            transientGeometry[shortName];
        // 2D
        public PlanarSketch sketch(object plane, bool useFaceEdges = false) =>
            getCompDef().Sketches.Add(plane, useFaceEdges);
        public Profile profile(PlanarSketch sketch) =>
            sketch.Profiles.AddForSolid();
        public SketchPoint point(PlanarSketch sketch, double x, double y, bool holeCenter = false) =>
            sketch.SketchPoints.Add(getTransGeom().CreatePoint2d(x, y), holeCenter);
        public SketchLine line(PlanarSketch sketch, SketchPoint point1, SketchPoint point2) =>
            sketch.SketchLines.AddByTwoPoints(point1, point2);
        public SketchCircle circle(PlanarSketch sketch, SketchPoint point, double radius) =>
            sketch.SketchCircles.AddByCenterRadius(point, radius);
        // 3D
        /// <param name="direction">
        /// 0-Positive;
        /// 1-Negative;
        /// 2-Symmetric;
        /// </param>
        /// <param name="operation">
        /// 0-Join;
        /// 1-Cut;
        /// </param>
        public ExtrudeFeature extrude(Profile profile, double distance, int direction, int operation)
        {
            PartFeatureExtentDirectionEnum extentDirection;
            switch (direction)
            {
                case 0:
                    extentDirection = PartFeatureExtentDirectionEnum.kPositiveExtentDirection;
                    break;
                case 1:
                    extentDirection = PartFeatureExtentDirectionEnum.kNegativeExtentDirection;
                    break;
                default:
                    extentDirection = PartFeatureExtentDirectionEnum.kSymmetricExtentDirection;
                    break;
            }
            PartFeatureOperationEnum extentOperation;
            switch (operation)
            {
                case 0:
                    extentOperation = PartFeatureOperationEnum.kJoinOperation;
                    break;
                default:
                    extentOperation = PartFeatureOperationEnum.kCutOperation;
                    break;
            }
            return getCompDef().Features.ExtrudeFeatures.AddByDistanceExtent(profile, distance, extentDirection, extentOperation, profile);
        }
        /// <param name="operation">
        /// 0-Join;
        /// 1-Cut;
        /// </param>
        public RevolveFeature revolve(Profile profile, object axis, int operation)
        {
            PartFeatureOperationEnum extentOperation;
            switch (operation)
            {
                case 0:
                    extentOperation = PartFeatureOperationEnum.kJoinOperation;
                    break;
                default:
                    extentOperation = PartFeatureOperationEnum.kCutOperation;
                    break;
            }
            return getCompDef().Features.RevolveFeatures.AddFull(profile, axis, extentOperation);
        }
        // Assembly
        public void Plane(int OccurrenceOne, int PartPlaneOne, int OccurrenceTwo, int PartPlaneTwo, string Offset = "0", bool MateOrFlush = false)
        {
            ComponentOccurrence oOcc1 = assemblyComponentDefinition.Occurrences[OccurrenceOne];
            ComponentOccurrence oOcc2 = assemblyComponentDefinition.Occurrences[OccurrenceTwo];
            PartComponentDefinition oPartComDef = (PartComponentDefinition)oOcc1.Definition;
            WorkPlane oPartPlane1 = oPartComDef.WorkPlanes[PartPlaneOne];
            oPartComDef = (PartComponentDefinition)oOcc2.Definition;
            WorkPlane oPartPlane2 = oPartComDef.WorkPlanes[PartPlaneTwo];
            oOcc1.CreateGeometryProxy(oPartPlane1, out object oAsmPlane1Obj);
            WorkPlaneProxy oAsmPlane1 = (WorkPlaneProxy)oAsmPlane1Obj;
            oOcc2.CreateGeometryProxy(oPartPlane2, out object oAsmPlane2Obj);
            WorkPlaneProxy oAsmPlane2 = (WorkPlaneProxy)oAsmPlane2Obj;
            if (MateOrFlush)
                assemblyComponentDefinition.Constraints.AddMateConstraint(oAsmPlane1, oAsmPlane2, Offset);
            else
                assemblyComponentDefinition.Constraints.AddFlushConstraint(oAsmPlane1, oAsmPlane2, Offset);
        }
        public void Axis(int OccurrenceOne, int PartAxisOne, int OccurrenceTwo, int PartAxisTwo, string Offset = "0")
        {
            ComponentOccurrence oOcc1 = assemblyComponentDefinition.Occurrences[OccurrenceOne];
            ComponentOccurrence oOcc2 = assemblyComponentDefinition.Occurrences[OccurrenceTwo];
            PartComponentDefinition oPartComDef = (PartComponentDefinition)oOcc1.Definition;
            WorkAxis oPartAxis1 = oPartComDef.WorkAxes[PartAxisOne];
            oPartComDef = (PartComponentDefinition)oOcc2.Definition;
            WorkAxis oPartAxis2 = oPartComDef.WorkAxes[PartAxisTwo];
            oOcc1.CreateGeometryProxy(oPartAxis1, out object oAsmAxis1Obj);
            WorkAxisProxy oAsmAxis1 = (WorkAxisProxy)oAsmAxis1Obj;
            oOcc2.CreateGeometryProxy(oPartAxis2, out object oAsmAxis2Obj);
            WorkAxisProxy oAsmAxis2 = (WorkAxisProxy)oAsmAxis2Obj;
            assemblyComponentDefinition.Constraints.AddMateConstraint(oAsmAxis1, oAsmAxis2, Offset);
        }
        public void PlaneAngle(int OccurrenceOne, int PartPlaneOne, int OccurrenceTwo, int PartPlaneTwo, string Offset = "0")
        {
            ComponentOccurrence oOcc1 = assemblyComponentDefinition.Occurrences[OccurrenceOne];
            ComponentOccurrence oOcc2 = assemblyComponentDefinition.Occurrences[OccurrenceTwo];
            PartComponentDefinition oPartComDef = (PartComponentDefinition)oOcc1.Definition;
            WorkPlane oPartPlane1 = oPartComDef.WorkPlanes[PartPlaneOne];
            oPartComDef = (PartComponentDefinition)oOcc2.Definition;
            WorkPlane oPartPlane2 = oPartComDef.WorkPlanes[PartPlaneTwo];
            oOcc1.CreateGeometryProxy(oPartPlane1, out object oAsmPlane1Obj);
            WorkPlaneProxy oAsmPlane1 = (WorkPlaneProxy)oAsmPlane1Obj;
            oOcc2.CreateGeometryProxy(oPartPlane2, out object oAsmPlane2Obj);
            WorkPlaneProxy oAsmPlane2 = (WorkPlaneProxy)oAsmPlane2Obj;
            assemblyComponentDefinition.Constraints.AddAngleConstraint(oAsmPlane1, oAsmPlane2, Offset);
        }
        public void Surface(int OccurrenceOne, int PartFaceOne, int OccurrenceTwo, int PartFaceTwo, double Offset = 0, bool MateOrFlush = true)
        {
            ComponentOccurrence oOcc1 = assemblyComponentDefinition.Occurrences[OccurrenceOne];
            ComponentOccurrence oOcc2 = assemblyComponentDefinition.Occurrences[OccurrenceTwo];
            Face oFace1 = oOcc1.SurfaceBodies[1].Faces[PartFaceOne];
            Face oFace2 = oOcc2.SurfaceBodies[1].Faces[PartFaceTwo];
            if (MateOrFlush)
                assemblyComponentDefinition.Constraints.AddMateConstraint(oFace1, oFace2, Offset);
            else
                assemblyComponentDefinition.Constraints.AddFlushConstraint(oFace1, oFace2, Offset);
        }
        // Additional features
        public ObjectCollection objectCollection() =>
            app.TransientObjects.CreateObjectCollection();
        public EdgeCollection edgeCollection() =>
            app.TransientObjects.CreateEdgeCollection();
        public ThreadFeatures threadFeatures() =>
            getCompDef().Features.ThreadFeatures;
    }
}