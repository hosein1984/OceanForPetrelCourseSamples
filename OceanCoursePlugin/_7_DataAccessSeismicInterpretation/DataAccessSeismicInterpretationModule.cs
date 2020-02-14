using System;
using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.UI;
using Slb.Ocean.Petrel.Workflow;

namespace OceanCoursePlugin._7_DataAccessSeismicInterpretation
{
    /// <summary>
    /// This class will control the lifecycle of the Module.
    /// The order of the methods are the same as the calling order.
    /// </summary>
    public class DataAccessSeismicInterpretationModule : IModule
    {
        private Process m_seismicinterpretationworkstepInstance;
        public DataAccessSeismicInterpretationModule()
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
            // TODO:  Add DataAccessSeismicInterpretationModule.Initialize implementation
        }

        /// <summary>
        /// This method runs once in the Module life. 
        /// In this method, you can do registrations of the not UI related components.
        /// (eg: datasource, plugin)
        /// </summary>
        public void Integrate()
        {
            // Register OceanCoursePlugin._7_DataAccessSeismicInterpretation.SeismicInterpretationWorkstep
            OceanCoursePlugin._7_DataAccessSeismicInterpretation.SeismicInterpretationWorkstep seismicinterpretationworkstepInstance = new OceanCoursePlugin._7_DataAccessSeismicInterpretation.SeismicInterpretationWorkstep();
            PetrelSystem.WorkflowEditor.Add(seismicinterpretationworkstepInstance);
            m_seismicinterpretationworkstepInstance = new Slb.Ocean.Petrel.Workflow.WorkstepProcessWrapper(seismicinterpretationworkstepInstance);
            PetrelSystem.ProcessDiagram.Add(m_seismicinterpretationworkstepInstance, "Plug-ins");

            // TODO:  Add DataAccessSeismicInterpretationModule.Integrate implementation
        }

        /// <summary>
        /// This method runs once in the Module life. 
        /// In this method, you can do registrations of the UI related components.
        /// (eg: settingspages, treeextensions)
        /// </summary>
        public void IntegratePresentation()
        {

            // TODO:  Add DataAccessSeismicInterpretationModule.IntegratePresentation implementation
        }

        /// <summary>
        /// This method runs once in the Module life.
        /// right before the module is unloaded. 
        /// It usually happens when the application is closing.
        /// </summary>
        public void Disintegrate()
        {
            PetrelSystem.ProcessDiagram.Remove(m_seismicinterpretationworkstepInstance);
            // TODO:  Add DataAccessSeismicInterpretationModule.Disintegrate implementation
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            // TODO:  Add DataAccessSeismicInterpretationModule.Dispose implementation
        }

        #endregion

    }


}