namespace BoomBang.Communication.Outgoing
{
    using BoomBang.Communication;
    using BoomBang.Game.Spaces;
    using BoomBang.Specialized;
    using System;
    using System.Collections.Generic;

    public static class SpaceUserWalkComposer
    {
        public static ServerMessage Compose(List<SpaceActor> Actors)
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.WALK, 0, false);
            foreach (SpaceActor actor in Actors)
            {
                if (actor.LastPosition != actor.Position)
                {
                    message.AppendParameter(true, false);
                    message.AppendParameter(actor.UInt32_0, false);
                    message.AppendParameter(actor.Position.Int32_0, false);
                    message.AppendParameter(actor.Position.Int32_1, false);
                    message.AppendParameter(actor.Rotation, false);
                    message.AppendParameter(750, false);
                    message.AppendParameter(actor.Pathfinder.IsCompleted, false);
                    actor.LastPosition = actor.Position;
                    if (actor.UInt32_0 != Actors[Actors.Count - 1].UInt32_0)
                    {
                        byte[] data = new byte[] { 0xb0, 0xb1, 0, 0, 0xb3, 0xb2 };
                        data[2] = (byte) FlagcodesOut.WALK;
                        message.AppendBytes(data);
                    }
                }
            }
            return message;
        }

        public static ServerMessage TestComposer(uint uint_0, Vector3 Position)
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.WALK, 0, false);
            message.AppendParameter(1, false);
            message.AppendParameter(uint_0, false);
            message.AppendParameter(Position.Int32_0, false);
            message.AppendParameter(Position.Int32_1, false);
            message.AppendParameter(4, false);
            message.AppendParameter(750, false);
            message.AppendParameter(1, false);
            return message;
        }
    }
}

