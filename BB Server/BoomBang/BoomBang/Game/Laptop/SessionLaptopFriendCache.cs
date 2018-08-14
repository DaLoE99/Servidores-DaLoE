namespace BoomBang.Game.Laptop
{
    using BoomBang.Communication;
    using BoomBang.Communication.Outgoing;
    using BoomBang.Game.Characters;
    using BoomBang.Game.Sessions;
    using BoomBang.Storage;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class SessionLaptopFriendCache : IDisposable
    {
        /* private scope */ Dictionary<uint, int> dictionary_0;
        /* private scope */ List<uint> list_0;
        /* private scope */ uint uint_0;

        public SessionLaptopFriendCache(SqlDatabaseClient MySqlClient, uint UserId)
        {
            this.uint_0 = UserId;
            this.list_0 = new List<uint>();
            this.dictionary_0 = new Dictionary<uint, int>();
            this.ReloadCache(MySqlClient);
        }

        public void AddToCache(uint FriendId)
        {
            lock (this.list_0)
            {
                if (!this.list_0.Contains(FriendId))
                {
                    this.list_0.Add(FriendId);
                    this.MarkUpdateNeeded(FriendId, 1);
                }
            }
        }

        public void BroadcastToFriends(ServerMessage ServerMessage)
        {
            List<uint> list = new List<uint>();
            lock (this.list_0)
            {
                list.AddRange(this.list_0);
            }
            foreach (uint num in list)
            {
                Session sessionByCharacterId = SessionManager.GetSessionByCharacterId(num);
                if (sessionByCharacterId != null)
                {
                    sessionByCharacterId.SendData(ServerMessage, false);
                }
            }
        }

        public ServerMessage ComposeUpdateList()
        {
            lock (this.dictionary_0)
            {
                List<LaptopUpdate> updates = new List<LaptopUpdate>();
                using (SqlDatabaseClient client = SqlDatabaseManager.GetClient())
                {
                    foreach (uint num in this.list_0)
                    {
                        if (this.dictionary_0.ContainsKey(num))
                        {
                            CharacterInfo characterInfo = CharacterInfoLoader.GetCharacterInfo(client, num);
                            if (characterInfo != null)
                            {
                                updates.Add(new LaptopUpdate(this.dictionary_0[num], characterInfo));
                            }
                        }
                    }
                    this.dictionary_0.Clear();
                }
                return LaptopUpdateListComposer.Compose(updates);
            }
        }

        public void Dispose()
        {
            if (this.list_0 != null)
            {
                this.list_0.Clear();
                this.list_0 = null;
            }
        }

        public void MarkUpdateNeeded(uint FriendId, int UpdateMode)
        {
            lock (this.dictionary_0)
            {
                if (!this.dictionary_0.ContainsKey(FriendId))
                {
                    this.dictionary_0.Add(FriendId, UpdateMode);
                }
            }
        }

        public void ReloadCache(SqlDatabaseClient MySqlClient)
        {
            lock (this.list_0)
            {
                this.list_0.Clear();
                this.dictionary_0.Clear();
                this.list_0.AddRange(LaptopHandler.GetFriendsForUser(MySqlClient, this.uint_0, 1));
            }
        }

        public void RemoveFromCache(uint FriendId)
        {
            lock (this.list_0)
            {
                if (this.list_0.Contains(FriendId))
                {
                    this.list_0.Remove(FriendId);
                }
                if (this.dictionary_0.ContainsKey(FriendId))
                {
                    this.dictionary_0.Remove(FriendId);
                }
            }
        }

        public ReadOnlyCollection<uint> Friends
        {
            get
            {
                lock (this.list_0)
                {
                    List<uint> list = new List<uint>();
                    list.AddRange(this.list_0);
                    return list.AsReadOnly();
                }
            }
        }
    }
}

