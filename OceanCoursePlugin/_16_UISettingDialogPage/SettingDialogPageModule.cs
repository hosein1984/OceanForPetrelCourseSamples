using System;
using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.UI;
using Slb.Ocean.Petrel.Workflow;

namespace OceanCoursePlugin._16_UISettingDialogPage
{
    /// <summary>
    /// This class will control the lifecycle of the Module.
    /// The order of the methods are the same as the calling order.
    /// </summary>
    public class SettingDialogPageModule : IModule
    {
        private OceanCoursePlugin._16_UISettingDialogPage.XYZObjectSettingPageFactory m_xYZObjectSettingPageFactory;
        private Process m_printsettingdataworkstepInstance;
        public SettingDialogPageModule()
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
            // TODO:  Add SettingDialogPageModule.Initialize implementation
        }

        /// <summary>
        /// This method runs once in the Module life. 
        /// In this method, you can do registrations of the not UI related components.
        /// (eg: datasource, plugin)
        /// </summary>
        public void Integrate()
        {
            // Register OceanCoursePlugin._16_UISettingDialogPage.XYZObjectSettingPage
            m_xYZObjectSettingPageFactory = new OceanCoursePlugin._16_UISettingDialogPage.XYZObjectSettingPageFactory();
            PetrelSystem.DialogBuilder.AddFactory(m_xYZObjectSettingPageFactory);
            // Register OceanCoursePlugin._16_UISettingDialogPage.PrintSettingDataWorkstep
            OceanCoursePlugin._16_UISettingDialogPage.PrintSettingDataWorkstep printsettingdataworkstepInstance = new OceanCoursePlugin._16_UISettingDialogPage.PrintSettingDataWorkstep();
            PetrelSystem.WorkflowEditor.Add(printsettingdataworkstepInstance);
            m_printsettingdataworkstepInstance = new Slb.Ocean.Petrel.Workflow.WorkstepProcessWrapper(printsettingdataworkstepInstance);
            PetrelSystem.ProcessDiagram.Add(m_printsettingdataworkstepInstance, "Plug-ins");

            // TODO:  Add SettingDialogPageModule.Integrate implementation
        }

        /// <summary>
        /// This method runs once in the Module life. 
        /// In this method, you can do registrations of the UI related components.
        /// (eg: settingspages, treeextensions)
        /// </summary>
        public void IntegratePresentation()
        {

            // TODO:  Add SettingDialogPageModule.IntegratePresentation implementation
        }

        /// <summary>
        /// This method runs once in the Module life.
        /// right before the module is unloaded. 
        /// It usually happens when the application is closing.
        /// </summary>
        public void Disintegrate()
        {
            // UnRegister OceanCoursePlugin._16_UISettingDialogPage.XYZObjectSettingPage
            PetrelSystem.DialogBuilder.RemoveFactory(m_xYZObjectSettingPageFactory);
            PetrelSystem.ProcessDiagram.Remove(m_printsettingdataworkstepInstance);
            // TODO:  Add SettingDialogPageModule.Disintegrate implementation
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            // TODO:  Add SettingDialogPageModule.Dispose implementation
        }

        #endregion

    }


}