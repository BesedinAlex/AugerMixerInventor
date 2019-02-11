using System.Collections.Generic;
using Inventor;

namespace BesedinCoursework
{
    /// <summary>
    /// Helps to work with Inventor API.
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
        /// To build assemblies.
        /// </summary>
        /// <param name="app">
        /// Links the app.
        /// </param>
        public InventorAPI(Application app, AssemblyComponentDefinition assemblyComponentDefinition)
        {

        }
        public string getShortName() =>
            shortName;
        public string getLongName() =>
            partDocument[shortName].DisplayName;
        // Accessing dictionaries
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
        /// 0: Positive;
        /// 1: Negative;
        /// 2: Symmetric;
        /// </param>
        /// <param name="operation">
        /// 0: Join;
        /// 1: Cut;
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
        /// 0: Join;
        /// 1: Cut;
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
        // Additional features
        public ObjectCollection objectCollection() =>
            app.TransientObjects.CreateObjectCollection();
        public EdgeCollection edgeCollection() =>
            app.TransientObjects.CreateEdgeCollection();
        public ThreadFeatures threadFeatures() =>
            getCompDef().Features.ThreadFeatures;
    }
}