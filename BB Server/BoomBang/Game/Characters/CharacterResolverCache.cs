using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snowlight.Storage;

namespace Snowlight.Game.Characters
{
    class CharacterResolverCache
    {
        static Dictionary<uint, string> dictionary_0 = new Dictionary<uint, string>();

        public static void AddToCache(uint uint_0, string Name, bool Override)
        {
            lock (dictionary_0)
            {
                if (dictionary_0.ContainsKey(uint_0))
                {
                    if (Override)
                    {
                        dictionary_0[uint_0] = Name;
                    }
                }
                else
                {
                    dictionary_0.Add(uint_0, Name);
                }
            }
        }

        public static string GetNameFromUid(uint UserId)
        {
            lock (dictionary_0)
            {
                if (dictionary_0.ContainsKey(UserId))
                {
                    return dictionary_0[UserId];
                }
                using (SqlDatabaseClient client = SqlDatabaseManager.GetClient())
                {
                    client.SetParameter("id", UserId);
                    string str = (string)client.ExecuteScalar("SELECT usuario FROM usuarios WHERE id = @id LIMIT 1");
                    if ((str != null) && (str.Length > 0))
                    {
                        dictionary_0.Add(UserId, str);
                        return str;
                    }
                }
            }
            return "Usuario desconocido";
        }

        public static uint GetUidFromName(string Name)
        {
            lock (dictionary_0)
            {
                foreach (KeyValuePair<uint, string> pair in dictionary_0)
                {
                    if (pair.Value.ToLower() == Name.ToLower())
                    {
                        return pair.Key;
                    }
                }
                using (SqlDatabaseClient client = SqlDatabaseManager.GetClient())
                {
                    client.SetParameter("username", Name);
                    object obj2 = client.ExecuteScalar("SELECT id FROM usuarios WHERE usuario = @username LIMIT 1");
                    if (obj2 != null)
                    {
                        uint key = (uint)obj2;
                        dictionary_0.Add(key, Name);
                        return key;
                    }
                }
            }
            return 0;
        }
    }
}
