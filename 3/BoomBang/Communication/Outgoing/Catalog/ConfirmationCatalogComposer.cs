using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snowlight.Communication
{
    class ConfirmationCatalogComposer
    {
        public static ServerMessage Compose()
        {
            ServerMessage message = new ServerMessage(Opcodes.CATALOGLOADCONFIRMATION);
            message.AppendNullParameter(false);
            return message;
        }
    }
}
