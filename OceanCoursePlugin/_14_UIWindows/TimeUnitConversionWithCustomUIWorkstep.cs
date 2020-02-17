using System;

using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.UI;
using Slb.Ocean.Petrel.Workflow;

namespace OceanCoursePlugin._14_UIWindows
{
    /// <summary>
    /// This class contains all the methods and subclasses of the TimeUnitConversionWithCustomUIWorkstep.
    /// Worksteps are displayed in the workflow editor.
    /// </summary>
    class TimeUnitConversionWithCustomUIWorkstep : Workstep<TimeUnitConversionWithCustomUIWorkstep.Arguments>, IExecutorSource, IAppearance, IDescriptionSource
    {
        #region Overridden Workstep methods

        /// <summary>
        /// Creates an empty Argument instance
        /// </summary>
        /// <returns>New Argument instance.</returns>

        protected override TimeUnitConversionWithCustomUIWorkstep.Arguments CreateArgumentPackageCore(IDataSourceManager dataSourceManager)
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
                return "c3fdfff7-c3d3-4d1a-b186-39ac9a6bf985";
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
                // extract input time value
                var displayTimeValue = arguments.DisplayTimeValue;
                //
                // find time measurment
                var timeMeasurement = PetrelProject.WellKnownTemplates.GeometricalGroup.TimeOneWay.UnitMeasurement;
                //
                // find core and ui units
                var displayUnit = PetrelUnitSystem.GetDisplayUnit(timeMeasurement);
                var invariantUnit = PetrelUnitSystem.GetInvariantUnit(timeMeasurement);
                //
                // do the real conversion
                var invariantTimeValue = PetrelUnitSystem.ConvertFromUI(timeMeasurement, displayTimeValue);
                //
                // update the argument package
                arguments.CoreTimeValue = invariantTimeValue;
                arguments.DisplayTimeUnit = displayUnit.DisplaySymbol;
                arguments.CoreTimeUnit = invariantUnit.DisplaySymbol;
            }
        }

        #endregion

        /// <summary>
        /// ArgumentPackage class for TimeUnitConversionWithCustomUIWorkstep.
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

            private double displayTimeValue = 1500;
            private string displayTimeUnit;
            private double coreTimeValue;
            private string coreTimeUnit;

            [Description("DisplayTimeValue", "Display Time Value")]
            public double DisplayTimeValue
            {
                internal get { return this.displayTimeValue; }
                set { this.displayTimeValue = value; }
            }

            [Description("DisplayTimeUnit", "Display Time Unit")]
            public string DisplayTimeUnit
            {
                get { return this.displayTimeUnit; }
                internal set { this.displayTimeUnit = value; }
            }

            [Description("CoreTimeValue", "Core Time Value")]
            public double CoreTimeValue
            {
                get { return this.coreTimeValue; }
                internal set { this.coreTimeValue = value; }
            }

            [Description("CoreTimeUnit", "Core Time Unit")]
            public string CoreTimeUnit
            {
                get { return this.coreTimeUnit; }
                internal set { this.coreTimeUnit = value; }
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
        /// Gets the description of the TimeUnitConversionWithCustomUIWorkstep
        /// </summary>
        public IDescription Description
        {
            get { return TimeUnitConversionWithCustomUIWorkstepDescription.Instance; }
        }

        /// <summary>
        /// This singleton class contains the description of the TimeUnitConversionWithCustomUIWorkstep.
        /// Contains Name, Shorter description and detailed description.
        /// </summary>
        public class TimeUnitConversionWithCustomUIWorkstepDescription : IDescription
        {
            /// <summary>
            /// Contains the singleton instance.
            /// </summary>
            private  static TimeUnitConversionWithCustomUIWorkstepDescription instance = new TimeUnitConversionWithCustomUIWorkstepDescription();
            /// <summary>
            /// Gets the singleton instance of this Description class
            /// </summary>
            public static TimeUnitConversionWithCustomUIWorkstepDescription Instance
            {
                get { return instance; }
            }

            #region IDescription Members

            /// <summary>
            /// Gets the name of TimeUnitConversionWithCustomUIWorkstep
            /// </summary>
            public string Name
            {
                get { return "TimeUnitConversionWithCustomUIWorkstep"; }
            }
            /// <summary>
            /// Gets the short description of TimeUnitConversionWithCustomUIWorkstep
            /// </summary>
            public string ShortDescription
            {
                get { return "Converts a time value from the UI unit to Core unit"; }
            }
            /// <summary>
            /// Gets the detailed description of TimeUnitConversionWithCustomUIWorkstep
            /// </summary>
            public string Description
            {
                get { return ""; }
            }

            #endregion
        }
        #endregion

        public class UIFactory : WorkflowEditorUIFactory
        {
            /// <summary>
            /// This method creates the dialog UI for the given workstep, arguments
            /// and context.
            /// </summary>
            /// <param name="workstep">the workstep instance</param>
            /// <param name="argumentPackage">the arguments to pass to the UI</param>
            /// <param name="context">the underlying context in which the UI is being used</param>
            /// <returns>a Windows.Forms.Control to edit the argument package with</returns>
            protected override System.Windows.Forms.Control CreateDialogUICore(Workstep workstep, object argumentPackage, WorkflowContext context)
            {
                return new TimeUnitConversionWithCustomUIWorkstepUI((TimeUnitConversionWithCustomUIWorkstep)workstep, (Arguments)argumentPackage, context);
            }
        }
    }
}