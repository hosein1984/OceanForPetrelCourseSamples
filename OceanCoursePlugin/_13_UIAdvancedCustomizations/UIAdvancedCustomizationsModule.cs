using System;
using OceanCoursePlugin.Properties;
using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.UI;
using Slb.Ocean.Petrel.Workflow;

namespace OceanCoursePlugin._13_UIAdvancedCustomizations
{
    /// <summary>
    /// This class will control the lifecycle of the Module.
    /// The order of the methods are the same as the calling order.
    /// </summary>
    public class UIAdvancedCustomizationsModule : IModule
    {
        public UIAdvancedCustomizationsModule()
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
            // TODO:  Add UIAdvancedCustomizationsModule.Initialize implementation
        }

        /// <summary>
        /// This method runs once in the Module life. 
        /// In this method, you can do registrations of the not UI related components.
        /// (eg: datasource, plugin)
        /// </summary>
        public void Integrate()
        {
            // Register UpdateWellNameTextCommandHandler
            PetrelSystem.CommandManager.CreateCommand(OceanCoursePlugin._13_UIAdvancedCustomizations.UpdateWellNameTextCommandHandler.ID, new OceanCoursePlugin._13_UIAdvancedCustomizations.UpdateWellNameTextCommandHandler());

            // TODO:  Add UIAdvancedCustomizationsModule.Integrate implementation
        }

        /// <summary>
        /// This method runs once in the Module life. 
        /// In this method, you can do registrations of the UI related components.
        /// (eg: settingspages, treeextensions)
        /// </summary>
        public void IntegratePresentation()
        {
            PetrelSystem.ConfigurationService.AddConfiguration(Resources.PetrelConfigFile);

            // TODO:  Add UIAdvancedCustomizationsModule.IntegratePresentation implementation
        }

        /// <summary>
        /// This method runs once in the Module life.
        /// right before the module is unloaded. 
        /// It usually happens when the application is closing.
        /// </summary>
        public void Disintegrate()
        {
            // TODO:  Add UIAdvancedCustomizationsModule.Disintegrate implementation
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            // TODO:  Add UIAdvancedCustomizationsModule.Dispose implementation
        }

        #endregion

    }


}