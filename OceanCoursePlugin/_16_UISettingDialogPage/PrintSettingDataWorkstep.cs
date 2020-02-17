using System;
using System.Collections.Generic;
using Slb.Ocean.Core;
using Slb.Ocean.Data.Hosting;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.DomainObject;
using Slb.Ocean.Petrel.UI;
using Slb.Ocean.Petrel.Workflow;

namespace OceanCoursePlugin._16_UISettingDialogPage
{
    /// <summary>
    /// This class contains all the methods and subclasses of the PrintSettingDataWorkstep.
    /// Worksteps are displayed in the workflow editor.
    /// </summary>
    class PrintSettingDataWorkstep : Workstep<PrintSettingDataWorkstep.Arguments>, IExecutorSource, IAppearance, IDescriptionSource
    {
        #region Overridden Workstep methods

        /// <summary>
        /// Creates an empty Argument instance
        /// </summary>
        /// <returns>New Argument instance.</returns>

        protected override PrintSettingDataWorkstep.Arguments CreateArgumentPackageCore(IDataSourceManager dataSourceManager)
        {
            return new Arguments(dataSourceManager);
        }
        /// <summary>
        /// Copies the Arguments instance.
        /// </summary>
        /// <param name="fromArgumentPackage">the source Arguments instance</param>
        /// <param name="toArgumentPackage">the target Arguments instance</param>
        protected override void CopyArgumentPackageCore(Arguments fromArgumentPackage, Arguments toArgumentPackage)
        {
            DescribedArgumentsHelper.Copy(fromArgumentPackage, toArgumentPackage);
        }

        /// <summary>
        /// Gets the unique identifier for this Workstep.
        /// </summary>
        protected override string UniqueIdCore
        {
            get
            {
                return "3e11c83f-a1d6-4726-8f85-27b4acce5ad8";
            }
        }
        #endregion

        #region IExecutorSource Members and Executor class

        /// <summary>
        /// Creates the Executor instance for this workstep. This class will do the work of the Workstep.
        /// </summary>
        /// <param name="argumentPackage">the argumentpackage to pass to the Executor</param>
        /// <param name="workflowRuntimeContext">the context to pass to the Executor</param>
        /// <returns>The Executor instance.</returns>
        public Slb.Ocean.Petrel.Workflow.Executor GetExecutor(object argumentPackage, WorkflowRuntimeContext workflowRuntimeContext)
        {
            return new Executor(argumentPackage as Arguments, workflowRuntimeContext);
        }

        public class Executor : Slb.Ocean.Petrel.Workflow.Executor
        {
            Arguments arguments;
            WorkflowRuntimeContext context;

            public Executor(Arguments arguments, WorkflowRuntimeContext context)
            {
                this.arguments = arguments;
                this.context = context;
            }

            public override void ExecuteSimple()
            {
                // extract domain object
                var domainObject = arguments.DomainObject;
                //
                if (domainObject == null) return;
                //
                /// Get the Info Factory services from the Core System
                INameInfoFactory nameInfoFactory = CoreSystem.GetService<INameInfoFactory>(domainObject);
                IColorInfoFactory colorInfoFactory = CoreSystem.GetService<IColorInfoFactory>(domainObject);
                ICommentInfoFactory commentInfoFactory = CoreSystem.GetService<ICommentInfoFactory>(domainObject);

                if (nameInfoFactory != null && colorInfoFactory != null && commentInfoFactory != null)
                {
                    NameInfo nameInfo = nameInfoFactory.GetNameInfo(domainObject);
                    ColorInfo colorInfo = colorInfoFactory.GetColorInfo(domainObject);
                    CommentInfo commentInfo = commentInfoFactory.GetCommentInfo(domainObject);

                    if (nameInfo != null && colorInfo != null && commentInfo != null)
                    {
                        /// Print the Borehole Domain objects settings data.
                        PetrelLogger.InfoOutputWindow(Environment.NewLine);
                        PetrelLogger.InfoOutputWindow("Settings Info Data Access for    : " + domainObject.GetType().Name);
                        PetrelLogger.InfoOutputWindow("---------------------------------------------------------------------------");
                        PetrelLogger.InfoOutputWindow(" Name                    : " + nameInfo.Name);
                        PetrelLogger.InfoOutputWindow(" Color                   : " + colorInfo.Color.ToString());
                        PetrelLogger.InfoOutputWindow(" TypeName                : " + nameInfo.TypeName);
                        PetrelLogger.InfoOutputWindow(" Comments                : " + commentInfo.Comment);
                        PetrelLogger.InfoOutputWindow(" Can change the Name     : " + nameInfo.CanChangeName.ToString());
                    }
                }

                IHistoryInfoEditor borholeHistoryEditor = HistoryService.GetHistoryInfoEditor(domainObject);
                if (borholeHistoryEditor != null)
                {
                    PetrelLogger.InfoOutputWindow(Environment.NewLine);
                    PetrelLogger.InfoOutputWindow("History Data Access for  : " + domainObject.GetType().Name);
                    PetrelLogger.InfoOutputWindow("---------------------------------------------------------------------------");

                    /// add an History Entry
                    HistoryEntry testHistoryEntry = new HistoryEntry(DateTime.Now, DateTime.Now, Environment.UserName, "Test", "Test Arguments", "Test Version");
                    borholeHistoryEditor.AddHistoryEntry(testHistoryEntry);

                    /// Update the last history record
                    borholeHistoryEditor.UpdateLastHistoryEntry(Environment.UserName, "Test", "Test Arguments", "Test Version");

                    /// access to old history                
                    foreach (HistoryEntry entry in borholeHistoryEditor.History)
                    {
                        PetrelLogger.InfoOutputWindow(" User Name               : " + entry.UserName);
                        PetrelLogger.InfoOutputWindow(" Operation               : " + entry.Operation);
                        PetrelLogger.InfoOutputWindow(" Arguments               : " + entry.Arguments);
                        PetrelLogger.InfoOutputWindow(" AppVersion              : " + entry.AppVersion);
                        PetrelLogger.InfoOutputWindow(" Begin Date              : " + entry.BeginDate.ToString());
                        PetrelLogger.InfoOutputWindow(" End Date                : " + entry.EndDate.ToString());
                        PetrelLogger.InfoOutputWindow(Environment.NewLine);
                    }
                }


                //Type type = typeof(argumentPackage.Borehole);
                if (StatisticsService.CanGetStatistics(domainObject.GetType()))
                {
                    Statistics stats = StatisticsService.GetStatistics(domainObject);
                    if (stats != null)
                    {
                        PetrelLogger.InfoOutputWindow("Statistics Data Access for   : " + domainObject.GetType().Name);
                        PetrelLogger.InfoOutputWindow("---------------------------------------------------------------------------");
                        PetrelLogger.InfoOutputWindow(Environment.NewLine);

                        PetrelLogger.InfoOutputWindow("Axis Info items : ");
                        PetrelLogger.InfoOutputWindow(Environment.NewLine);
                        foreach (AxisInfoItem axis in stats.AxisInfo)
                        {
                            PetrelLogger.InfoOutputWindow(" Axis Name               : " + axis.Name);
                            PetrelLogger.InfoOutputWindow(" Axis Min Value          : " + axis.Min);
                            PetrelLogger.InfoOutputWindow(" Axis Max Value          : " + axis.Max);
                            PetrelLogger.InfoOutputWindow(" Axis Delta Value        : " + axis.Delta);
                            PetrelLogger.InfoOutputWindow(" Axis UnitMeasurement    : " + axis.UnitMeasurement.ToString());
                            PetrelLogger.InfoOutputWindow(Environment.NewLine);
                        }

                        PetrelLogger.InfoOutputWindow("Key Value Pair items : ");
                        PetrelLogger.InfoOutputWindow(Environment.NewLine);
                        foreach (KeyValuePair<string, string> keypair in stats.Attributes)
                        {
                            PetrelLogger.InfoOutputWindow(" Key                     : " + keypair.Key);
                            PetrelLogger.InfoOutputWindow(" Value                   : " + keypair.Value);
                            PetrelLogger.InfoOutputWindow(Environment.NewLine);
                        }
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// ArgumentPackage class for PrintSettingDataWorkstep.
        /// Each public property is an argument in the package.  The name, type and
        /// input/output role are taken from the property and modified by any
        /// attributes applied.
        /// </summary>
        public class Arguments : DescribedArgumentsByReflection
        {
            public Arguments()
                : this(DataManager.DataSourceManager)
            {                
            }

            public Arguments(IDataSourceManager dataSourceManager)
            {
            }

            private IDomainObject domainObject;

            [Description("DomainObject", "Domain Object")]
            public IDomainObject DomainObject
            {
                internal get { return this.domainObject; }
                set { this.domainObject = value; }
            }


        }
    
        #region IAppearance Members
        public event EventHandler<TextChangedEventArgs> TextChanged;
        protected void RaiseTextChanged()
        {
            if (this.TextChanged != null)
                this.TextChanged(this, new TextChangedEventArgs(this));
        }

        public string Text
        {
            get { return Description.Name; }
            private set 
            {
                // TODO: implement set
                this.RaiseTextChanged();
            }
        }

        public event EventHandler<ImageChangedEventArgs> ImageChanged;
        protected void RaiseImageChanged()
        {
            if (this.ImageChanged != null)
                this.ImageChanged(this, new ImageChangedEventArgs(this));
        }

        public System.Drawing.Bitmap Image
        {
            get { return PetrelImages.Modules; }
            private set 
            {
                // TODO: implement set
                this.RaiseImageChanged();
            }
        }
        #endregion

        #region IDescriptionSource Members

        /// <summary>
        /// Gets the description of the PrintSettingDataWorkstep
        /// </summary>
        public IDescription Description
        {
            get { return PrintSettingDataWorkstepDescription.Instance; }
        }

        /// <summary>
        /// This singleton class contains the description of the PrintSettingDataWorkstep.
        /// Contains Name, Shorter description and detailed description.
        /// </summary>
        public class PrintSettingDataWorkstepDescription : IDescription
        {
            /// <summary>
            /// Contains the singleton instance.
            /// </summary>
            private  static PrintSettingDataWorkstepDescription instance = new PrintSettingDataWorkstepDescription();
            /// <summary>
            /// Gets the singleton instance of this Description class
            /// </summary>
            public static PrintSettingDataWorkstepDescription Instance
            {
                get { return instance; }
            }

            #region IDescription Members

            /// <summary>
            /// Gets the name of PrintSettingDataWorkstep
            /// </summary>
            public string Name
            {
                get { return "PrintSettingDataWorkstep"; }
            }
            /// <summary>
            /// Gets the short description of PrintSettingDataWorkstep
            /// </summary>
            public string ShortDescription
            {
                get { return "Domain Object Settings access"; }
            }
            /// <summary>
            /// Gets the detailed description of PrintSettingDataWorkstep
            /// </summary>
            public string Description
            {
                get { return "Access to Settins Info, History entries and statistics data for a Borehole domain object"; }
            }

            #endregion
        }
        #endregion


    }
}