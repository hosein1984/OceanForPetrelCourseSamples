using System;
using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.UI;
using Slb.Ocean.Petrel.Workflow;

namespace OceanCoursePlugin._14_UIWindows
{
    /// <summary>
    /// This class will control the lifecycle of the Module.
    /// The order of the methods are the same as the calling order.
    /// </summary>
    public class UIWindowsModule : IModule
    {
        private Process m_displaywellheadinformationwithcustomuiworkstepInstance;
        private Process m_timeunitconversionwithcustomuiworkstepInstance;
        public UIWindowsModule()
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
            // TODO:  Add UIWindowsModule.Initialize implementation
        }

        /// <summary>
        /// This method runs once in the Module life. 
        /// In this method, you can do registrations of the not UI related components.
        /// (eg: datasource, plugin)
        /// </summary>
        public void Integrate()
        {
            // Register OceanCoursePlugin._14_UIWindows.DisplayWellHeadInformationWithCustomUIWorkstep
            OceanCoursePlugin._14_UIWindows.DisplayWellHeadInformationWithCustomUIWorkstep displaywellheadinformationwithcustomuiworkstepInstance = new OceanCoursePlugin._14_UIWindows.DisplayWellHeadInformationWithCustomUIWorkstep();
            PetrelSystem.WorkflowEditor.AddUIFactory<OceanCoursePlugin._14_UIWindows.DisplayWellHeadInformationWithCustomUIWorkstep.Arguments>(new OceanCoursePlugin._14_UIWindows.DisplayWellHeadInformationWithCustomUIWorkstep.UIFactory());
            PetrelSystem.WorkflowEditor.Add(displaywellheadinformationwithcustomuiworkstepInstance);
            m_displaywellheadinformationwithcustomuiworkstepInstance = new Slb.Ocean.Petrel.Workflow.WorkstepProcessWrapper(displaywellheadinformationwithcustomuiworkstepInstance);
            PetrelSystem.ProcessDiagram.Add(m_displaywellheadinformationwithcustomuiworkstepInstance, "Plug-ins");
            // Register OceanCoursePlugin._14_UIWindows.TimeUnitConversionWithCustomUIWorkstep
            OceanCoursePlugin._14_UIWindows.TimeUnitConversionWithCustomUIWorkstep timeunitconversionwithcustomuiworkstepInstance = new OceanCoursePlugin._14_UIWindows.TimeUnitConversionWithCustomUIWorkstep();
            PetrelSystem.WorkflowEditor.AddUIFactory<OceanCoursePlugin._14_UIWindows.TimeUnitConversionWithCustomUIWorkstep.Arguments>(new OceanCoursePlugin._14_UIWindows.TimeUnitConversionWithCustomUIWorkstep.UIFactory());
            PetrelSystem.WorkflowEditor.Add(timeunitconversionwithcustomuiworkstepInstance);
            m_timeunitconversionwithcustomuiworkstepInstance = new Slb.Ocean.Petrel.Workflow.WorkstepProcessWrapper(timeunitconversionwithcustomuiworkstepInstance);
            PetrelSystem.ProcessDiagram.Add(m_timeunitconversionwithcustomuiworkstepInstance, "Plug-ins");
            
        }

        /// <summary>
        /// This method runs once in the Module life. 
        /// In this method, you can do registrations of the UI related components.
        /// (eg: settingspages, treeextensions)
        /// </summary>
        public void IntegratePresentation()
        {

            // TODO:  Add UIWindowsModule.IntegratePresentation implementation
        }

        /// <summary>
        /// This method runs once in the Module life.
        /// right before the module is unloaded. 
        /// It usually happens when the application is closing.
        /// </summary>
        public void Disintegrate()
        {
            // Unregister OceanCoursePlugin._14_UIWindows.DisplayWellHeadInformationWithCustomUIWorkstep
            PetrelSystem.WorkflowEditor.RemoveUIFactory<OceanCoursePlugin._14_UIWindows.DisplayWellHeadInformationWithCustomUIWorkstep.Arguments>();
            PetrelSystem.ProcessDiagram.Remove(m_displaywellheadinformationwithcustomuiworkstepInstance);
            // Unregister OceanCoursePlugin._14_UIWindows.TimeUnitConversionWithCustomUIWorkstep
            PetrelSystem.WorkflowEditor.RemoveUIFactory<OceanCoursePlugin._14_UIWindows.TimeUnitConversionWithCustomUIWorkstep.Arguments>();
            PetrelSystem.ProcessDiagram.Remove(m_timeunitconversionwithcustomuiworkstepInstance);
            // TODO:  Add UIWindowsModule.Disintegrate implementation
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            // TODO:  Add UIWindowsModule.Dispose implementation
        }

        #endregion

    }


}