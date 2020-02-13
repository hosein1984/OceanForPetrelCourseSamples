using System;

using Slb.Ocean.Core;
using Slb.Ocean.Geometry;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.UI;
using Slb.Ocean.Petrel.Workflow;
using Slb.Ocean.Petrel.DomainObject.Well;

namespace OceanCoursePlugin._8_DataAccessWellsAndLogs
{
    /// <summary>
    /// This class contains all the methods and subclasses of the CreateWellPathWorkstep.
    /// Worksteps are displayed in the workflow editor.
    /// </summary>
    class CreateWellPathWorkstep : Workstep<CreateWellPathWorkstep.Arguments>, IExecutorSource, IAppearance, IDescriptionSource
    {
        #region Overridden Workstep methods

        /// <summary>
        /// Creates an empty Argument instance
        /// </summary>
        /// <returns>New Argument instance.</returns>

        protected override CreateWellPathWorkstep.Arguments CreateArgumentPackageCore(IDataSourceManager dataSourceManager)
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
                return "0c095411-2a7d-4ca7-a72d-a4cf9598cc16";
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
                var boreholeCollection = arguments.WellCollection;
                var wellName = arguments.WellName;
                //
                using (var transaction = DataManager.NewTransaction())
                {
                    transaction.Lock(boreholeCollection);
                    //
                    var borehole = boreholeCollection.CreateBorehole(wellName);
                    //
                    // update borehole data
                    borehole.WellHead = new Point2(1500, 1500);
                    borehole.WorkingReferenceLevel = new ReferenceLevel("My Reference Level", 0, "My Awesome Reference Level");
                    //
                    // lock trajectory collection
                    var trajectoryCollection = borehole.Trajectory.TrajectoryCollection;
                    transaction.Lock(trajectoryCollection);
                    //
                    // update borehole trajectory data
                    var xyzTrajectory = trajectoryCollection.CreateTrajectory<XyzTrajectory>("XYZ Trajectory");
                    //
                    xyzTrajectory.Records = new[]
                    {
                        new XyzTrajectoryRecord(1500, 1500, -1000),
                        new XyzTrajectoryRecord(1500, 1500, -1200),
                        new XyzTrajectoryRecord(1500, 1500, -1500),
                        new XyzTrajectoryRecord(1500, 1500, -1600),
                        new XyzTrajectoryRecord(1500, 1500, -2000),
                    };
                    //
                    xyzTrajectory.Settings = new XyzTrajectorySettings(CalculationAlgorithmType.MinimumCurvature);
                    //
                    trajectoryCollection.DefinitiveSurvey = xyzTrajectory;
                    //
                    // update output data
                    arguments.ResultWell = borehole;
                    //
                    transaction.Commit();
                }

            }
        }

        #endregion

        /// <summary>
        /// ArgumentPackage class for CreateWellPathWorkstep.
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

            private Slb.Ocean.Petrel.DomainObject.Well.BoreholeCollection wellCollection;
            private string wellName = "Wildcat";
            private Slb.Ocean.Petrel.DomainObject.Well.Borehole resultWell;

            [Description("WellCollection", "Well Collection")]
            public Slb.Ocean.Petrel.DomainObject.Well.BoreholeCollection WellCollection
            {
                internal get { return this.wellCollection; }
                set { this.wellCollection = value; }
            }

            [Description("WellName", "Well Name")]
            public string WellName
            {
                internal get { return this.wellName; }
                set { this.wellName = value; }
            }

            [Description("ResultWell", "ResultWell")]
            public Slb.Ocean.Petrel.DomainObject.Well.Borehole ResultWell
            {
                get { return this.resultWell; }
                internal set { this.resultWell = value; }
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
        /// Gets the description of the CreateWellPathWorkstep
        /// </summary>
        public IDescription Description
        {
            get { return CreateWellPathWorkstepDescription.Instance; }
        }

        /// <summary>
        /// This singleton class contains the description of the CreateWellPathWorkstep.
        /// Contains Name, Shorter description and detailed description.
        /// </summary>
        public class CreateWellPathWorkstepDescription : IDescription
        {
            /// <summary>
            /// Contains the singleton instance.
            /// </summary>
            private  static CreateWellPathWorkstepDescription instance = new CreateWellPathWorkstepDescription();
            /// <summary>
            /// Gets the singleton instance of this Description class
            /// </summary>
            public static CreateWellPathWorkstepDescription Instance
            {
                get { return instance; }
            }

            #region IDescription Members

            /// <summary>
            /// Gets the name of CreateWellPathWorkstep
            /// </summary>
            public string Name
            {
                get { return "CreateWellPathWorkstep"; }
            }
            /// <summary>
            /// Gets the short description of CreateWellPathWorkstep
            /// </summary>
            public string ShortDescription
            {
                get { return "Create a Borehole"; }
            }
            /// <summary>
            /// Gets the detailed description of CreateWellPathWorkstep
            /// </summary>
            public string Description
            {
                get { return "Creation of borehole with trajectory records"; }
            }

            #endregion
        }
        #endregion


    }
}