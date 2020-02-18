using OceanCoursePlugin._15_UITrees;
using OIV.Inventor.Nodes;
using Slb.Ocean.Petrel.DomainObject;
using Slb.Ocean.Petrel.UI;

namespace OceanCoursePlugin._17_UIVisualization
{
    public class XYZObjectWindow3DRenderer : IWindow3DRenderer
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
    }
}
