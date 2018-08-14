using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Threading;
using Snowlight.Game.Contest;
using Snowlight.Specialized;
using Snowlight.Game.Sessions;
using Snowlight.Communication;
using Snowlight.Storage;
using Snowlight.Communication.Outgoing.Chat;
using System.Data;
using Snowlight.Communication.Outgoing;
using Snowlight.Communication.Outgoing.Spaces;

namespace Snowlight.Game.Spaces
{
    public class SpaceInstance
    {
        internal bool bool_0;
        /* private scope */
        bool bool_1;
        /* private scope */
        bool bool_2;
        /* private scope */
        ConcurrentDictionary<uint, SpaceActor> concurrentDictionary_0;
        /* private scope */
        Dictionary<uint, ContestItem> dictionary_0;
        /* private scope */
        Dictionary<uint, ContestItem> dictionary_1;
        /* private scope */
        Dictionary<uint, double> dictionary_2;
        /* private scope */
        double double_0;
        /* private scope */
        static Func<KeyValuePair<uint, SpaceActor>, uint> func_0;
        /* private scope */
        static Func<KeyValuePair<uint, SpaceActor>, SpaceActor> func_1;
        /* private scope */
        const int int_0 = 0x2bf20;
        /* private scope */
        int int_1;
        /* private scope */
        int int_2;
        /* private scope */
        int int_3;
        /* private scope */
        int int_4;
        /* private scope */
        int int_5;
        /* private scope */
        List<SpaceActor>[,] list_0;
        /* private scope */
        List<uint> list_1;
        internal List<uint> list_2;
        /* private scope */
        object object_0;
        /* private scope */
        object object_1 = new object();
        /* private scope */
        object object_2;
        internal SpaceInfo spaceInfo_0;
        internal SpaceModel spaceModel_0;
        /* private scope */
        string string_0;
        /* private scope */
        Thread thread_0;
        /* private scope */
        TileState[,] tileState_0;
        /* private scope */
        uint uint_0;
        /* private scope */
        uint uint_1;
        /* private scope */
        UserMovementNode[,] userMovementNode_0;
        /* private scope */
        Vector2[,] vector2_0;

        public SpaceInstance(uint InstanceId, SpaceInfo Info, SpaceModel Model)
        {
            this.uint_0 = InstanceId;
            this.spaceInfo_0 = Info;
            this.concurrentDictionary_0 = new ConcurrentDictionary<uint, SpaceActor>();
            this.spaceModel_0 = Model;
            this.string_0 = string.Empty;
            this.list_2 = new List<uint> { 
                1, 2, 3, 4, 5, 6, 6, 8, 9, 10, 11, 12, 13, 14, 15, 0x10, 
                0x11, 0x12, 0x13, 20, 0x15
             };
            this.object_2 = new object();
            this.tileState_0 = new TileState[this.spaceModel_0.Heightmap.SizeX, this.spaceModel_0.Heightmap.SizeY];
            this.list_0 = new List<SpaceActor>[this.spaceModel_0.Heightmap.SizeX, this.spaceModel_0.Heightmap.SizeY];
            SqlDatabaseClient mySqlClient = SqlDatabaseManager.GetClient();
            try
            {
                this.InitializeContestWorker(mySqlClient);
            }
            catch (Exception exception)
            {
                Output.WriteLine("[ContestMgr] An exception has been trhown while trying to start the worker. The thread was already destroyed. Stack trace:\r\n" + exception.ToString(), OutputLevel.CriticalError);
            }
            finally
            {
                if (mySqlClient != null)
                {
                    mySqlClient.Dispose();
                }
            }
            this.RegenerateRelativeHeightmap(false);
        }

        public bool AddUserToSpace(Session Session)
        {
            if ((Session.AbsoluteSpaceId == this.SpaceId) && Session.Authenticated)
            {
                if (this.HumanActorCount >= this.spaceInfo_0.MaxUsers)
                {
                    Session.SendData(SpaceUserRemovedComposer.SingleCompose());
                    return false;
                }
                uint num = this.method_3();
                if ((num != 0) && (num <= 0x15))
                {
                    Vector3 position = new Vector3(this.spaceModel_0.DoorPosition.Int32_0, this.spaceModel_0.DoorPosition.Int32_1, 0);
                    SpaceActor actor = SpaceActor.TryCreateActor(num, SpaceActorType.UserCharacter, Session.CharacterId, Session.CharacterInfo, position, this.spaceModel_0.CharacterRotation, this);
                    if (actor == null)
                    {
                        return false;
                    }
                    this.method_4(actor);
                    if (Session.CurrentEffect > 0)
                    {
                        actor.ApplyEffect(Session.CurrentEffect, true);
                    }
                    actor.UpdateNeeded = true;
                    return true;
                }
                Session.SendData(SpaceUserRemovedComposer.SingleCompose());
                return false;
            }
            Session.SendData(SpaceUserRemovedComposer.SingleCompose());
            return false;
        }

        public void BroadcastChatMessage(SpaceActor Actor, string MessageText, bool Whisper, int MessageColor)
        {
            lock (this.object_1)
            {
                foreach (SpaceActor actor in this.concurrentDictionary_0.Values)
                {
                    ServerMessage message = SpaceChatComposer.Compose(Actor.UInt32_0, MessageText, MessageColor, Whisper ? ChatType.Whisper : ChatType.Say);
                    if (actor.Type == SpaceActorType.UserCharacter)
                    {
                        Session sessionByCharacterId = SessionManager.GetSessionByCharacterId(actor.ReferenceId);
                        if ((sessionByCharacterId != null) && ((Actor.Type != SpaceActorType.UserCharacter) || !sessionByCharacterId.IgnoreCache.UserIsIgnored(Actor.ReferenceId)))
                        {
                            sessionByCharacterId.SendData(message);
                        }
                    }
                }
            }
        }

        public void BroadcastMessage(ServerMessage Message, uint SkipId = 0, bool Skip = false)
        {
            foreach (SpaceActor actor in this.concurrentDictionary_0.Values)
            {
                if (actor.Type == SpaceActorType.UserCharacter)
                {
                    Session sessionByCharacterId = SessionManager.GetSessionByCharacterId(actor.ReferenceId);
                    if (sessionByCharacterId != null)
                    {
                        if (Skip)
                        {
                            if (sessionByCharacterId.CharacterId != SkipId)
                            {
                                sessionByCharacterId.SendData(Message);
                            }
                        }
                        else
                        {
                            sessionByCharacterId.SendData(Message);
                        }
                    }
                }
            }
        }

        public bool CanInitiateMoveToPosition(Vector2 vector2_1)
        {
            return (((this.IsValidPosition(vector2_1) && (this.tileState_0[vector2_1.Int32_0, vector2_1.Int32_1] != TileState.Blocked)) && (this.list_0[vector2_1.Int32_0, vector2_1.Int32_1] == null)) && (this.userMovementNode_0[vector2_1.Int32_0, vector2_1.Int32_1] != UserMovementNode.Blocked));
        }

        public void DeleteArea(Session Session)
        {
            using (SqlDatabaseClient client = SqlDatabaseManager.GetClient())
            {
                SpaceManager.DeleteArea(client, this.SpaceId);
            }
            this.Unload();
        }

        public void DeleteIsland(Session Session)
        {
            lock (this.object_1)
            {
                using (SqlDatabaseClient client = SqlDatabaseManager.GetClient())
                {
                    SpaceManager.DeleteIsland(client, this.SpaceId);
                }
                this.Unload();
            }
        }

        public void Dispose()
        {
            if (!this.bool_0)
            {
                this.Unload();
            }
            this.spaceInfo_0 = null;
        }

        public void DoActorCountSync()
        {
            int humanActorCount = this.HumanActorCount;
            this.int_4 = humanActorCount;
            this.bool_1 = false;
        }

        public SpaceActor GetActor(uint ActorId)
        {
            SpaceActor actor = null;
            this.concurrentDictionary_0.TryGetValue(ActorId, out actor);
            return actor;
        }

        public SpaceActor GetActorByReferenceId(uint ReferenceId, SpaceActorType ReferenceType)
        {
            foreach (SpaceActor actor in this.concurrentDictionary_0.Values)
            {
                if ((actor.Type == ReferenceType) && (actor.ReferenceId == ReferenceId))
                {
                    return actor;
                }
            }
            return null;
        }

        public List<SpaceActor> GetActorsOnPosition(Vector2 Position)
        {
            if (this.list_0[Position.Int32_0, Position.Int32_1] != null)
            {
                return this.list_0[Position.Int32_0, Position.Int32_1];
            }
            return new List<SpaceActor>();
        }

        public ContestItem GetDefinition(string Name)
        {
            lock (this.dictionary_0)
            {
                foreach (ContestItem item in this.dictionary_0.Values)
                {
                    if (item.DefinitionName == Name)
                    {
                        return item;
                    }
                }
            }
            return null;
        }

        public Vector3 GetRedirectedTarget(Vector3 Target)
        {
            return Target;
        }

        public double GetUserStepHeight(Vector2 Position)
        {
            return 0.0;
        }

        public void InitializeContestWorker(SqlDatabaseClient MySqlClient)
        {
            this.dictionary_1 = new Dictionary<uint, ContestItem>();
            this.dictionary_0 = new Dictionary<uint, ContestItem>();
            this.ReloadContestItems(MySqlClient);
            this.thread_0 = new Thread(new ThreadStart(this.method_1));
            this.thread_0.Name = "ContestThread";
            this.thread_0.Priority = ThreadPriority.Highest;
            this.thread_0.Start();
            this.uint_1 = 1;
            this.object_0 = new object();
        }

        public bool IsValidPosition(Vector2 Position)
        {
            return ((((Position.Int32_0 >= 0) && (Position.Int32_1 >= 0)) && (Position.Int32_0 < this.spaceModel_0.Heightmap.SizeX)) && (Position.Int32_1 < this.spaceModel_0.Heightmap.SizeY));
        }

        public bool IsValidStep(Vector2 From, Vector2 vector2_1, bool EndOfPath, List<SpaceActor>[,] ActorBlockGrid = null)
        {
            if (ActorBlockGrid == null)
            {
                ActorBlockGrid = this.list_0;
            }
            if (((!this.IsValidPosition(vector2_1) || (this.tileState_0[vector2_1.Int32_0, vector2_1.Int32_1] == TileState.Blocked)) || (this.userMovementNode_0[vector2_1.Int32_0, vector2_1.Int32_1] == UserMovementNode.Blocked)) || ((this.userMovementNode_0[vector2_1.Int32_0, vector2_1.Int32_1] == UserMovementNode.WalkableEndOfRoute) && !EndOfPath))
            {
                return false;
            }
            return true;
        }

        public void KickSpace(bool SkipModerators)
        {
            foreach (SpaceActor actor in this.Actors)
            {
                if (actor.Type == SpaceActorType.UserCharacter)
                {
                    if (SessionManager.GetSessionByCharacterId(actor.ReferenceId) == null)
                    {
                        this.RemoveActorFromSpace(actor.UInt32_0);
                    }
                    else
                    {
                        this.KickUser(actor.ReferenceId);
                    }
                }
            }
        }

        public void KickUser(uint UserId)
        {
            SpaceActor actorByReferenceId = this.GetActorByReferenceId(UserId, SpaceActorType.UserCharacter);
            if ((actorByReferenceId != null) && (actorByReferenceId.Type == SpaceActorType.UserCharacter))
            {
                SpaceManager.RemoveUserFromSpace(SessionManager.GetSessionByCharacterId(actorByReferenceId.ReferenceId), true);
                this.RemoveCharacterFromSpace(UserId);
            }
        }

        public void MarkActorCountSyncNeeded()
        {
            this.bool_1 = true;
        }

        private uint method_0()
        {
            lock (this.object_0)
            {
                return this.uint_1++;
            }
        }

        private void method_1()
        {
            try
            {
                while (!this.bool_0)
                {
                    Dictionary<uint, ContestItem> dictionary = new Dictionary<uint, ContestItem>();
                    lock (this.dictionary_1)
                    {
                        foreach (KeyValuePair<uint, ContestItem> pair in this.dictionary_1)
                        {
                            dictionary.Add(pair.Key, pair.Value);
                        }
                    }
                    List<uint> list = new List<uint>();
                    foreach (ContestItem item in dictionary.Values)
                    {
                        if (!item.IsActive)
                        {
                            list.Add(item.UInt32_0);
                        }
                        else
                        {
                            lock (this.dictionary_1)
                            {
                                foreach (uint num in list)
                                {
                                    if (this.dictionary_1.ContainsKey(num))
                                    {
                                        this.dictionary_1.Remove(num);
                                        Output.WriteLine("[SpaceMgr] Erased ContestItem #" + num + ".", OutputLevel.DebugInformation);
                                    }
                                }
                            }
                        }
                    }
                    lock (this.dictionary_1)
                    {
                        uint[] numArray = new uint[this.dictionary_0.Count];
                        int index = 0;
                        foreach (KeyValuePair<uint, ContestItem> pair2 in this.dictionary_0)
                        {
                            if ((pair2.Value.SpaceId == this.spaceInfo_0.UInt32_0) || (pair2.Value.SpaceId == 0))
                            {
                                numArray[index] = pair2.Value.UInt32_0;
                                index++;
                            }
                        }
                        Vector2[] vectorArray = new Vector2[this.spaceModel_0.Heightmap.TileStates.Length];
                        for (int i = 0; i < this.spaceModel_0.Heightmap.OpenTiles.Length; i++)
                        {
                            vectorArray[i] = this.spaceModel_0.Heightmap.OpenTiles[i];
                        }
                        Vector2 position = null;
                        while (position == null)
                        {
                            int num4 = new Random().Next(0, vectorArray.Length - 1);
                            if (vectorArray[num4] != null)
                            {
                                position = vectorArray[num4];
                            }
                        }
                        ContestItem item2 = null;
                        while (item2 == null)
                        {
                            int num5 = new Random().Next(0, numArray.Length - 1);
                            if ((num5 == 1) || (num5 != this.int_2))
                            {
                                if (num5 == 1)
                                {
                                    this.int_3++;
                                }
                                this.int_2 = num5;
                                if (this.int_3 < 3)
                                {
                                    item2 = this.dictionary_0[numArray[num5]];
                                }
                                else
                                {
                                    this.int_3 = 0;
                                }
                            }
                        }
                        if ((item2 != null) && (position != null))
                        {
                            ContestItem item3 = new ContestItem(this.method_0(), this.uint_0, item2.DefinitionId, item2.RankingId, position, item2.GoldCredits, item2.SilverCredits, item2.ObjectId, item2.DefinitionName, item2.ShowName, item2.FallType, item2.CatchType);
                            this.dictionary_1.Add(item3.UInt32_0, item3);
                            this.BroadcastMessage(SpaceFallingItemComposer.Compose(item3), 0, false);
                        }
                    }
                    int millisecondsTimeout = new Random().Next(0xafc8, 0x249f0);
                    Output.WriteLine(string.Concat(new object[] { "[ContestMgr] Item will fall on Instance #", this.uint_0, " in ", millisecondsTimeout, " MilliSeconds." }), OutputLevel.DebugInformation);
                    Thread.Sleep(millisecondsTimeout);
                }
            }
            catch (ThreadAbortException)
            {
            }
            catch (ThreadInterruptedException)
            {
            }
            catch (Exception)
            {
                this.method_2();
            }
        }

        private void method_2()
        {
            this.dictionary_1 = null;
            this.dictionary_0 = null;
            this.uint_1 = 1;
            this.thread_0.Abort();
        }

        private uint method_3()
        {
            uint item = 0;
            lock (this.object_2)
            {
                if (this.list_2.Count != 0)
                {
                    item = this.list_2[0];
                    if (this.list_2.Contains(item))
                    {
                        this.list_2.Remove(item);
                    }
                }
                return item;
            }
        }

        private bool method_4(SpaceActor spaceActor_0)
        {
            lock (this.object_1)
            {
                if (this.concurrentDictionary_0.ContainsKey(spaceActor_0.UInt32_0))
                {
                    Session sessionByCharacterId = SessionManager.GetSessionByCharacterId(spaceActor_0.ReferenceId);
                    if (sessionByCharacterId != null)
                    {
                        sessionByCharacterId.SendData(SpaceUserRemovedComposer.SingleCompose());
                    }
                    return false;
                }
                this.concurrentDictionary_0.TryAdd(spaceActor_0.UInt32_0, spaceActor_0);
                this.MarkActorCountSyncNeeded();
                return true;
            }
        }

        public int Mutespace(SqlDatabaseClient MySqlClient, int TimeToMute)
        {
            int num = 0;
            lock (this.object_1)
            {
                foreach (SpaceActor actor in this.concurrentDictionary_0.Values)
                {
                    if (actor.Type == SpaceActorType.UserCharacter)
                    {
                        num++;
                    }
                }
            }
            return num;
        }

        public void PerformUpdate()
        {
            List<SpaceActor> actors = new List<SpaceActor>();
            List<SpaceActor> list2 = new List<SpaceActor>();
            Dictionary<uint, SpaceActor> dictionary = new Dictionary<uint, SpaceActor>();
            List<SpaceActor>[,] actorBlockGrid = new List<SpaceActor>[this.spaceModel_0.Heightmap.SizeX, this.spaceModel_0.Heightmap.SizeY];
            lock (this.object_1)
            {
                if (func_0 == null)
                {
                    func_0 = new Func<KeyValuePair<uint, SpaceActor>, uint>(SpaceInstance.smethod_0);
                }
                if (func_1 == null)
                {
                    func_1 = new Func<KeyValuePair<uint, SpaceActor>, SpaceActor>(SpaceInstance.smethod_1);
                }
                dictionary = this.concurrentDictionary_0.ToDictionary<KeyValuePair<uint, SpaceActor>, uint, SpaceActor>(func_0, func_1);
            }
            foreach (SpaceActor actor in dictionary.Values)
            {
                if (this.bool_0)
                {
                    list2.Add(actor);
                    continue;
                }
                if (actor.UserStatusses.ContainsKey("mv"))
                {
                    actor.RemoveStatus("mv");
                    actor.UpdateNeeded = true;
                }
                if (actor.PositionToSet != null)
                {
                    actor.Position = new Vector3(actor.PositionToSet.Int32_0, actor.PositionToSet.Int32_1, actor.Rotation);
                    actor.PositionToSet = null;
                }
                if (actor.IsMoving)
                {
                    Vector3 nextStep = actor.GetNextStep();
                    bool endOfPath = !actor.IsMoving;
                    if ((nextStep != null) && ((!actor.ClippingEnabled && this.IsValidPosition(nextStep.GetVector2())) || this.IsValidStep(actor.Position.GetVector2(), nextStep.GetVector2(), endOfPath, actorBlockGrid)))
                    {
                        actor.SetStatus("mv", string.Concat(new object[] { nextStep.Int32_0, ",", nextStep.Int32_1, ",", nextStep.Int32_2 }));
                        actor.Position = nextStep;
                        try
                        {
                            if (actorBlockGrid[nextStep.Int32_0, nextStep.Int32_1] == null)
                            {
                                actorBlockGrid[nextStep.Int32_0, nextStep.Int32_1] = new List<SpaceActor>();
                            }
                            actorBlockGrid[nextStep.Int32_0, nextStep.Int32_1].Add(actor);
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                        actor.Rotation = nextStep.Int32_2;
                        actor.UpdateNeeded = true;
                        goto Label_032E;
                    }
                    actor.StopMoving();
                    try
                    {
                        if (actorBlockGrid[nextStep.Int32_0, nextStep.Int32_1] == null)
                        {
                            actorBlockGrid[nextStep.Int32_0, nextStep.Int32_1] = new List<SpaceActor>();
                        }
                        actorBlockGrid[nextStep.Int32_0, nextStep.Int32_1].Add(actor);
                        goto Label_032E;
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
                try
                {
                    if (actorBlockGrid[actor.Position.Int32_0, actor.Position.Int32_1] == null)
                    {
                        actorBlockGrid[actor.Position.Int32_0, actor.Position.Int32_1] = new List<SpaceActor>();
                    }
                    actorBlockGrid[actor.Position.Int32_0, actor.Position.Int32_1].Add(actor);
                }
                catch (Exception)
                {
                    continue;
                }
            Label_032E:
                this.UpdateActorStatus(actor);
                if (actor.UpdateNeeded)
                {
                    actor.UpdateNeeded = false;
                    actors.Add(actor);
                }
            }
            foreach (SpaceActor actor2 in list2)
            {
                lock (this.object_1)
                {
                    if (actors.Contains(actor2))
                    {
                        actors.Remove(actor2);
                    }
                }
                this.KickUser(actor2.ReferenceId);
            }
            if (actors.Count > 0)
            {
                this.BroadcastMessage(SpaceUserWalkComposer.Compose(actors), 0, false);
            }
            actorBlockGrid[this.spaceModel_0.DoorPosition.Int32_0, this.spaceModel_0.DoorPosition.Int32_1] = null;
            lock (this.object_1)
            {
                this.list_0 = actorBlockGrid;
            }
        }

        public void RegenerateRelativeHeightmap(bool Broadcast = false)
        {
            this.userMovementNode_0 = new UserMovementNode[this.spaceModel_0.Heightmap.SizeX, this.spaceModel_0.Heightmap.SizeY];
            this.vector2_0 = new Vector2[this.spaceModel_0.Heightmap.SizeX, this.spaceModel_0.Heightmap.SizeY];
            for (int i = 0; i < this.spaceModel_0.Heightmap.SizeY; i++)
            {
                for (int j = 0; j < this.spaceModel_0.Heightmap.SizeX; j++)
                {
                    this.userMovementNode_0[j, i] = UserMovementNode.Walkable;
                    this.vector2_0[j, i] = null;
                }
            }
            lock (this.tileState_0)
            {
                StringBuilder builder = new StringBuilder();
                this.tileState_0 = new TileState[this.spaceModel_0.Heightmap.SizeX, this.spaceModel_0.Heightmap.SizeY];
                for (int k = 0; k < this.spaceModel_0.Heightmap.SizeY; k++)
                {
                    for (int m = 0; m < this.spaceModel_0.Heightmap.SizeX; m++)
                    {
                        if (((this.spaceInfo_0.SpaceType == SpaceType.Island) && (this.spaceModel_0.DoorPosition.Int32_0 == m)) && (this.spaceModel_0.DoorPosition.Int32_1 == k))
                        {
                            this.tileState_0[m, k] = TileState.Blocked;
                            builder.Append('1');
                        }
                        else if (this.spaceModel_0.Heightmap.TileStates[m, k] == TileState.Blocked)
                        {
                            this.tileState_0[m, k] = TileState.Blocked;
                            builder.Append('1');
                        }
                        else
                        {
                            this.tileState_0[m, k] = TileState.Open;
                            builder.Append('0');
                        }
                    }
                    builder.Append(Convert.ToChar(13));
                }
                lock (this.string_0)
                {
                    if (builder.ToString() != this.string_0)
                    {
                        this.string_0 = builder.ToString();
                    }
                }
            }
        }

        public void ReloadContestItems(SqlDatabaseClient MySqlClient)
        {
            lock (this.dictionary_0)
            {
                this.dictionary_0.Clear();
                foreach (DataRow row in MySqlClient.ExecuteQueryTable("SELECT * FROM objetos_ranking WHERE activado = '1'").Rows)
                {
                    this.dictionary_0.Add((uint)row["id"], new ContestItem((uint)row["id"], (uint)row["area_especifica"], (uint)row["id_objeto"], (uint)row["id_ranking"], new Vector2(0, 0), (uint)row["creditos_oro"], (uint)row["creditos_plata"], (uint)row["id_premio"], (string)row["nombre_objeto"], (string)row["nombre_mostrar"], (uint)row["tipo_caida"], (uint)row["tipo_captura"]));
                }
            }
        }

        public bool RemoveActorFromSpace(uint ActorId)
        {
            bool flag = false;
            lock (this.object_1)
            {
                if (this.GetActor(ActorId) == null)
                {
                    return false;
                }
                SpaceActor actor2 = null;
                if (flag = this.concurrentDictionary_0.TryRemove(ActorId, out actor2))
                {
                    this.BroadcastMessage(SpaceUserRemovedComposer.BroadcastCompose(ActorId), 0, false);
                    this.MarkActorCountSyncNeeded();
                }
            }
            return flag;
        }

        public bool RemoveCharacterFromSpace(uint CharacterId)
        {
            uint item = 0;
            lock (this.object_1)
            {
                using (IEnumerator<SpaceActor> enumerator = this.concurrentDictionary_0.Values.GetEnumerator())
                {
                    SpaceActor current;
                    while (enumerator.MoveNext())
                    {
                        current = enumerator.Current;
                        if ((current.Type == SpaceActorType.UserCharacter) && (current.ReferenceId == CharacterId))
                        {
                            goto Label_004F;
                        }
                    }
                    goto Label_0070;
                Label_004F:
                    item = current.UInt32_0;
                }
            }
        Label_0070:
            if (item <= 0)
            {
                return false;
            }
            if (!this.list_2.Contains(item))
            {
                this.list_2.Add(item);
            }
            return this.RemoveActorFromSpace(item);
        }

        public void SendObjects(Session Session)
        {
            List<SpaceActor> actorContainer = new List<SpaceActor>();
            lock (this.object_1)
            {
                foreach (SpaceActor actor in this.concurrentDictionary_0.Values)
                {
                    actorContainer.Add(actor);
                }
            }
            Session.SendData(SpaceObjectListComposer.SingleCompose(actorContainer));
            Session.SendData(SpacePricesComposer.Compose(this.spaceInfo_0.AllowUppercuts, this.spaceInfo_0.AllowCoconuts, 250, 0x19));
        }

        private static uint smethod_0(KeyValuePair<uint, SpaceActor> keyValuePair_0)
        {
            return keyValuePair_0.Key;
        }

        private static SpaceActor smethod_1(KeyValuePair<uint, SpaceActor> keyValuePair_0)
        {
            return keyValuePair_0.Value;
        }

        public static SpaceInstance TryCreateSpaceInstance(uint InstanceId, uint SpaceId)
        {
            SpaceInfo spaceInfo = SpaceInfoLoader.GetSpaceInfo(SpaceId);
            if (spaceInfo == null)
            {
                return null;
            }
            SpaceModel model = spaceInfo.TryGetModel();
            if (model == null)
            {
                return null;
            }
            return new SpaceInstance(InstanceId, spaceInfo, model);
        }

        public void Unload()
        {
            if (!this.bool_0)
            {
                this.bool_0 = true;
                if (this.bool_1)
                {
                    this.DoActorCountSync();
                }
                this.BroadcastMessage(SpaceUserRemovedComposer.SingleCompose(), 0, false);
                lock (this.object_1)
                {
                    this.concurrentDictionary_0.Clear();
                }
                this.double_0 = UnixTimestamp.GetCurrent();
            }
        }

        public void UpdateActorStatus(SpaceActor Actor)
        {
            Vector2 vector = this.vector2_0[Actor.Position.Int32_0, Actor.Position.Int32_1];
            if (vector != null)
            {
                Actor.Position = new Vector3(vector.Int32_0, vector.Int32_1, Actor.Rotation);
            }
        }

        public int ActorCount
        {
            get
            {
                return this.concurrentDictionary_0.Count;
            }
        }

        public bool ActorCountDatabaseWritebackNeeded
        {
            get
            {
                return this.bool_1;
            }
            set
            {
                this.bool_1 = value;
            }
        }

        public ConcurrentBag<SpaceActor> Actors
        {
            get
            {
                ConcurrentBag<SpaceActor> bag = new ConcurrentBag<SpaceActor>();
                foreach (SpaceActor actor in this.concurrentDictionary_0.Values)
                {
                    bag.Add(actor);
                }
                return bag;
            }
        }

        public int CachedNavigatorUserCount
        {
            get
            {
                return this.int_4;
            }
        }

        public List<uint> FreeIdsList
        {
            get
            {
                return this.list_2;
            }
            set
            {
                this.list_2 = value;
            }
        }

        public int HumanActorCount
        {
            get
            {
                int num = 0;
                foreach (SpaceActor actor in this.concurrentDictionary_0.Values)
                {
                    if (!actor.IsBot)
                    {
                        num++;
                    }
                }
                return num;
            }
        }

        public SpaceInfo Info
        {
            get
            {
                return this.spaceInfo_0;
            }
        }

        public uint InstanceId
        {
            get
            {
                return this.uint_0;
            }
        }

        public Dictionary<uint, ContestItem> Items
        {
            get
            {
                return this.dictionary_1;
            }
        }

        public int MarkedAsEmpty
        {
            get
            {
                return this.int_1;
            }
            set
            {
                this.int_1 = value;
            }
        }

        public SpaceModel Model
        {
            get
            {
                return this.spaceModel_0;
            }
        }

        public string RelativeHeightmap
        {
            get
            {
                return this.string_0;
            }
        }

        public uint SpaceId
        {
            get
            {
                return this.spaceInfo_0.UInt32_0;
            }
        }

        public bool SpaceMuted
        {
            get
            {
                return this.bool_2;
            }
            set
            {
                this.bool_2 = value;
            }
        }

        public double TimeUnloaded
        {
            get
            {
                if (!this.bool_0)
                {
                    return 0.0;
                }
                return (UnixTimestamp.GetCurrent() - this.double_0);
            }
        }

        public bool Unloaded
        {
            get
            {
                return this.bool_0;
            }
        }
    }
}
