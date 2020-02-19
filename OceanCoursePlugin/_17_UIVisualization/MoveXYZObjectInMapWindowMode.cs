using System.Drawing;
using System.Windows.Forms;
using OceanCoursePlugin._15_UITrees;
using Slb.Ocean.Geometry;
using Slb.Ocean.Petrel.UI;

namespace OceanCoursePlugin._17_UIVisualization
{
    public class MoveXYZObjectInMapWindowMode : WindowMode
    {
        private static readonly string ID = "OceanCoursePlugin._17_UIVisualization.MoveXYZObjectInMapWindowMode";
        private Point2 _pickedPointFromCircleCenterOffset;

        public MoveXYZObjectInMapWindowMode() : base(ID)
        {
        }

        protected override string ToolTipCore => "Move XYZObject in MapWindow";

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
            _pickedPointFromCircleCenterOffset =
                new Point2(
                    xyzObject.X - mapPickedPoint.World.X,
                    xyzObject.Y - mapPickedPoint.World.Y);
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
            xyzObject.X = (float) (mapPickedPoint.World.X + _pickedPointFromCircleCenterOffset.X);
            xyzObject.Y = (float) (mapPickedPoint.World.Y + _pickedPointFromCircleCenterOffset.Y);
        }

        protected override bool CanActivateCore(WindowModeContext context)
        {
            return context.Window is MapWindow;
        }
    }
}
