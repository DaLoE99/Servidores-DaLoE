using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Snowlight.Communication.Outgoing
{
    class LaptopLoadMessagesComposer
    {
        public static ServerMessage Compose(DataTable dataTable)
        {
            ServerMessage message = new ServerMessage(Opcodes.LAPTOPLOADMESSAGES);
            message.Append(dataTable.Rows.Count);
            foreach (DataRow row in dataTable.Rows)
            {
                message.Append(uint.Parse(row["id"].ToString()));
                message.Append(uint.Parse(row["emisor"].ToString()));
                message.Append(UnixTimestamp.GetDateTimeFromUnixTimestamp(double.Parse(row["timestamp"].ToString())).ToString("yyyy-MM-dd HH:mm:ss"));
                message.Append((string)row["contenido"]);
                message.Append(uint.Parse(row["color"].ToString()));
            }
            return message;
        }
    }
}
