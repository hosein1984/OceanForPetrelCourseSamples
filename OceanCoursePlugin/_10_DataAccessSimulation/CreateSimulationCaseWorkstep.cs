using System;
using System.Collections.Generic;
using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.UI;
using Slb.Ocean.Petrel.Workflow;
using Slb.Ocean.Petrel.DomainObject.PillarGrid;
using Slb.Ocean.Petrel.DomainObject.Simulation;
using Slb.Ocean.Petrel.DomainObject.Simulation.DevelopmentStrategies;
using Slb.Ocean.Petrel.Simulation;

namespace OceanCoursePlugin._10_DataAccessSimulation
{
    /// <summary>
    /// This class contains all the methods and subclasses of the CreateSimulationCaseWorkstep.
    /// Worksteps are displayed in the workflow editor.
    /// </summary>
    class CreateSimulationCaseWorkstep : Workstep<CreateSimulationCaseWorkstep.Arguments>, IExecutorSource, IAppearance, IDescriptionSource
    {
        #region Overridden Workstep methods

        /// <summary>
        /// Creates an empty Argument instance
        /// </summary>
        /// <returns>New Argument instance.</returns>

        protected override CreateSimulationCaseWorkstep.Arguments CreateArgumentPackageCore(IDataSourceManager dataSourceManager)
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
                return "798e13f3-3e73-4795-a648-883c70614fff";
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
                // extract data from input arguments
                var caseName = arguments.Name;
                var grid = arguments.Porosity.Grid;
                
                //
                // get e100  simulator
                ECLIPSE100 e100 = WellKnownSimulators.ECLIPSE100;
                Case simulationCase = e100.CreateSimulationCase(caseName, grid, PorosityPermeabilityType.Single);
                var caseArguments = e100.GetEclipseFormatSimulatorArguments(simulationCase);
                //
                // set grid properties
                caseArguments.Grid.Matrix.Porosity = new GridItem<Property>(arguments.Porosity);
                caseArguments.Grid.Matrix.PermeabilityI = new GridItem<Property>(arguments.Permeability);
                caseArguments.Grid.Matrix.PermeabilityJ = new GridItem<Property>(arguments.Permeability);
                caseArguments.Grid.Matrix.PermeabilityK = new GridItem<Property>(arguments.Permeability);
                //
                // set functions
                caseArguments.Functions.Matrix.DrainageSaturationFunctionItem = new SaturationFunctionItem(new SaturationFunctionItem.Region(arguments.SaturationFunction));
                caseArguments.Functions.Matrix.RockCompaction= new FunctionItem<RockCompactionFunction>(new FunctionRegion<RockCompactionFunction>(arguments.RockCompactionFunction));
                caseArguments.InitializeByEquilibration = true;
                caseArguments.Functions.Matrix.BlackOil = new FunctionItem<BlackOil>(new FunctionRegion<BlackOil>(arguments.Blackoil));
                //
                // set development strategy
                caseArguments.Strategies.Strategies = new List<Strategy>() {arguments.DevelopmentStrategy};
                //
                arguments.SimulationCase = simulationCase;
            }
        }

        #endregion

        /// <summary>
        /// ArgumentPackage class for CreateSimulationCaseWorkstep.
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

            private string name = "OceanCase";
            private Slb.Ocean.Petrel.DomainObject.PillarGrid.Property porosity;
            private Slb.Ocean.Petrel.DomainObject.PillarGrid.Property permeability;
            private Slb.Ocean.Petrel.DomainObject.Simulation.SaturationFunction saturationFunction;
            private Slb.Ocean.Petrel.DomainObject.Simulation.RockCompactionFunction rockCompactionFunction;
            private Slb.Ocean.Petrel.DomainObject.Simulation.BlackOil blackoil;
            private Slb.Ocean.Petrel.DomainObject.Simulation.DevelopmentStrategies.Strategy developmentStrategy;
            private Slb.Ocean.Petrel.DomainObject.Simulation.Case simulationCase;

            public string Name
            {
                internal get { return this.name; }
                set { this.name = value; }
            }

            [Description("Porosity", "Grid porosity property")]
            public Slb.Ocean.Petrel.DomainObject.PillarGrid.Property Porosity
            {
                internal get { return this.porosity; }
                set { this.porosity = value; }
            }

            [Description("Permeability", "Grid permeability property")]
            public Slb.Ocean.Petrel.DomainObject.PillarGrid.Property Permeability
            {
                internal get { return this.permeability; }
                set { this.permeability = value; }
            }

            [Description("SaturationFunction", "Sand Saturation Function")]
            public Slb.Ocean.Petrel.DomainObject.Simulation.SaturationFunction SaturationFunction
            {
                internal get { return this.saturationFunction; }
                set { this.saturationFunction = value; }
            }

            [Description("RockCompactionFunction", "Rock Compaction Function")]
            public Slb.Ocean.Petrel.DomainObject.Simulation.RockCompactionFunction RockCompactionFunction
            {
                internal get { return this.rockCompactionFunction; }
                set { this.rockCompactionFunction = value; }
            }

            [Description("Blackoil", "Black oil fluid model")]
            public Slb.Ocean.Petrel.DomainObject.Simulation.BlackOil Blackoil
            {
                internal get { return this.blackoil; }
                set { this.blackoil = value; }
            }

            [Description("DevelopmentStrategy", "Development Strategy")]
            public Slb.Ocean.Petrel.DomainObject.Simulation.DevelopmentStrategies.Strategy DevelopmentStrategy
            {
                internal get { return this.developmentStrategy; }
                set { this.developmentStrategy = value; }
            }

            [Description("SimulationCase", "Simulation Case")]
            public Slb.Ocean.Petrel.DomainObject.Simulation.Case SimulationCase
            {
                get { return this.simulationCase; }
                internal set { this.simulationCase = value; }
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
        /// Gets the description of the CreateSimulationCaseWorkstep
        /// </summary>
        public IDescription Description
        {
            get { return CreateSimulationCaseWorkstepDescription.Instance; }
        }

        /// <summary>
        /// This singleton class contains the description of the CreateSimulationCaseWorkstep.
        /// Contains Name, Shorter description and detailed description.
        /// </summary>
        public class CreateSimulationCaseWorkstepDescription : IDescription
        {
            /// <summary>
            /// Contains the singleton instance.
            /// </summary>
            private  static CreateSimulationCaseWorkstepDescription instance = new CreateSimulationCaseWorkstepDescription();
            /// <summary>
            /// Gets the singleton instance of this Description class
            /// </summary>
            public static CreateSimulationCaseWorkstepDescription Instance
            {
                get { return instance; }
            }

            #region IDescription Members

            /// <summary>
            /// Gets the name of CreateSimulationCaseWorkstep
            /// </summary>
            public string Name
            {
                get { return "CreateSimulationCaseWorkstep"; }
            }
            /// <summary>
            /// Gets the short description of CreateSimulationCaseWorkstep
            /// </summary>
            public string ShortDescription
            {
                get { return "Create simulation case"; }
            }
            /// <summary>
            /// Gets the detailed description of CreateSimulationCaseWorkstep
            /// </summary>
            public string Description
            {
                get { return "Create simulation case and set its arguments"; }
            }

            #endregion
        }
        #endregion


    }
}