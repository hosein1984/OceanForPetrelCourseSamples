﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.Workflow;

namespace OceanCoursePlugin._5_DataAccessShapes
{
    public class DataAccessShapesModule : IModule
    {
        private Process m_createpolylinesetworkstepInstance;
        private Process m_createpointsetworkstepInstance;
        public void Dispose()
        {
            
        }

        public void Initialize()
        {
        }

        public void Disintegrate()
        {
            PetrelSystem.ProcessDiagram.Remove(m_createpolylinesetworkstepInstance);
            PetrelSystem.ProcessDiagram.Remove(m_createpointsetworkstepInstance);
        }

        public void Integrate()
        {
            // Register OceanCoursePlugin._5_DataAccessShapes.CreatePolylineSetWorkstep
            OceanCoursePlugin._5_DataAccessShapes.CreatePolylineSetWorkstep createpolylinesetworkstepInstance = new OceanCoursePlugin._5_DataAccessShapes.CreatePolylineSetWorkstep();
            PetrelSystem.WorkflowEditor.Add(createpolylinesetworkstepInstance);
            m_createpolylinesetworkstepInstance = new Slb.Ocean.Petrel.Workflow.WorkstepProcessWrapper(createpolylinesetworkstepInstance);
            PetrelSystem.ProcessDiagram.Add(m_createpolylinesetworkstepInstance, "Plug-ins");
            // Register OceanCoursePlugin._5_DataAccessShapes.CreatePointSetWorkstep
            OceanCoursePlugin._5_DataAccessShapes.CreatePointSetWorkstep createpointsetworkstepInstance = new OceanCoursePlugin._5_DataAccessShapes.CreatePointSetWorkstep();
            PetrelSystem.WorkflowEditor.Add(createpointsetworkstepInstance);
            m_createpointsetworkstepInstance = new Slb.Ocean.Petrel.Workflow.WorkstepProcessWrapper(createpointsetworkstepInstance);
            PetrelSystem.ProcessDiagram.Add(m_createpointsetworkstepInstance, "Plug-ins");
        }

        public void IntegratePresentation()
        {
        }
    }
}
