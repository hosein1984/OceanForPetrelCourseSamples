using System;

using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.UI;
using Slb.Ocean.Petrel.Workflow;
using Slb.Ocean.Units;

namespace OceanCoursePlugin._1_CoreAndServices
{
    /// <summary>
    /// This class contains all the methods and subclasses of the HelloWorldWorkstep.
    /// Worksteps are displayed in the workflow editor.
    /// </summary>
    class HelloWorldWorkstep : Workstep<HelloWorldWorkstep.Arguments>, IExecutorSource, IAppearance, IDescriptionSource
    {
        #region Overridden Workstep methods

        /// <summary>
        /// Creates an empty Argument instance
        /// </summary>
        /// <returns>New Argument instance.</returns>

        protected override HelloWorldWorkstep.Arguments CreateArgumentPackageCore(IDataSourceManager dataSourceManager)
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
                return "86f93521-fd26-4ec0-9f33-93c7471865ed";
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
                CoreLogger.Debug("Hello World from CoreLogger!");
                PetrelLogger.InfoOutputWindow("Hello World from PetrelLogger!");
                //
                //
                var unitServiceSettings = CoreSystem.GetService<IUnitServiceSettings>();
                // even if we do not already set a coordinate reference system, this method call will pop up the window so we could select one
                var coordinateReferenceSystemName = PetrelProject.PrimaryProject.GetCoordinateReferenceSystem().Name;
                var coordinateReferenceSystemDescription = PetrelProject.PrimaryProject.GetCoordinateReferenceSystem().Description;
                //
                PetrelLogger.InfoOutputWindow(unitServiceSettings.CurrentUISystem.Name);
                PetrelLogger.InfoOutputWindow(coordinateReferenceSystemName);
                PetrelLogger.InfoOutputWindow(coordinateReferenceSystemDescription);
            }
        }

        #endregion

        /// <summary>
        /// ArgumentPackage class for HelloWorldWorkstep.
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
        /// Gets the description of the HelloWorldWorkstep
        /// </summary>
        public IDescription Description
        {
            get { return HelloWorldWorkstepDescription.Instance; }
        }

        /// <summary>
        /// This singleton class contains the description of the HelloWorldWorkstep.
        /// Contains Name, Shorter description and detailed description.
        /// </summary>
        public class HelloWorldWorkstepDescription : IDescription
        {
            /// <summary>
            /// Contains the singleton instance.
            /// </summary>
            private  static HelloWorldWorkstepDescription instance = new HelloWorldWorkstepDescription();
            /// <summary>
            /// Gets the singleton instance of this Description class
            /// </summary>
            public static HelloWorldWorkstepDescription Instance
            {
                get { return instance; }
            }

            #region IDescription Members

            /// <summary>
            /// Gets the name of HelloWorldWorkstep
            /// </summary>
            public string Name
            {
                get { return "HelloWorldWorkstep"; }
            }
            /// <summary>
            /// Gets the short description of HelloWorldWorkstep
            /// </summary>
            public string ShortDescription
            {
                get { return "Short description of the hello world workstep"; }
            }
            /// <summary>
            /// Gets the detailed description of HelloWorldWorkstep
            /// </summary>
            public string Description
            {
                get { return "Long description of the hello world workstep"; }
            }

            #endregion
        }
        #endregion


    }
}