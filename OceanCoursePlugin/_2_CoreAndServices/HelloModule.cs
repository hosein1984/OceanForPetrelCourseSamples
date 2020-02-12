using System.Reflection;
using Slb.Ocean.Core;

namespace OceanCoursePlugin._2_CoreAndServices
{
    class HelloModule : IModule
    {
        private static readonly string ClassName = typeof(HelloModule).Name;

        public HelloModule()
        {
            CoreLogger.Info($"{ClassName}: {MethodBase.GetCurrentMethod().Name}");
        }

        public void Dispose()
        {
            CoreLogger.Info($"{ClassName}: {MethodBase.GetCurrentMethod().Name}");
        }

        public void Initialize()
        {
            CoreLogger.Info($"{ClassName}: {MethodBase.GetCurrentMethod().Name}");
        }

        public void Disintegrate()
        {
            CoreLogger.Info($"{ClassName}: {MethodBase.GetCurrentMethod().Name}");
        }

        public void Integrate()
        {
            CoreLogger.Info($"{ClassName}: {MethodBase.GetCurrentMethod().Name}");
        }

        public void IntegratePresentation()
        {
            CoreLogger.Info($"{ClassName}: {MethodBase.GetCurrentMethod().Name}");
        }
    }
}
