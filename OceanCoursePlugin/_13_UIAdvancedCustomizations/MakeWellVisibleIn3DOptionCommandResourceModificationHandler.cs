using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.Commands;
using Slb.Ocean.Petrel.Contexts;
using Slb.Ocean.Petrel.DomainObject.Well;
using Slb.Ocean.Petrel.Resources;
using Slb.Ocean.Petrel.UI;

namespace OceanCoursePlugin._13_UIAdvancedCustomizations
{
    class MakeWellVisibleIn3DOptionCommandResourceModificationHandler : ResourceModificationHandler
    {
        public static readonly string ID =
            "OceanCoursePlugin._13_UIAdvancedCustomizations.MakeWellVisibleIn3DOptionCommandResourceModificationHandler";
        public override void Modify(Resource resource, Context context)
        {
            // find the current item
            Borehole borehole = context.GetParameter<Borehole>(CommandParameterIds.Option);
            //
            if (borehole == Borehole.NullObject) return;
            //
            // set attributes
            PetrelSystem.ResourceManager.SetAttributeValue(resource, WellKnownResourceAttributes.OptionText, borehole.Name);
            //
            var imageInfoFactory = CoreSystem.GetService<IImageInfoFactory>(borehole);
            var boreholeImage = ImageData.FromImage(imageInfoFactory.GetImageInfo(borehole).GetDisplayImage(new ImageInfoContext()));
            PetrelSystem.ResourceManager.SetAttributeValue(resource, WellKnownResourceAttributes.OptionImage16, boreholeImage);
            PetrelSystem.ResourceManager.SetAttributeValue(resource, WellKnownResourceAttributes.OptionImage32, boreholeImage);
        }
    }
}
