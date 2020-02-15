using System.Collections.Generic;
using Slb.Ocean.Petrel.Commands;
using Slb.Ocean.Petrel.Contexts;

namespace OceanCoursePlugin._13_UIAdvancedCustomizations
{
    class HelloWorldGroupCommandHandler : GroupCommandHandler
    {
        public static string ID = "OceanCoursePlugin._13_UIAdvancedCustomizations.HelloWorldGroupCommandHandler";

        #region Methods

        public override bool CanExecute(Context context)
        {
            return true;
        }

        public override void Execute(Context context)
        {
        }

        public override IEnumerable<CommandItem> GetCommands(Context context)
        {
            yield return new CommandItem(HelloWorldCommandHandler.ID, 1);
            yield return new CommandItem(HelloWorldCommandHandler.ID, 2);
            yield return new CommandItem(HelloWorldCommandHandler.ID, 3);
            yield return new CommandItem(HelloWorldCommandHandler.ID, 4);
            yield return new CommandItem(HelloWorldCommandHandler.ID, 5);
            yield return new CommandItem(HelloWorldCommandHandler.ID, 6);
        }

        #endregion
    }
}