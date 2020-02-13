using System;
using System.Collections.Generic;
using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.UI;
using Slb.Ocean.Petrel.Workflow;
using Slb.Ocean.Petrel.DomainObject.Shapes;

namespace OceanCoursePlugin._5_DataAccessShapes
{
    /// <summary>
    /// This class contains all the methods and subclasses of the AddAzimuthPropertyToPointSet.
    /// Worksteps are displayed in the workflow editor.
    /// </summary>
    class AddAzimuthPropertyToPointSet : Workstep<AddAzimuthPropertyToPointSet.Arguments>, IExecutorSource, IAppearance, IDescriptionSource
    {
        #region Overridden Workstep methods

        /// <summary>
        /// Creates an empty Argument instance
        /// </summary>
        /// <returns>New Argument instance.</returns>

        protected override AddAzimuthPropertyToPointSet.Arguments CreateArgumentPackageCore(IDataSourceManager dataSourceManager)
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
                return "59fda83a-d03f-4366-95c1-82f78e4a2503";
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
                var pointSet = arguments.PointSet;
                //
                //  find appropriate azimuth point set property type
                var azimuthPropertyType = WellKnownPointSetPropertyTypes.DipAzimuth;
                //
                var azimuthProperty = GetOrCreateWellKnownPointProperty(pointSet, azimuthPropertyType);
                //
                //
                var azimuthConverter = PetrelUnitSystem.GetConverterFromUI(PetrelProject.WellKnownTemplates.GeometricalGroup.DipAzimuth);
                //
                var rng = new Random();
                // put some values in the azimuth property
                using (var transaction = DataManager.NewTransaction())
                {
                    transaction.Lock(azimuthProperty);
                    //
                    var azimuthValues = new List<double>();
                    //
                    foreach (var _ in azimuthProperty.Records)
                    {
                        double valueInDegree = rng.NextDouble() * 180;
                        double invariantValue = azimuthConverter.Convert(valueInDegree);
                        azimuthValues.Add(invariantValue);
                    }
                    //
                    // finally update the azimuth values
                    azimuthProperty.SetRecordValues(azimuthProperty.Records, azimuthValues);
                    //
                    transaction.Commit();
                }
                //
                // finally update the output arguments
                arguments.Azimuth = azimuthProperty;
            }

            public static PointProperty GetOrCreateWellKnownPointProperty(PointSet pointSet,
                PointSetPropertyType pointSetPropertyType)
            {
                // get the point property if it already exists
                if (pointSet.HasWellKnownProperty(pointSetPropertyType))
                {
                    return pointSet.GetWellKnownProperty(pointSetPropertyType);
                }
                //
                PointProperty pointProperty = PointProperty.NullObject;
                // otherwise create it and then return it
                using (var transaction = DataManager.NewTransaction())
                {
                    transaction.Lock(pointSet);
                    //
                    pointProperty = pointSet.CreateWellKnownProperty(pointSetPropertyType);
                    //
                    transaction.Commit();
                }
                //
                return pointProperty;
            }
        }

        #endregion

        /// <summary>
        /// ArgumentPackage class for AddAzimuthPropertyToPointSet.
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
            private Object azimuth;

            public Slb.Ocean.Petrel.DomainObject.Shapes.PointSet PointSet
            {
                internal get { return this.pointSet; }
                set { this.pointSet = value; }
            }

            public Object Azimuth
            {
                get { return this.azimuth; }
                internal set { this.azimuth = value; }
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
        /// Gets the description of the AddAzimuthPropertyToPointSet
        /// </summary>
        public IDescription Description
        {
            get { return AddAzimuthPropertyToPointSetDescription.Instance; }
        }

        /// <summary>
        /// This singleton class contains the description of the AddAzimuthPropertyToPointSet.
        /// Contains Name, Shorter description and detailed description.
        /// </summary>
        public class AddAzimuthPropertyToPointSetDescription : IDescription
        {
            /// <summary>
            /// Contains the singleton instance.
            /// </summary>
            private  static AddAzimuthPropertyToPointSetDescription instance = new AddAzimuthPropertyToPointSetDescription();
            /// <summary>
            /// Gets the singleton instance of this Description class
            /// </summary>
            public static AddAzimuthPropertyToPointSetDescription Instance
            {
                get { return instance; }
            }

            #region IDescription Members

            /// <summary>
            /// Gets the name of AddAzimuthPropertyToPointSet
            /// </summary>
            public string Name
            {
                get { return "AddAzimuthPropertyToPointSet"; }
            }
            /// <summary>
            /// Gets the short description of AddAzimuthPropertyToPointSet
            /// </summary>
            public string ShortDescription
            {
                get { return ""; }
            }
            /// <summary>
            /// Gets the detailed description of AddAzimuthPropertyToPointSet
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