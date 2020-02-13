using System;
using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.UI;
using Slb.Ocean.Petrel.Workflow;

namespace OceanCoursePlugin._8_DataAccessWellsAndLogs
{
    /// <summary>
    /// This class will control the lifecycle of the Module.
    /// The order of the methods are the same as the calling order.
    /// </summary>
    public class DataAccessWellsAndLogsModule : IModule
    {
        private Process m_copywelllogwithmultiplierInstance;
        private Process m_createwellpathworkstepInstance;
        public DataAccessWellsAndLogsModule()
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
            // TODO:  Add DataAccessWellsAndLogsModule.Initialize implementation
        }

        /// <summary>
        /// This method runs once in the Module life. 
        /// In this method, you can do registrations of the not UI related components.
        /// (eg: datasource, plugin)
        /// </summary>
        public void Integrate()
        {
            // Register OceanCoursePlugin._8_DataAccessWellsAndLogs.CopyWellLogWithMultiplier
            OceanCoursePlugin._8_DataAccessWellsAndLogs.CopyWellLogWithMultiplier copywelllogwithmultiplierInstance = new OceanCoursePlugin._8_DataAccessWellsAndLogs.CopyWellLogWithMultiplier();
            PetrelSystem.WorkflowEditor.Add(copywelllogwithmultiplierInstance);
            m_copywelllogwithmultiplierInstance = new Slb.Ocean.Petrel.Workflow.WorkstepProcessWrapper(copywelllogwithmultiplierInstance);
            PetrelSystem.ProcessDiagram.Add(m_copywelllogwithmultiplierInstance, "Plug-ins");
            // Register OceanCoursePlugin._8_DataAccessWellsAndLogs.CreateWellPathWorkstep
            OceanCoursePlugin._8_DataAccessWellsAndLogs.CreateWellPathWorkstep createwellpathworkstepInstance = new OceanCoursePlugin._8_DataAccessWellsAndLogs.CreateWellPathWorkstep();
            PetrelSystem.WorkflowEditor.Add(createwellpathworkstepInstance);
            m_createwellpathworkstepInstance = new Slb.Ocean.Petrel.Workflow.WorkstepProcessWrapper(createwellpathworkstepInstance);
            PetrelSystem.ProcessDiagram.Add(m_createwellpathworkstepInstance, "Plug-ins");

            // TODO:  Add DataAccessWellsAndLogsModule.Integrate implementation
        }

        /// <summary>
        /// This method runs once in the Module life. 
        /// In this method, you can do registrations of the UI related components.
        /// (eg: settingspages, treeextensions)
        /// </summary>
        public void IntegratePresentation()
        {

            // TODO:  Add DataAccessWellsAndLogsModule.IntegratePresentation implementation
        }

        /// <summary>
        /// This method runs once in the Module life.
        /// right before the module is unloaded. 
        /// It usually happens when the application is closing.
        /// </summary>
        public void Disintegrate()
        {
            PetrelSystem.ProcessDiagram.Remove(m_copywelllogwithmultiplierInstance);
            PetrelSystem.ProcessDiagram.Remove(m_createwellpathworkstepInstance);
            // TODO:  Add DataAccessWellsAndLogsModule.Disintegrate implementation
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            // TODO:  Add DataAccessWellsAndLogsModule.Dispose implementation
        }

        #endregion

    }


}