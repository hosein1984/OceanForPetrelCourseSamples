using System;
using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.UI;
using Slb.Ocean.Petrel.Workflow;

namespace OceanCoursePlugin._1_CoreAndServices
{
    /// <summary>
    /// This class will control the lifecycle of the Module.
    /// The order of the methods are the same as the calling order.
    /// </summary>
    public class CoreAndServicesModule : IModule
    {
        public CoreAndServicesModule()
        {
        }

        #region IModule Members

        /// <summary>
        /// This method runs once in the Module life; when it loaded into the petrel.
        /// This method called first.
        /// </summary>
        public void Initialize()
        {
            // TODO:  Add CoreAndServicesModule.Initialize implementation
        }

        /// <summary>
        /// This method runs once in the Module life. 
        /// In this method, you can do registrations of the not UI related components.
        /// (eg: datasource, plugin)
        /// </summary>
        public void Integrate()
        {
            // Register OceanCoursePlugin._1_CoreAndServices.HelloWorldWorkstep
            OceanCoursePlugin._1_CoreAndServices.HelloWorldWorkstep helloworldworkstepInstance = new OceanCoursePlugin._1_CoreAndServices.HelloWorldWorkstep();
            PetrelSystem.WorkflowEditor.Add(helloworldworkstepInstance);

            // TODO:  Add CoreAndServicesModule.Integrate implementation
        }

        /// <summary>
        /// This method runs once in the Module life. 
        /// In this method, you can do registrations of the UI related components.
        /// (eg: settingspages, treeextensions)
        /// </summary>
        public void IntegratePresentation()
        {

            // TODO:  Add CoreAndServicesModule.IntegratePresentation implementation
        }

        /// <summary>
        /// This method runs once in the Module life.
        /// right before the module is unloaded. 
        /// It usually happens when the application is closing.
        /// </summary>
        public void Disintegrate()
        {
            // TODO:  Add CoreAndServicesModule.Disintegrate implementation
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            // TODO:  Add CoreAndServicesModule.Dispose implementation
        }

        #endregion

    }


}