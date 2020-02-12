using System;

using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.UI;
using Slb.Ocean.Petrel.Workflow;

namespace OceanCoursePlugin._4_DataAccessOverview
{
    /// <summary>
    /// This class contains all the methods and subclasses of the TimeUnitConvertorWorkstep.
    /// Worksteps are displayed in the workflow editor.
    /// </summary>
    class TimeUnitConvertorWorkstep : Workstep<TimeUnitConvertorWorkstep.Arguments>, IExecutorSource, IAppearance, IDescriptionSource
    {
        #region Overridden Workstep methods

        /// <summary>
        /// Creates an empty Argument instance
        /// </summary>
        /// <returns>New Argument instance.</returns>

        protected override TimeUnitConvertorWorkstep.Arguments CreateArgumentPackageCore(IDataSourceManager dataSourceManager)
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
                return "787697a3-66a6-480f-b8bb-13074d414414";
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
                var displayTimeValue = arguments.DisplayUnitValue;
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
                arguments.SIUnitValue = invariantTimeValue;
                arguments.DisplayUnit = displayUnit.DisplaySymbol;
                arguments.SIUnit = invariantUnit.DisplaySymbol;
            }
        }

        #endregion

        /// <summary>
        /// ArgumentPackage class for TimeUnitConvertorWorkstep.
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

            private double displayUnitValue = 0;
            private double sIUnitValue;
            private string displayUnit;
            private string sIUnit;

            public double DisplayUnitValue
            {
                internal get { return this.displayUnitValue; }
                set { this.displayUnitValue = value; }
            }

            public double SIUnitValue
            {
                get { return this.sIUnitValue; }
                internal set { this.sIUnitValue = value; }
            }

            public string DisplayUnit
            {
                get { return this.displayUnit; }
                internal set { this.displayUnit = value; }
            }

            public string SIUnit
            {
                get { return this.sIUnit; }
                internal set { this.sIUnit = value; }
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
        /// Gets the description of the TimeUnitConvertorWorkstep
        /// </summary>
        public IDescription Description
        {
            get { return TimeUnitConvertorWorkstepDescription.Instance; }
        }

        /// <summary>
        /// This singleton class contains the description of the TimeUnitConvertorWorkstep.
        /// Contains Name, Shorter description and detailed description.
        /// </summary>
        public class TimeUnitConvertorWorkstepDescription : IDescription
        {
            /// <summary>
            /// Contains the singleton instance.
            /// </summary>
            private  static TimeUnitConvertorWorkstepDescription instance = new TimeUnitConvertorWorkstepDescription();
            /// <summary>
            /// Gets the singleton instance of this Description class
            /// </summary>
            public static TimeUnitConvertorWorkstepDescription Instance
            {
                get { return instance; }
            }

            #region IDescription Members

            /// <summary>
            /// Gets the name of TimeUnitConvertorWorkstep
            /// </summary>
            public string Name
            {
                get { return "TimeUnitConvertorWorkstep"; }
            }
            /// <summary>
            /// Gets the short description of TimeUnitConvertorWorkstep
            /// </summary>
            public string ShortDescription
            {
                get { return "Use PetrelUnitSystem to convert time data from one unit to another"; }
            }
            /// <summary>
            /// Gets the detailed description of TimeUnitConvertorWorkstep
            /// </summary>
            public string Description
            {
                get { return "Use PetrelUnitSystem to convert time data from one unit to another, output the Display unit and SI unit"; }
            }

            #endregion
        }
        #endregion


    }
}