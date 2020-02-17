using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Slb.Ocean.Petrel.Commands;
using Slb.Ocean.Petrel;

namespace OceanCoursePlugin._15_UITrees
{
    class NewXYZObjectCommandHandler : SimpleCommandHandler
    {
        public static string ID = "OceanCoursePlugin._15_UITrees.NewXYZObjectCommandHandler";

        #region SimpleCommandHandler Members

        public override bool CanExecute(Slb.Ocean.Petrel.Contexts.Context context)
        { 
            return true;
        }

        public override void Execute(Slb.Ocean.Petrel.Contexts.Context context)
        {          
            //TODO: Add command execution logic here
            PetrelLogger.InfoOutputWindow(string.Format("{0} clicked", @"New XYZ Object" ));
        }
    
        #endregion
    }
}
