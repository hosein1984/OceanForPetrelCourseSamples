using System;
using System.Collections.Generic;
using Slb.Ocean.Basics;
using Slb.Ocean.Core;
using Slb.Ocean.Geometry;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.DomainObject;
using Slb.Ocean.Petrel.UI;
using Slb.Ocean.Petrel.Workflow;
using Slb.Ocean.Petrel.DomainObject.Seismic;

namespace OceanCoursePlugin._6_DataAccessSeismicData
{
    /// <summary>
    /// This class contains all the methods and subclasses of the PrintSeismicCubeCornersWorkstep.
    /// Worksteps are displayed in the workflow editor.
    /// </summary>
    class PrintSeismicCubeCornersWorkstep : Workstep<PrintSeismicCubeCornersWorkstep.Arguments>, IExecutorSource, IAppearance, IDescriptionSource
    {
        #region Overridden Workstep methods

        /// <summary>
        /// Creates an empty Argument instance
        /// </summary>
        /// <returns>New Argument instance.</returns>

        protected override PrintSeismicCubeCornersWorkstep.Arguments CreateArgumentPackageCore(IDataSourceManager dataSourceManager)
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
                return "0a5f7645-82fa-4e31-8848-0ebdc35e27b0";
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
                var cube = arguments.Cube;
                //
                if (cube == null)
                {
                    PetrelLogger.ErrorStatus("PrintSeismicCubeCornersWorkstep: cube cannot be null");
                    return;
                }
                //
                // extract max i, j and k values 
                int maxI = cube.NumSamplesIJK.I - 1;
                int maxJ = cube.NumSamplesIJK.J - 1;
                int maxK = cube.NumSamplesIJK.K - 1;
                //
                // create positions indecies
                List<Index3> corners = new List<Index3>()
                {
                    new Index3(0, 0, 0),
                    new Index3(0, maxJ, 0),
                    new Index3(maxI, maxJ, 0),
                    new Index3(maxI, 0, 0),
                    new Index3(0, 0, maxK),
                    new Index3(0, maxJ, maxK),
                    new Index3(maxI, maxJ, maxK),
                    new Index3(maxI, 0, maxK),
                };
                //
                //
                var cornerPoints = new List<Point3>();
                var uiCornerPoints = new List<Point3>();
                //
                foreach (var corner in corners)
                {
                    var cornerPoint = cube.PositionAtIndex(corner.ToIndexDouble3());
                    //
                    // convert to UI
                    var x = PetrelUnitSystem.ConvertToUI(Domain.X, cornerPoint.X);
                    var y = PetrelUnitSystem.ConvertToUI(Domain.Y, cornerPoint.Y);
                    var z = PetrelUnitSystem.ConvertToUI(cube.Domain, cornerPoint.Z);
                    //
                    cornerPoints.Add(cornerPoint);
                    uiCornerPoints.Add(new Point3(x, y, z));
                }
                //
                for (var index = 0; index < corners.Count; index++)
                {
                    var corner = corners[index];
                    var cornerPoint = uiCornerPoints[index];
                    //
                    PetrelLogger.InfoOutputWindow($"Index: {corner}");
                    PetrelLogger.InfoOutputWindow($"\tPosition: {cornerPoint}");
                }
                //
                // create pointset for the corner points
                var project = PetrelProject.PrimaryProject;
                Collection collection = Collection.NullObject;
                //
                // 
                using (var transaction = DataManager.NewTransaction())
                {
                    transaction.Lock(project);
                    //
                    collection = project.CreateCollection("My Collection");
                    //
                    var pointSet = collection.CreatePointSet("My Seismic Corners");
                    //
                    pointSet.Domain = cube.Domain;
                    //
                    pointSet.Points = new Point3Set(cornerPoints);
                    //
                    transaction.Commit();
                }
            }
        }

        #endregion

        /// <summary>
        /// ArgumentPackage class for PrintSeismicCubeCornersWorkstep.
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

            private Slb.Ocean.Petrel.DomainObject.Seismic.SeismicCube cube;

            [Description("Cube", "Seismic Cube")]
            public Slb.Ocean.Petrel.DomainObject.Seismic.SeismicCube Cube
            {
                internal get { return this.cube; }
                set { this.cube = value; }
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
        /// Gets the description of the PrintSeismicCubeCornersWorkstep
        /// </summary>
        public IDescription Description
        {
            get { return PrintSeismicCubeCornersWorkstepDescription.Instance; }
        }

        /// <summary>
        /// This singleton class contains the description of the PrintSeismicCubeCornersWorkstep.
        /// Contains Name, Shorter description and detailed description.
        /// </summary>
        public class PrintSeismicCubeCornersWorkstepDescription : IDescription
        {
            /// <summary>
            /// Contains the singleton instance.
            /// </summary>
            private  static PrintSeismicCubeCornersWorkstepDescription instance = new PrintSeismicCubeCornersWorkstepDescription();
            /// <summary>
            /// Gets the singleton instance of this Description class
            /// </summary>
            public static PrintSeismicCubeCornersWorkstepDescription Instance
            {
                get { return instance; }
            }

            #region IDescription Members

            /// <summary>
            /// Gets the name of PrintSeismicCubeCornersWorkstep
            /// </summary>
            public string Name
            {
                get { return "PrintSeismicCubeCornersWorkstep"; }
            }
            /// <summary>
            /// Gets the short description of PrintSeismicCubeCornersWorkstep
            /// </summary>
            public string ShortDescription
            {
                get { return "Print corners of a seismic cube"; }
            }
            /// <summary>
            /// Gets the detailed description of PrintSeismicCubeCornersWorkstep
            /// </summary>
            public string Description
            {
                get { return "Print corners of a seismic cube and use points to show them in 3D"; }
            }

            #endregion
        }
        #endregion


    }
}