namespace BoomBang.Game.Misc.Chat
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using BoomBang.Communication;
    using BoomBang.Communication.Incoming;
    using BoomBang.Communication.Outgoing;
    using BoomBang.Game.Characters;
    using BoomBang.Game.Contests;
    using BoomBang.Game.Sessions;
    using BoomBang.Game.Spaces;
    using BoomBang.Storage;
    using BoomBang.Utils;
    using MySql.Data.MySqlClient;


    public static class ChatCommands
    {
        /* private scope */
        static List<uint> list_0;
        /* private scope */
        static List<string> list_1;
        /* private scope */
        static object object_0;



        /* private scope */
        static Random random_0 = new Random();
        private static bool CharacterId;
        private static bool Text;
        private static bool Color;
        public static bool block_upper = false;
        public static bool block_coco = false;
        public static List<string> UpperBlock = new List<string>();
        public static List<uint> UpperBlockUser = new List<uint>();



        public static bool HandleCommand(Session Session, string Input)
        {
            Input = Input.Substring(1, Input.Length - 1);
            string[] input = Input.Split(new char[] { ' ' });
            SpaceInstance instanceBySpaceId = SpaceManager.GetInstanceBySpaceId(Session.CurrentSpaceId);
            SpaceActor actor = (instanceBySpaceId == null) ? null : instanceBySpaceId.GetActorByReferenceId(Session.CharacterId, SpaceActorType.UserCharacter);
            CharacterInfo referenceObject = (CharacterInfo)actor.ReferenceObject;

            switch (input[0])
            {
                case "position":
                    if (referenceObject.Staff == 1 || referenceObject.Staff == 2 || referenceObject.Staff == 6)
                    {
                        Session.SendData(SpaceChatComposer.Compose(0, string.Concat(new object[] { "Tu posici\x00f3n actual es - X: ", actor.Position.Int32_0, ", Y: ", actor.Position.Int32_1, ", Z: ", actor.Position.Int32_2 }), 3, ChatType.Say), false);
                        return true;
                    }
                    return false;

                case "bm":
                    if (referenceObject.Staff == 2 || referenceObject.Staff == 6)
                    {
                        if (!UpperBlock.Contains(actor.Name))
                        {
                            string str = string.Format("{0}: Has activated Block User Upper.", actor.Name);
                            instanceBySpaceId.BroadcastMessage(SpaceChatComposer.Compose(1, str, 3, ChatType.Say), 0, false);
                            UpperBlock.Add(actor.Name);
                        }
                        return true;
                    }
                    return false;

                case "uub":
                    if (referenceObject.Staff == 2 || referenceObject.Staff == 6)
                    {
                        Input = Input.Replace("uub", "");
                        Input = Input.Replace(" ", "");

                        uint IDtoblock = CharacterResolverCache.GetUidFromName(Input);



                        if (!UpperBlockUser.Contains(IDtoblock))
                        {
                            string str = string.Format("{0}: Has activated Block User Upper For {1}", actor.Name, Input);
                            instanceBySpaceId.BroadcastMessage(SpaceChatComposer.Compose(1, str, 3, ChatType.Say), 0, false);
                            UpperBlockUser.Add(IDtoblock);
                        }
                        return true;
                    }
                    return false;

                case "ub":
                    if (referenceObject.Staff == 2 || referenceObject.Staff == 6)
                    {
                        Input = Input.Replace("ub", "");
                        Input = Input.Replace(" ", "");

                        uint IDtoblock = CharacterResolverCache.GetUidFromName(Input);



                        if (UpperBlockUser.Contains(IDtoblock))
                        {
                            string str = string.Format("{0}: Has deactivated Block User Upper For {1}", actor.Name, Input);
                            instanceBySpaceId.BroadcastMessage(SpaceChatComposer.Compose(1, str, 3, ChatType.Say), 0, false);
                            UpperBlockUser.Remove(IDtoblock);
                        }
                        return true;
                    }
                    return false;

                case "ubm":
                    if (referenceObject.Staff == 2 || referenceObject.Staff == 6)
                    {
                        if (UpperBlock.Contains(actor.Name))
                        {
                            string str = string.Format("{0}: Has deactivated Block User Upper.", actor.Name);
                            instanceBySpaceId.BroadcastMessage(SpaceChatComposer.Compose(1, str, 3, ChatType.Say), 0, false);
                            UpperBlock.Remove(actor.Name);
                        }
                        return true;
                    }
                    return false;


                case "block_upper":
                    if (referenceObject.Staff == 1 || referenceObject.Staff == 2 || referenceObject.Staff == 6)
                    {
                        instanceBySpaceId.BroadcastMessage(SpaceChatComposer.Compose(1, actor.Name + " ha bloqueado el punch.", 3, ChatType.Say), 0, false);
                        instanceBySpaceId.BroadcastMessage(SpaceChatComposer.Compose(1, actor.Name + ": Has turned Block Punch On.", 3, ChatType.Say), 0, false);
                        block_upper = true;
                        return true;
                    }
                    return false;

                case "unblock_upper":
                    if (referenceObject.Staff == 1 || referenceObject.Staff == 2 || referenceObject.Staff == 6)
                    {
                        instanceBySpaceId.BroadcastMessage(SpaceChatComposer.Compose(1, actor.Name + " ha desbloqueado el punch.", 3, ChatType.Say), 0, false);
                        instanceBySpaceId.BroadcastMessage(SpaceChatComposer.Compose(1, actor.Name + ": Has turned Block Punch Off.", 3, ChatType.Say), 0, false);
                        block_upper = false;
                        return true;
                    }
                    return false;

                case "checkcredits":
                    if (referenceObject.Staff == 1 || referenceObject.Staff == 2 || referenceObject.Staff == 6)
                    {
                        Input = Input.Replace("checkcredits", "");
                        Input = Input.Replace(" ", "");

                        uint ID = CharacterResolverCache.GetUidFromName(actor.Name);
                        actor.Whisper(checkcredits(Input), ID, false);

                        return true;
                    }
                    return false;

                case "block_coco":
                    if (referenceObject.Staff == 1 || referenceObject.Staff == 2 || referenceObject.Staff == 6)
                    {

                        instanceBySpaceId.BroadcastMessage(SpaceChatComposer.Compose(1, actor.Name + ": Ha bloqueado el coco.", 3, ChatType.Say), 0, false);
                        instanceBySpaceId.BroadcastMessage(SpaceChatComposer.Compose(1, actor.Name + ": Has turned Block Coco On.", 3, ChatType.Say), 0, false);
                        block_coco = true;

                        return true;
                    }
                    return false;



                case "unblock_coco":
                    if (referenceObject.Staff == 1 || referenceObject.Staff == 2 || referenceObject.Staff == 6)
                    {
                        instanceBySpaceId.BroadcastMessage(SpaceChatComposer.Compose(1, actor.Name + ": ha desbloqueado el coco.", 3, ChatType.Say), 0, false);
                        instanceBySpaceId.BroadcastMessage(SpaceChatComposer.Compose(1, actor.Name + ": Has turned Block Coco Off.", 3, ChatType.Say), 0, false);
                        block_coco = false;

                        return true;
                    }
                    return false;



                case "ban":
                    if (referenceObject.Staff == 1 || referenceObject.Staff == 2 && actor.Name != "DaLoe" || referenceObject.Staff == 6)
                    {
                        SqlDatabaseClient mySqlClient = SqlDatabaseManager.GetClient();
                        string Input2 = Input;
                        string reason2 = Input2.Substring(Input2.IndexOf(',') + 1);
                        Input = Input.Replace(",", "");
                        string[] split = Input.Split(' ');
                        string username = (split[1]);
                        string reason = string.Join(" ", Input, 2, Input.Length - 2);
                        uint UserID = CharacterResolverCache.GetUidFromName(username);
                        Moderation.ModerationBanManager.BanHammer(mySqlClient, UserID, reason2, actor.Name);
                        string ban = string.Format("{0}: Banned: {1}, For: {2}", actor.Name, username, reason2);

                        Input = Input.Replace(" ", "");
                        uint ID = CharacterResolverCache.GetUidFromName(username);

                        if (SessionManager.ContainsCharacterId(ID))
                        {
                            Session Session_ID = SessionManager.GetSessionByCharacterId(ID);
                            uint session_id2 = Session_ID.CharacterInfo.SessionId;
                            SessionManager.StopSession(session_id2);
                        }

                        else
                        {
                        }

                        instanceBySpaceId.BroadcastMessage(SpaceChatComposer.Compose(1, ban, 3, ChatType.Say), 0, false);


                        return true;
                    }
                    return false;

                case "unban":
                    if (referenceObject.Staff == 1 || referenceObject.Staff == 2 && actor.Name != "DaLoe" && actor.Name != "Fran" || referenceObject.Staff == 6)
                    {
                        SqlDatabaseClient mySqlClient = SqlDatabaseManager.GetClient();
                        Input = Input.Replace("unban", "");
                        Input = Input.Replace(" ", "");

                        uint ID = CharacterResolverCache.GetUidFromName(Input);



                        Moderation.ModerationBanManager.Unban(mySqlClient, ID);

                        string unban = string.Format("{0}: Unbanned User {1}", actor.Name, Input);

                        instanceBySpaceId.BroadcastMessage(SpaceChatComposer.Compose(1, unban, 3, ChatType.Say), 0, false);


                        return true;
                    }
                    return false;

                case "ipban":
                    if (referenceObject.Staff == 2 && actor.Name != "DaLoE" && actor.Name != "Fran" || referenceObject.Staff == 6)
                    {
                        SqlDatabaseClient mySqlClient = SqlDatabaseManager.GetClient();
                        string Input2 = Input;
                        string reason2 = Input2.Substring(Input2.IndexOf(',') + 1);
                        Input = Input.Replace(",", "");
                        string[] split = Input.Split(' ');
                        string username = (split[1]);
                        string reason = string.Join(" ", Input, 2, Input.Length - 2);
                        uint UserID = CharacterResolverCache.GetUidFromName(username);

                        Sessions.Session session_0;
                        session_0 = SessionManager.GetSessionByCharacterId(UserID);
                        string str = session_0.RemoteAddress.ToString();

                        Moderation.ModerationBanManager.BanHammer3(mySqlClient, UserID, reason2, actor.Name, str);
                        string ban = string.Format("{0}: IP Banned: {1}, For: {2}", actor.Name, username, reason2);

                        Input = Input.Replace(" ", "");
                        uint ID = CharacterResolverCache.GetUidFromName(username);

                        if (SessionManager.ContainsCharacterId(ID))
                        {
                            Session Session_ID = SessionManager.GetSessionByCharacterId(ID);
                            uint session_id2 = Session_ID.CharacterInfo.SessionId;
                            SessionManager.StopSession(session_id2);
                        }

                        else
                        {
                        }

                        instanceBySpaceId.BroadcastMessage(SpaceChatComposer.Compose(1, ban, 3, ChatType.Say), 0, false);


                        return true;
                    }
                    return false;

                case "alert":
                    if (referenceObject.Staff == 1 || referenceObject.Staff == 2 || referenceObject.Staff == 6)
                    {
                        string messageText = InputFilter.FilterString(InputFilter.MergeString(input, 1), false);
                        instanceBySpaceId.BroadcastMessage(SpaceChatComposer.Compose(0, messageText, 3, ChatType.Say), 0, false);
                        return true;
                    }
                    return false;

                case "la":
                    if (referenceObject.Staff == 1 || referenceObject.Staff == 2 || referenceObject.Staff == 6)
                    {
                        if (actor.Name == "Brandon" || actor.Name == "Mike")
                        {
                            foreach (KeyValuePair<uint, Session> pair in SessionManager.Sessions)
                            {
                                Input = Input.Replace("la", "");
                                string send = string.Format("{0} - From Burbian Creator {1}", Input, actor.Name);
                                pair.Value.SendData(LaptopMessageComposer.Compose(pair.Value.CharacterId, send, 2));
                            }
                        }


                        else if (actor.Name == "Kyle")
                        {
                            foreach (KeyValuePair<uint, Session> pair in SessionManager.Sessions)
                            {
                                Input = Input.Replace("la", "");
                                string send = string.Format("{0} - From Burbian Main Programmer {1}", Input, actor.Name);
                                pair.Value.SendData(LaptopMessageComposer.Compose(pair.Value.CharacterId, send, 2));
                            }
                        }

                        else if (referenceObject.Staff == 1)
                        {
                            foreach (KeyValuePair<uint, Session> pair in SessionManager.Sessions)
                            {
                                Input = Input.Replace("la", "");
                                string send = string.Format("{0} - From Burbian Moderator {1}", Input, actor.Name);
                                pair.Value.SendData(LaptopMessageComposer.Compose(pair.Value.CharacterId, send, 2));
                            }
                        }


                        else if (referenceObject.Staff == 2 && actor.Name != "Brandon" && actor.Name != "Mike")
                        {
                            foreach (KeyValuePair<uint, Session> pair in SessionManager.Sessions)
                            {
                                Input = Input.Replace("la", "");
                                string send = string.Format("{0} - From Burbian Administrator {1}", Input, actor.Name);
                                pair.Value.SendData(LaptopMessageComposer.Compose(pair.Value.CharacterId, send, 2));
                            }
                        }


                        else if (referenceObject.Staff == 6 && actor.Name != "Kyle")
                        {
                            foreach (KeyValuePair<uint, Session> pair in SessionManager.Sessions)
                            {
                                Input = Input.Replace("la", "");
                                string send = string.Format("{0} - From Burbian Programmer {1}", Input, actor.Name);
                                pair.Value.SendData(LaptopMessageComposer.Compose(pair.Value.CharacterId, send, 2));
                            }
                        }

                        else
                        {

                            foreach (KeyValuePair<uint, Session> pair in SessionManager.Sessions)
                            {
                                Input = Input.Replace("la", "");
                                string send = string.Format("{0} - From Burbian Moderator {1}", Input, actor.Name);
                                pair.Value.SendData(LaptopMessageComposer.Compose(pair.Value.CharacterId, send, 2));
                            }

                        }

                        return true;
                    }
                    return false;

                case "effect":
                    if (referenceObject.Vip == 1 || referenceObject.Staff == 2 || referenceObject.Staff == 6)
                    {
                        Input = Input.Replace("effect", "");
                        Input = Input.Replace(" ", "");

                        switch (int.Parse(Input))
                        {
                            case 1:
                                actor.ApplyEffect(1, true);
                                actor.UpdateNeeded = true;
                                break;
                            case 2:
                                actor.ApplyEffect(2, true);
                                actor.UpdateNeeded = true;
                                break;
                            case 3:
                                actor.ApplyEffect(3, true);
                                actor.UpdateNeeded = true;
                                break;
                            case 4:
                                actor.ApplyEffect(4, true);
                                actor.UpdateNeeded = true;
                                break;


                        }



                        return true;
                    }
                    return false;


                case "give":
                    if (referenceObject.Staff == 2 || referenceObject.Staff == 6)
                    {
                        Session session_0;




                        string userthatadded = actor.Name;


                        SqlDatabaseClient mySqlClient = SqlDatabaseManager.GetClient();

                        int credits = 0;

                        string Input2 = Input;
                        string reason2 = Input2.Substring(Input2.IndexOf(',') + 1);
                        credits = int.Parse(reason2);
                        Input = Input.Replace(",", "");
                        string[] split = Input.Split(' ');
                        string username = (split[1]);
                        string reason = string.Join(" ", Input, 2, Input.Length - 2);
                        uint ID = CharacterResolverCache.GetUidFromName(username);

                        session_0 = SessionManager.GetSessionByCharacterId(ID);

                        //givecredits2(mySqlClient, credits, username);
                        referenceObject.UpdateGoldCreditsBalance(mySqlClient, +credits);
                        session_0.SendData(CharacterCoinsComposer.AddGoldCompose(Convert.ToUInt32(credits)), false); //Punch cost 200
                        string str = string.Format("{0}: Gave {1}, {2} Credits!", userthatadded, username, credits);
                        instanceBySpaceId.BroadcastMessage(SpaceChatComposer.Compose(0, str, 3, ChatType.Say), 0, false);


                        return true;
                    }
                    return false;

                case "share":
                    if (referenceObject.Staff == 0 || referenceObject.Staff == 1 || referenceObject.Staff == 2 || referenceObject.Staff == 3 || referenceObject.Staff == 4 || referenceObject.Staff == 5 || referenceObject.Staff == 6)
                    {
                        string BeforeComma = string.Empty;
                        string AfterComma = string.Empty;
                        SqlDatabaseClient client = SqlDatabaseManager.GetClient();
                        Session session_0;
                        Session session_1;

                        Input = Input.Replace("share", "");
                        Input = Input.Replace(" ", "");

                        BeforeComma = Input.Remove(Input.LastIndexOf(','));
                        AfterComma = Input.Substring(Input.IndexOf(',') + 1);

                        string username = BeforeComma.Replace(" ", "");
                        string amountofcredits_0 = AfterComma.Replace(" ", "");
                        int amount = int.Parse(amountofcredits_0);


                        uint actorID = CharacterResolverCache.GetUidFromName(actor.Name);
                        uint actorID2 = CharacterResolverCache.GetUidFromName(username);

                        session_0 = SessionManager.GetSessionByCharacterId(actorID);
                        session_1 = SessionManager.GetSessionByCharacterId(actorID2);

                        SpaceInstance instanceBySpaceId2 = SpaceManager.GetInstanceBySpaceId(session_1.CurrentSpaceId);
                        SpaceActor actor2 = (instanceBySpaceId2 == null) ? null : instanceBySpaceId2.GetActorByReferenceId(session_1.CharacterId, SpaceActorType.UserCharacter);
                        CharacterInfo referenceObject2 = (CharacterInfo)actor2.ReferenceObject;



                        if (enoughcredits(session_0, amount))
                        {
                            referenceObject.UpdateGoldCreditsBalance(client, -amount);
                            session_0.SendData(CharacterCoinsComposer.RemoveGoldCompose(Convert.ToUInt32(amount)), false);

                            referenceObject2.UpdateGoldCreditsBalance(client, +amount);
                            session_1.SendData(CharacterCoinsComposer.AddGoldCompose(Convert.ToUInt32(amount)), false);

                            string str = string.Format("{0}: Shared {1} Credits To {2}!", actor.Name, amount, username);
                            instanceBySpaceId.BroadcastMessage(SpaceChatComposer.Compose(0, str, 3, ChatType.Say), 0, false);


                        }

                        else
                        {
                            string str = string.Format("Could not send {0} the credits, did you have enough?", username);
                            actor.Whisper(str, actorID, false);
                        }





                        return true;
                    }
                    return false;


                case "take":
                    if (referenceObject.Staff == 2 || referenceObject.Staff == 6)
                    {

                        Session session_0;
                        Session session_1;


                        uint actoeid = CharacterResolverCache.GetUidFromName(actor.Name);


                        string userthatadded = actor.Name;


                        SqlDatabaseClient mySqlClient = SqlDatabaseManager.GetClient();

                        int credits = 0;

                        string Input2 = Input;
                        string reason2 = Input2.Substring(Input2.IndexOf(',') + 1);
                        credits = int.Parse(reason2);
                        Input = Input.Replace(",", "");
                        string[] split = Input.Split(' ');
                        string username = (split[1]);
                        string reason = string.Join(" ", Input, 2, Input.Length - 2);
                        uint ID = CharacterResolverCache.GetUidFromName(username);

                        session_0 = SessionManager.GetSessionByCharacterId(ID);

                        //givecredits2(mySqlClient, credits, username);
                        session_1 = SessionManager.GetSessionByCharacterId(ID);
                        int gold = session_1.CharacterInfo.GoldCoins;
                        int minusgold = gold - credits;

                        if (minusgold < 0)
                        {
                            actor.Whisper("Error, the amount you tried to give was below 0", actoeid, false);
                        }

                        else
                        {
                            referenceObject.UpdateGoldCreditsBalance(mySqlClient, -credits);
                            session_0.SendData(CharacterCoinsComposer.RemoveGoldCompose(Convert.ToUInt32(credits)), false);
                            string str = string.Format("{0}: Took Away {1} Credits From {2}!", userthatadded, credits, username);
                            instanceBySpaceId.BroadcastMessage(SpaceChatComposer.Compose(0, str, 3, ChatType.Say), 0, false);
                        }

                        return true;
                    }
                    return false;





                case "kick":
                    if (referenceObject.Staff == 1 || referenceObject.Staff == 2 || referenceObject.Staff == 6)
                    {
                        Session session_0;

                        Input = Input.Replace("kick", "");
                        Input = Input.Replace(" ", "");
                        uint ID = CharacterResolverCache.GetUidFromName(Input);
                        if (SessionManager.ContainsCharacterId(ID))
                        {
                            session_0 = SessionManager.GetSessionByCharacterId(ID);
                            SpaceManager.RemoveUserFromSpace(session_0);
                            string str = string.Format("{0}: Has Kicked {1} From Island.", actor.Name, Input);
                            instanceBySpaceId.BroadcastMessage(SpaceChatComposer.Compose(0, str, 3, ChatType.Say), 0, false);
                        }

                        else
                        {
                        }

                        //SpaceManager.GetInstanceBySpaceId

                        //SpaceUserRemovedComposer.BroadcastCompose(



                        return true;
                    }
                    return false;

                case "kickoffline":
                    if (referenceObject.Staff == 2 || referenceObject.Staff == 6)
                    {
                        Session session_0;

                        Input = Input.Replace("kickoffline", "");
                        Input = Input.Replace(" ", "");
                        uint ID = CharacterResolverCache.GetUidFromName(Input);
                        if (SessionManager.ContainsCharacterId(ID))
                        {
                            session_0 = SessionManager.GetSessionByCharacterId(ID);
                            uint session_id2 = session_0.CharacterInfo.SessionId;
                            SessionManager.StopSession(session_id2);
                            string str = string.Format("{0}: Has kicked {1} Offline!", actor.Name, Input);
                            instanceBySpaceId.BroadcastMessage(SpaceChatComposer.Compose(0, str, 3, ChatType.Say), 0, false);
                        }

                        else
                        {
                        }

                        //SpaceManager.GetInstanceBySpaceId

                        //SpaceUserRemovedComposer.BroadcastCompose(



                        return true;
                    }
                    return false;

                case "mod":
                    if (actor.Name == "Kyle" || actor.Name == "Brandon" || actor.Name == "Mike")
                    {
                        Session session_0;

                        SqlDatabaseClient client;
                        client = SqlDatabaseManager.GetClient();
                        Input = Input.Replace("mod", "");
                        Input = Input.Replace(" ", "");
                        string Input2 = Input;

                        string level = Input2.Substring(Input2.IndexOf(",") + 1);
                        uint mod = Convert.ToUInt32(level);

                        if (Input.Contains(","))
                            Input = Input.Substring(0, Input.LastIndexOf(","));
                        givemod(client, Input, mod);
                        string str = string.Format("{0}: Gave {1} Level {2} Mod Ability.", actor.Name, Input, mod);
                        instanceBySpaceId.BroadcastMessage(SpaceChatComposer.Compose(0, str, 3, ChatType.Say), 0, false);

                        uint ID = CharacterResolverCache.GetUidFromName(Input);
                        session_0 = SessionManager.GetSessionByCharacterId(ID);
                        uint session_id2 = session_0.CharacterInfo.SessionId;
                        SessionManager.StopSession(session_id2);








                        return true;
                    }
                    return false;

                case "getmod":
                    if (actor.Name == "Kyle" || actor.Name == "Brandon" || actor.Name == "Mike")
                    {
                        SqlDatabaseClient MySqlClient = SqlDatabaseManager.GetClient();
                        Input = Input.Replace("getmod", "");
                        Input = Input.Replace(" ", "");

                        uint ID2 = CharacterResolverCache.GetUidFromName(actor.Name);
                        uint ID = CharacterResolverCache.GetUidFromName(Input);

                        Session session_1;

                        session_1 = SessionManager.GetSessionByCharacterId(ID);
                        uint modlevel = session_1.CharacterInfo.Staff;

                        string str = string.Format("User {0} is mod level {1}", Input, modlevel);
                        actor.Whisper(str, ID2, false);

                        SessionManager.StopSession(session_1.AbsoluteSpaceId);


                        return true;
                    }
                    return false;






            }


            return false;
        }

        public static bool enoughcredits(Session session_1, int amount)
        {
            int credits = session_1.CharacterInfo.GoldCoins - amount;

            if (credits < 0)
            {
                return false;
            }

            else
            {
                return true;
            }
        }


        public static void givemod(SqlDatabaseClient MySqlClient, string username, uint mod)
        {
            uint ID = CharacterResolverCache.GetUidFromName(username);
            Session session_1992;
            session_1992 = SessionManager.GetSessionByCharacterId(ID);



            MySqlClient.SetParameter("id", ID);
            MySqlClient.SetParameter("modlevel", mod);
            MySqlClient.ExecuteNonQuery("UPDATE usuarios SET moderador = @modlevel WHERE id = @id");
        }

        public static string checkcredits(string username)
        {
            uint ID = CharacterResolverCache.GetUidFromName(username);
            Session session_1;

            session_1 = SessionManager.GetSessionByCharacterId(ID);
            int gold = session_1.CharacterInfo.GoldCoins;

            string str = string.Format("The User {0} Has {1} Credits", username, gold);

            return str;
        }




        public static void givecredits2(SqlDatabaseClient MySqlClient, int Amount, string name)
        {
            uint ID = CharacterResolverCache.GetUidFromName(name);
            CharacterInfo info = CharacterInfoLoader.GetCharacterInfo(MySqlClient, ID);
            int int_0 = info.int_0;

            int_0 += Amount;
            MySqlClient.SetParameter("id", ID);
            MySqlClient.SetParameter("credits", int_0);
            MySqlClient.ExecuteNonQuery("UPDATE usuarios SET creditos_oro = @credits WHERE id = @id");



        }

        public static void Takeaway(SqlDatabaseClient MySqlClient, int Amount, string name)
        {
            uint ID = CharacterResolverCache.GetUidFromName(name);
            CharacterInfo info = CharacterInfoLoader.GetCharacterInfo(MySqlClient, ID);
            int int_0 = info.int_0;

            int_0 -= Amount;
            MySqlClient.SetParameter("id", ID);
            MySqlClient.SetParameter("credits", int_0);
            MySqlClient.ExecuteNonQuery("UPDATE usuarios SET creditos_oro = @credits WHERE id = @id");



        }


    }
}
 

