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
        static Random random_0 = new Random();
        public static bool block = false;


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
                    {
                        Session.SendData(SpaceChatComposer.Compose(0, string.Concat(new object[] { "Tu posici\x00f3n actual es - X: ", actor.Position.Int32_0, ", Y: ", actor.Position.Int32_1, ", Z: ", actor.Position.Int32_2 }), 3, ChatType.Say), false);
                        return true;
                    }

                case "block":
                    if (referenceObject.Staff != 0)
                    {
                        instanceBySpaceId.BroadcastMessage(SpaceChatComposer.Compose(1, actor.Name + " ha bloqueado el punch.", 3, ChatType.Say), 0, false);
                        block = true;
                        return true;
                    }
                        return false;

                case "unblock":
                        if (referenceObject.Staff != 0)
                    {
                        instanceBySpaceId.BroadcastMessage(SpaceChatComposer.Compose(1, actor.Name + " ha desbloqueado el punch.", 3, ChatType.Say), 0, false);
                        block = false;
                        return true;
                    }
                    return false;

                case "ban":
                    if (referenceObject.Staff == 2)
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
                        string ban = string.Format("{0} ha baneado a {1} por {2}",actor.Name, username, reason2);

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
                    if (referenceObject.Staff != 0)
                    {
                        string messageText = InputFilter.FilterString(InputFilter.MergeString(input, 1), false);
                        instanceBySpaceId.BroadcastMessage(SpaceChatComposer.Compose(0, messageText, 3, ChatType.Say), 0, false);
                        return true;
                    }
                    return false;

                case "la":
                    if (referenceObject.Staff != 0)
                    {

                        foreach (KeyValuePair<uint, Session> pair in SessionManager.Sessions)
                        {
                            pair.Value.SendData(LaptopMessageComposer.Compose(pair.Value.CharacterId, Input.Replace("la", "") , 2));
                        }
                        return true;
                    }
                    return false;


                case "give":
                    if (referenceObject.Staff == 2)
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
                        string str = string.Format("{0} le ha dado a {1} {2} Creditos!", userthatadded, username, credits);
                        instanceBySpaceId.BroadcastMessage(SpaceChatComposer.Compose(0, str , 3, ChatType.Say), 0, false);


                        return true;
                    }
                    return false;


                case "take":
                    if (referenceObject.Staff == 2)
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

                        referenceObject.UpdateGoldCreditsBalance(mySqlClient, -credits);
                        session_0.SendData(CharacterCoinsComposer.RemoveGoldCompose(Convert.ToUInt32(credits)), false); //Punch cost 200
                        string str = string.Format("{0} le ha quitado {1} Creditos a {2}!", userthatadded, credits, username);
                        instanceBySpaceId.BroadcastMessage(SpaceChatComposer.Compose(0, str, 3, ChatType.Say), 0, false);


                        return true;
                    }
                    return false;
                    
                    
                case "kick":
                    if (referenceObject.Staff != 0)
                    {
                        Session session_0;

                        Input = Input.Replace("kick", "");
                        Input = Input.Replace(" ", "");
                        uint ID = CharacterResolverCache.GetUidFromName(Input);
                        if (SessionManager.ContainsCharacterId(ID))
                        {
                            session_0 = SessionManager.GetSessionByCharacterId(ID);
                            SpaceManager.RemoveUserFromSpace(session_0);
                            string str = string.Format("{0} echa a {1} de la isla", actor.Name, Input);
                            instanceBySpaceId.BroadcastMessage(SpaceChatComposer.Compose(0, str, 3, ChatType.Say), 0, false);
                        }
                        return true;
                    }
                    return false;

                case "kickoffline":
                    if (referenceObject.Staff == 2)
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
                            string str = string.Format("{0} desconectó a {1}", actor.Name, Input);
                            instanceBySpaceId.BroadcastMessage(SpaceChatComposer.Compose(0, str, 3, ChatType.Say), 0, false);
                        }

                        //SpaceManager.GetInstanceBySpaceId

                        //SpaceUserRemovedComposer.BroadcastCompose(



                        return true;
                    }
                    return false;

                case "chest1":
                   if (referenceObject.Staff == 2)
                   {
                       
                        


                       return true;
                      }
                    return false;

                     
                     
                    
            }


            return false;       
        }

        public static void spawnchest1(Session session_0, ClientMessage clientMessage_0)
        {
             uint itemId = clientMessage_0.ReadUnsignedInteger();
            SpaceInstance instanceBySpaceId = SpaceManager.GetInstanceBySpaceId(session_0.CurrentSpaceId);
            if (instanceBySpaceId != null)
            {
                SpaceActor actorByReferenceId = instanceBySpaceId.GetActorByReferenceId(session_0.CharacterId, SpaceActorType.UserCharacter);
                if (actorByReferenceId != null)
                {
                    CharacterInfo referenceObject = (CharacterInfo)actorByReferenceId.ReferenceObject;
                    try
                    {
                        ContestItem item = instanceBySpaceId.Items[itemId];
                        if ((item != null) && item.IsActive)
                        {
                            using (SqlDatabaseClient client = SqlDatabaseManager.GetClient())
                            {
                                item.CatchItem(client, (int)session_0.CharacterId);
                                string definitionName = item.DefinitionName;

                                instanceBySpaceId.BroadcastChatMessage(actorByReferenceId, string.Concat(new object[] { actorByReferenceId.Name, " ha atrapado un cofre y obtiene: ", item.GoldCredits, " monedas de oro." }), false, 3);
                                instanceBySpaceId.BroadcastChatMessage(actorByReferenceId, string.Concat(new object[] { actorByReferenceId.Name, " has caught a chest and obtained: ", item.GoldCredits, " Gold Credits!" }), false, 3);
                                referenceObject.UpdateGoldCreditsBalance(client, (int)item.GoldCredits);
                                session_0.SendData(CharacterCoinsComposer.AddGoldCompose(item.GoldCredits), false);

                                instanceBySpaceId.BroadcastMessage(SpaceCatchItemComposer.Compose(itemId), 0, false);
                                instanceBySpaceId.BroadcastMessage(SpaceRemoveItemComposer.Compose(itemId), 0, false);


                            }
                        }
                    }
                    catch
                    {
                    }
                    
                }
            }
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


