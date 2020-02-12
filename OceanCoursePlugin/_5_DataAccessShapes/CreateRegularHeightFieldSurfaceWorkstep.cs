using System;
using System.Collections.Generic;
using Slb.Ocean.Basics;
using Slb.Ocean.Core;
using Slb.Ocean.Geometry;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.DomainObject;
using Slb.Ocean.Petrel.DomainObject.Shapes;
using Slb.Ocean.Petrel.UI;
using Slb.Ocean.Petrel.Workflow;

namespace OceanCoursePlugin._5_DataAccessShapes
{
    /// <summary>
    /// This class contains all the methods and subclasses of the CreateRegularHeightFieldSurfaceWorkstep.
    /// Worksteps are displayed in the workflow editor.
    /// </summary>
    class CreateRegularHeightFieldSurfaceWorkstep : Workstep<CreateRegularHeightFieldSurfaceWorkstep.Arguments>, IExecutorSource, IAppearance, IDescriptionSource
    {
        #region Overridden Workstep methods

        /// <summary>
        /// Creates an empty Argument instance
        /// </summary>
        /// <returns>New Argument instance.</returns>

        protected override CreateRegularHeightFieldSurfaceWorkstep.Arguments CreateArgumentPackageCore(IDataSourceManager dataSourceManager)
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
                return "5884d011-f514-4502-a0f5-07efac81d1d2";
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
                var originX = arguments.X;
                var originY = arguments.Y;
                var pointsX = arguments.XPoints;
                var pointsY = arguments.YPoints;
                var spacingX = arguments.XSpacing;
                var spacingY = arguments.YSpacing;
                //
                // get coordinate reference system
                var coordinateReferenceSystem = PetrelProject.PrimaryProject.GetCoordinateReferenceSystem();
                //
                Index2 sizeIJ = new Index2(pointsX, pointsY);
                Point2 worldOrigin = new Point2(originX, originY);
                Vector2 worldSpacing = new Vector2(spacingX, spacingY);
                Angle rotation = new Angle(0);
                bool axisOrientationClockwise = true;
                bool annoationValid = true;
                //
                SpatialLatticeInfo latticeInfo = new SpatialLatticeInfo(
                    new LatticeInfo(sizeIJ, worldOrigin, worldSpacing, rotation, axisOrientationClockwise, worldOrigin,
                        worldSpacing, annoationValid), coordinateReferenceSystem);
                //
                Project project = PetrelProject.PrimaryProject;
                //
                double value = 0.0;
                //
                using (var transaction = DataManager.NewTransaction())
                {
                    transaction.Lock(project);
                    //
                    var collection = project.CreateCollection("My Collection");
                    //
                    var surface = collection.CreateRegularHeightFieldSurface("My surface", latticeInfo);
                    surface.Domain = Domain.ELEVATION_DEPTH;
                    //
                    var samples = new List<RegularHeightFieldSample>();
                    //
                    for (int i = 0; i < sizeIJ.I; i++)
                    {
                        for (int j = 0; j < sizeIJ.J; j++)
                        {
                            samples.Add(new RegularHeightFieldSample(i, j, value += 1.0));
                            PetrelLogger.InfoOutputWindow($"I = {i}; J = {j}; Value = {value}");
                        }
                    }
                    //
                    surface.Samples = samples;
                    transaction.Commit();
                }
            }
        }

        #endregion

        /// <summary>
        /// ArgumentPackage class for CreateRegularHeightFieldSurfaceWorkstep.
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

            private double x = 0;
            private double y = 0;
            private double xSpacing = 50;
            private double ySpacing = 50;
            private int xPoints = 100;
            private int yPoints = 100;

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

            public double XSpacing
            {
                internal get { return this.xSpacing; }
                set { this.xSpacing = value; }
            }

            public double YSpacing
            {
                internal get { return this.ySpacing; }
                set { this.ySpacing = value; }
            }

            public int XPoints
            {
                internal get { return this.xPoints; }
                set { this.xPoints = value; }
            }

            public int YPoints
            {
                internal get { return this.yPoints; }
                set { this.yPoints = value; }
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
        /// Gets the description of the CreateRegularHeightFieldSurfaceWorkstep
        /// </summary>
        public IDescription Description
        {
            get { return CreateRegularHeightFieldSurfaceWorkstepDescription.Instance; }
        }

        /// <summary>
        /// This singleton class contains the description of the CreateRegularHeightFieldSurfaceWorkstep.
        /// Contains Name, Shorter description and detailed description.
        /// </summary>
        public class CreateRegularHeightFieldSurfaceWorkstepDescription : IDescription
        {
            /// <summary>
            /// Contains the singleton instance.
            /// </summary>
            private  static CreateRegularHeightFieldSurfaceWorkstepDescription instance = new CreateRegularHeightFieldSurfaceWorkstepDescription();
            /// <summary>
            /// Gets the singleton instance of this Description class
            /// </summary>
            public static CreateRegularHeightFieldSurfaceWorkstepDescription Instance
            {
                get { return instance; }
            }

            #region IDescription Members

            /// <summary>
            /// Gets the name of CreateRegularHeightFieldSurfaceWorkstep
            /// </summary>
            public string Name
            {
                get { return "CreateRegularHeightFieldSurfaceWorkstep"; }
            }
            /// <summary>
            /// Gets the short description of CreateRegularHeightFieldSurfaceWorkstep
            /// </summary>
            public string ShortDescription
            {
                get { return ""; }
            }
            /// <summary>
            /// Gets the detailed description of CreateRegularHeightFieldSurfaceWorkstep
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