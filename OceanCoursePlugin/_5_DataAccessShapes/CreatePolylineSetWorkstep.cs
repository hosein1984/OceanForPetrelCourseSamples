using System;
using System.Linq;
using OceanCoursePlugin.Extensions;
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
    /// This class contains all the methods and subclasses of the CreatePolylineSetWorkstep.
    /// Worksteps are displayed in the workflow editor.
    /// </summary>
    class CreatePolylineSetWorkstep : Workstep<CreatePolylineSetWorkstep.Arguments>, IExecutorSource, IAppearance, IDescriptionSource
    {
        #region Overridden Workstep methods

        /// <summary>
        /// Creates an empty Argument instance
        /// </summary>
        /// <returns>New Argument instance.</returns>

        protected override CreatePolylineSetWorkstep.Arguments CreateArgumentPackageCore(IDataSourceManager dataSourceManager)
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
                return "376a84bb-a62a-4133-b7b6-1c4865490fb4";
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
                PointSet pointSet = arguments.PointSet;
                var nPoints = arguments.NPoints;
                //
                // create plolyLine set
                var polyLines = pointSet.Points.Batch(nPoints).Select(points => new Polyline3(points)).ToList();
                //
                // find parent collection
                Collection collection = (Collection) pointSet.ParentCollection;
                //
                using (var transaction = DataManager.NewTransaction())
                {
                    transaction.Lock(collection);
                    //
                    var polylineSet = collection.CreatePolylineSet("My Polyline");
                    // 
                    // set polylines properties
                    polylineSet.Domain = Domain.ELEVATION_DEPTH;
                    polylineSet.Polylines = polyLines;
                    //
                    // update arguments
                    arguments.PolylineSet = polylineSet;
                    //
                    transaction.Commit();
                }
            }
        }

        #endregion

        /// <summary>
        /// ArgumentPackage class for CreatePolylineSetWorkstep.
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

            private Slb.Ocean.Petrel.DomainObject.Shapes.PointSet pointSet;
            private int nPoints = 5;
            private Slb.Ocean.Petrel.DomainObject.Shapes.PolylineSet polylineSet;

            public Slb.Ocean.Petrel.DomainObject.Shapes.PointSet PointSet
            {
                internal get { return this.pointSet; }
                set { this.pointSet = value; }
            }

            public int NPoints
            {
                internal get { return this.nPoints; }
                set { this.nPoints = value; }
            }

            public Slb.Ocean.Petrel.DomainObject.Shapes.PolylineSet PolylineSet
            {
                get { return this.polylineSet; }
                internal set { this.polylineSet = value; }
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
        /// Gets the description of the CreatePolylineSetWorkstep
        /// </summary>
        public IDescription Description
        {
            get { return CreatePolylineSetWorkstepDescription.Instance; }
        }

        /// <summary>
        /// This singleton class contains the description of the CreatePolylineSetWorkstep.
        /// Contains Name, Shorter description and detailed description.
        /// </summary>
        public class CreatePolylineSetWorkstepDescription : IDescription
        {
            /// <summary>
            /// Contains the singleton instance.
            /// </summary>
            private  static CreatePolylineSetWorkstepDescription instance = new CreatePolylineSetWorkstepDescription();
            /// <summary>
            /// Gets the singleton instance of this Description class
            /// </summary>
            public static CreatePolylineSetWorkstepDescription Instance
            {
                get { return instance; }
            }

            #region IDescription Members

            /// <summary>
            /// Gets the name of CreatePolylineSetWorkstep
            /// </summary>
            public string Name
            {
                get { return "CreatePolylineSetWorkstep"; }
            }
            /// <summary>
            /// Gets the short description of CreatePolylineSetWorkstep
            /// </summary>
            public string ShortDescription
            {
                get { return ""; }
            }
            /// <summary>
            /// Gets the detailed description of CreatePolylineSetWorkstep
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