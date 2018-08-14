namespace BoomBang.Game.Spaces
{
    using BoomBang.Communication;
    using BoomBang.Communication.Incoming;
    using BoomBang.Communication.Outgoing;
    using BoomBang.Game.Characters;
    using BoomBang.Game.Contests;
    using BoomBang.Game.Laptop;
    using BoomBang.Game.Misc.Chat;
    using BoomBang.Game.Sessions;
    using BoomBang.Specialized;
    using BoomBang.Storage;
    using BoomBang.Utils;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;

    public static class SpaceHandler
    {
        public static void Initialize()
        {
            DataRouter.RegisterHandler(FlagcodesIn.SPACES, ItemcodesIn.SPACES_ENTER_SCENE, new ProcessRequestCallback(SpaceHandler.smethod_0), false);
            DataRouter.RegisterHandler(FlagcodesIn.SPACES, ItemcodesIn.SPACES_ENTER_RANDSCENE, new ProcessRequestCallback(SpaceHandler.smethod_0), false);
            DataRouter.RegisterHandler(FlagcodesIn.SPACES, ItemcodesIn.SPACES_LOAD_USERS, new ProcessRequestCallback(SpaceHandler.smethod_2), false);
            DataRouter.RegisterHandler(FlagcodesIn.SPACES, ItemcodesIn.SPACES_EXIT_LAND, new ProcessRequestCallback(SpaceHandler.smethod_3), false);
            DataRouter.RegisterHandler(FlagcodesIn.ISLANDS, ItemcodesIn.ISLAND_CREATE, new ProcessRequestCallback(SpaceHandler.smethod_23), false);
            DataRouter.RegisterHandler(FlagcodesIn.ISLANDS, ItemcodesIn.ISLAND_CREATE_ZONE, new ProcessRequestCallback(SpaceHandler.smethod_25), false);
            DataRouter.RegisterHandler(FlagcodesIn.ISLANDS, ItemcodesIn.ISLAND_PRE, new ProcessRequestCallback(SpaceHandler.smethod_24), false);
            DataRouter.RegisterHandler(FlagcodesIn.SPACE_ITEM, ItemcodesIn.SPACE_ITEM_CATCH, new ProcessRequestCallback(SpaceHandler.smethod_20), false);
            DataRouter.RegisterHandler(FlagcodesIn.USER_ACTIONS, 0, new ProcessRequestCallback(SpaceHandler.smethod_5), false);
            DataRouter.RegisterHandler(FlagcodesIn.USER_ROTATION, 0, new ProcessRequestCallback(SpaceHandler.smethod_7), false);
            DataRouter.RegisterHandler(FlagcodesIn.USER_HOBBYS, 0, new ProcessRequestCallback(SpaceHandler.smethod_8), false);
            DataRouter.RegisterHandler(FlagcodesIn.USER_WISHES, 0, new ProcessRequestCallback(SpaceHandler.smethod_9), false);
            DataRouter.RegisterHandler(FlagcodesIn.USER_VOTES, 0, new ProcessRequestCallback(SpaceHandler.smethod_10), false);
            DataRouter.RegisterHandler(FlagcodesIn.USER_MOTTO, 0, new ProcessRequestCallback(SpaceHandler.smethod_17), false);
            DataRouter.RegisterHandler(FlagcodesIn.USER_INTERACT, ItemcodesIn.USER_INTERACT_SEND, new ProcessRequestCallback(SpaceHandler.smethod_11), false);
            DataRouter.RegisterHandler(FlagcodesIn.USER_INTERACT, ItemcodesIn.USER_INTERACT_ACCEPT, new ProcessRequestCallback(SpaceHandler.smethod_12), false);
            DataRouter.RegisterHandler(FlagcodesIn.USER_INTERACT, ItemcodesIn.USER_INTERACT_CANCEL, new ProcessRequestCallback(SpaceHandler.smethod_13), false);
            DataRouter.RegisterHandler(FlagcodesIn.USER_INTERACT, ItemcodesIn.TARGETUSER_INTERACT_CANCEL, new ProcessRequestCallback(SpaceHandler.smethod_14), false);
            DataRouter.RegisterHandler(FlagcodesIn.SEND_UPPERCUT, 0, new ProcessRequestCallback(SpaceHandler.smethod_21), false);
            DataRouter.RegisterHandler(FlagcodesIn.SEND_COCONUT, 0, new ProcessRequestCallback(SpaceHandler.smethod_22), false);
            DataRouter.RegisterHandler(FlagcodesIn.LATENCY_TEST, 0, new ProcessRequestCallback(SpaceHandler.smethod_6), false);
            DataRouter.RegisterHandler(FlagcodesIn.WALK, 0, new ProcessRequestCallback(SpaceHandler.smethod_4), false);
            DataRouter.RegisterHandler(FlagcodesIn.USER_CHAT, 0, new ProcessRequestCallback(SpaceHandler.smethod_18), false);
            DataRouter.RegisterHandler(FlagcodesIn.USER_WHISPER, 0, new ProcessRequestCallback(SpaceHandler.smethod_19), false);
            DataRouter.RegisterHandler(FlagcodesIn.COLOR_UPPERCUT, 0, new ProcessRequestCallback(SpaceHandler.smethod_15), false);
            DataRouter.RegisterHandler(FlagcodesIn.COLOR_COCONUT, 0, new ProcessRequestCallback(SpaceHandler.smethod_16), false);
        }

        private static void smethod_0(Session session_0, ClientMessage clientMessage_0)
        {
            clientMessage_0.ReadUnsignedInteger();
            uint num = clientMessage_0.ReadUnsignedInteger();
            session_0.AbsoluteSpaceId = num;
            smethod_1(session_0, num, "", false);
        }

        private static void smethod_1(Session session_0, uint uint_0, string string_0 = "", bool bool_0 = false)
        {
            SpaceManager.RemoveUserFromSpace(session_0, false);
            SpaceInfo spaceInfo = SpaceInfoLoader.GetSpaceInfo(uint_0);
            if (spaceInfo == null)
            {
                session_0.SendData(SpaceFullComposer.Compose(), false);
            }
            else
            {
                if (spaceInfo.ParentId == 0)
                {
                    int num = new Random().Next(0, spaceInfo.SubIds.Count - 1);
                    SpaceInfo info2 = spaceInfo;
                    spaceInfo = SpaceInfoLoader.GetSpaceInfo(info2.SubIds[num]);
                }
                if (spaceInfo == null)
                {
                    session_0.SendData(SpaceFullComposer.Compose(), false);
                }
                else if (spaceInfo.TryGetModel() == null)
                {
                    session_0.SendData(SpaceFullComposer.Compose(), false);
                }
                else if ((spaceInfo.CurrentUsers < spaceInfo.MaxUsers) && (spaceInfo.CurrentUsers < 0x15))
                {
                    session_0.AbsoluteSpaceId = spaceInfo.UInt32_0;
                    session_0.AbsoluteSpaceName = spaceInfo.Name;
                    session_0.SpaceAuthed = bool_0 || (spaceInfo.OwnerId == session_0.CharacterId);
                    session_0.SpaceJoined = false;
                    session_0.SendData(SpaceLoadPublicSceneComposer.Compose(spaceInfo), false);
                }
                else
                {
                    session_0.SendData(SpaceFullComposer.Compose(), false);
                }
            }
        }

        private static void smethod_10(Session session_0, ClientMessage clientMessage_0)
        {
            uint actorId = clientMessage_0.ReadUnsignedInteger();
            uint colorId = clientMessage_0.ReadUnsignedInteger();
            int vote = clientMessage_0.ReadInteger();
            SpaceInstance instanceBySpaceId = SpaceManager.GetInstanceBySpaceId(session_0.CurrentSpaceId);
            if (instanceBySpaceId != null)
            {
                SpaceActor actorByReferenceId = instanceBySpaceId.GetActorByReferenceId(session_0.CharacterId, SpaceActorType.UserCharacter);
                if (actorByReferenceId != null)
                {
                    CharacterInfo referenceObject = (CharacterInfo)actorByReferenceId.ReferenceObject;
                    using (SqlDatabaseClient client = SqlDatabaseManager.GetClient())
                    {
                        referenceObject.UpdateVotes(client, colorId, vote);
                    }
                    instanceBySpaceId.BroadcastMessage(SpaceUserVoteUpdateComposer.Compose(actorId, colorId, vote), 0, false);
                }
            }
        }

        private static void smethod_11(Session session_0, ClientMessage clientMessage_0)
        {
            uint actionId = clientMessage_0.ReadUnsignedInteger();
            uint actorId = clientMessage_0.ReadUnsignedInteger();
            SpaceInstance instanceBySpaceId = SpaceManager.GetInstanceBySpaceId(session_0.CurrentSpaceId);
            if (instanceBySpaceId != null)
            {
                SpaceActor actorByReferenceId = instanceBySpaceId.GetActorByReferenceId(session_0.CharacterId, SpaceActorType.UserCharacter);
                SpaceActor actor = instanceBySpaceId.GetActor(actorId);
                if ((actor != null) && (actorByReferenceId != null))
                {
                    Session sessionByCharacterId = SessionManager.GetSessionByCharacterId(actor.ReferenceId);
                    if (sessionByCharacterId != null)
                    {
                        sessionByCharacterId.SendData(SpaceUserSendActionComposer.Compose(actionId, actorByReferenceId.UInt32_0), false);
                    }
                }
            }
        }

        private static void smethod_12(Session session_0, ClientMessage clientMessage_0)
        {
            uint actionId = clientMessage_0.ReadUnsignedInteger();
            uint actorId = clientMessage_0.ReadUnsignedInteger();
            SpaceInstance instanceBySpaceId = SpaceManager.GetInstanceBySpaceId(session_0.CurrentSpaceId);
            if (instanceBySpaceId == null)
            {
                return;
            }
            SpaceActor actorByReferenceId = instanceBySpaceId.GetActorByReferenceId(session_0.CharacterId, SpaceActorType.UserCharacter);
            SpaceActor actor = instanceBySpaceId.GetActor(actorId);
            if ((actor == null) || (actorByReferenceId == null))
            {
                return;
            }
            CharacterInfo referenceObject = (CharacterInfo)actorByReferenceId.ReferenceObject;
            CharacterInfo info2 = (CharacterInfo)actor.ReferenceObject;
            if (actorByReferenceId.IsLocked || actor.IsLocked)
            {
                return;
            }
            actorByReferenceId.StopMoving();
            actor.StopMoving();
            if (SessionManager.GetSessionByCharacterId(actor.ReferenceId) == null)
            {
                return;
            }
            using (SqlDatabaseClient client = SqlDatabaseManager.GetClient())
            {
                switch (actionId)
                {
                    case 1:
                        referenceObject.UpdateKisses(client, false);
                        info2.UpdateKisses(client, true);
                        actor.Lock(3, false, false);
                        actorByReferenceId.Lock(3, false, false);
                        goto Label_0165;

                    case 2:
                        referenceObject.UpdateCocktails(client, false);
                        info2.UpdateCocktails(client, true);
                        actor.Lock(9, false, false);
                        actorByReferenceId.Lock(9, false, false);
                        goto Label_0165;

                    case 3:
                        referenceObject.UpdateRoses(client, false);
                        info2.UpdateRoses(client, true);
                        actor.Lock(4, false, false);
                        actorByReferenceId.Lock(4, false, false);
                        goto Label_0165;
                }
                referenceObject.UpdateKisses(client, false);
                info2.UpdateKisses(client, true);
                actor.Lock(3, false, false);
                actorByReferenceId.Lock(3, false, false);
            }
        Label_0165:
            instanceBySpaceId.BroadcastMessage(SpaceUpdateUserStatistics.Compose(actorByReferenceId, actionId), 0, false);
            instanceBySpaceId.BroadcastMessage(SpaceUpdateUserStatistics.Compose(actor, actionId), 0, false);
            instanceBySpaceId.BroadcastMessage(SpaceUserAcceptInteract.Compose(actor, actorByReferenceId, actionId), 0, false);
        }

        private static void smethod_13(Session session_0, ClientMessage clientMessage_0)
        {
            clientMessage_0.ReadUnsignedInteger();
            uint actorId = clientMessage_0.ReadUnsignedInteger();
            SpaceInstance instanceBySpaceId = SpaceManager.GetInstanceBySpaceId(session_0.CurrentSpaceId);
            if (instanceBySpaceId != null)
            {
                SpaceActor actorByReferenceId = instanceBySpaceId.GetActorByReferenceId(session_0.CharacterId, SpaceActorType.UserCharacter);
                SpaceActor actor = instanceBySpaceId.GetActor(actorId);
                if ((actor != null) && (actorByReferenceId != null))
                {
                    Session sessionByCharacterId = SessionManager.GetSessionByCharacterId(actor.ReferenceId);
                    if (sessionByCharacterId != null)
                    {
                        sessionByCharacterId.SendData(SpaceUserCancelActionComposer.SenderCompose(actorByReferenceId.UInt32_0), false);
                    }
                }
            }
        }

        private static void smethod_14(Session session_0, ClientMessage clientMessage_0)
        {
            clientMessage_0.ReadUnsignedInteger();
            uint actorId = clientMessage_0.ReadUnsignedInteger();
            SpaceInstance instanceBySpaceId = SpaceManager.GetInstanceBySpaceId(session_0.CurrentSpaceId);
            if (instanceBySpaceId != null)
            {
                SpaceActor actorByReferenceId = instanceBySpaceId.GetActorByReferenceId(session_0.CharacterId, SpaceActorType.UserCharacter);
                SpaceActor actor = instanceBySpaceId.GetActor(actorId);
                if ((actor != null) && (actorByReferenceId != null))
                {
                    Session sessionByCharacterId = SessionManager.GetSessionByCharacterId(actor.ReferenceId);
                    if (sessionByCharacterId != null)
                    {
                        sessionByCharacterId.SendData(SpaceUserCancelActionComposer.TargetCompose(actorByReferenceId.UInt32_0), false);
                    }
                }
            }
        }

        private static void smethod_15(Session session_0, ClientMessage clientMessage_0)
        {
            uint color = clientMessage_0.ReadUnsignedInteger();
            SpaceInstance instanceBySpaceId = SpaceManager.GetInstanceBySpaceId(session_0.CurrentSpaceId);
            if (instanceBySpaceId != null)
            {
                instanceBySpaceId.BroadcastMessage(UpdateUppercutColor.Compose(session_0.CharacterId, color), 0, false);
            }
        }

        private static void smethod_16(Session session_0, ClientMessage clientMessage_0)
        {
            uint color = clientMessage_0.ReadUnsignedInteger();
            SpaceInstance instanceBySpaceId = SpaceManager.GetInstanceBySpaceId(session_0.CurrentSpaceId);
            if (instanceBySpaceId != null)
            {
                instanceBySpaceId.BroadcastMessage(UpdateCoconutColor.Compose(session_0.CharacterId, color), 0, false);
            }
        }

        private static void smethod_17(Session session_0, ClientMessage clientMessage_0)
        {
            clientMessage_0.ReadUnsignedInteger();
            string motto = clientMessage_0.ReadString();
            SpaceInstance instanceBySpaceId = SpaceManager.GetInstanceBySpaceId(session_0.CurrentSpaceId);
            if (instanceBySpaceId != null)
            {
                SpaceActor actorByReferenceId = instanceBySpaceId.GetActorByReferenceId(session_0.CharacterId, SpaceActorType.UserCharacter);
                if (actorByReferenceId != null)
                {
                    CharacterInfo referenceObject = (CharacterInfo)actorByReferenceId.ReferenceObject;
                    using (SqlDatabaseClient client = SqlDatabaseManager.GetClient())
                    {
                        referenceObject.UpdateMotto(client, motto);
                    }
                    instanceBySpaceId.BroadcastMessage(SpaceUserMottoUpdateComposer.Compose(actorByReferenceId.UInt32_0, motto), 0, false);
                }
            }
        }

        private static void smethod_18(Session session_0, ClientMessage clientMessage_0)
        {
            if (!session_0.CharacterInfo.IsMuted)
            {
                SpaceInstance instanceBySpaceId = SpaceManager.GetInstanceBySpaceId(session_0.CurrentSpaceId);
                if (instanceBySpaceId != null)
                {
                    SpaceActor actorByReferenceId = instanceBySpaceId.GetActorByReferenceId(session_0.CharacterId, SpaceActorType.UserCharacter);
                    if (actorByReferenceId != null)
                    {
                        CharacterInfo referenceObject = (CharacterInfo)actorByReferenceId.ReferenceObject;
                        clientMessage_0.ReadUnsignedInteger();
                        
                        string input = InputFilter.FilterString(clientMessage_0.ReadString().Trim(), false);
                        int messageColor = clientMessage_0.ReadInteger();
                        if (input.Length != 0)
                        {

                            if (input.Length > 100)
                            {
                                input = input.Substring(0, 100);
                            }

                            if (referenceObject.Staff == 0)
                            {
                                messageColor = 1;
                            }

                            if (referenceObject.Staff == 1)
                            {
                                messageColor = 2;
                            }

                            if (referenceObject. Staff == 2)
                            {
                                messageColor = 7;
                            }

                            if (referenceObject.Staff == 3)
                            {
                                messageColor = 1;
                            }

                            if (referenceObject.Staff == 4)
                            {
                                messageColor = 9;
                            }

                            if (referenceObject.Staff == 5)
                            {
                                messageColor = 10;
                            }

                            if (referenceObject.Staff == 6)
                            {
                                messageColor = 8;
                            }

                            if (!input.StartsWith("@") || !ChatCommands.HandleCommand(session_0, input))
                            {
                                actorByReferenceId.Chat(input, messageColor, referenceObject.Staff == 1);
                            }

                            //1 white, 2 gold, 6 pink, 7 red, 8 blue, 9 orange, 10 green


                            /*  
                           
                                White	 = 		Normal
                                Gold	 = 		RetroMods
                                Green    = 		Designer 
                                Blue     = 		Translators
                                Pink     = 		VIP
                                Red 	 = 		Admin
                                Orange   = 		XAT Staff
                             
                             */

                        }
                    }
                }
            }
        }

        private static void smethod_19(Session session_0, ClientMessage clientMessage_0)
        {
            if (!session_0.CharacterInfo.IsMuted)
            {
                uint actorId = clientMessage_0.ReadUnsignedInteger();
                clientMessage_0.ReadUnsignedInteger();
                string messageText = InputFilter.FilterString(clientMessage_0.ReadString().Trim(), false);
                clientMessage_0.ReadUnsignedInteger();
                SpaceInstance instanceBySpaceId = SpaceManager.GetInstanceBySpaceId(session_0.CurrentSpaceId);
                if (instanceBySpaceId != null)
                {
                    SpaceActor actorByReferenceId = instanceBySpaceId.GetActorByReferenceId(session_0.CharacterId, SpaceActorType.UserCharacter);
                    if (actorByReferenceId != null)
                    {
                        SpaceActor actor = instanceBySpaceId.GetActor(actorId);
                        if ((actor != null) && (messageText.Length != 0))
                        {
                            if (messageText.Length > 100)
                            {
                                messageText = messageText.Substring(0, 100);
                            }
                            uint referenceId = actor.ReferenceId;
                            if (referenceId > 0)
                            {
                                actorByReferenceId.Whisper(messageText, referenceId, false);
                            }
                        }
                    }
                }
            }
        }

        private static void smethod_2(Session session_0, ClientMessage clientMessage_0)
        {
            if (session_0 != null)
            {
                if (!SpaceManager.InstanceIsLoadedForSpace(session_0.AbsoluteSpaceId))
                {
                    if (SpaceManager.TryLoadSpaceInstance(session_0.AbsoluteSpaceId))
                    {
                    }
                    else
                    {
                        Console.WriteLine("ERROR");
                    }
                }
                SpaceInstance instanceBySpaceId = SpaceManager.GetInstanceBySpaceId(session_0.AbsoluteSpaceId);
                if (instanceBySpaceId != null)
                {
                    session_0.SpaceAuthed = true;
                }
                if (((instanceBySpaceId != null) && !session_0.SpaceJoined) && (session_0.SpaceAuthed && !instanceBySpaceId.Info.BlackList.Contains(session_0.CharacterInfo.Username)))
                {
                    if (!instanceBySpaceId.AddUserToSpace(session_0))
                    {
                        SpaceManager.RemoveUserFromSpace(session_0, true);
                    }
                    else
                    {
                        session_0.SpaceAuthed = true;
                        session_0.SpaceJoined = true;
                        instanceBySpaceId.SendObjects(session_0);
                        LaptopHandler.MarkUpdateNeeded(session_0, 0, false);
                        SpaceActor actorByReferenceId = instanceBySpaceId.GetActorByReferenceId(session_0.CharacterId, SpaceActorType.UserCharacter);
                        if (actorByReferenceId == null)
                        {
                            session_0.SendData(SpaceUserRemovedComposer.SingleCompose());
                        }
                        else
                        {
                            instanceBySpaceId.BroadcastMessage(SpaceObjectListComposer.BroadcastCompose(actorByReferenceId), session_0.CharacterId, true);
                        }
                    }
                }
                else
                {
                    session_0.SendData(SpaceUserRemovedComposer.SingleCompose());
                }
            }
        }

        public static void smethod_20(Session session_0, ClientMessage clientMessage_0)
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
                                if (definitionName != null)
                                {
                                    if (definitionName != "cofre_oro")
                                    {
                                        if (!(definitionName == "cofre_plata"))
                                        {
                                            if (definitionName == "")
                                            {
                                                instanceBySpaceId.BroadcastChatMessage(actorByReferenceId, actorByReferenceId.Name + " ha cogido un diamante.", false, 3);
                                                instanceBySpaceId.BroadcastChatMessage(actorByReferenceId, actorByReferenceId.Name + " has caught a diamond", false, 3);

                                            }
                                            else
                                            {
                                                instanceBySpaceId.BroadcastChatMessage(actorByReferenceId, string.Concat(new object[] { actorByReferenceId.Name, " ha atrapado un cofre y obtiene: ", item.SilverCredits, " monedas de plata." }), false, 3);
                                                instanceBySpaceId.BroadcastChatMessage(actorByReferenceId, string.Concat(new object[] { actorByReferenceId.Name, " has caught a chest and obtained: ", item.GoldCredits, " silver credits." }), false, 3);
                                                referenceObject.UpdateSilverCreditsBalance(client, (int)item.SilverCredits);
                                                session_0.SendData(CharacterCoinsComposer.AddSilverCompose(item.SilverCredits), false);
                                            }
                                        }
                                        else
                                        {
                                            instanceBySpaceId.BroadcastChatMessage(actorByReferenceId, string.Concat(new object[] { actorByReferenceId.Name, " ha atrapado un cofre y obtiene: ", item.GoldCredits, " monedas de oro." }), false, 3);
                                            instanceBySpaceId.BroadcastChatMessage(actorByReferenceId, string.Concat(new object[] { actorByReferenceId.Name, " has caught a chest and obtained: ", item.GoldCredits, " gold credits." }), false, 3);
                                            referenceObject.UpdateGoldCreditsBalance(client, (int)item.GoldCredits);
                                            session_0.SendData(CharacterCoinsComposer.AddGoldCompose(item.GoldCredits), false);
                                        }
                                    }
                                }
                                instanceBySpaceId.BroadcastMessage(SpaceCatchItemComposer.Compose(itemId), 0, false);
                                instanceBySpaceId.BroadcastMessage(SpaceRemoveItemComposer.Compose(itemId), 0, false);
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }



        private static void smethod_21(Session session_0, ClientMessage clientMessage_0)
        {
            clientMessage_0.ReadInteger();
            uint actorId = clientMessage_0.ReadUnsignedInteger();
            clientMessage_0.ReadInteger();
            clientMessage_0.ReadInteger();
            uint num2 = clientMessage_0.ReadUnsignedInteger();
            clientMessage_0.ReadInteger();
            clientMessage_0.ReadInteger();
            SpaceInstance instanceBySpaceId = SpaceManager.GetInstanceBySpaceId(session_0.CurrentSpaceId);
            
            if (instanceBySpaceId != null)
            {
                SpaceActor actor = instanceBySpaceId.GetActor(actorId);
                SpaceActor actor2 = instanceBySpaceId.GetActor(num2);
                uint ID1 = CharacterResolverCache.GetUidFromName(actor.Name);
                uint ID2 = CharacterResolverCache.GetUidFromName(actor2.Name);
                if ((actor != null) && (actor2 != null) )
                {
                    CharacterInfo referenceObject = (CharacterInfo)actor.ReferenceObject;
                    CharacterInfo info2 = (CharacterInfo)actor2.ReferenceObject;
                    if (!actor.IsLocked && !actor2.IsLocked && ChatCommands.block_upper == false && !ChatCommands.UpperBlock.Contains(actor2.Name) && !ChatCommands.UpperBlockUser.Contains(ID1) && !ChatCommands.UpperBlockUser.Contains(ID2))
                    {
                        actor.StopMoving();
                        using (SqlDatabaseClient client = SqlDatabaseManager.GetClient())
                        {
                            if (referenceObject.Staff == 1)
                            {
                                referenceObject.UpdateGoldCreditsBalance(client, -250);//Punch cost 500
                                referenceObject.UpdateUppercuts(client, true);
                                info2.UpdateUppercuts(client, false);
                                session_0.SendData(CharacterCoinsComposer.RemoveGoldCompose(250), false);//Punch cost 500
                                instanceBySpaceId.BroadcastMessage(SpaceUpdateUserStatistics.Compose(actor, 4), 0, false);
                                instanceBySpaceId.BroadcastMessage(SpaceUpdateUserStatistics.Compose(actor2, 4), 0, false);
                                actor.Lock(13, false, false);
                                actor2.Lock(10, true, false);
                                instanceBySpaceId.BroadcastMessage(SpaceUserSendUppercut.Compose(actor, actor2), 0, false);
                            }
                            else
                            {
                                referenceObject.UpdateGoldCreditsBalance(client, -250);//Punch cost 200
                                referenceObject.UpdateUppercuts(client, true);
                                info2.UpdateUppercuts(client, false);
                                session_0.SendData(CharacterCoinsComposer.RemoveGoldCompose(250), false);//Punch cost 200
                                instanceBySpaceId.BroadcastMessage(SpaceUpdateUserStatistics.Compose(actor, 4), 0, false);
                                instanceBySpaceId.BroadcastMessage(SpaceUpdateUserStatistics.Compose(actor2, 4), 0, false);
                                actor.Lock(13, false, false);
                                actor2.Lock(10, true, false);
                                instanceBySpaceId.BroadcastMessage(SpaceUserSendUppercut.Compose(actor, actor2), 0, false);
                            }
                            
                        }
                    }
                }
            }
        }

        private static void smethod_22(Session session_0, ClientMessage clientMessage_0)
        {

            uint actorId = clientMessage_0.ReadUnsignedInteger();
            uint num2 = clientMessage_0.ReadUnsignedInteger();
            SpaceInstance instanceBySpaceId = SpaceManager.GetInstanceBySpaceId(session_0.CurrentSpaceId);
            if (instanceBySpaceId != null)
            {
                SpaceActor actor = instanceBySpaceId.GetActor(actorId);
                SpaceActor actor2 = instanceBySpaceId.GetActor(num2);
                if ((actor != null) && (actor2 != null) && ChatCommands.block_coco == false)
                {
                    CharacterInfo referenceObject = (CharacterInfo)actor.ReferenceObject;
                    CharacterInfo info2 = (CharacterInfo)actor2.ReferenceObject;
                    if (!actor2.IsLocked)
                    {
                        using (SqlDatabaseClient client = SqlDatabaseManager.GetClient())
                        {
                            referenceObject.UpdateGoldCreditsBalance(client, -200);//Coconut cost 10
                            referenceObject.UpdateCoconuts(client, true);
                            info2.UpdateCoconuts(client, false);
                            session_0.SendData(CharacterCoinsComposer.RemoveGoldCompose(200), false);//Coconut cost 10
                            instanceBySpaceId.BroadcastMessage(SpaceUpdateUserStatistics.Compose(actor, 5), 0, false);
                            instanceBySpaceId.BroadcastMessage(SpaceUpdateUserStatistics.Compose(actor2, 5), 0, false);
                            actor2.Lock(6, false, true);
                            instanceBySpaceId.BroadcastMessage(SpaceUserSendCoconut.SendAndBlock(actor2.ReferenceId), 0, false);
                        }
                    }
                }
            }
        }

        private static void smethod_23(Session session_0, ClientMessage clientMessage_0)
        {
            string str = clientMessage_0.ReadString();
            uint num = clientMessage_0.ReadUnsignedInteger();
            using (SqlDatabaseClient client = SqlDatabaseManager.GetClient())
            {
                client.SetParameter("name", str);
                if (client.ExecuteQueryRow("SELECT id FROM islas WHERE nombre = @name AND id_parent = '0' LIMIT 1") != null)
                {
                    session_0.SendData(CreateIslandComposer.TakenNameComposer(), false);
                }
                else
                {
                    client.SetParameter("name", str);
                    client.SetParameter("model", num);
                    client.SetParameter("userid", session_0.CharacterId);
                    client.ExecuteNonQuery("INSERT INTO islas (nombre,modelo_area,id_usuario) VALUES (@name, @model, @userid)");
                    client.SetParameter("name", str);
                    DataRow row2 = client.ExecuteQueryRow("SELECT id FROM islas WHERE nombre = @name AND id_parent = '0' LIMIT 1");
                    if (row2 != null)
                    {
                        session_0.SendData(CreateIslandComposer.Compose((uint)row2["id"]), false);
                    }
                }
            }
        }

        private static void smethod_24(Session session_0, ClientMessage clientMessage_0)
        {
            uint num = clientMessage_0.ReadUnsignedInteger();
            using (SqlDatabaseClient client = SqlDatabaseManager.GetClient())
            {
                client.SetParameter("islandid", num);
                DataRow island = client.ExecuteQueryRow("SELECT * FROM islas WHERE id = @islandid LIMIT 1");
                if (island != null)
                {
                    session_0.SendData(PreEnterIslandComposer.Compose(session_0.CharacterInfo, island), false);
                }
            }
        }

        private static void smethod_25(Session session_0, ClientMessage clientMessage_0)
        {
        }

        private static void smethod_3(Session session_0, ClientMessage clientMessage_0)
        {
            SpaceInstance instanceBySpaceId = SpaceManager.GetInstanceBySpaceId(session_0.CurrentSpaceId);
            if (instanceBySpaceId != null)
            {
                SpaceActor actorByReferenceId = instanceBySpaceId.GetActorByReferenceId(session_0.CharacterId, SpaceActorType.UserCharacter);
                if ((actorByReferenceId != null) && !actorByReferenceId.IsLocked)
                {
                    SpaceManager.RemoveUserFromSpace(session_0, true);
                }
            }
        }

        private static void smethod_4(Session session_0, ClientMessage clientMessage_0)
        {
            SpaceInstance instanceBySpaceId = SpaceManager.GetInstanceBySpaceId(session_0.CurrentSpaceId);
            if (instanceBySpaceId != null)
            {
                SpaceActor actorByReferenceId = instanceBySpaceId.GetActorByReferenceId(session_0.CharacterId, SpaceActorType.UserCharacter);
                if ((actorByReferenceId != null) && !actorByReferenceId.IsLocked)
                {
                    clientMessage_0.ReadUnsignedInteger();
                    string str = clientMessage_0.ReadString();
                    string str2 = str;
                    List<Vector3> stepList = new List<Vector3>();
                    while (str2 != "")
                    {
                        stepList.Add(new Vector3(int.Parse(str2.Substring(0, 2)), int.Parse(str2.Substring(2, 2)), int.Parse(str2.Substring(4, 1))));
                        str2 = str2.Substring(5);
                    }
                    Vector3 toPosition = new Vector3(int.Parse(str.Substring(str.Length - 5, 2)), int.Parse(str.Substring(str.Length - 3, 2)), int.Parse(str.Substring(str.Length - 1, 1)));
                    actorByReferenceId.MoveTo(stepList, toPosition, false, false, false);
                }
            }
        }

        private static void smethod_5(Session session_0, ClientMessage clientMessage_0)
        {
            clientMessage_0.ReadUnsignedInteger();
            uint actionId = clientMessage_0.ReadUnsignedInteger();
            SpaceInstance instanceBySpaceId = SpaceManager.GetInstanceBySpaceId(session_0.CurrentSpaceId);
            if (instanceBySpaceId != null)
            {
                SpaceActor actorByReferenceId = instanceBySpaceId.GetActorByReferenceId(session_0.CharacterId, SpaceActorType.UserCharacter);
                if ((actorByReferenceId != null) && !actorByReferenceId.IsPreLocked)
                {
                    actorByReferenceId.PreLock(2);
                    instanceBySpaceId.BroadcastMessage(SpaceUserActionComposer.Compose(session_0.CharacterId, actionId), 0, false);
                }
            }
        }

        private static void smethod_6(Session session_0, ClientMessage clientMessage_0)
        {
            session_0.SendData(SpaceLatencyTestComposer.Compose(), false);
        }

        private static void smethod_7(Session session_0, ClientMessage clientMessage_0)
        {
            clientMessage_0.ReadUnsignedInteger();
            int rotation = clientMessage_0.ReadInteger();
            SpaceInstance instanceBySpaceId = SpaceManager.GetInstanceBySpaceId(session_0.CurrentSpaceId);
            if (instanceBySpaceId != null)
            {
                SpaceActor actorByReferenceId = instanceBySpaceId.GetActorByReferenceId(session_0.CharacterId, SpaceActorType.UserCharacter);
                if ((actorByReferenceId != null) && !actorByReferenceId.IsLocked)
                {
                    instanceBySpaceId.BroadcastMessage(SpaceUserRotationComposer.Compose(actorByReferenceId.UInt32_0, actorByReferenceId.Position.Int32_0, actorByReferenceId.Position.Int32_1, rotation), 0, false);
                }
            }
        }

        private static void smethod_8(Session session_0, ClientMessage clientMessage_0)
        {
            clientMessage_0.ReadUnsignedInteger();
            uint labelId = clientMessage_0.ReadUnsignedInteger();
            string mHobby = clientMessage_0.ReadString();
            SpaceInstance instanceBySpaceId = SpaceManager.GetInstanceBySpaceId(session_0.CurrentSpaceId);
            if (instanceBySpaceId != null)
            {
                SpaceActor actorByReferenceId = instanceBySpaceId.GetActorByReferenceId(session_0.CharacterId, SpaceActorType.UserCharacter);
                if (actorByReferenceId != null)
                {
                    CharacterInfo referenceObject = (CharacterInfo)actorByReferenceId.ReferenceObject;
                    using (SqlDatabaseClient client = SqlDatabaseManager.GetClient())
                    {
                        referenceObject.UpdateHobbies(client, labelId, mHobby);
                    }
                    instanceBySpaceId.BroadcastMessage(SpaceUserHobbyUpdateComposer.Compose(actorByReferenceId.UInt32_0, labelId, mHobby), 0, false);
                }
            }
        }

        private static void smethod_9(Session session_0, ClientMessage clientMessage_0)
        {
            clientMessage_0.ReadUnsignedInteger();
            uint labelId = clientMessage_0.ReadUnsignedInteger();
            string mWish = clientMessage_0.ReadString();
            SpaceInstance instanceBySpaceId = SpaceManager.GetInstanceBySpaceId(session_0.CurrentSpaceId);
            if (instanceBySpaceId != null)
            {
                SpaceActor actorByReferenceId = instanceBySpaceId.GetActorByReferenceId(session_0.CharacterId, SpaceActorType.UserCharacter);
                if (actorByReferenceId != null)
                {
                    CharacterInfo referenceObject = (CharacterInfo)actorByReferenceId.ReferenceObject;
                    using (SqlDatabaseClient client = SqlDatabaseManager.GetClient())
                    {
                        referenceObject.UpdateWishes(client, labelId, mWish);
                    }
                    instanceBySpaceId.BroadcastMessage(SpaceUserWishUpdateComposer.Compose(actorByReferenceId.UInt32_0, labelId, mWish), 0, false);
                }
            }
        }
    }
}

