using System.ComponentModel;
using OceanCoursePlugin._15_UITrees;
using OIV.Inventor.Nodes;
using Slb.Ocean.Geometry;
using Slb.Ocean.Petrel.UI;

namespace OceanCoursePlugin._17_UIVisualization
{
    public sealed class SoXYZNode : SoSeparator
    {
        private readonly XYZObject _xyzObject;
        private readonly Window3D _window;
        private readonly SoTranslation _translation;
        private readonly SoSphere _sphere;

        internal SoXYZNode(XYZObject xyzObject, Window3D window)
        {
            _xyzObject = xyzObject;
            _window = window;
            //
            // subscribe to property changed o fthe object
            _xyzObject.PropertyChanged += XyzObjectOnPropertyChanged;
            //
            // create a translation
            _translation = new SoTranslation();
            UpdateSoTranslation();
            this.AddChild(_translation);
            //
            // create a sphere
            _sphere = new SoSphere();
            UpdateSoSphere();
            this.AddChild(_sphere);
        }

        private void UpdateSoTranslation()
        {
            var sphereCenter = new Point3(_xyzObject.X, _xyzObject.Y, _xyzObject.Z);
            //
            var originToSphereCenterVector = _window.WorldToWindow(sphereCenter);
            //
            _translation.translation.Value = originToSphereCenterVector;
        }

        private void UpdateSoSphere()
        {
            _sphere.radius.Value = _xyzObject.Radius;
        }

        private void XyzObjectOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            UpdateSoTranslation();
            UpdateSoSphere();
        }

        protected override void Dispose(bool a0)
        {
            _xyzObject.PropertyChanged -= XyzObjectOnPropertyChanged;
            //
            base.Dispose(a0);
        }
    }
}
