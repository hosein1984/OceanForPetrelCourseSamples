using System;
using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.UI;
using Slb.Ocean.Petrel.Workflow;

namespace OceanCoursePlugin._10_DataAccessSimulation
{
    /// <summary>
    /// This class will control the lifecycle of the Module.
    /// The order of the methods are the same as the calling order.
    /// </summary>
    public class DataAccessSimulationModule : IModule
    {
        private Process m_runcaseworkstepInstance;
        private Process m_createsimulationcaseworkstepInstance;
        public DataAccessSimulationModule()
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
            // TODO:  Add DataAccessSimulationModule.Initialize implementation
        }

        /// <summary>
        /// This method runs once in the Module life. 
        /// In this method, you can do registrations of the not UI related components.
        /// (eg: datasource, plugin)
        /// </summary>
        public void Integrate()
        {
            // Register OceanCoursePlugin._10_DataAccessSimulation.RunCaseWorkstep
            OceanCoursePlugin._10_DataAccessSimulation.RunCaseWorkstep runcaseworkstepInstance = new OceanCoursePlugin._10_DataAccessSimulation.RunCaseWorkstep();
            PetrelSystem.WorkflowEditor.Add(runcaseworkstepInstance);
            m_runcaseworkstepInstance = new Slb.Ocean.Petrel.Workflow.WorkstepProcessWrapper(runcaseworkstepInstance);
            PetrelSystem.ProcessDiagram.Add(m_runcaseworkstepInstance, "Plug-ins");
            // Register OceanCoursePlugin._10_DataAccessSimulation.CreateSimulationCaseWorkstep
            OceanCoursePlugin._10_DataAccessSimulation.CreateSimulationCaseWorkstep createsimulationcaseworkstepInstance = new OceanCoursePlugin._10_DataAccessSimulation.CreateSimulationCaseWorkstep();
            PetrelSystem.WorkflowEditor.Add(createsimulationcaseworkstepInstance);
            m_createsimulationcaseworkstepInstance = new Slb.Ocean.Petrel.Workflow.WorkstepProcessWrapper(createsimulationcaseworkstepInstance);
            PetrelSystem.ProcessDiagram.Add(m_createsimulationcaseworkstepInstance, "Plug-ins");

            // TODO:  Add DataAccessSimulationModule.Integrate implementation
        }

        /// <summary>
        /// This method runs once in the Module life. 
        /// In this method, you can do registrations of the UI related components.
        /// (eg: settingspages, treeextensions)
        /// </summary>
        public void IntegratePresentation()
        {

            // TODO:  Add DataAccessSimulationModule.IntegratePresentation implementation
        }

        /// <summary>
        /// This method runs once in the Module life.
        /// right before the module is unloaded. 
        /// It usually happens when the application is closing.
        /// </summary>
        public void Disintegrate()
        {
            PetrelSystem.ProcessDiagram.Remove(m_runcaseworkstepInstance);
            PetrelSystem.ProcessDiagram.Remove(m_createsimulationcaseworkstepInstance);
            // TODO:  Add DataAccessSimulationModule.Disintegrate implementation
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            // TODO:  Add DataAccessSimulationModule.Dispose implementation
        }

        #endregion

    }


}