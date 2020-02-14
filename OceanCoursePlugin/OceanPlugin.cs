using System;
using System.Collections.Generic;
using System.Linq;
using OceanCoursePlugin._10_DataAccessSimulation;
using OceanCoursePlugin._11_UIOverview;
using OceanCoursePlugin._3_WorkflowAndWorksteps;
using OceanCoursePlugin._4_DataAccessOverview;
using OceanCoursePlugin._5_DataAccessShapes;
using OceanCoursePlugin._6_DataAccessSeismicData;
using OceanCoursePlugin._7_DataAccessSeismicInterpretation;
using OceanCoursePlugin._8_DataAccessWellsAndLogs;
using OceanCoursePlugin._9_DataAccessPillarGrid;
using Slb.Ocean.Core;

namespace OceanCoursePlugin
{
    public class OceanPlugin : Plugin
    {
        public override string AppVersion => "2016.1";

        public override string Author => "Hosein";

        public override string Contact => "contact@company.info";

        public override IEnumerable<PluginIdentifier> Dependencies => Enumerable.Empty<PluginIdentifier>();

        public override string Description => "this is the main registery point for the plugin";

        public override string ImageResourceName => null;

        public override Uri PluginUri => new Uri("http://www.pluginuri.info");

        public override IEnumerable<ModuleReference> Modules
        {
            get
            {
                //yield return new ModuleReference(typeof(CoreAndServicesModule));
                //yield return new ModuleReference(typeof(HelloModule));
                //yield return new ModuleReference(typeof(WorkflowAndWorkstepsModule));
                //yield return new ModuleReference(typeof(DataAccessOverviewModule));
                //yield return new ModuleReference(typeof(DataAccessShapesModule));
                //yield return new ModuleReference(typeof(DataAccessSeismicDataModule));
                //yield return new ModuleReference(typeof(DataAccessSeismicInterpretationModule));
                //yield return new ModuleReference(typeof(DataAccessSeismicDataModule));
                //yield return new ModuleReference(typeof(DataAccessWellsAndLogsModule));
                //yield return new ModuleReference(typeof(DataAccessPillarGridModule));
                //yield return new ModuleReference(typeof(DataAccessSimulationModule));
                yield return new ModuleReference(typeof(UIOverviewModule));
            }
        }

        public override string Name => "OceanPlugin";

        public override PluginIdentifier PluginId => new PluginIdentifier(GetType().FullName, GetType().Assembly.GetName().Version);

        public override ModuleTrust Trust => new ModuleTrust("Default");
    }
}
