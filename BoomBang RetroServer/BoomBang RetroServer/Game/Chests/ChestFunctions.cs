using BoomBang_RetroServer.Game.Spaces.Areas;
using BoomBang_RetroServer.Game.Users;
using BoomBang_RetroServer.Sessions;
using BoomBang_RetroServer.Sockets.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoomBang_RetroServer.Game.Chests
{
    class ChestFunctions
    {
        public static void CathChest(int IDCofre, Session Session)
        {
            Random Rand = new Random();
            if (Session.User.SpaceInstance is AreaInstance)
            {
                AreaInstance Area = (AreaInstance)Session.User.SpaceInstance;
                if(Area.Chests.ContainsKey(IDCofre))
                {
                    int ChestsID = Area.Chests[IDCofre];
                    ChestData Chest = ChestManager.Chests[ChestsID];
                    Area.Chests.Remove(IDCofre);
                    Area.ChestsPoints.Remove(IDCofre);
                    //Sendtoall num 16-04-12 16:56    min 00:09
                    //Sendtoall num 16-04-12 16:56    min 00:09
                    if (Chest.Amount_type == "gold")
                    {
                        Session.SendMessage(new ServerMessage(new byte[] { 162 }, new object[] { Chest.Amount }));
                        Session.User.GoldCoins += Chest.Amount;
                        UserManager.UpdateCoins(Session.User);
                    }
                    if (Chest.Amount_type == "silver")
                    {
                        Session.SendMessage(new ServerMessage(new byte[] { 166 }, new object[] { Chest.Amount }));
                        Session.User.SilverCoins += Chest.Amount;
                        UserManager.UpdateCoins(Session.User);
                    }
                    if (Chest.Amount_type == "contest" && Chest.ContestID > 0)
                    {
                        UserManager.UpdateContest(Session.User, Chest.Amount, Chest.ContestID);
                    }
                }
            }
        }
    }
}
