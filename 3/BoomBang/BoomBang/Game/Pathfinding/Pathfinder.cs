namespace BoomBang.Game.Pathfinding
{
    using BoomBang.Game.Spaces;
    using BoomBang.Specialized;
    using System;
    using System.Collections.Generic;

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

