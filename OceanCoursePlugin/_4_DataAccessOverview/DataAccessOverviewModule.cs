using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.Workflow;

namespace OceanCoursePlugin._4_DataAccessOverview
{
    public class DataAccessOverviewModule : IModule
    {
        private Process m_createtimetemplateworkstepInstance;
        private Process m_listallwellsinprojectworkstepInstance;
        public void Dispose()
        {
        }

        public void Initialize()
        {
        }

        public void Disintegrate()
        {
            PetrelSystem.ProcessDiagram.Remove(m_createtimetemplateworkstepInstance);
            PetrelSystem.ProcessDiagram.Remove(m_listallwellsinprojectworkstepInstance);
        }

        public void Integrate()
        {
            // Register OceanCoursePlugin._4_DataAccessOverview.CreateTimeTemplateWorkstep
            OceanCoursePlugin._4_DataAccessOverview.CreateTimeTemplateWorkstep createtimetemplateworkstepInstance = new OceanCoursePlugin._4_DataAccessOverview.CreateTimeTemplateWorkstep();
            PetrelSystem.WorkflowEditor.Add(createtimetemplateworkstepInstance);
            m_createtimetemplateworkstepInstance = new Slb.Ocean.Petrel.Workflow.WorkstepProcessWrapper(createtimetemplateworkstepInstance);
            PetrelSystem.ProcessDiagram.Add(m_createtimetemplateworkstepInstance, "Plug-ins");
            //
            // Register OceanCoursePlugin._4_DataAccessOverview.ListAllWellsInProjectWorkstep
            ListAllWellsInProjectWorkstep listallwellsinprojectworkstepInstance = new ListAllWellsInProjectWorkstep();
            PetrelSystem.WorkflowEditor.Add(listallwellsinprojectworkstepInstance);
            m_listallwellsinprojectworkstepInstance = new WorkstepProcessWrapper(listallwellsinprojectworkstepInstance);
            PetrelSystem.ProcessDiagram.Add(m_listallwellsinprojectworkstepInstance, "Plug-ins");
        }

        public void IntegratePresentation()
        {
        }
    }
}
