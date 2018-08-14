using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snowlight.Communication.Incoming;
using Snowlight.Communication;
using System.Collections.ObjectModel;
using Snowlight.Storage;
using Snowlight.Game.Sessions;
using System.Data;
using Snowlight.Communication.Outgoing;
using Snowlight.Util;
using Snowlight.Game.Characters;
using Snowlight.Communication.Outgoing.Laptop;

namespace Snowlight.Game.Laptop
{
    class LaptopHandler
    {
        public static void CreateFriendship(SqlDatabaseClient MySqlClient, uint UserId1, uint UserId2, bool Confirmed)
        {
            // This item is obfuscated and can not be translated.
            int num = 0;
            while (true)
            {
                if (Confirmed)
                {
                }
                if (num >= 2)
                {
                    return;
                }
                string query;
                if (num == 1)
                {
                    if (Confirmed)
                        query = "INSERT INTO laptop_amigos (id_usuario,id_amigo,aceptado) VALUES ('" + UserId1 + "','" + UserId2 + "',1)";
                    else
                        query = "INSERT INTO laptop_amigos (id_usuario,id_amigo,aceptado) VALUES ('" + UserId1 + "','" + UserId2 + "',0)";
                }
                else
                {
                    if (Confirmed)
                        query = "INSERT INTO laptop_amigos (id_usuario,id_amigo,aceptado) VALUES ('" + UserId2 + "','" + UserId1 + "',1)";
                    else
                        query = "INSERT INTO laptop_amigos (id_usuario,id_amigo,aceptado) VALUES ('" + UserId2 + "','" + UserId1 + "',0)";
                }

                MySqlClient.ExecuteNonQuery(query);
                Console.WriteLine(query);
                num++;
            }
        }

        public static bool DestroyFriendship(SqlDatabaseClient MySqlClient, uint UserId1, uint UserId2)
        {
            int num = 0;
            for (int i = 0; i < 2; i++)
            {
                MySqlClient.SetParameter("user1", (i == 1) ? UserId1 : UserId2);
                MySqlClient.SetParameter("user2", (i == 1) ? UserId2 : UserId1);
                num += MySqlClient.ExecuteNonQuery("DELETE FROM laptop_amigos WHERE id_usuario = @user1 AND id_amigo = @user2 LIMIT 1");
            }
            return (num > 0);
        }

        public static void ForceLaptopUpdateForSession(Session SessionToUpdate)
        {
            SessionToUpdate.SendData(SessionToUpdate.LaptopFriendCache.ComposeUpdateList());
        }

        public static bool FriendshipExists(SqlDatabaseClient MySqlClient, uint UserId1, uint UserId2, bool ConfirmedOnly)
        {
            MySqlClient.SetParameter("user1", UserId1);
            MySqlClient.SetParameter("user2", UserId2);
            MySqlClient.SetParameter("confirmed", ConfirmedOnly ? 0 : 1);
            return (MySqlClient.ExecuteQueryRow("SELECT null FROM laptop_amigos WHERE id_usuario = @user1 AND id_amigo = @user2 AND aceptado != @confirmed OR id_amigo = @user1 AND id_usuario = @user2 AND aceptado != @confirmed LIMIT 1") != null);
        }

        public static List<uint> GetFriendsForUser(SqlDatabaseClient MySqlClient, uint UserId, int Confirmed)
        {
            List<uint> list = new List<uint>();
            MySqlClient.SetParameter("id", UserId);
            MySqlClient.SetParameter("confirmed", Confirmed);
            foreach (DataRow row in MySqlClient.ExecuteQueryTable("SELECT id_amigo FROM laptop_amigos WHERE id_usuario = "+UserId+" AND aceptado = '" + Confirmed+"'").Rows)
            {
                list.Add(uint.Parse(row[0].ToString()));
            }
            return list;
        }

        public static void Initialize()
        {
            DataRouter.RegisterHandler(Opcodes.LAPTOPLOADFRIENDS, new ProcessRequestCallback(LoadFriends), false);
            DataRouter.RegisterHandler(Opcodes.LAPTOPLOADMESSAGES, new ProcessRequestCallback(LoadMessages), false);
            DataRouter.RegisterHandler(Opcodes.LAPTOPSEARCHBUDDY, new ProcessRequestCallback(LaptopHandler.smethod_3), false);
            DataRouter.RegisterHandler(Opcodes.LAPTOPUPDATELAPTOP, new ProcessRequestCallback(LaptopHandler.smethod_2), false);
            DataRouter.RegisterHandler(Opcodes.LAPTOPUPDATEBUDDY, new ProcessRequestCallback(LaptopHandler.smethod_4), false);
            DataRouter.RegisterHandler(Opcodes.LAPTOPFRIENREQUEST, new ProcessRequestCallback(LaptopHandler.smethod_5), false);
            DataRouter.RegisterHandler(Opcodes.LAPTOPFRIENDDECLINE, new ProcessRequestCallback(LaptopHandler.smethod_7), false);
            DataRouter.RegisterHandler(Opcodes.LAPTOPFRIENDACCEPT, new ProcessRequestCallback(LaptopHandler.smethod_6), false);
            DataRouter.RegisterHandler(Opcodes.LAPTOPFRIENDDELETE, new ProcessRequestCallback(LaptopHandler.smethod_8), false);
            DataRouter.RegisterHandler(Opcodes.LAPTOPSENDMESSAGE, new ProcessRequestCallback(LaptopHandler.smethod_9), false);
            DataRouter.RegisterHandler(Opcodes.LAPTOPDELETEMESSAGE, new ProcessRequestCallback(LaptopHandler.smethod_10), false);
        }

        private static void LoadFriends(Session Session, ClientMessage Message)
        {
            try
            {
                ReadOnlyCollection<uint> friends = Session.LaptopFriendCache.Friends;
                List<uint> requests = new List<uint>();
                using (SqlDatabaseClient client = SqlDatabaseManager.GetClient())
                {
                    requests = GetFriendsForUser(client, Session.CharacterId, 0);
                }
                Session.SendData(LaptopFriendListComposer.Compose(friends, requests));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static void MarkUpdateNeeded(Session UpdatedSession, int Mode, bool ForceInstant)
        {
            ReadOnlyCollection<uint> friends = UpdatedSession.LaptopFriendCache.Friends;
            if (friends != null)
            {
                foreach (uint num in friends)
                {
                    Session sessionByCharacterId = SessionManager.GetSessionByCharacterId(num);
                    if (sessionByCharacterId != null)
                    {
                        sessionByCharacterId.LaptopFriendCache.MarkUpdateNeeded(UpdatedSession.CharacterId, Mode);
                        ForceLaptopUpdateForSession(sessionByCharacterId);
                    }
                }
            }
        }

        private static void LoadMessages(Session Session, ClientMessage Message)
        {
            using (SqlDatabaseClient client = SqlDatabaseManager.GetClient())
            {
                client.SetParameter("receptor", Session.CharacterId);
                DataTable table = client.ExecuteQueryTable("SELECT * FROM laptop_mensajes WHERE (receptor = @receptor AND leido = '0') OR general = '1'");
                Session.SendData(LaptopLoadMessagesComposer.Compose(table));
            }
        }

        private static void smethod_10(Session session_0, ClientMessage clientMessage_0)
        {
            uint num = clientMessage_0.ReadUnsignedInteger();
            using (SqlDatabaseClient client = SqlDatabaseManager.GetClient())
            {
                client.SetParameter("id", num);
                client.SetParameter("receptor", session_0.CharacterId);
                client.ExecuteNonQuery("UPDATE laptop_mensajes SET leido = '1' WHERE id = @id AND receptor = @receptor LIMIT 1");
            }
        }

        private static void smethod_2(Session session_0, ClientMessage clientMessage_0)
        {
            ForceLaptopUpdateForSession(session_0);
        }

        private static void smethod_3(Session session_0, ClientMessage clientMessage_0)
        {
            string str = InputFilter.FilterString(clientMessage_0.ReadString().Replace('%', ' '), false);
            if (str.Length >= 1)
            {
                CharacterInfo characterInfo = null;
                using (SqlDatabaseClient client = SqlDatabaseManager.GetClient())
                {
                    client.SetParameter("query", str);
                    DataRow row = client.ExecuteQueryRow("SELECT id FROM usuarios WHERE usuario = @query LIMIT 1");
                    if (row != null)
                    {
                        characterInfo = CharacterInfoLoader.GetCharacterInfo(client, uint.Parse(row["id"].ToString()));
                    }
                }
                session_0.SendData(LaptopSearchResultComposer.Compose(characterInfo));
            }
        }

        private static void smethod_4(Session session_0, ClientMessage clientMessage_0)
        {
        }

        private static void smethod_5(Session session_0, ClientMessage clientMessage_0)
        {
            uint characterId = clientMessage_0.ReadUnsignedInteger();
            if ((characterId >= 1) && (characterId != session_0.CharacterId))
            {
                CharacterInfo characterInfo = null;
                using (SqlDatabaseClient client = SqlDatabaseManager.GetClient())
                {
                    characterInfo = CharacterInfoLoader.GetCharacterInfo(client, characterId);
                    if (FriendshipExists(client, session_0.CharacterId, characterInfo.Id, false))
                    {
                        return;
                    }
                    CreateFriendship(client, session_0.CharacterId, characterInfo.Id, false);
                }
                Session sessionByCharacterId = SessionManager.GetSessionByCharacterId(characterInfo.Id);
                if (sessionByCharacterId != null)
                {
                    sessionByCharacterId.SendData(LaptopRequestNotificationComposer.Compose(session_0.CharacterInfo));
                }
            }
        }

        private static void smethod_6(Session session_0, ClientMessage clientMessage_0)
        {
            uint num = clientMessage_0.ReadUnsignedInteger();
            using (SqlDatabaseClient client = SqlDatabaseManager.GetClient())
            {
                client.SetParameter("user1", session_0.CharacterId);
                client.SetParameter("user2", num);
                client.SetParameter("confirmed", 1);
                string query1 = "UPDATE laptop_amigos SET aceptado = '1' WHERE id_usuario = " + session_0.CharacterId + " AND id_amigo = " + num + " LIMIT 1";
                Console.WriteLine(query1);
                if (client.ExecuteNonQuery(query1) > 0)
                {
                    client.ExecuteNonQuery("UPDATE laptop_amigos SET aceptado = '1' WHERE id_usuario = " + num + " AND id_amigo = " + session_0.CharacterId + " LIMIT 1");
                    session_0.LaptopFriendCache.AddToCache(num);
                    Session sessionByCharacterId = SessionManager.GetSessionByCharacterId(num);
                    if (sessionByCharacterId != null)
                    {
                        sessionByCharacterId.LaptopFriendCache.AddToCache(session_0.CharacterId);
                        ForceLaptopUpdateForSession(sessionByCharacterId);
                        sessionByCharacterId.SendData(LaptopNewFriendComposer.Compose(session_0.CharacterInfo));
                    }
                    session_0.SendData(LaptopAcceptFriendComposer.Compose(num));
                }
            }
        }

        private static void smethod_7(Session session_0, ClientMessage clientMessage_0)
        {
            uint num = clientMessage_0.ReadUnsignedInteger();
            using (SqlDatabaseClient client = SqlDatabaseManager.GetClient())
            {
                DestroyFriendship(client, session_0.CharacterId, num);
            }
            session_0.SendData(LaptopDeclineBuddyComposer.Compose(session_0.CharacterId));
        }

        private static void smethod_8(Session session_0, ClientMessage clientMessage_0)
        {
            uint num = clientMessage_0.ReadUnsignedInteger();
            using (SqlDatabaseClient client = SqlDatabaseManager.GetClient())
            {
                if (DestroyFriendship(client, session_0.CharacterId, num))
                {
                    session_0.LaptopFriendCache.RemoveFromCache(num);
                    Session sessionByCharacterId = SessionManager.GetSessionByCharacterId(num);
                    if (sessionByCharacterId != null)
                    {
                        sessionByCharacterId.LaptopFriendCache.RemoveFromCache(session_0.CharacterId);
                        sessionByCharacterId.SendData(LaptopDeleteFriendComposer.Compose(session_0.CharacterId));
                    }
                }
            }
            session_0.SendData(LaptopDeleteFriendComposer.Compose(num));
        }

        private static void smethod_9(Session session_0, ClientMessage clientMessage_0)
        {
            uint num = clientMessage_0.ReadUnsignedInteger();
            string str = InputFilter.FilterString(clientMessage_0.ReadString(), false).Trim();
            if ((((num > 0) && (str.Length >= 1)) && (num != session_0.CharacterId)) && session_0.LaptopFriendCache.Friends.Contains(num))
            {
                Session sessionByCharacterId = SessionManager.GetSessionByCharacterId(num);
                if (sessionByCharacterId == null)
                {
                    using (SqlDatabaseClient client = SqlDatabaseManager.GetClient())
                    {
                        client.SetParameter("sender", session_0.CharacterId);
                        client.SetParameter("receptor", num);
                        client.SetParameter("text", str);
                        client.SetParameter("time", UnixTimestamp.GetCurrent());
                        client.SetParameter("color", session_0.CharacterInfo.Staff);
                        client.ExecuteNonQuery("INSERT INTO laptop_mensajes (emisor, receptor, contenido, timestamp, color, leido) VALUES (@sender, @receptor, @text, @time, @color, 0)");
                    }
                }
                else
                {
                    sessionByCharacterId.SendData(LaptopMessageComposer.Compose(session_0.CharacterId, str, session_0.CharacterInfo.Staff));
                }
            }
        }
    }
}
