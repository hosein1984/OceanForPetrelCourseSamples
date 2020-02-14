using System;
using Slb.Ocean.Basics;
using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.DomainObject;
using Slb.Ocean.Petrel.UI;
using Slb.Ocean.Petrel.Workflow;
using Slb.Ocean.Petrel.DomainObject.Seismic;

namespace OceanCoursePlugin._7_DataAccessSeismicInterpretation
{
    /// <summary>
    /// This class contains all the methods and subclasses of the SeismicInterpretationWorkstep.
    /// Worksteps are displayed in the workflow editor.
    /// </summary>
    class SeismicInterpretationWorkstep : Workstep<SeismicInterpretationWorkstep.Arguments>, IExecutorSource, IAppearance, IDescriptionSource
    {
        #region Overridden Workstep methods

        /// <summary>
        /// Creates an empty Argument instance
        /// </summary>
        /// <returns>New Argument instance.</returns>

        protected override SeismicInterpretationWorkstep.Arguments CreateArgumentPackageCore(IDataSourceManager dataSourceManager)
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
                return "52a5ebbc-698d-4b7f-8db4-790f1ed2f967";
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
                // extract input data from arguments
                var cube = arguments.Cube;
                var horizon = arguments.Horizon;
                var horizonProperty = arguments.Horizon3DProperty;
                //
                // make sure that correct input arguments are provided
                if (cube == null || horizon == null)
                {
                    PetrelLogger.ErrorStatus("SeismicInterpretationWorkstep: Arguments cannot be empty");
                    return;
                }
                //
                if (!Equals(cube.Domain, horizon.Domain))
                {
                    PetrelLogger.ErrorStatus("SeismicInterpretationWorkstep: Cube and Horizon must be in the same domain");
                    return;
                }
                //
                //
                using (var transaction = DataManager.NewTransaction())
                {
                    // create an output horizon property if the user did not supply one
                    if (horizonProperty == null)
                    {
                        transaction.Lock(horizon);
                        //
                        // create the property, use the template of the cube
                        var horizonInterpretation = horizon.GetHorizonInterpretation3D(cube.SeismicCollection);
                        //
                        if (horizonInterpretation == null)
                        {
                            PetrelLogger.ErrorStatus("HelloSeismic: Unable to get Horizon Interpretation 3D from HzInt");
                            return;
                        }
                        //
                        horizonProperty = horizonInterpretation.CreateProperty(cube.Template);
                        horizonProperty.Name = "My New Property";
                    }
                    else
                    {
                        transaction.Lock(horizonProperty);
                    }
                    //
                    // process the horizon property points
                    foreach (PointPropertyRecord record in horizonProperty)
                    {
                        record.Value = double.NaN;
                        //
                        var point = record.Geometry;
                        //
                        // find the seismic sample at the point
                        var pointIndex = cube.IndexAtPosition(point);
                        //
                        // Process the point if we have a valid seismic location.
                        // IndexAtPosition returns NaNs if the position does not map to IJK in cube
                        if (IsIndexDouble3Defined(pointIndex))
                        {
                            Index3 seismicIndex = pointIndex.ToIndex3();
                            //
                            // get the trace containing the seismic sample
                            ITrace trace = cube.GetTrace(seismicIndex.I, seismicIndex.J);
                            //
                            // set the property value to the corresponding trace sample value
                            record.Value = trace[seismicIndex.K];
                        }
                    }
                }
                // update output arguments
                arguments.Horizon3DProperty = horizonProperty;
            }

            private static bool IsIndexDouble3Defined(IndexDouble3 index)
            {
                return !double.IsNaN(index.I) &&
                       !double.IsNaN(index.J) &&
                       !double.IsNaN(index.K);
            }

        }

        #endregion

        /// <summary>
        /// ArgumentPackage class for SeismicInterpretationWorkstep.
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
            private Slb.Ocean.Petrel.DomainObject.Seismic.HorizonInterpretation horizon;
            private Slb.Ocean.Petrel.DomainObject.Seismic.HorizonProperty3D horizon3DProperty;

            [Description("Cube", "Seismic Cube to Extract Data From")]
            public Slb.Ocean.Petrel.DomainObject.Seismic.SeismicCube Cube
            {
                internal get { return this.cube; }
                set { this.cube = value; }
            }

            [Description("Horizon", "Horizon to Drive Extraction")]
            public Slb.Ocean.Petrel.DomainObject.Seismic.HorizonInterpretation Horizon
            {
                internal get { return this.horizon; }
                set { this.horizon = value; }
            }

            [Description("Horizon3DProperty", "Extracted Data Stored in Property")]
            public Slb.Ocean.Petrel.DomainObject.Seismic.HorizonProperty3D Horizon3DProperty
            {
                get { return this.horizon3DProperty; }
                set { this.horizon3DProperty = value; }
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
        /// Gets the description of the SeismicInterpretationWorkstep
        /// </summary>
        public IDescription Description
        {
            get { return SeismicInterpretationWorkstepDescription.Instance; }
        }

        /// <summary>
        /// This singleton class contains the description of the SeismicInterpretationWorkstep.
        /// Contains Name, Shorter description and detailed description.
        /// </summary>
        public class SeismicInterpretationWorkstepDescription : IDescription
        {
            /// <summary>
            /// Contains the singleton instance.
            /// </summary>
            private  static SeismicInterpretationWorkstepDescription instance = new SeismicInterpretationWorkstepDescription();
            /// <summary>
            /// Gets the singleton instance of this Description class
            /// </summary>
            public static SeismicInterpretationWorkstepDescription Instance
            {
                get { return instance; }
            }

            #region IDescription Members

            /// <summary>
            /// Gets the name of SeismicInterpretationWorkstep
            /// </summary>
            public string Name
            {
                get { return "SeismicInterpretationWorkstep"; }
            }
            /// <summary>
            /// Gets the short description of SeismicInterpretationWorkstep
            /// </summary>
            public string ShortDescription
            {
                get { return "Seismic amplitude extraction"; }
            }
            /// <summary>
            /// Gets the detailed description of SeismicInterpretationWorkstep
            /// </summary>
            public string Description
            {
                get { return "Extract seismic amplitudes along horizon"; }
            }

            #endregion
        }
        #endregion


    }
}