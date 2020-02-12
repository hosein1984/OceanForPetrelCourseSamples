using System.Reflection;
using Slb.Ocean.Core;
using Slb.Ocean.Petrel;

namespace OceanCoursePlugin._2_CoreAndServices
{
    /// <summary>
    /// This class will control the lifecycle of the Module.
    /// The order of the methods are the same as the calling order.
    /// </summary>
    public class CoreAndServicesModule : IModule
    {
        private static readonly string ClassName = typeof(CoreAndServicesModule).Name;

        public CoreAndServicesModule()
        {
            CoreLogger.Info($"{ClassName}: {MethodBase.GetCurrentMethod().Name}");
        }

        #region IModule Members

        /// <summary>
        /// This method runs once in the Module life; when it loaded into the petrel.
        /// This method called first.
        /// </summary>
        public void Initialize()
        {
            CoreLogger.Info($"{ClassName}: {MethodBase.GetCurrentMethod().Name}");
        }

        /// <summary>
        /// This method runs once in the Module life. 
        /// In this method, you can do registrations of the not UI related components.
        /// (eg: datasource, plugin)
        /// </summary>
        public void Integrate()
        {
            // Register OceanCoursePlugin._1_CoreAndServices.HelloWorldWorkstep
            HelloWorldWorkstep helloworldworkstepInstance = new HelloWorldWorkstep();
            PetrelSystem.WorkflowEditor.Add(helloworldworkstepInstance);

            CoreLogger.Info($"{ClassName}: {MethodBase.GetCurrentMethod().Name}");
        }

        /// <summary>
        /// This method runs once in the Module life. 
        /// In this method, you can do registrations of the UI related components.
        /// (eg: settingspages, treeextensions)
        /// </summary>
        public void IntegratePresentation()
        {
            CoreLogger.Info($"{ClassName}: {MethodBase.GetCurrentMethod().Name}");
        }

        /// <summary>
        /// This method runs once in the Module life.
        /// right before the module is unloaded. 
        /// It usually happens when the application is closing.
        /// </summary>
        public void Disintegrate()
        {
            CoreLogger.Info($"{ClassName}: {MethodBase.GetCurrentMethod().Name}");
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            CoreLogger.Info($"{ClassName}: {MethodBase.GetCurrentMethod().Name}");
        }

        
        #endregion

    }


}