using System;
using System.Collections.Generic;
using System.Linq;
using OceanCoursePlugin.Extensions;
using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.DomainObject;
using Slb.Ocean.Petrel.DomainObject.Well;
using Slb.Ocean.Petrel.UI;
using Slb.Ocean.Petrel.Workflow;

namespace OceanCoursePlugin._4_DataAccessOverview
{
    /// <summary>
    /// This class contains all the methods and subclasses of the ListAllWellsInProjectWorkstep.
    /// Worksteps are displayed in the workflow editor.
    /// </summary>
    class ListAllWellsInProjectWorkstep : Workstep<ListAllWellsInProjectWorkstep.Arguments>, IExecutorSource, IAppearance, IDescriptionSource
    {
        #region Overridden Workstep methods

        /// <summary>
        /// Creates an empty Argument instance
        /// </summary>
        /// <returns>New Argument instance.</returns>

        protected override ListAllWellsInProjectWorkstep.Arguments CreateArgumentPackageCore(IDataSourceManager dataSourceManager)
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
                return "c2e5ef3b-83cf-4c6b-81af-903a6f5e433d";
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
                // get current project
                Project project = PetrelProject.PrimaryProject;
                //
                // get well root
                WellRoot wellRoot = WellRoot.Get(project);
                //
                // get main borehole collection and marker collections
                var mainBoreholeCollection = wellRoot.BoreholeCollection;
                var markerCollections = wellRoot.MarkerCollections;
                //
                //
                PetrelLogger.InfoOutputWindow("-------------------Well Root Navigation-------------");
                //
                PetrelLogger.InfoOutputWindow("---------------Boreholes-------------------");
                ListAllBoreholesRecursively(mainBoreholeCollection, 0);
                //
                PetrelLogger.InfoOutputWindow("---------------Markers---------------------");
                ListAllMarkers(markerCollections);
            }

            private void ListAllMarkers(IEnumerable<MarkerCollection> markerCollections)
            {
                //
                // print all of the markers
                foreach (MarkerCollection markerCollection in markerCollections)
                {
                    PetrelLogger.InfoOutputWindow("------------------------------------------------");
                    PetrelLogger.InfoOutputWindow("Marker Collection Name: " + markerCollection.Name);
                    PetrelLogger.InfoOutputWindow("Markers Count: " + markerCollection.MarkerCount);
                    //
                    foreach (Marker marker in markerCollection)
                    {
                        PetrelLogger.InfoOutputWindow("\t" + marker.DumpToString());
                    }
                }
            }

            private void ListAllBoreholesRecursively(BoreholeCollection boreholeCollection, int indentationLevel)
            {
                string prefixIndentation =
                    Enumerable
                        .Range(0, indentationLevel)
                        .Aggregate("", (acc, curr) => acc + "\t");
                //
                // print borehole collection name
                PetrelLogger.InfoOutputWindow(prefixIndentation + "Borehole Collection: " + boreholeCollection.Name + " has " + boreholeCollection.Count + " wells");
                //
                // print all of the boreholes in the borehole collection
                foreach (Borehole borehole in boreholeCollection)
                {
                    PetrelLogger.InfoOutputWindow(prefixIndentation + "\t" + "Well: " + borehole.Name);
                }
                //
                foreach (var childBoreholeCollection in boreholeCollection.BoreholeCollections)
                {
                    ListAllBoreholesRecursively(childBoreholeCollection, indentationLevel + 1);
                }
            }
        }

        #endregion

        /// <summary>
        /// ArgumentPackage class for ListAllWellsInProjectWorkstep.
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
        /// Gets the description of the ListAllWellsInProjectWorkstep
        /// </summary>
        public IDescription Description
        {
            get { return ListAllWellsInProjectWorkstepDescription.Instance; }
        }

        /// <summary>
        /// This singleton class contains the description of the ListAllWellsInProjectWorkstep.
        /// Contains Name, Shorter description and detailed description.
        /// </summary>
        public class ListAllWellsInProjectWorkstepDescription : IDescription
        {
            /// <summary>
            /// Contains the singleton instance.
            /// </summary>
            private  static ListAllWellsInProjectWorkstepDescription instance = new ListAllWellsInProjectWorkstepDescription();
            /// <summary>
            /// Gets the singleton instance of this Description class
            /// </summary>
            public static ListAllWellsInProjectWorkstepDescription Instance
            {
                get { return instance; }
            }

            #region IDescription Members

            /// <summary>
            /// Gets the name of ListAllWellsInProjectWorkstep
            /// </summary>
            public string Name
            {
                get { return "ListAllWellsInProjectWorkstep"; }
            }
            /// <summary>
            /// Gets the short description of ListAllWellsInProjectWorkstep
            /// </summary>
            public string ShortDescription
            {
                get { return ""; }
            }
            /// <summary>
            /// Gets the detailed description of ListAllWellsInProjectWorkstep
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