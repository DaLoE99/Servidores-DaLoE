namespace BoomBang.Communication.Outgoing
{
    using BoomBang.Communication;
    using BoomBang.Game.Characters;
    using System;
    using System.Data;

    public static class PreEnterIslandComposer
    {
        public static ServerMessage Compose(CharacterInfo Info, DataRow Island)
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.ISLANDS, ItemcodesOut.ISLAND_PRE, false);
            message.AppendParameter((uint) Island["id"], false);
            message.AppendParameter((string) Island["nombre"], false);
            message.AppendNullParameter(false);
            message.AppendParameter((uint) Island["modelo_area"], false);
            message.AppendParameter(0, false);
            message.AppendParameter(Info.UInt32_0, false);
            message.AppendParameter(Info.Username, false);
            message.AppendParameter(Info.AvatarType, false);
            message.AppendParameter(Info.AvatarColors, false);
            message.AppendNullParameter(false);
            message.AppendNullParameter(false);
            message.AppendNullParameter(false);
            message.AppendNullParameter(false);
            message.AppendNullParameter(false);
            message.AppendNullParameter(false);
            message.AppendNullParameter(false);
            message.AppendNullParameter(false);
            message.AppendNullParameter(false);
            message.AppendNullParameter(false);
            message.AppendNullParameter(false);
            message.AppendNullParameter(false);
            message.AppendNullParameter(false);
            message.AppendNullParameter(false);
            message.AppendNullParameter(false);
            message.AppendNullParameter(false);
            message.AppendParameter(0, false);
            return message;
        }
    }
}

