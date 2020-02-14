using System;
using OceanCoursePlugin.Properties;
using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.Commands;
using Slb.Ocean.Petrel.DomainObject.Well;
using Slb.Ocean.Petrel.UI;
using Slb.Ocean.Petrel.UI.Tools;
using Slb.Ocean.Petrel.Workflow;

namespace OceanCoursePlugin._12_UICustomizations
{
    /// <summary>
    /// This class will control the lifecycle of the Module.
    /// The order of the methods are the same as the calling order.
    /// </summary>
    public class UICustomizationsModule : IModule
    {
        public UICustomizationsModule()
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
            // TODO:  Add UICustomizationsModule.Initialize implementation
        }

        /// <summary>
        /// This method runs once in the Module life. 
        /// In this method, you can do registrations of the not UI related components.
        /// (eg: datasource, plugin)
        /// </summary>
        public void Integrate()
        {
            // Register PrintWellMDRangeCommandHandler
            PetrelSystem.CommandManager.CreateCommand(PrintWellMDRangeCommandHandler.ID, new PrintWellMDRangeCommandHandler());
            // Register AccessCommandHandler
            PetrelSystem.CommandManager.CreateCommand(AccessCommandHandler.ID, new AccessCommandHandler());
            // Register DisplaySelectedWellsCommandHandler
            PetrelSystem.CommandManager.CreateCommand(DisplaySelectedWellsCommandHandler.ID, new DisplaySelectedWellsCommandHandler());
            // Register HelloOceanWithWizardCommandHandler
            PetrelSystem.CommandManager.CreateCommand(HelloOceanWithWizardCommandHandler.ID, new HelloOceanWithWizardCommandHandler());
            // adding HelloOceanWithoutWizardCommandHandler to system
            var helloOceanWithoutWizardCommandHandler = new HelloOceanWithoutWizardCommandHandler();
            PetrelSystem.CommandManager.CreateCommand(HelloOceanWithoutWizardCommandHandler.ID,
                helloOceanWithoutWizardCommandHandler);
        }

        /// <summary>
        /// This method runs once in the Module life. 
        /// In this method, you can do registrations of the UI related components.
        /// (eg: settingspages, treeextensions)
        /// </summary>
        public void IntegratePresentation()
        {
            PetrelSystem.ConfigurationService.AddConfiguration(Resources.PetrelConfigFile);
            //
            // Add PrintWellMDRangeCommand to context menu
            CommandItem printWellMDRangeCommandItem = new CommandItem(PrintWellMDRangeCommandHandler.ID);
            var printWellMDRangeContextMenuHandler = new SimpleCommandContextMenuHandler<Borehole>(printWellMDRangeCommandItem);
            PetrelSystem.ToolService.AddCommandContextMenuHandler(printWellMDRangeContextMenuHandler);
        }

        /// <summary>
        /// This method runs once in the Module life.
        /// right before the module is unloaded. 
        /// It usually happens when the application is closing.
        /// </summary>
        public void Disintegrate()
        {
            // TODO:  Add UICustomizationsModule.Disintegrate implementation
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            // TODO:  Add UICustomizationsModule.Dispose implementation
        }

        #endregion

    }


}