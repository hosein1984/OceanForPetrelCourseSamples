using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Media;
using OceanCoursePlugin._15_UITrees;
using Slb.Ocean.Geometry;
using Slb.Ocean.Petrel.UI;
using Color = System.Drawing.Color;

namespace OceanCoursePlugin._17_UIVisualization
{
    public class XYZObjectMapRenderer : IMapRenderer, IMapPicking
    {
        public bool CanDraw(object domainObj, MapRendererContext context)
        {
            return domainObj is XYZObject;
        }

        public void Initialize(object domainObj, MapRendererContext context)
        {
            // configure rendering layer
            context.RenderingLayers = RenderingLayers.Solid;
            //
            var xyzObject = domainObj as XYZObject;
            if (xyzObject != null)
            {
                // create the property changed handler
                PropertyChangedEventHandler xyzObjectChangedHandler = (sender, args) => { context.Window.Invalidate(xyzObject); };
                //
                // register the change handler
                xyzObject.PropertyChanged += xyzObjectChangedHandler;
                //
                // set the change handler as the context
                context.UserContext = xyzObjectChangedHandler;
            }
        }

        public void Draw(object domainObj, MapRendererContext context)
        {
            var xyzObject = domainObj as XYZObject;
            //
            if (xyzObject == null) return;
            //
            using (var brush = new SolidBrush(Color.Aqua))
            {
                var x = xyzObject.X - xyzObject.Radius;
                var y = xyzObject.Y - xyzObject.Radius;
                var width = 2 * xyzObject.Radius;
                var height = 2 * xyzObject.Radius;
                //
                // draw the circle
                context.World.FillEllipse(brush, x, y, width, height);
                //
                // anonotate the drawn circle with the name of the object. 
                // place the annotation at the end of the segment
                var bottomRightCorner = new Point2(
                    xyzObject.X + xyzObject.Radius,
                    xyzObject.Y - xyzObject.Radius);
                Point2 annotationPoint = bottomRightCorner;
                // draw the annotation using the paper graphics
                context.Paper.DrawString(
                    xyzObject.Name, 
                    SystemFonts.DefaultFont, 
                    brush,
                    context.WorldToPaper(annotationPoint));
            }
        }

        public Box2 GetBounds(object domainObj, MapRendererContext context)
        {
            var xyzObject = domainObj as XYZObject;
            //
            if (xyzObject == null) return Box2.Null;
            //
            var topLeftCorner     = new Point2(xyzObject.X - xyzObject.Radius, xyzObject.Y - xyzObject.Radius);
            var bottomRightCorner = new Point2(xyzObject.X + xyzObject.Radius, xyzObject.Y + xyzObject.Radius);
            //
            return new Box2(topLeftCorner, bottomRightCorner);
        }

        public void Dispose(object domainObj, MapRendererContext context)
        {
            var xyzObject = domainObj as XYZObject;
            if (xyzObject != null)
            {
                var propertyChangedEventHandler = context.UserContext as PropertyChangedEventHandler;
                //
                // unsubscribe the change handler
                xyzObject.PropertyChanged -= propertyChangedEventHandler;
            }

        }

        public void GetPickInfo(object domainObj, MapPickedPoint point, MapRendererContext context)
        {
            var xyzObject = domainObj as XYZObject;
            //
            if (xyzObject == null) return;
            //
            var circleCenter = new Point2(xyzObject.X, xyzObject.Y);
            var pickedPoint  = new Point2(point.Ray.Origin.X, point.Ray.Origin.Y);
            //
            Segment2 fromPickedPointToCircleCenter = new Segment2(circleCenter, pickedPoint);
            //
            // if the segment length is smaller than the radius then we assume that the object is selected
            if (fromPickedPointToCircleCenter.Length <= xyzObject.Radius)
            {
                point.PickString = new[] {"Picked XYZ"};
                point.IsHit = true;
            }
        }
    }
}
