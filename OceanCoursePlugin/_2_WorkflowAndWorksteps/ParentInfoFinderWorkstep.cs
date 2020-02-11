using System;

using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.Basics;
using Slb.Ocean.Petrel.UI;
using Slb.Ocean.Petrel.Workflow;

namespace OceanCoursePlugin._2_WorkflowAndWorksteps
{
    /// <summary>
    /// This class contains all the methods and subclasses of the ParentInfoFinderWorkstep.
    /// Worksteps are displayed in the workflow editor.
    /// </summary>
    class ParentInfoFinderWorkstep : Workstep<ParentInfoFinderWorkstep.Arguments>, IExecutorSource, IAppearance, IDescriptionSource
    {
        #region Overridden Workstep methods

        /// <summary>
        /// Creates an empty Argument instance
        /// </summary>
        /// <returns>New Argument instance.</returns>

        protected override ParentInfoFinderWorkstep.Arguments CreateArgumentPackageCore(IDataSourceManager dataSourceManager)
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
                return "36b0523b-6c2a-4ce8-ae38-7914e110a5a9";
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
                PetrelLogger.InfoOutputWindow($"Executing {typeof(ParentInfoFinderWorkstep).Name}");
                //
                object petrelObject = arguments.PetrelTreeObject;
                var parentSourceFactory = CoreSystem.GetService<IParentSourceFactory>(petrelObject);
                //
                if (parentSourceFactory != null)
                {
                    var parentSource = parentSourceFactory.GetParentSource(petrelObject);
                    var parentObject = parentSource.Parent;
                    //
                    var nameInfoFactory = CoreSystem.GetService<INameInfoFactory>(parentObject);
                    //
                    if (nameInfoFactory != null)
                    {
                        var parentNameInfo = nameInfoFactory.GetNameInfo(parentObject);
                        PetrelLogger.InfoOutputWindow($"Parent Info are: Name = {parentNameInfo.Name}; DisplayName = {parentNameInfo.DisplayName}; TypeName = {parentNameInfo.TypeName}");
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// ArgumentPackage class for ParentInfoFinderWorkstep.
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

            private Object petrelTreeObject;

            public Object PetrelTreeObject
            {
                internal get { return this.petrelTreeObject; }
                set { this.petrelTreeObject = value; }
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
        /// Gets the description of the ParentInfoFinderWorkstep
        /// </summary>
        public IDescription Description
        {
            get { return ParentInfoFinderWorkstepDescription.Instance; }
        }

        /// <summary>
        /// This singleton class contains the description of the ParentInfoFinderWorkstep.
        /// Contains Name, Shorter description and detailed description.
        /// </summary>
        public class ParentInfoFinderWorkstepDescription : IDescription
        {
            /// <summary>
            /// Contains the singleton instance.
            /// </summary>
            private  static ParentInfoFinderWorkstepDescription instance = new ParentInfoFinderWorkstepDescription();
            /// <summary>
            /// Gets the singleton instance of this Description class
            /// </summary>
            public static ParentInfoFinderWorkstepDescription Instance
            {
                get { return instance; }
            }

            #region IDescription Members

            /// <summary>
            /// Gets the name of ParentInfoFinderWorkstep
            /// </summary>
            public string Name
            {
                get { return "ParentInfoFinderWorkstep"; }
            }
            /// <summary>
            /// Gets the short description of ParentInfoFinderWorkstep
            /// </summary>
            public string ShortDescription
            {
                get { return "Parent Info Finder Worstep short description"; }
            }
            /// <summary>
            /// Gets the detailed description of ParentInfoFinderWorkstep
            /// </summary>
            public string Description
            {
                get { return "Parent Info Finder Worstep long description"; }
            }

            #endregion
        }
        #endregion


    }
}