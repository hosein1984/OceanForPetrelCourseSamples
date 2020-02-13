using System;
using System.Linq;
using System.Security.Principal;
using Slb.Ocean.Basics;
using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.DomainObject;
using Slb.Ocean.Petrel.UI;
using Slb.Ocean.Petrel.Workflow;
using Slb.Ocean.Petrel.DomainObject.PillarGrid;

namespace OceanCoursePlugin._9_DataAccessPillarGrid
{
    /// <summary>
    /// This class contains all the methods and subclasses of the CreateDistanceAboveContactPropertyWorkstep.
    /// Worksteps are displayed in the workflow editor.
    /// </summary>
    class CreateDistanceAboveContactPropertyWorkstep : Workstep<CreateDistanceAboveContactPropertyWorkstep.Arguments>, IExecutorSource, IAppearance, IDescriptionSource
    {
        #region Overridden Workstep methods

        /// <summary>
        /// Creates an empty Argument instance
        /// </summary>
        /// <returns>New Argument instance.</returns>

        protected override CreateDistanceAboveContactPropertyWorkstep.Arguments CreateArgumentPackageCore(IDataSourceManager dataSourceManager)
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
                return "e936cfe6-82e7-4bad-b295-488a5f9782d3";
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
                var grid = arguments.Grid;
                var contactDepth = arguments.ContactDepth;
                //
                if (grid == null)
                {
                    PetrelLogger.ErrorStatus("CreateDistanceAboveContactPropertyWorkstep: Must provide input grid");
                    return;
                }
                //
                PropertyCollection properties = grid.PropertyCollection;
                PropertyCollection oceanProperties = FindOrCreatePropertyCollection("Ocean Properties", grid); ;
                Property property = Property.NullObject;
                Template aboveContactTemplate = PetrelProject.WellKnownTemplates.GeometricalGroup.AboveContact;
                // 
                using (var transaction = DataManager.NewTransaction())
                {
                    transaction.Lock(oceanProperties);
                    //
                    property = oceanProperties.CreateProperty(aboveContactTemplate);
                    property.Name = "Above Contact " + contactDepth;
                    //
                    // we are computing the difference from the constant contact and the grid.Z value
                    double constantDepth = PetrelUnitSystem.ConvertFromUI(grid.Domain, contactDepth);
                    //
                    // set data on the property for all the cells in the grid
                    double cellCenterDepth = double.NaN;
                    double propertyValue = double.NaN;
                    Index3 currentCell = new Index3();
                    //
                    for (int i = 0; i < grid.NumCellsIJK.I; i++)
                    {
                        for (int j = 0; j < grid.NumCellsIJK.J; j++)
                        {
                            for (int k = 0; k < grid.NumCellsIJK.K; k++)
                            {
                                currentCell.I = i;
                                currentCell.J = j;
                                currentCell.K = k;
                                //
                                // if the cell is defined and has volume, set property value
                                if (grid.IsCellDefined(currentCell) && grid.HasCellVolume(currentCell))
                                {
                                    // get cell center depth, Z value
                                    cellCenterDepth = grid.GetCellCenter(currentCell).Z;
                                    //
                                    // if the cell center is above the constant depth, set the property value as the difference
                                    propertyValue = cellCenterDepth > constantDepth
                                        ? cellCenterDepth - constantDepth
                                        : 0.0;
                                    //
                                    property[currentCell] = (float) propertyValue;
                                }

                            }
                        }
                    }
                    //
                    transaction.Commit();
                }
                //
                // update output arguments
                arguments.AboveContact = property;
            }

            private static PropertyCollection FindOrCreatePropertyCollection(string propertyCollectionName, Grid grid)
            {
                PropertyCollection propertyCollection = PropertyCollection.NullObject;
                //
                // try to find the property collection if it already exists
                propertyCollection = grid.PropertyCollection.PropertyCollections.FirstOrDefault(pc =>
                    pc.Name.Equals(propertyCollectionName));
                //
                if (propertyCollection == PropertyCollection.NullObject) // otherwise create it
                {
                    using (var transaction = DataManager.NewTransaction())
                    {
                        transaction.Lock(grid.PropertyCollection);
                        //
                        propertyCollection = grid.PropertyCollection.CreatePropertyCollection(propertyCollectionName);
                        //
                        transaction.Commit();
                    }
                }
                //
                return propertyCollection;
            }
        }

        #endregion

        /// <summary>
        /// ArgumentPackage class for CreateDistanceAboveContactPropertyWorkstep.
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

            private Slb.Ocean.Petrel.DomainObject.PillarGrid.Grid grid;
            private double contactDepth = -2000;
            private Slb.Ocean.Petrel.DomainObject.PillarGrid.Property aboveContact;

            public Slb.Ocean.Petrel.DomainObject.PillarGrid.Grid Grid
            {
                internal get { return this.grid; }
                set { this.grid = value; }
            }

            [Description("ContactDepth", "Contact Depth")]
            public double ContactDepth
            {
                internal get { return this.contactDepth; }
                set { this.contactDepth = value; }
            }

            [Description("AboveContact", "Above Contact")]
            public Slb.Ocean.Petrel.DomainObject.PillarGrid.Property AboveContact
            {
                get { return this.aboveContact; }
                internal set { this.aboveContact = value; }
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
        /// Gets the description of the CreateDistanceAboveContactPropertyWorkstep
        /// </summary>
        public IDescription Description
        {
            get { return CreateDistanceAboveContactPropertyWorkstepDescription.Instance; }
        }

        /// <summary>
        /// This singleton class contains the description of the CreateDistanceAboveContactPropertyWorkstep.
        /// Contains Name, Shorter description and detailed description.
        /// </summary>
        public class CreateDistanceAboveContactPropertyWorkstepDescription : IDescription
        {
            /// <summary>
            /// Contains the singleton instance.
            /// </summary>
            private  static CreateDistanceAboveContactPropertyWorkstepDescription instance = new CreateDistanceAboveContactPropertyWorkstepDescription();
            /// <summary>
            /// Gets the singleton instance of this Description class
            /// </summary>
            public static CreateDistanceAboveContactPropertyWorkstepDescription Instance
            {
                get { return instance; }
            }

            #region IDescription Members

            /// <summary>
            /// Gets the name of CreateDistanceAboveContactPropertyWorkstep
            /// </summary>
            public string Name
            {
                get { return "CreateDistanceAboveContactPropertyWorkstep"; }
            }
            /// <summary>
            /// Gets the short description of CreateDistanceAboveContactPropertyWorkstep
            /// </summary>
            public string ShortDescription
            {
                get { return "Show distance above constant contact"; }
            }
            /// <summary>
            /// Gets the detailed description of CreateDistanceAboveContactPropertyWorkstep
            /// </summary>
            public string Description
            {
                get { return "Creates a property showing distance above a constant contact"; }
            }

            #endregion
        }
        #endregion


    }
}