using System;
using Slb.Ocean.Basics;
using Slb.Ocean.Core;
using Slb.Ocean.Geometry;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.DomainObject;
using Slb.Ocean.Petrel.UI;
using Slb.Ocean.Petrel.Workflow;
using Slb.Ocean.Petrel.DomainObject.PillarGrid;

namespace OceanCoursePlugin._9_DataAccessPillarGrid
{
    /// <summary>
    /// This class contains all the methods and subclasses of the HelloGridWorkstep.
    /// Worksteps are displayed in the workflow editor.
    /// </summary>
    class HelloGridWorkstep : Workstep<HelloGridWorkstep.Arguments>, IExecutorSource, IAppearance, IDescriptionSource
    {
        #region Overridden Workstep methods

        /// <summary>
        /// Creates an empty Argument instance
        /// </summary>
        /// <returns>New Argument instance.</returns>

        protected override HelloGridWorkstep.Arguments CreateArgumentPackageCore(IDataSourceManager dataSourceManager)
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
                return "e9ba627d-71a4-4b05-8b04-df20f6b2cb01";
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
                if (grid == null)
                {
                    PetrelLogger.ErrorStatus("HelloGridWorkstep: Grid argument cannot be empty");
                    return;
                }
                //
                // get total number of cells in the grid
                Index3 numCells = grid.NumCellsIJK;
                arguments.NumCells = numCells.I * numCells.J * numCells.K;
                //
                // get cell and volume
                double px = PetrelUnitSystem.ConvertFromUI(Domain.X, arguments.PointX);
                double py = PetrelUnitSystem.ConvertFromUI(Domain.Y, arguments.PointY);
                double pz = PetrelUnitSystem.ConvertFromUI(grid.Domain, arguments.PointZ);
                //
                Point3 point = new Point3(px, py, pz);
                //
                Index3 cellIndex = grid.GetCellAtPoint(point);
                //
                arguments.CellVolume = Double.NaN;
                if (cellIndex != null && grid.HasCellVolume(cellIndex))
                {
                    arguments.CellVolume = grid.GetCellVolume(cellIndex);
                }
                //
                // get cell corners
                PetrelLogger.InfoOutputWindow("The corner points of the cell are: ");
                foreach (Point3 cellCorner in grid.GetCellCorners(cellIndex, CellCornerSet.All))
                {
                    double x = PetrelUnitSystem.ConvertToUI(Domain.X, cellCorner.X);
                    double y = PetrelUnitSystem.ConvertToUI(Domain.Y, cellCorner.Y);
                    double z = PetrelUnitSystem.ConvertToUI(grid.Domain, cellCorner.Z);
                    //
                    PetrelLogger.InfoOutputWindow($"X = {x}, Y = {y}, Z = {z}");
                }
                //
                // check to see if node is defined at (10,10,10)
                Index3 nodeIndex3 = new Index3(10,10,10);
                Direction direction = Direction.NorthEast;
                if (grid.IsNodeDefined(nodeIndex3, direction))
                {
                    PetrelLogger.InfoOutputWindow("The node is defined at 10, 10, 10 with NorthWest direction");
                }
                //
                // check to see if node is faulted at 10, 10
                Index2 nodeIndex2 = new Index2(10,10);
                if (grid.IsNodeFaulted(nodeIndex2))
                {
                    // then get faults at the given node
                    foreach (PillarFault fault in grid.GetPillarFaultsAtNode(nodeIndex2))
                    {
                        PetrelLogger.InfoOutputWindow("The fault name at node is " + fault.Description.Name);
                    }
                }
                //
                // get all the horizons in the grid and output each one's name, type, and K index
                PetrelLogger.InfoOutputWindow("\nThere are " + grid.HorizonCount + " horizons in the grid, and their names are:");
                foreach (Horizon hz in grid.Horizons)
                {
                    PetrelLogger.InfoOutputWindow(hz.Name + " is of type " + hz.HorizonType.ToString() + " and is at K index " + hz.K);
                }
                //
                // get all the zones in grid and output their names; these are hierachical
                PetrelLogger.InfoOutputWindow("\nThere are " + grid.ZoneCount + " zones in the grid, and their names are:");
                foreach (Zone z in grid.Zones)
                {
                    PetrelLogger.InfoOutputWindow(z.Name + " has a zone count of " + z.ZoneCount + " and contains these zones:");
                    foreach (Zone subZone in z.Zones)
                    {
                        PetrelLogger.InfoOutputWindow(subZone.Name);
                    }
                }
                //
                //
                PrintPillarFaultsInCollection(grid.FaultCollection);
                //
                // get all the segments in the grid and output their names and cell count
                PetrelLogger.InfoOutputWindow("\nThere are " + grid.SegmentCount + " segments in the grid, and their names are:");
                foreach (Segment seg in grid.Segments)
                {
                    PetrelLogger.InfoOutputWindow(seg.Name + " has a cell count of " + seg.CellCount);
                }
            }

            private void PrintPillarFaultsInCollection(FaultCollection faultCollection)
            {
                PetrelLogger.InfoOutputWindow("\nFault collection " + faultCollection.Name + " has a pillar fault count of " + faultCollection.PillarFaultCount);
                foreach (PillarFault fault in faultCollection.PillarFaults)
                    PetrelLogger.InfoOutputWindow(fault.Name + " has a face count of " + fault.FaceCount + " and a node count of " + fault.NodeCount);
                foreach (FaultCollection fc in faultCollection.FaultCollections)
                    PrintPillarFaultsInCollection(fc);
            }
        }

        #endregion

        /// <summary>
        /// ArgumentPackage class for HelloGridWorkstep.
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
            private double pointX = 5587.74;
            private double pointY = 6980.73;
            private double pointZ = -2538.76;
            private int numCells;
            private double cellVolume;

            public Slb.Ocean.Petrel.DomainObject.PillarGrid.Grid Grid
            {
                internal get { return this.grid; }
                set { this.grid = value; }
            }

            [Description("PointX", "Point X")]
            public double PointX
            {
                internal get { return this.pointX; }
                set { this.pointX = value; }
            }

            [Description("PointY", "Point Y")]
            public double PointY
            {
                internal get { return this.pointY; }
                set { this.pointY = value; }
            }

            [Description("PointZ", "Point Z")]
            public double PointZ
            {
                internal get { return this.pointZ; }
                set { this.pointZ = value; }
            }

            [Description("NumCells", "Number of Cells")]
            public int NumCells
            {
                get { return this.numCells; }
                internal set { this.numCells = value; }
            }

            [Description("CellVolume", "Cell Volume")]
            public double CellVolume
            {
                get { return this.cellVolume; }
                internal set { this.cellVolume = value; }
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
        /// Gets the description of the HelloGridWorkstep
        /// </summary>
        public IDescription Description
        {
            get { return HelloGridWorkstepDescription.Instance; }
        }

        /// <summary>
        /// This singleton class contains the description of the HelloGridWorkstep.
        /// Contains Name, Shorter description and detailed description.
        /// </summary>
        public class HelloGridWorkstepDescription : IDescription
        {
            /// <summary>
            /// Contains the singleton instance.
            /// </summary>
            private  static HelloGridWorkstepDescription instance = new HelloGridWorkstepDescription();
            /// <summary>
            /// Gets the singleton instance of this Description class
            /// </summary>
            public static HelloGridWorkstepDescription Instance
            {
                get { return instance; }
            }

            #region IDescription Members

            /// <summary>
            /// Gets the name of HelloGridWorkstep
            /// </summary>
            public string Name
            {
                get { return "HelloGridWorkstep"; }
            }
            /// <summary>
            /// Gets the short description of HelloGridWorkstep
            /// </summary>
            public string ShortDescription
            {
                get { return "Hello Grid"; }
            }
            /// <summary>
            /// Gets the detailed description of HelloGridWorkstep
            /// </summary>
            public string Description
            {
                get { return "Navigate a pillar grid and display information about it"; }
            }

            #endregion
        }
        #endregion


    }
}