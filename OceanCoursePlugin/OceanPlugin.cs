using System;
using System.Collections.Generic;
using System.Linq;
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

        public override IEnumerable<ModuleReference> Modules => Enumerable.Empty<ModuleReference>();

        public override string Name => "OceanPlugin";

        public override PluginIdentifier PluginId => new PluginIdentifier(GetType().FullName, GetType().Assembly.GetName().Version);

        public override ModuleTrust Trust => new ModuleTrust("Default");
    }
}
