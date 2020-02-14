using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Slb.Ocean.Petrel.Commands;
using Slb.Ocean.Petrel;

namespace OceanCoursePlugin._12_UICustomizations
{
    class HelloOceanWithWizardCommandHandler : SimpleCommandHandler
    {
        public static string ID = "OceanCoursePlugin._12_UICustomizations.HelloOceanWithWizardCommandHandler";


        public override bool CanExecute(Slb.Ocean.Petrel.Contexts.Context context)
        { 
            return true;
        }

        public override void Execute(Slb.Ocean.Petrel.Contexts.Context context)
        {
            PetrelLogger.InfoBox($"Hello From {typeof(HelloOceanWithoutWizardCommandHandler).Name}");
        }
    }
}
