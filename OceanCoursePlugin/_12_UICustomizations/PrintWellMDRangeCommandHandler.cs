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
    class PrintWellMDRangeCommandHandler : SimpleCommandHandler
    {
        public static string ID = "OceanCoursePlugin._12_UICustomizations.PrintWellMDRangeCommandHandler";

        #region SimpleCommandHandler Members

        public override bool CanExecute(Slb.Ocean.Petrel.Contexts.Context context)
        { 
            return context.GetSelectedObjects().OfType<Borehole>().Any();
        }

        public override void Execute(Slb.Ocean.Petrel.Contexts.Context context)
        {
            IEnumerable<Borehole> selectedBoreHoles = context.GetSelectedObjects().OfType<Borehole>();
            //
            // 
            foreach (var borehole in selectedBoreHoles)
            {
                PetrelLogger.InfoOutputWindow($"Well: {borehole.Name} has (Min MD = {borehole.MDRange.Min}) and (Max Md = {borehole.MDRange.Max})");
            }
        }
    
        #endregion
    }
}
