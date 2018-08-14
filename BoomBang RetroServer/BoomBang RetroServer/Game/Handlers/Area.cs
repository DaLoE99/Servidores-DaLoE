using BoomBang_RetroServer.Database;
using BoomBang_RetroServer.Sessions;
using BoomBang_RetroServer.Sockets.Messages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoomBang_RetroServer.Game.Handlers
{
    class Area : Handler
    {
        public void Handler_199_121()
        {
            int IDSessionChanges = Convert.ToInt32(Message.Parameters[0, 0]);
            int BuyItemID = Convert.ToInt32(Message.Parameters[1, 0]);
            int itemID = Convert.ToInt32(Message.Parameters[2, 0]);
            int something_6 = Convert.ToInt32(Message.Parameters[3, 0]);

            int OtherUser = IDSessionChanges - User.ID;

            using(DatabaseClient DatabaseClient = DatabaseManager.GetClient())
            {
                Session SessionByFriendID = SessionsManager.GetSession(OtherUser);
                DataRow Row = DatabaseClient.ExecuteScalarRow("SELECT * FROM boombang_buyitems WHERE id = " + BuyItemID + " AND itemID = " + itemID);
                if(SessionByFriendID !=null)
                {
                    SessionByFriendID.SendMessage(new ServerMessage(new byte[] {199,121}, new object[] { BuyItemID, itemID, (Row["color"].ToString() == "") ? null : Row["color"].ToString(), (Row["rgb_ratio"].ToString() == "") ? null : Row["rgb_Ratio"].ToString());
                }
                SendMessage(new ServerMessage(new byte[] { 199, 125 }, new object[] {1,BuyItemID,itemID,1}));
            }
        }
    }
}