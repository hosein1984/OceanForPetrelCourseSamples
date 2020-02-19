using OceanCoursePlugin._15_UITrees;
using OIV.Inventor.Nodes;
using Slb.Ocean.Geometry;
using Slb.Ocean.Petrel.DomainObject;
using Slb.Ocean.Petrel.UI;

namespace OceanCoursePlugin._17_UIVisualization
{
    public class XYZObjectWindow3DRenderer : IWindow3DRenderer, IWindow3DPicking
    {
        public bool CanCreate(object domainObj, Window3DRendererContext context)
        {
            return domainObj is XYZObject && 
                   Equals(context.Window.Domain, Domain.ELEVATION_DEPTH);
        }

        public SoNode Create(object domainObj, Window3DRendererContext context)
        {
            var xyzObject = domainObj as XYZObject;
            var soXyzNode = new SoXYZNode(xyzObject, context.Window);
            return soXyzNode;
        }

        public void Update(SoNode node, object domainObj, Window3DRendererContext context)
        {
        }

        public void Dispose(SoNode node, object domainObj, Window3DRendererContext context)
        {
            if (node.IsDisposable)
                node.Dispose();
        }

        public void GetPickInfo(object domainObject, Window3DPickedPoint point, Window3DRendererContext context)
        {
            var xyzObject = domainObject as XYZObject;
            //
            if (xyzObject == null) return;
            //
            var sphereCenter = new Point3(xyzObject.X, xyzObject.Y, xyzObject.Z);
            var pickedPoint = new Point3(point.Ray.Origin.X, point.Ray.Origin.Y, point.Ray.Origin.Z);
            //
            Segment3 fromPickedPointToCircleCenter = new Segment3(sphereCenter, pickedPoint);
            //
            // if the segment length is smaller than the radius then we assume that the object is selected
            if (fromPickedPointToCircleCenter.Length <= xyzObject.Radius)
            {
                point.PickString = new[] { "Picked XYZ" };
                point.World = point.WorldHit;
            }
        }
    }
}
