using System.Linq;
using OceanCoursePlugin.Extensions;
using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.Commands;
using Slb.Ocean.Petrel.Contexts;
using Slb.Ocean.Petrel.DomainObject.Well;

namespace OceanCoursePlugin._13_UIAdvancedCustomizations
{
    class UpdateWellNameTextCommandHandler : TextCommandHandler
    {
        public static string ID = "OceanCoursePlugin._13_UIAdvancedCustomizations.UpdateWellNameTextCommandHandler";

        private string _text = "Default Well Name";

        #region Overrides of TextCommandHandler

        public override bool CanExecute(Context context)
        {
            return IsAWellSelected(context);
        }

        private static bool IsAWellSelected(Context context)
        {
            var selectedBoreholes = context.GetSelectedObjects().OfType<Borehole>().ToList();
            return selectedBoreholes.Count == 1;
        }

        public override void Execute(Context context)
        {
            //
            // update the stored text in Execute method
            _text = context.GetParameter<string>(CommandParameterIds.Text);
            //
            var selectedBorehole = context.GetSelectedObjects().OfType<Borehole>().FirstOrDefault();
            //
            if (selectedBorehole != null)
            {
                using (var transaction = DataManager.NewTransaction())
                {
                    transaction.Lock(selectedBorehole);
                    //
                    selectedBorehole.Name = _text;
                    //
                    transaction.Commit();
                }
            }
        }

        public override bool CanAcceptText(Context context, string text)
        {
            return !text.HasAnyDigits();
        }

        public override bool CanEditText(Context context)
        {
            if (!CanExecute(context))
                return false;
            return true;
        }

        public override string GetEditText(Context context)
        {
            return this._text + "*";
        }

        public override string GetText(Context context)
        {
            return this._text;
        }

        #endregion
    }
}

