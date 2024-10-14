using System.Collections.Generic;
using RTSEngine;
using RTSEngine.EntityComponent;

namespace ADC
{
    public class AstreanTechManager : PendingTaskEntityComponentBase
    {
        public IAstreanTechType AstreanTech { get; set; }

        public override IReadOnlyList<IEntityComponentTaskInput> Tasks { get; }

        protected override void OnPendingInit()
        {
            AstreanTech?.Start();
        }

        protected override ErrorMessage CompleteTaskActionLocal(int taskID, bool playerCommand)
        {
            throw new System.NotImplementedException();
        }

        protected override string GetTaskTooltipText(IEntityComponentTaskInput taskInput)
        {
            throw new System.NotImplementedException();
        }
    }
}
