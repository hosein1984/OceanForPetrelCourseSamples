using System;

using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.UI;
using Slb.Ocean.Petrel.Workflow;
using Slb.Ocean.Petrel.DomainObject.Simulation;
using Slb.Ocean.Petrel.Simulation;

namespace OceanCoursePlugin._10_DataAccessSimulation
{
    /// <summary>
    /// This class contains all the methods and subclasses of the RunCaseWorkstep.
    /// Worksteps are displayed in the workflow editor.
    /// </summary>
    class RunCaseWorkstep : Workstep<RunCaseWorkstep.Arguments>, IExecutorSource, IAppearance, IDescriptionSource
    {
        #region Overridden Workstep methods

        /// <summary>
        /// Creates an empty Argument instance
        /// </summary>
        /// <returns>New Argument instance.</returns>

        protected override RunCaseWorkstep.Arguments CreateArgumentPackageCore(IDataSourceManager dataSourceManager)
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
                return "c726e384-2322-4ae6-b703-35f4a9b95a05";
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
                // extract input argument
                var simulationCase = arguments.SimulationCase;
                if (simulationCase == null)
                {
                    PetrelLogger.ErrorStatus("RunCaseWorkstep: simulation case cannot be null");
                    return;
                }
                //
                // get case runner for the case
                var caseRunner = SimulationSystem.GetCaseRunner(simulationCase);
                //
                // export and run the case
                caseRunner.Export();
                caseRunner.Run();
            }
        }

        #endregion

        /// <summary>
        /// ArgumentPackage class for RunCaseWorkstep.
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

            private Slb.Ocean.Petrel.DomainObject.Simulation.Case simulationCase;

            [Description("SimulationCase", "Input Simulation Case to Run")]
            public Slb.Ocean.Petrel.DomainObject.Simulation.Case SimulationCase
            {
                internal get { return this.simulationCase; }
                set { this.simulationCase = value; }
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
        /// Gets the description of the RunCaseWorkstep
        /// </summary>
        public IDescription Description
        {
            get { return RunCaseWorkstepDescription.Instance; }
        }

        /// <summary>
        /// This singleton class contains the description of the RunCaseWorkstep.
        /// Contains Name, Shorter description and detailed description.
        /// </summary>
        public class RunCaseWorkstepDescription : IDescription
        {
            /// <summary>
            /// Contains the singleton instance.
            /// </summary>
            private  static RunCaseWorkstepDescription instance = new RunCaseWorkstepDescription();
            /// <summary>
            /// Gets the singleton instance of this Description class
            /// </summary>
            public static RunCaseWorkstepDescription Instance
            {
                get { return instance; }
            }

            #region IDescription Members

            /// <summary>
            /// Gets the name of RunCaseWorkstep
            /// </summary>
            public string Name
            {
                get { return "RunCaseWorkstep"; }
            }
            /// <summary>
            /// Gets the short description of RunCaseWorkstep
            /// </summary>
            public string ShortDescription
            {
                get { return "Run a case"; }
            }
            /// <summary>
            /// Gets the detailed description of RunCaseWorkstep
            /// </summary>
            public string Description
            {
                get { return "Start a simulator run on the case"; }
            }

            #endregion
        }
        #endregion


    }
}