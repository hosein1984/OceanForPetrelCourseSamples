using System;
using System.Collections.Generic;
using Slb.Ocean.Core;
using Slb.Ocean.Geometry;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.DomainObject;
using Slb.Ocean.Petrel.UI;
using Slb.Ocean.Petrel.Workflow;
using Slb.Ocean.Petrel.DomainObject.Shapes;

namespace OceanCoursePlugin._5_DataAccessShapes
{
    /// <summary>
    /// This class contains all the methods and subclasses of the CreatePointSetWorkstep.
    /// Worksteps are displayed in the workflow editor.
    /// </summary>
    class CreatePointSetWorkstep : Workstep<CreatePointSetWorkstep.Arguments>, IExecutorSource, IAppearance, IDescriptionSource
    {
        #region Overridden Workstep methods

        /// <summary>
        /// Creates an empty Argument instance
        /// </summary>
        /// <returns>New Argument instance.</returns>

        protected override CreatePointSetWorkstep.Arguments CreateArgumentPackageCore(IDataSourceManager dataSourceManager)
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
                return "b2d15d88-0329-4513-b0eb-94a8e63d1dcc";
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
                var startX = PetrelUnitSystem.ConvertFromUI(Domain.X, arguments.X);
                var startY = PetrelUnitSystem.ConvertFromUI(Domain.Y, arguments.Y);
                var startZ = PetrelUnitSystem.ConvertFromUI(Domain.ELEVATION_DEPTH, arguments.Z);
                var spacing = PetrelUnitSystem.ConvertFromUI(Domain.ELEVATION_DEPTH, arguments.Spacing);
                var nLines = arguments.NLines;
                //
                // define default increments
                double incrementX = 500.0;
                double incrementY = 100.0;
                double incrementZ = 100.0;
                //
                // Create points
                var points = new List<Point3>();
                //
                for (int lineIndex = 0; lineIndex < nLines; lineIndex++)
                {
                    for (int pointIndex = 0; pointIndex < nLines; pointIndex++)
                    {
                        double verticalSpacing = pointIndex * spacing;
                        points.Add(new Point3(startX, startY, startZ + verticalSpacing));
                    }
                    //
                    startX += incrementX;
                    startY += incrementY;
                    startZ += incrementZ;
                }
                //
                // create empty collection
                Collection collection = Collection.NullObject;
                Project project = PetrelProject.PrimaryProject;
                //
                using (var transaction = DataManager.NewTransaction())
                {
                    transaction.Lock(project);
                    //
                    collection = project.CreateCollection("My Collection");
                    //
                    var pointSet = collection.CreatePointSet("My point set");
                    //
                    // set point set properties
                    pointSet.Domain = Domain.ELEVATION_DEPTH;
                    pointSet.Points = new Point3Set(points);
                    //
                    // update the output argument
                    arguments.PointSet = pointSet;
                    //
                    transaction.Commit();
                }
            }
        }

        #endregion

        /// <summary>
        /// ArgumentPackage class for CreatePointSetWorkstep.
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

            private double x = 453829;
            private double y = 6788392;
            private double z = -2024;
            private double spacing = 50;
            private int nLines = 5;
            private Slb.Ocean.Petrel.DomainObject.Shapes.PointSet pointSet;

            public double X
            {
                internal get { return this.x; }
                set { this.x = value; }
            }

            public double Y
            {
                internal get { return this.y; }
                set { this.y = value; }
            }

            public double Z
            {
                internal get { return this.z; }
                set { this.z = value; }
            }

            public double Spacing
            {
                internal get { return this.spacing; }
                set { this.spacing = value; }
            }

            public int NLines
            {
                internal get { return this.nLines; }
                set { this.nLines = value; }
            }

            public Slb.Ocean.Petrel.DomainObject.Shapes.PointSet PointSet
            {
                get { return this.pointSet; }
                internal set { this.pointSet = value; }
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
        /// Gets the description of the CreatePointSetWorkstep
        /// </summary>
        public IDescription Description
        {
            get { return CreatePointSetWorkstepDescription.Instance; }
        }

        /// <summary>
        /// This singleton class contains the description of the CreatePointSetWorkstep.
        /// Contains Name, Shorter description and detailed description.
        /// </summary>
        public class CreatePointSetWorkstepDescription : IDescription
        {
            /// <summary>
            /// Contains the singleton instance.
            /// </summary>
            private  static CreatePointSetWorkstepDescription instance = new CreatePointSetWorkstepDescription();
            /// <summary>
            /// Gets the singleton instance of this Description class
            /// </summary>
            public static CreatePointSetWorkstepDescription Instance
            {
                get { return instance; }
            }

            #region IDescription Members

            /// <summary>
            /// Gets the name of CreatePointSetWorkstep
            /// </summary>
            public string Name
            {
                get { return "CreatePointSetWorkstep"; }
            }
            /// <summary>
            /// Gets the short description of CreatePointSetWorkstep
            /// </summary>
            public string ShortDescription
            {
                get { return ""; }
            }
            /// <summary>
            /// Gets the detailed description of CreatePointSetWorkstep
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