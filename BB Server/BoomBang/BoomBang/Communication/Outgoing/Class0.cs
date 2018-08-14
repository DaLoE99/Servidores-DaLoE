namespace BoomBang.Communication.Outgoing
{
    using BoomBang;
    using BoomBang.Communication;
    using System;
    using System.Data;

    internal class Class0
    {
        public static ServerMessage smethod_0(DataTable dataTable_0)
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.LAPTOP, ItemcodesOut.LAPTOP_LOAD_MESSAGES, false);
            message.AppendParameter(dataTable_0.Rows.Count, false);
            foreach (DataRow row in dataTable_0.Rows)
            {
                message.AppendParameter((uint) row["id"], false);
                message.AppendParameter((uint) row["emisor"], false);
                message.AppendParameter(UnixTimestamp.GetDateTimeFromUnixTimestamp((double) row["timestamp"]).ToString("yyyy-MM-dd HH:mm:ss"), false);
                message.AppendParameter((string) row["contenido"], false);
                message.AppendParameter((uint) row["color"], false);
            }
            return message;
        }
    }
}

