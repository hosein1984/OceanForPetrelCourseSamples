using System;
using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.UI;
using Slb.Ocean.Petrel.Workflow;

namespace OceanCoursePlugin._6_DataAccessSeismicData
{
    /// <summary>
    /// This class contains all the methods and subclasses of the CopySeismicCubeWithReversedPolarityWorkstep.
    /// Worksteps are displayed in the workflow editor.
    /// </summary>
    class CopySeismicCubeWithReversedPolarityWorkstep : Workstep<CopySeismicCubeWithReversedPolarityWorkstep.Arguments>, IExecutorSource, IAppearance, IDescriptionSource
    {
        #region Overridden Workstep methods

        /// <summary>
        /// Creates an empty Argument instance
        /// </summary>
        /// <returns>New Argument instance.</returns>

        protected override CopySeismicCubeWithReversedPolarityWorkstep.Arguments CreateArgumentPackageCore(IDataSourceManager dataSourceManager)
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
                return "d9c5b68e-65db-4d52-9d63-17df2b7fca39";
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
                // extract input arguments
                var cube = arguments.Cube;
                var newCubeName = arguments.NewCubeName;
                //
                if (cube == null)
                {
                    PetrelLogger.InfoOutputWindow("CopySeismicCubeWithReversedPolarityWorkstep: cube cannot be null");
                    return;
                }
                //
                // 
                var seismicCollection = cube.SeismicCollection;
                //
                if (!seismicCollection.CanCreateSeismicCube(cube))
                {
                    return;
                }
                //
                //
                using (var transaction = DataManager.NewTransaction())
                {
                    transaction.Lock(seismicCollection);
                    //
                    var newCube = seismicCollection.CreateSeismicCube(cube, cube.Template);
                    newCube.Name = newCubeName;
                    //
                    //
                    int numI = cube.NumSamplesIJK.I;
                    int numJ = cube.NumSamplesIJK.J;
                    int numK = cube.NumSamplesIJK.K;
                    //
                    for (int i = 0; i < numI; i++) // inline samples
                    {
                        for (int j = 0; j < numJ; j++) // cross line samples
                        {
                            //
                            var originalTrace = cube.GetTrace(i, j);
                            var copiedTrace = newCube.GetTrace(i, j);
                            //
                            for (int k = 0; k < numK; k++)
                            {
                                copiedTrace[k] = -1.0f * originalTrace[k];
                            }
                        }
                    }
                    //
                    transaction.Commit();
                }
            }
        }

        #endregion

        /// <summary>
        /// ArgumentPackage class for CopySeismicCubeWithReversedPolarityWorkstep.
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

            private Slb.Ocean.Petrel.DomainObject.Seismic.SeismicCube cube;
            private string newCubeName = "Copied Cube";

            [Description("Cube", "Seismic Cube")]
            public Slb.Ocean.Petrel.DomainObject.Seismic.SeismicCube Cube
            {
                internal get { return this.cube; }
                set { this.cube = value; }
            }

            [Description("NewCubeName", "New Cube Name")]
            public string NewCubeName
            {
                internal get { return this.newCubeName; }
                set { this.newCubeName = value; }
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
        /// Gets the description of the CopySeismicCubeWithReversedPolarityWorkstep
        /// </summary>
        public IDescription Description
        {
            get { return CopySeismicCubeWithReversedPolarityWorkstepDescription.Instance; }
        }

        /// <summary>
        /// This singleton class contains the description of the CopySeismicCubeWithReversedPolarityWorkstep.
        /// Contains Name, Shorter description and detailed description.
        /// </summary>
        public class CopySeismicCubeWithReversedPolarityWorkstepDescription : IDescription
        {
            /// <summary>
            /// Contains the singleton instance.
            /// </summary>
            private  static CopySeismicCubeWithReversedPolarityWorkstepDescription instance = new CopySeismicCubeWithReversedPolarityWorkstepDescription();
            /// <summary>
            /// Gets the singleton instance of this Description class
            /// </summary>
            public static CopySeismicCubeWithReversedPolarityWorkstepDescription Instance
            {
                get { return instance; }
            }

            #region IDescription Members

            /// <summary>
            /// Gets the name of CopySeismicCubeWithReversedPolarityWorkstep
            /// </summary>
            public string Name
            {
                get { return "CopySeismicCubeWithReversedPolarityWorkstep"; }
            }
            /// <summary>
            /// Gets the short description of CopySeismicCubeWithReversedPolarityWorkstep
            /// </summary>
            public string ShortDescription
            {
                get { return "Copy seismic cube and its data"; }
            }
            /// <summary>
            /// Gets the detailed description of CopySeismicCubeWithReversedPolarityWorkstep
            /// </summary>
            public string Description
            {
                get { return "Copy seismic cube and its data and reverse the polarity of the data"; }
            }

            #endregion
        }
        #endregion


    }
}