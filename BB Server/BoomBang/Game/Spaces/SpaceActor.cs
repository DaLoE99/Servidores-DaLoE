using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snowlight.Specialized;
using Snowlight.Storage;
using System.Timers;
using Snowlight.Game.Sessions;
using Snowlight.Game.Pathfinding;
using Snowlight.Game.Characters;
using Snowlight.Communication;
using Snowlight.Communication.Outgoing.Chat;
using Snowlight.Communication.Outgoing;
using Snowlight.Communication.Outgoing.Spaces;

namespace Snowlight.Game.Spaces
{
    public class SpaceActor
    {
        /* private scope */
        bool bool_0;
        /* private scope */
        bool bool_1;
        /* private scope */
        bool bool_2;
        /* private scope */
        bool bool_3;
        /* private scope */
        bool bool_4;
        /* private scope */
        bool bool_5;
        /* private scope */
        bool bool_6;
        /* private scope */
        Dictionary<string, string> dictionary_0;
        /* private scope */
        double double_0;
        /* private scope */
        double double_1;
        /* private scope */
        int int_0;
        /* private scope */
        int int_1;
        /* private scope */
        int int_2;
        /* private scope */
        int int_3;
        internal object object_0;
        /* private scope */
        object object_1;
        /* private scope */
        Game.Pathfinding.Pathfinder pathfinder_0;
        internal SpaceActorType spaceActorType_0;
        /* private scope */
        SpaceInstance spaceInstance_0;
        internal uint uint_0;
        internal uint uint_1;
        /* private scope */
        Vector2 vector2_0;
        internal Vector3 vector3_0;
        /* private scope */
        Vector3 vector3_1;

        public SpaceActor(uint uint_2, SpaceActorType Type, uint ReferenceId, object ReferenceObject, Vector3 Position, int Rotation, SpaceInstance Instance)
        {
            this.uint_0 = uint_2;
            this.spaceActorType_0 = Type;
            this.uint_1 = ReferenceId;
            this.object_0 = ReferenceObject;
            this.vector3_0 = Position;
            this.vector3_1 = Position;
            this.int_0 = Rotation;
            this.bool_0 = false;
            this.bool_6 = false;
            this.bool_3 = false;
            this.bool_4 = false;
            this.dictionary_0 = new Dictionary<string, string>();
            this.spaceInstance_0 = Instance;
            this.bool_1 = true;
            this.object_1 = new object();
            this.pathfinder_0 = PathfinderManager.CreatePathfinderInstance();
            this.pathfinder_0.SetSpaceInstance(this.spaceInstance_0, uint_2);
            this.method_1(500);
            this.method_2(500);
        }

        public void ApplyEffect(int EffectId, bool Broadcast = true)
        {
            if (this.int_1 != EffectId)
            {
                this.int_1 = EffectId;
            }
        }

        public void BlockWalking()
        {
            this.StopMoving();
            this.bool_5 = true;
        }

        public void Chat(string MessageText, int MessageColor = 0, bool CanOverrideSpaceMute = false)
        {
            if (!this.spaceInstance_0.SpaceMuted || CanOverrideSpaceMute)
            {
                this.spaceInstance_0.BroadcastChatMessage(this, MessageText, false, MessageColor);
            }
        }

        public void ClearStatusses()
        {
            lock (this.dictionary_0)
            {
                this.dictionary_0.Clear();
            }
        }

        public Vector3 GetNextStep()
        {
            lock (this.object_1)
            {
                return this.pathfinder_0.GetNextStep();
            }
        }

        public void LeaveSpace()
        {
            this.bool_6 = true;
        }

        public void Lock(int Ticks, bool ByUppercut = false, bool ByCoconut = false)
        {
            this.pathfinder_0.Clear();
            this.double_0 = UnixTimestamp.GetCurrent() + Ticks;
            if (ByUppercut)
            {
                this.bool_3 = true;
            }
            else if (ByCoconut)
            {
                this.bool_4 = true;
            }
        }

        private void method_0(SqlDatabaseClient sqlDatabaseClient_0)
        {
            this.int_2++;
            if (this.int_3 == -1)
            {
                this.int_3 = 0x10;
            }
            else if (this.int_2 >= 5)
            {
                CharacterInfo info = (CharacterInfo)this.object_0;
                SessionManager.GetSessionByCharacterId(info.Id);
            }
        }

        private void method_1(int int_4)
        {
            Timer timer = new Timer();
            timer.Elapsed += new ElapsedEventHandler(this.method_3);
            timer.Interval = int_4;
            timer.Enabled = true;
            GC.KeepAlive(timer);
        }

        private void method_2(int int_4)
        {
            Timer timer = new Timer();
            timer.Elapsed += new ElapsedEventHandler(this.method_4);
            timer.Interval = int_4;
            timer.Enabled = true;
            GC.KeepAlive(timer);
        }

        private void method_3(object sender, ElapsedEventArgs e)
        {
            if (!this.IsLocked && this.bool_3)
            {
                this.bool_3 = false;
                Session sessionByCharacterId = SessionManager.GetSessionByCharacterId(this.uint_1);
                if (sessionByCharacterId != null)
                {
                    sessionByCharacterId.SendData(SpaceUserSendUppercut.RemoveComposer());
                }
            }
        }

        private void method_4(object sender, ElapsedEventArgs e)
        {
            if (!this.IsLocked && this.bool_4)
            {
                this.bool_4 = false;
                this.spaceInstance_0.BroadcastMessage(SpaceUserSendCoconut.Unlock(this.uint_1), 0, false);
            }
        }

        public void MoveTo(List<Vector3> StepList, Vector3 ToPosition, bool IgnoreCanInitiate = false, bool IgnoreRedirections = false, bool DisableClipping = false)
        {
            if (this.spaceInstance_0.IsValidPosition(new Vector2(ToPosition.Int32_0, ToPosition.Int32_1)))
            {
                this.bool_1 = !DisableClipping;
                if (!this.ClippingEnabled)
                {
                    IgnoreCanInitiate = true;
                }
                if (!IgnoreRedirections)
                {
                    ToPosition = this.spaceInstance_0.GetRedirectedTarget(ToPosition);
                }
                if ((((ToPosition.Int32_0 != this.vector3_0.Int32_0) || (ToPosition.Int32_1 != this.vector3_0.Int32_1)) && (IgnoreCanInitiate || this.spaceInstance_0.CanInitiateMoveToPosition(new Vector2(ToPosition.Int32_0, ToPosition.Int32_1)))) && (!this.bool_5 || DisableClipping))
                {
                    lock (this.object_1)
                    {
                        if (this.vector2_0 != null)
                        {
                            this.vector3_0.Int32_0 = this.vector2_0.Int32_0;
                            this.vector3_0.Int32_1 = this.vector2_0.Int32_1;
                            this.vector3_0.Int32_2 = this.int_0;
                            this.vector2_0 = null;
                        }
                        this.StopMoving();
                        this.pathfinder_0.MoveTo(StepList, ToPosition);
                    }
                }
            }
        }

        public void PreLock(int Ticks)
        {
            this.double_1 = UnixTimestamp.GetCurrent() + Ticks;
        }

        public bool RemoveStatus(string Key)
        {
            lock (this.dictionary_0)
            {
                return this.dictionary_0.Remove(Key);
            }
        }

        public void SetStatus(string Key, string Value = "")
        {
            lock (this.dictionary_0)
            {
                if (!this.dictionary_0.ContainsKey(Key))
                {
                    this.dictionary_0.Add(Key, Value);
                }
                else
                {
                    this.dictionary_0[Key] = Value;
                }
            }
        }

        public void StopMoving()
        {
            lock (this.object_1)
            {
                this.pathfinder_0.Clear();
            }
        }

        public static SpaceActor TryCreateActor(uint uint_2, SpaceActorType Type, uint ReferenceId, object ReferenceObject, Vector3 Position, int Rotation, SpaceInstance Instance)
        {
            if (ReferenceObject == null)
            {
                return null;
            }
            return new SpaceActor(uint_2, Type, ReferenceId, ReferenceObject, Position, Rotation, Instance);
        }

        public void UnblockWalking()
        {
            this.bool_5 = false;
        }

        public void Whisper(string MessageText, uint TargetUserId, bool CanOverridespaceMute = false)
        {
            CharacterInfo info = (CharacterInfo)this.object_0;
            ServerMessage message = SpaceChatComposer.Compose(this.uint_0, MessageText, (info.Staff == 1) ? 2 : 1, ChatType.Whisper);
            if (this.spaceActorType_0 == SpaceActorType.UserCharacter)
            {
                Session sessionByCharacterId = SessionManager.GetSessionByCharacterId(this.uint_1);
                if (sessionByCharacterId != null)
                {
                    sessionByCharacterId.SendData(message);
                }
            }
            if (TargetUserId != this.uint_1)
            {
                SpaceActor actorByReferenceId = this.spaceInstance_0.GetActorByReferenceId(TargetUserId, SpaceActorType.UserCharacter);
                if ((actorByReferenceId != null) && (actorByReferenceId.Type == SpaceActorType.UserCharacter))
                {
                    Session session2 = SessionManager.GetSessionByCharacterId(actorByReferenceId.ReferenceId);
                    if ((session2 != null) && !session2.IgnoreCache.UserIsIgnored(this.uint_1))
                    {
                        session2.SendData(message);
                    }
                }
            }
        }

        public string AvatarColor
        {
            get
            {
                return ((CharacterInfo)this.object_0).AvatarColors;
            }
            set
            {
                ((CharacterInfo)this.object_0).AvatarColors = value;
            }
        }

        public int AvatarEffectId
        {
            get
            {
                return this.int_1;
            }
        }

        public uint AvatarType
        {
            get
            {
                return ((CharacterInfo)this.object_0).AvatarType;
            }
            set
            {
                ((CharacterInfo)this.object_0).AvatarType = value;
            }
        }

        public bool ClippingEnabled
        {
            get
            {
                return (!this.bool_2 && this.bool_1);
            }
            set
            {
                this.bool_1 = value;
            }
        }

        public bool IsBot
        {
            get
            {
                return (this.spaceActorType_0 != SpaceActorType.UserCharacter);
            }
        }

        public bool IsLocked
        {
            get
            {
                if (this.double_0 <= UnixTimestamp.GetCurrent())
                {
                    return false;
                }
                return true;
            }
        }

        public bool IsMoving
        {
            get
            {
                return !this.pathfinder_0.IsCompleted;
            }
        }

        public bool IsPreLocked
        {
            get
            {
                if (this.double_1 <= UnixTimestamp.GetCurrent())
                {
                    return false;
                }
                return true;
            }
        }

        public Vector3 LastPosition
        {
            get
            {
                return this.vector3_1;
            }
            set
            {
                this.vector3_1 = value;
            }
        }

        public bool LeaveSpaceRequest
        {
            get
            {
                return this.bool_6;
            }
        }

        public string Motto
        {
            get
            {
                return ((CharacterInfo)this.object_0).Motto;
            }
        }

        public string Name
        {
            get
            {
                return ((CharacterInfo)this.object_0).Username;
            }
        }

        public bool OverrideClipping
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

        public Game.Pathfinding.Pathfinder Pathfinder
        {
            get
            {
                return this.pathfinder_0;
            }
        }

        public Vector3 Position
        {
            get
            {
                return this.vector3_0;
            }
            set
            {
                this.vector3_0 = value;
            }
        }

        public Vector2 PositionToSet
        {
            get
            {
                return this.vector2_0;
            }
            set
            {
                this.vector2_0 = value;
            }
        }

        public uint ReferenceId
        {
            get
            {
                return this.uint_1;
            }
        }

        public object ReferenceObject
        {
            get
            {
                return this.object_0;
            }
        }

        public int Rotation
        {
            get
            {
                return this.int_0;
            }
            set
            {
                this.int_0 = value;
            }
        }

        public SpaceActorType Type
        {
            get
            {
                return this.spaceActorType_0;
            }
        }

        public uint UInt32_0
        {
            get
            {
                return this.uint_0;
            }
        }

        public bool UpdateNeeded
        {
            get
            {
                return this.bool_0;
            }
            set
            {
                this.bool_0 = value;
            }
        }

        public Dictionary<string, string> UserStatusses
        {
            get
            {
                lock (this.dictionary_0)
                {
                    Dictionary<string, string> dictionary = new Dictionary<string, string>();
                    foreach (KeyValuePair<string, string> pair in this.dictionary_0)
                    {
                        dictionary.Add(pair.Key, pair.Value);
                    }
                    return new Dictionary<string, string>(dictionary);
                }
            }
        }
    }
}
