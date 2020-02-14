using System;
using System.Diagnostics;
using System.Windows.Forms;
using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.UI;
using Slb.Ocean.Petrel.Workflow;

namespace OceanCoursePlugin._11_UIOverview
{
    /// <summary>
    /// This class contains all the methods and subclasses of the ProgressDemoWorkstep.
    /// Worksteps are displayed in the workflow editor.
    /// </summary>
    class ProgressDemoWorkstep : Workstep<ProgressDemoWorkstep.Arguments>, IExecutorSource, IAppearance, IDescriptionSource
    {
        #region Overridden Workstep methods

        /// <summary>
        /// Creates an empty Argument instance
        /// </summary>
        /// <returns>New Argument instance.</returns>

        protected override ProgressDemoWorkstep.Arguments CreateArgumentPackageCore(IDataSourceManager dataSourceManager)
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
                return "1b615e1c-ed49-4969-8b1d-4b05881b4086";
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
                int worksteps = arguments.Worksteps;
                //
                //
                using (var progress = PetrelLogger.NewProgress(1, worksteps, ProgressType.Default, Cursors.Cross))
                {
                    for (int i = 1; i <= worksteps; i++)
                    {
                        PetrelLogger.InfoOutputWindow("Doing work item: " + i);
                        //
                        progress.SetProgressText("Doing work item: " + i);
                        progress.ProgressStatus = i;
                        WasteTime(1000);
                    }
                }
                
            }

            private static void WasteTime(int milliSeconds)
            {
                var stopWatch = Stopwatch.StartNew();
                //
                while (stopWatch.ElapsedMilliseconds < milliSeconds)
                {
                    // do nothing
                }
            }
        }

        #endregion

        /// <summary>
        /// ArgumentPackage class for ProgressDemoWorkstep.
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

            private int worksteps = 10;

            [Description("Worksteps", "Number of Works to do")]
            public int Worksteps
            {
                internal get { return this.worksteps; }
                set { this.worksteps = value; }
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
        /// Gets the description of the ProgressDemoWorkstep
        /// </summary>
        public IDescription Description
        {
            get { return ProgressDemoWorkstepDescription.Instance; }
        }

        /// <summary>
        /// This singleton class contains the description of the ProgressDemoWorkstep.
        /// Contains Name, Shorter description and detailed description.
        /// </summary>
        public class ProgressDemoWorkstepDescription : IDescription
        {
            /// <summary>
            /// Contains the singleton instance.
            /// </summary>
            private  static ProgressDemoWorkstepDescription instance = new ProgressDemoWorkstepDescription();
            /// <summary>
            /// Gets the singleton instance of this Description class
            /// </summary>
            public static ProgressDemoWorkstepDescription Instance
            {
                get { return instance; }
            }

            #region IDescription Members

            /// <summary>
            /// Gets the name of ProgressDemoWorkstep
            /// </summary>
            public string Name
            {
                get { return "ProgressDemoWorkstep"; }
            }
            /// <summary>
            /// Gets the short description of ProgressDemoWorkstep
            /// </summary>
            public string ShortDescription
            {
                get { return ""; }
            }
            /// <summary>
            /// Gets the detailed description of ProgressDemoWorkstep
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