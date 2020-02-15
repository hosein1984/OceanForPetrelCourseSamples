using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Slb.Ocean.Petrel.Commands;
using Slb.Ocean.Petrel;

namespace OceanCoursePlugin._13_UIAdvancedCustomizations
{
    class HelloWorldCommandHandler : SimpleCommandHandler
    {
        public static string ID = "OceanCoursePlugin._13_UIAdvancedCustomizations.HelloWorldCommandHandler";

        #region SimpleCommandHandler Members

        public override bool CanExecute(Slb.Ocean.Petrel.Contexts.Context context)
        { 
            return true;
        }

        public override void Execute(Slb.Ocean.Petrel.Contexts.Context context)
        {
            var helloWorldVersion = context.GetDefaultParameter<int>();
            PetrelLogger.InfoOutputWindow($"Hello World with Argument {helloWorldVersion}");
        }
    
        #endregion
    }
}
