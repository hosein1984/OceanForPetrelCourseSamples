using System;
using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.UI;
using Slb.Ocean.Petrel.Workflow;

namespace OceanCoursePlugin._9_DataAccessPillarGrid
{
    /// <summary>
    /// This class will control the lifecycle of the Module.
    /// The order of the methods are the same as the calling order.
    /// </summary>
    public class DataAccessPillarGridModule : IModule
    {
        private Process m_createdistanceabovecontactpropertyworkstepInstance;
        private Process m_hellogridworkstepInstance;
        public DataAccessPillarGridModule()
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
            // TODO:  Add DataAccessPillarGridModule.Initialize implementation
        }

        /// <summary>
        /// This method runs once in the Module life. 
        /// In this method, you can do registrations of the not UI related components.
        /// (eg: datasource, plugin)
        /// </summary>
        public void Integrate()
        {
            // Register OceanCoursePlugin._9_DataAccessPillarGrid.CreateDistanceAboveContactPropertyWorkstep
            OceanCoursePlugin._9_DataAccessPillarGrid.CreateDistanceAboveContactPropertyWorkstep createdistanceabovecontactpropertyworkstepInstance = new OceanCoursePlugin._9_DataAccessPillarGrid.CreateDistanceAboveContactPropertyWorkstep();
            PetrelSystem.WorkflowEditor.Add(createdistanceabovecontactpropertyworkstepInstance);
            m_createdistanceabovecontactpropertyworkstepInstance = new Slb.Ocean.Petrel.Workflow.WorkstepProcessWrapper(createdistanceabovecontactpropertyworkstepInstance);
            PetrelSystem.ProcessDiagram.Add(m_createdistanceabovecontactpropertyworkstepInstance, "Plug-ins");
            // Register OceanCoursePlugin._9_DataAccessPillarGrid.HelloGridWorkstep
            OceanCoursePlugin._9_DataAccessPillarGrid.HelloGridWorkstep hellogridworkstepInstance = new OceanCoursePlugin._9_DataAccessPillarGrid.HelloGridWorkstep();
            PetrelSystem.WorkflowEditor.Add(hellogridworkstepInstance);
            m_hellogridworkstepInstance = new Slb.Ocean.Petrel.Workflow.WorkstepProcessWrapper(hellogridworkstepInstance);
            PetrelSystem.ProcessDiagram.Add(m_hellogridworkstepInstance, "Plug-ins");

            // TODO:  Add DataAccessPillarGridModule.Integrate implementation
        }

        /// <summary>
        /// This method runs once in the Module life. 
        /// In this method, you can do registrations of the UI related components.
        /// (eg: settingspages, treeextensions)
        /// </summary>
        public void IntegratePresentation()
        {

            // TODO:  Add DataAccessPillarGridModule.IntegratePresentation implementation
        }

        /// <summary>
        /// This method runs once in the Module life.
        /// right before the module is unloaded. 
        /// It usually happens when the application is closing.
        /// </summary>
        public void Disintegrate()
        {
            PetrelSystem.ProcessDiagram.Remove(m_createdistanceabovecontactpropertyworkstepInstance);
            PetrelSystem.ProcessDiagram.Remove(m_hellogridworkstepInstance);
            // TODO:  Add DataAccessPillarGridModule.Disintegrate implementation
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            // TODO:  Add DataAccessPillarGridModule.Dispose implementation
        }

        #endregion

    }


}