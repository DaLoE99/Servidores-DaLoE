using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snowlight.Specialized;
using Snowlight.Game.Spaces;

namespace Snowlight.Game.Pathfinding
{
    public abstract class Pathfinder
    {
        protected Pathfinder()
        {
        }

        public abstract void Clear();
        public abstract Vector3 GetNextStep();
        public abstract void MoveTo(List<Vector3> StepList, Vector3 PositionToSet);
        public abstract void SetSpaceInstance(SpaceInstance Space, uint ActorId);

        public abstract bool IsCompleted { get; }

        public abstract Vector3 Target { get; }
    }
}
