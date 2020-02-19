using System;
using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.UI;
using Slb.Ocean.Petrel.Workflow;

namespace OceanCoursePlugin._15_UITrees
{
    /// <summary>
    /// This class will control the lifecycle of the Module.
    /// The order of the methods are the same as the calling order.
    /// </summary>
    public class UITreesModule : IModule
    {
        public UITreesModule()
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
            // TODO:  Add UITreesModule.Initialize implementation
        }

        /// <summary>
        /// This method runs once in the Module life. 
        /// In this method, you can do registrations of the not UI related components.
        /// (eg: datasource, plugin)
        /// </summary>
        public void Integrate()
        {
            // Register OceanCoursePlugin._15_UITrees.XYZOjectDataSourceFactory
            PetrelSystem.AddDataSourceFactory(OceanCoursePlugin._15_UITrees.XYZOjectDataSourceFactory.Instance);
            // Register TreeItem
            CoreSystem.Services.AddService(typeof(OceanCoursePlugin._15_UITrees.XYZObject), typeof(Slb.Ocean.Petrel.UI.INameInfoFactory), OceanCoursePlugin._15_UITrees.XYZObjectFactory.Instance);
            CoreSystem.Services.AddService(typeof(OceanCoursePlugin._15_UITrees.XYZObject), typeof(Slb.Ocean.Petrel.UI.IImageInfoFactory), OceanCoursePlugin._15_UITrees.XYZObjectFactory.Instance);
            PetrelSystem.CommandManager.CreateCommand(OceanCoursePlugin._15_UITrees.XYZObjectCommandHandler.ID, new OceanCoursePlugin._15_UITrees.XYZObjectCommandHandler());

            // TODO:  Add UITreesModule.Integrate implementation
        }

        /// <summary>
        /// This method runs once in the Module life. 
        /// In this method, you can do registrations of the UI related components.
        /// (eg: settingspages, treeextensions)
        /// </summary>
        public void IntegratePresentation()
        {
            // Add Ribbon Configuration file
            PetrelSystem.ConfigurationService.AddConfiguration(OceanCoursePlugin.Properties.Resources.OceanRibbon);
            PetrelSystem.ConfigurationService.AddConfiguration(OceanCoursePlugin.Properties.Resources.PetrelConfigFile);

            // TODO:  Add UITreesModule.IntegratePresentation implementation
        }

        /// <summary>
        /// This method runs once in the Module life.
        /// right before the module is unloaded. 
        /// It usually happens when the application is closing.
        /// </summary>
        public void Disintegrate()
        {
            // Unregister XYZOjectDataSourceFactory
            PetrelSystem.RemoveDataSourceFactory(OceanCoursePlugin._15_UITrees.XYZOjectDataSourceFactory.Instance);
            CoreSystem.Services.RemoveService(typeof(OceanCoursePlugin._15_UITrees.XYZObject), typeof(Slb.Ocean.Petrel.UI.INameInfoFactory));
            CoreSystem.Services.RemoveService(typeof(OceanCoursePlugin._15_UITrees.XYZObject), typeof(Slb.Ocean.Petrel.UI.IImageInfoFactory));
            // TODO:  Add UITreesModule.Disintegrate implementation
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            // TODO:  Add UITreesModule.Dispose implementation
        }

        #endregion

    }


}