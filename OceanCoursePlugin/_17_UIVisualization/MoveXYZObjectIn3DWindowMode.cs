using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OceanCoursePlugin._15_UITrees;
using Slb.Ocean.Geometry;
using Slb.Ocean.Petrel.UI;

namespace OceanCoursePlugin._17_UIVisualization
{
    public class MoveXYZObjectIn3DWindowMode : WindowMode
    {
        private static readonly string ID = "OceanCoursePlugin._17_UIVisualization.MoveXYZObjectIn3DWindowMode";
        private Point3 _pickedPointFromSphereCenterOffset;

        public MoveXYZObjectIn3DWindowMode() : base(ID)
        {
        }

        protected override string ToolTipCore => "Move XYZObject in 3DWindow";

        protected override Bitmap BitmapCore => PetrelImages.Wand;

        protected override Cursor GetCursorCore(PickedPoint args)
        {
            return args.DomainObject is XYZObject
                ? Cursors.Cross
                : Cursors.Default;
        }

        protected override void OnDragStartCore(PickedPoint args)
        {
            var mapPickedPoint = args as MapPickedPoint;
            //
            if (mapPickedPoint == null || mapPickedPoint.World == null)
            {
                return;
            }
            //
            var xyzObject = mapPickedPoint.DomainObject as XYZObject;
            //
            if (xyzObject == null) return;
            //
            //
            _pickedPointFromSphereCenterOffset =
                new Point3(
                    xyzObject.X - mapPickedPoint.World.X,
                    xyzObject.Y - mapPickedPoint.World.Y,
                    xyzObject.Z - mapPickedPoint.World.Z);
        }

        protected override void OnDragCore(PickedPoint args)
        {
            var mapPickedPoint = args as MapPickedPoint;
            //
            if (mapPickedPoint == null || mapPickedPoint.World == null)
            {
                return;
            }
            //
            var xyzObject = mapPickedPoint.DomainObject as XYZObject;
            //
            if (xyzObject == null) return;
            //
            //
            xyzObject.X = (float)(mapPickedPoint.World.X + _pickedPointFromSphereCenterOffset.X);
            xyzObject.Y = (float)(mapPickedPoint.World.Y + _pickedPointFromSphereCenterOffset.Y);
            xyzObject.Y = (float)(mapPickedPoint.World.Z + _pickedPointFromSphereCenterOffset.Z);
        }

        protected override bool CanActivateCore(WindowModeContext context)
        {
            return context.Window is Window3D;
        }
    }
}
