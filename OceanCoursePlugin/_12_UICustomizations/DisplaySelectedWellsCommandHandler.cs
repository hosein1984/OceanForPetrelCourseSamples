using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Slb.Ocean.Petrel.Commands;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.Contexts;
using Slb.Ocean.Petrel.DomainObject.Well;

namespace OceanCoursePlugin._12_UICustomizations
{
    class DisplaySelectedWellsCommandHandler : SimpleCommandHandler
    {
        public static string ID = "OceanCoursePlugin._12_UICustomizations.DisplaySelectedWellsCommandHandler";

        public override bool CanExecute(Slb.Ocean.Petrel.Contexts.Context context)
        {
            IEnumerable<object> selectedObjects = context.GetSelectedObjects();
            //
            // return true if there are any boreholes selected
            return selectedObjects.OfType<Borehole>().Any();
        }

        public override void Execute(Slb.Ocean.Petrel.Contexts.Context context)
        {
            PetrelLogger.InfoOutputWindow("Executing " + typeof(DisplaySelectedWellsCommandHandler));
            //
            IEnumerable<object> selectedObjects = context.GetSelectedObjects();
            //
            foreach (Borehole borehole in selectedObjects.OfType<Borehole>())
            {
                PetrelLogger.InfoOutputWindow(borehole.Name);
            }
            //
            PetrelLogger.InfoOutputWindow(Environment.NewLine);
        }
    }
}
