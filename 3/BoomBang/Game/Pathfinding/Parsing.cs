using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snowlight.Specialized;
using Snowlight.Game.Spaces;

namespace Snowlight.Game.Pathfinding
{
    public class Parsing : Pathfinder
    {
        /* private scope */
        List<Vector3> list_0;
        /* private scope */
        SpaceInstance spaceInstance_0;
        /* private scope */
        TileState[,] tileState_0;
        /* private scope */
        uint uint_0;
        /* private scope */
        Vector3 vector3_0;

        public override void Clear()
        {
            this.list_0.Clear();
            this.vector3_0 = null;
        }

        public override Vector3 GetNextStep()
        {
            if ((!this.IsCompleted && (this.tileState_0[this.list_0[0].Int32_0, this.list_0[0].Int32_1] != TileState.Blocked)) && (this.tileState_0[this.list_0[0].Int32_0, this.list_0[0].Int32_1] != TileState.Door))
            {
                Vector3 item = this.list_0[0];
                this.list_0.Remove(item);
                this.spaceInstance_0.RegenerateRelativeHeightmap(false);
                return item;
            }
            this.Clear();
            return null;
        }

        public override void MoveTo(List<Vector3> StepList, Vector3 PositionToSet)
        {
            this.list_0.Clear();
            this.vector3_0 = PositionToSet;
            this.list_0 = StepList;
        }

        public override void SetSpaceInstance(SpaceInstance Space, uint ActorId)
        {
            this.spaceInstance_0 = Space;
            this.tileState_0 = Space.Model.Heightmap.TileStates;
            this.uint_0 = ActorId;
            this.list_0 = new List<Vector3>();
            this.vector3_0 = null;
        }

        public override bool IsCompleted
        {
            get
            {
                return (this.list_0.Count == 0);
            }
        }

        public override Vector3 Target
        {
            get
            {
                return this.vector3_0;
            }
        }
    }
}
