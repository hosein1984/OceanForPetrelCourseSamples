using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Slb.Ocean.Petrel.Commands;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.Contexts;
using Slb.Ocean.Petrel.DomainObject.Well;
using Slb.Ocean.Petrel.UI;

namespace OceanCoursePlugin._13_UIAdvancedCustomizations
{
    class MakeWellVisibleIn3DOptionCommandHandler : OptionCommandHandler
    {
        public static string ID = "OceanCoursePlugin._13_UIAdvancedCustomizations.MakeWellVisibleIn3DOptionCommandHandler";

        private Borehole _selectedBorehole = Borehole.NullObject;

        public override bool CanExecute(Context context)
        {
            var window3D = context.GetActiveWindow() as Window3D;
            //
            return window3D != null;
        }

        public override void Execute(Context context)
        {
            if (!CanExecute(context)) return;
            //
            var window3D = context.GetActiveWindow() as Window3D;
            //
            // if there is alreay a well made visible by this command hide it
            if (_selectedBorehole != Borehole.NullObject)
            {
                window3D?.HideObject(_selectedBorehole);
            }
            //
            // then update the selected borehole
            _selectedBorehole = context.GetParameter<Borehole>(CommandParameterIds.Option);
            //
            // and finally show the newly selected borehole
            if (_selectedBorehole != Borehole.NullObject)
            {
                window3D?.ShowObject(_selectedBorehole);
            }
        }

        public override IEnumerable<object> GetOptions(Context context)
        {
            var boreholes = new List<Borehole>();
            //
            WellRoot wellRoot = WellRoot.Get(PetrelProject.PrimaryProject);
            var mainBoreholeCollection = wellRoot.BoreholeCollection;
            //
            FindAllBoreholesRecursively(mainBoreholeCollection, boreholes);
            //
            return boreholes;
        }

        public override object GetSelectedOption(Context context)
        {
            return _selectedBorehole;
        }

        private void FindAllBoreholesRecursively(BoreholeCollection boreholeCollection, List<Borehole> boreholes)
        {
            foreach (Borehole borehole in boreholeCollection)
            {
                boreholes.Add(borehole);
            }
            //
            foreach (var childBoreholeCollection in boreholeCollection.BoreholeCollections)
            {
                FindAllBoreholesRecursively(childBoreholeCollection, boreholes);
            }
        }
    }
}
