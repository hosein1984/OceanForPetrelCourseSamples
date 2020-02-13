using System;
using System.Collections.Generic;
using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.DomainObject;
using Slb.Ocean.Petrel.UI;
using Slb.Ocean.Petrel.Workflow;
using Slb.Ocean.Petrel.DomainObject.Well;

namespace OceanCoursePlugin._8_DataAccessWellsAndLogs
{
    /// <summary>
    /// This class contains all the methods and subclasses of the CopyWellLogWithMultiplier.
    /// Worksteps are displayed in the workflow editor.
    /// </summary>
    class CopyWellLogWithMultiplier : Workstep<CopyWellLogWithMultiplier.Arguments>, IExecutorSource, IAppearance, IDescriptionSource
    {
        #region Overridden Workstep methods

        /// <summary>
        /// Creates an empty Argument instance
        /// </summary>
        /// <returns>New Argument instance.</returns>

        protected override CopyWellLogWithMultiplier.Arguments CreateArgumentPackageCore(IDataSourceManager dataSourceManager)
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
                return "e1e0c109-ac6f-4e45-8bb6-585fc2ade758";
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
                // extract input data
                var originalWellLog = arguments.InputWellLog;
                var multiplier = arguments.Multiplier;
                //
                // create a well log version for the new well log 
                var wellLogVersion = CreateWellLogVersionFromWellLog(originalWellLog);
                //
                var newWellLog = CreateWellLog(originalWellLog, multiplier, wellLogVersion);
                //
                // update output arguments
                arguments.ResultWellLog = newWellLog;
            }

            private WellLog CreateWellLog(WellLog originalWellLog, double multiplier, WellLogVersion wellLogVersion)
            {
                //
                WellLog wellLog = WellLog.NullObject;
                //
                var borehole = originalWellLog.Borehole;
                //
                using (var transaction = DataManager.NewTransaction())
                {
                    transaction.Lock(borehole);
                    //
                    wellLog = borehole.Logs.CreateWellLog(wellLogVersion);
                    //
                    var wellLogSamples = new List<WellLogSample>();
                    foreach (var wellLogSample in originalWellLog.Samples)
                    {
                        wellLogSamples.Add(new WellLogSample(wellLogSample.MD, (float) (multiplier * wellLogSample.Value)));
                    }
                    //
                    wellLog.Samples = wellLogSamples.ToArray();
                    //
                    transaction.Commit();
                }
                //
                return wellLog;
            }

            public static WellLogVersion CreateWellLogVersionFromWellLog(WellLog wellLog)
            {
                WellLogVersion wellLogVersion = WellLogVersion.NullObject;
                //
                Borehole borehole = wellLog.Borehole;
                //
                Template wellLogTemplate = wellLog.WellLogVersion.Template;
                //
                WellRoot wellRoot = WellRoot.Get(PetrelProject.PrimaryProject);
                //
                var logVersionCollection = wellRoot.LogVersionCollection;
                //
                if (!borehole.Logs.CanCreateWellLog(wellLog.WellLogVersion))
                {
                    using (var transaction = DataManager.NewTransaction())
                    {
                        transaction.Lock(logVersionCollection);
                        //
                        wellLogVersion = logVersionCollection.CreateWellLogVersion("Copied " + wellLog.WellLogVersion.Name,
                            wellLogTemplate);
                        //
                        transaction.Commit();
                    }
                }
                //
                return wellLogVersion;
            }
        }

        #endregion

        /// <summary>
        /// ArgumentPackage class for CopyWellLogWithMultiplier.
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

            private Slb.Ocean.Petrel.DomainObject.Well.WellLog inputWellLog;
            private double multiplier = 1.0;
            private Slb.Ocean.Petrel.DomainObject.Well.WellLog resultWellLog;

            [Description("InputWellLog", "Input Well Log")]
            public Slb.Ocean.Petrel.DomainObject.Well.WellLog InputWellLog
            {
                internal get { return this.inputWellLog; }
                set { this.inputWellLog = value; }
            }

            public double Multiplier
            {
                internal get { return this.multiplier; }
                set { this.multiplier = value; }
            }

            [Description("ResultWellLog", "Result Well Log")]
            public Slb.Ocean.Petrel.DomainObject.Well.WellLog ResultWellLog
            {
                get { return this.resultWellLog; }
                internal set { this.resultWellLog = value; }
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
        /// Gets the description of the CopyWellLogWithMultiplier
        /// </summary>
        public IDescription Description
        {
            get { return CopyWellLogWithMultiplierDescription.Instance; }
        }

        /// <summary>
        /// This singleton class contains the description of the CopyWellLogWithMultiplier.
        /// Contains Name, Shorter description and detailed description.
        /// </summary>
        public class CopyWellLogWithMultiplierDescription : IDescription
        {
            /// <summary>
            /// Contains the singleton instance.
            /// </summary>
            private  static CopyWellLogWithMultiplierDescription instance = new CopyWellLogWithMultiplierDescription();
            /// <summary>
            /// Gets the singleton instance of this Description class
            /// </summary>
            public static CopyWellLogWithMultiplierDescription Instance
            {
                get { return instance; }
            }

            #region IDescription Members

            /// <summary>
            /// Gets the name of CopyWellLogWithMultiplier
            /// </summary>
            public string Name
            {
                get { return "CopyWellLogWithMultiplier"; }
            }
            /// <summary>
            /// Gets the short description of CopyWellLogWithMultiplier
            /// </summary>
            public string ShortDescription
            {
                get { return ""; }
            }
            /// <summary>
            /// Gets the detailed description of CopyWellLogWithMultiplier
            /// </summary>
            public string Description
            {
                get { return ""; }
            }

            #endregion
        }
        #endregion


    }
}