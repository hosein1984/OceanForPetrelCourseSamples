using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.Commands;
using Slb.Ocean.Petrel.Contexts;

namespace OceanCoursePlugin._12_UICustomizations
{
    public class HelloOceanWithoutWizardCommandHandler : SimpleCommandHandler
    {
        public static readonly string ID =
            "OceanCoursePlugin._12_UICustomizations.HelloOceanWithoutWizardCommandHandler";

        public override bool CanExecute(Context context)
        {
            return true;
        }

        public override void Execute(Context context)
        {
            PetrelLogger.InfoBox($"Hello From {typeof(HelloOceanWithoutWizardCommandHandler).Name}");
        }
    }
}
