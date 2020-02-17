using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Slb.Ocean.Core;
using Slb.Ocean.Petrel.Commands;
using Slb.Ocean.Petrel;

namespace OceanCoursePlugin._15_UITrees
{
    class XYZObjectCommandHandler : SimpleCommandHandler
    {
        public static string ID = "OceanCoursePlugin._15_UITrees.XYZObjectCommand";

        #region SimpleCommandHandler Members

        public override bool CanExecute(Slb.Ocean.Petrel.Contexts.Context context)
        { 
            return true;
        }

        public override void Execute(Slb.Ocean.Petrel.Contexts.Context context)
        {
            var project = PetrelProject.PrimaryProject;
            //
            using (var transaction = DataManager.NewTransaction())
            {
                transaction.Lock(project);
                //
                   project.Extensions.Add(new XYZObject());
                //
                transaction.Commit();
            }
        }
    
        #endregion
    }
}
