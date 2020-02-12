using System;

using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.DomainObject;
using Slb.Ocean.Petrel.DomainObject.ColorTables;
using Slb.Ocean.Petrel.UI;
using Slb.Ocean.Petrel.Workflow;
using Slb.Ocean.Units;

namespace OceanCoursePlugin._4_DataAccessOverview
{
    /// <summary>
    /// This class contains all the methods and subclasses of the CreateTimeTemplateWorkstep.
    /// Worksteps are displayed in the workflow editor.
    /// </summary>
    class CreateTimeTemplateWorkstep : Workstep<CreateTimeTemplateWorkstep.Arguments>, IExecutorSource, IAppearance, IDescriptionSource
    {
        #region Overridden Workstep methods

        /// <summary>
        /// Creates an empty Argument instance
        /// </summary>
        /// <returns>New Argument instance.</returns>

        protected override CreateTimeTemplateWorkstep.Arguments CreateArgumentPackageCore(IDataSourceManager dataSourceManager)
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
                return "f5e558dd-00cf-4788-b179-3dcf4be0661d";
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
                // extract data from arguments
                var templateName = arguments.Name;
                var templatePrecision = arguments.Precision;
                var templateDisplayUnit = arguments.DisplayUnit;
                var templateLabel = arguments.Label;
                //
                // find an appropriate measurment
                var timeMeasurement = PetrelUnitSystem.GetUnitMeasurement("Time");
                //
                // find an appropriate template collection
                var timeTwoWayTemplate = PetrelProject.WellKnownTemplates.GeometricalGroup.TimeTwoWay;
                var geometricalTemplateCollection = timeTwoWayTemplate.TemplateCollection;
                //
                // find an appropriate color table
                var colorTableRoot = ColorTableRoot.Get(PetrelProject.PrimaryProject);
                var generalContinuousColorTable = colorTableRoot.WellKnownColorTables.Other.GeneralContinuous;
                //
                // find appropriate unit
                var unitServiceSettingsService = CoreSystem.GetService<IUnitServiceSettings>();
                var templateUnit = unitServiceSettingsService?.CurrentCatalog.GetUnit(templateDisplayUnit);
                //
                using (var transaction = DataManager.NewTransaction())
                {
                    transaction.Lock(geometricalTemplateCollection);
                    //
                    var uniqueTemplateName = PetrelSystem.TemplateService.GetUniqueName(templateName);
                    var canCreateTemplate = geometricalTemplateCollection.CanCreateTemplate(timeTwoWayTemplate);
                    if (canCreateTemplate)
                    {
                        var newTemplate = geometricalTemplateCollection.CreateTemplate(uniqueTemplateName, generalContinuousColorTable, timeMeasurement, templateUnit);
                        if (newTemplate != null)
                        {
                            PetrelLogger.InfoOutputWindow("Create a new template with name " + uniqueTemplateName);
                        }
                    }
                    //
                    transaction.Commit();
                }
            }
        }

        #endregion

        /// <summary>
        /// ArgumentPackage class for CreateTimeTemplateWorkstep.
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

            private int precision = 0;
            private string displayUnit = "ms";
            private string label = "Two-way time [ms]";
            private string name = "My New Time Template";

            public int Precision
            {
                internal get { return this.precision; }
                set { this.precision = value; }
            }

            public string DisplayUnit
            {
                internal get { return this.displayUnit; }
                set { this.displayUnit = value; }
            }

            public string Label
            {
                internal get { return this.label; }
                set { this.label = value; }
            }

            public string Name
            {
                internal get { return this.name; }
                set { this.name = value; }
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
        /// Gets the description of the CreateTimeTemplateWorkstep
        /// </summary>
        public IDescription Description
        {
            get { return CreateTimeTemplateWorkstepDescription.Instance; }
        }

        /// <summary>
        /// This singleton class contains the description of the CreateTimeTemplateWorkstep.
        /// Contains Name, Shorter description and detailed description.
        /// </summary>
        public class CreateTimeTemplateWorkstepDescription : IDescription
        {
            /// <summary>
            /// Contains the singleton instance.
            /// </summary>
            private  static CreateTimeTemplateWorkstepDescription instance = new CreateTimeTemplateWorkstepDescription();
            /// <summary>
            /// Gets the singleton instance of this Description class
            /// </summary>
            public static CreateTimeTemplateWorkstepDescription Instance
            {
                get { return instance; }
            }

            #region IDescription Members

            /// <summary>
            /// Gets the name of CreateTimeTemplateWorkstep
            /// </summary>
            public string Name
            {
                get { return "CreateTimeTemplateWorkstep"; }
            }
            /// <summary>
            /// Gets the short description of CreateTimeTemplateWorkstep
            /// </summary>
            public string ShortDescription
            {
                get { return ""; }
            }
            /// <summary>
            /// Gets the detailed description of CreateTimeTemplateWorkstep
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