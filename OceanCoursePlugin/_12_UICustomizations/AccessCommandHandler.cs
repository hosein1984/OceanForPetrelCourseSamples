using System.Collections.Generic;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.Commands;
using Slb.Ocean.Petrel.Contexts;

namespace OceanCoursePlugin._12_UICustomizations
{
    class AccessCommandHandler : GroupCommandHandler
    {
        public static string ID = "OceanCoursePlugin._12_UICustomizations.AccessCommandHandler";

        public override bool CanExecute(Context context)
        {
            return true;
        }

        public override void Execute(Context context)
        {
            PetrelLogger.InfoOutputWindow("Executing " + typeof(AccessCommandHandler).Name);
        }

        public override IEnumerable<CommandItem> GetCommands(Context context)
        {
            yield return new CommandItem(HelloOceanWithoutWizardCommandHandler.ID);
            yield return new CommandItem(HelloOceanWithWizardCommandHandler.ID);
            yield return new CommandItem(DisplaySelectedWellsCommandHandler.ID);
        }

    }
}