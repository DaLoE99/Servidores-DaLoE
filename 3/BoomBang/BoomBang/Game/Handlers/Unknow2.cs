using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoomBang.Communication;
  

class Unknow2Composer
    {
        public static ServerMessage Compose()
        {
            ServerMessage message = new ServerMessage(ItemcodesIn.UNKNOW2);
            return message;
        }
    }

