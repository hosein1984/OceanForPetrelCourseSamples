using System;
using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.UI;
using Slb.Ocean.Petrel.Workflow;

namespace OceanCoursePlugin._6_DataAccessSeismicData
{
    /// <summary>
    /// This class will control the lifecycle of the Module.
    /// The order of the methods are the same as the calling order.
    /// </summary>
    public class DataAccessSeismicDataModule : IModule
    {
        private Process m_copyseismiccubewithreversedpolarityworkstepInstance;
        private Process m_printseismiccubecornersworkstepInstance;
        public DataAccessSeismicDataModule()
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
            // TODO:  Add DataAccessSeismicDataModule.Initialize implementation
        }

        /// <summary>
        /// This method runs once in the Module life. 
        /// In this method, you can do registrations of the not UI related components.
        /// (eg: datasource, plugin)
        /// </summary>
        public void Integrate()
        {
            // Register OceanCoursePlugin._5_DataAccessShapes.CopySeismicCubeWithReversedPolarityWorkstep
            CopySeismicCubeWithReversedPolarityWorkstep copyseismiccubewithreversedpolarityworkstepInstance = new CopySeismicCubeWithReversedPolarityWorkstep();
            PetrelSystem.WorkflowEditor.Add(copyseismiccubewithreversedpolarityworkstepInstance);
            m_copyseismiccubewithreversedpolarityworkstepInstance = new Slb.Ocean.Petrel.Workflow.WorkstepProcessWrapper(copyseismiccubewithreversedpolarityworkstepInstance);
            PetrelSystem.ProcessDiagram.Add(m_copyseismiccubewithreversedpolarityworkstepInstance, "Plug-ins");
            // Register OceanCoursePlugin._6_DataAccessSeismicData.PrintSeismicCubeCornersWorkstep
            OceanCoursePlugin._6_DataAccessSeismicData.PrintSeismicCubeCornersWorkstep printseismiccubecornersworkstepInstance = new OceanCoursePlugin._6_DataAccessSeismicData.PrintSeismicCubeCornersWorkstep();
            PetrelSystem.WorkflowEditor.Add(printseismiccubecornersworkstepInstance);
            m_printseismiccubecornersworkstepInstance = new Slb.Ocean.Petrel.Workflow.WorkstepProcessWrapper(printseismiccubecornersworkstepInstance);
            PetrelSystem.ProcessDiagram.Add(m_printseismiccubecornersworkstepInstance, "Plug-ins");

            // TODO:  Add DataAccessSeismicDataModule.Integrate implementation
        }

        /// <summary>
        /// This method runs once in the Module life. 
        /// In this method, you can do registrations of the UI related components.
        /// (eg: settingspages, treeextensions)
        /// </summary>
        public void IntegratePresentation()
        {

            // TODO:  Add DataAccessSeismicDataModule.IntegratePresentation implementation
        }

        /// <summary>
        /// This method runs once in the Module life.
        /// right before the module is unloaded. 
        /// It usually happens when the application is closing.
        /// </summary>
        public void Disintegrate()
        {
            PetrelSystem.ProcessDiagram.Remove(m_copyseismiccubewithreversedpolarityworkstepInstance);
            PetrelSystem.ProcessDiagram.Remove(m_printseismiccubecornersworkstepInstance);
            // TODO:  Add DataAccessSeismicDataModule.Disintegrate implementation
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            // TODO:  Add DataAccessSeismicDataModule.Dispose implementation
        }

        #endregion

    }


}