using System;
using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.UI;
using Slb.Ocean.Petrel.Workflow;

namespace OceanCoursePlugin._11_UIOverview
{
    /// <summary>
    /// This class will control the lifecycle of the Module.
    /// The order of the methods are the same as the calling order.
    /// </summary>
    public class UIOverviewModule : IModule
    {
        private Process m_progressdemoworkstepInstance;
        public UIOverviewModule()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region IModule Members

        /// <summary>
        /// This method runs once in the Module life; when it loaded into the petrel.
        /// This method called first.
        /// </summary>
        public void Initialize()
        {
            // TODO:  Add UIOverviewModule.Initialize implementation
        }

        /// <summary>
        /// This method runs once in the Module life. 
        /// In this method, you can do registrations of the not UI related components.
        /// (eg: datasource, plugin)
        /// </summary>
        public void Integrate()
        {
            // Register OceanCoursePlugin._11_UIOverview.ProgressDemoWorkstep
            OceanCoursePlugin._11_UIOverview.ProgressDemoWorkstep progressdemoworkstepInstance = new OceanCoursePlugin._11_UIOverview.ProgressDemoWorkstep();
            PetrelSystem.WorkflowEditor.Add(progressdemoworkstepInstance);
            m_progressdemoworkstepInstance = new Slb.Ocean.Petrel.Workflow.WorkstepProcessWrapper(progressdemoworkstepInstance);
            PetrelSystem.ProcessDiagram.Add(m_progressdemoworkstepInstance, "Plug-ins");

            // TODO:  Add UIOverviewModule.Integrate implementation
        }

        /// <summary>
        /// This method runs once in the Module life. 
        /// In this method, you can do registrations of the UI related components.
        /// (eg: settingspages, treeextensions)
        /// </summary>
        public void IntegratePresentation()
        {

            // TODO:  Add UIOverviewModule.IntegratePresentation implementation
        }

        /// <summary>
        /// This method runs once in the Module life.
        /// right before the module is unloaded. 
        /// It usually happens when the application is closing.
        /// </summary>
        public void Disintegrate()
        {
            PetrelSystem.ProcessDiagram.Remove(m_progressdemoworkstepInstance);
            // TODO:  Add UIOverviewModule.Disintegrate implementation
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            // TODO:  Add UIOverviewModule.Dispose implementation
        }

        #endregion

    }


}