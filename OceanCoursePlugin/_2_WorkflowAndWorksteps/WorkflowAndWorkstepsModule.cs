using System;
using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.Workflow;

namespace OceanCoursePlugin._2_WorkflowAndWorksteps
{
    class WorkflowAndWorkstepsModule : IModule
    {
        public void Dispose()
        {
        }

        public void Initialize()
        {
        }

        public void Disintegrate()
        {
        }

        public void Integrate()
        {
            // Register OceanCoursePlugin._2_WorkflowAndWorksteps.ParentInfoFinderWorkstep
            ParentInfoFinderWorkstep parentinfofinderworkstepInstance = new ParentInfoFinderWorkstep();
            PetrelSystem.WorkflowEditor.Add(parentinfofinderworkstepInstance);
            //
            // adding the workstep to the Process's diagram
            WorkstepProcessWrapper workstepProcessWrapper = new WorkstepProcessWrapper(parentinfofinderworkstepInstance);
            PetrelSystem.ProcessDiagram.Add(workstepProcessWrapper);
        }

        public void IntegratePresentation()
        {
        }
    }
}
