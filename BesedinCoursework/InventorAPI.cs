using System.Collections.Generic;
using Inventor;

namespace BesedinCoursework
{
    public class InventorAPI
    {
        private string shortName;
        private Application app = null;
        private static Dictionary<string, PartDocument> partDocument = new Dictionary<string, PartDocument>();
        private static Dictionary<string, string> fileName = new Dictionary<string, string>();
        private static Dictionary<string, PartComponentDefinition> partComponentDefinition = new Dictionary<string, PartComponentDefinition>();
        private static Dictionary<string, TransientGeometry> transientGeometry = new Dictionary<string, TransientGeometry>();
        // Extends dictionaries
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
        // Accessing dictionaries
        public PartDocument getPartDoc(string name)
        {
            return partDocument[name];
        }
        public string getFileName(string name)
        {
            return fileName[name];
        }
        public void setFileName(string name, string setName)
        {
            fileName[name] = setName;
        }
        public PartComponentDefinition getCompDef(string name)
        {
            return partComponentDefinition[name];
        }
        public TransientGeometry getTransGeom(string name)
        {
            return transientGeometry[name];
        }
        // Sketch
        public PlanarSketch sketch(object plane, bool useFaceEdges = false)
        {
            return getCompDef(shortName).Sketches.Add(plane, useFaceEdges);
        }
        public Profile profile(PlanarSketch sketch)
        {
            return sketch.Profiles.AddForSolid();
        }
        public SketchPoint point(PlanarSketch sketch, double x, double y)
        {
            return sketch.SketchPoints.Add(getTransGeom(shortName).CreatePoint2d(x, y), false);
        }
        public SketchLine line(PlanarSketch sketch, SketchPoint point1, SketchPoint point2)
        {
            return sketch.SketchLines.AddByTwoPoints(point1, point2);
        }
        public SketchCircle circle(PlanarSketch sketch, SketchPoint point, double radius)
        {
            return sketch.SketchCircles.AddByCenterRadius(point, radius);
        }
    }
}