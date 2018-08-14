namespace BoomBang.Communication.Incoming
{
    using BoomBang;
    using BoomBang.Communication;
    using BoomBang.Game.Sessions;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public static class DataRouter
    {
        /* private scope */ static Dictionary<KeyValuePair<uint, uint>, ProcessRequestCallback> dictionary_0;
        /* private scope */ static List<KeyValuePair<uint, uint>> list_0;

        public static void HandleData(Session Session, ClientMessage Message)
        {
            if (((Session != null) && !Session.Stopped) && (Message != null))
            {
                if (!dictionary_0.ContainsKey(new KeyValuePair<uint, uint>(Message.Flag, Message.Item)))
                {
                    Output.WriteLine(string.Concat(new object[] { "Unhandled packet -> Flag: ", Message.Flag, " (", Message.FlagString, "), Item: ", Message.Item, " (", Message.ItemString, "), no suitable handler found." }), OutputLevel.Warning);
                }
                else if (Session.Authenticated || list_0.Contains(new KeyValuePair<uint, uint>(Message.Flag, Message.Item)))
                {
                    dictionary_0[new KeyValuePair<uint, uint>(Message.Flag, Message.Item)](Session, Message);
                }
            }
        }

        public static void Initialize()
        {
            dictionary_0 = new Dictionary<KeyValuePair<uint, uint>, ProcessRequestCallback>();
            list_0 = new List<KeyValuePair<uint, uint>>();
        }

        public static bool RegisterHandler(uint MessageFlag, uint MessageItem, ProcessRequestCallback Callback, bool PermitedUnauthenticated = false)
        {
            if (Callback == null)
            {
                return false;
            }
            if (dictionary_0.ContainsKey(new KeyValuePair<uint, uint>(MessageFlag, MessageItem)))
            {
                return false;
            }
            dictionary_0.Add(new KeyValuePair<uint, uint>(MessageFlag, MessageItem), Callback);
            if (PermitedUnauthenticated)
            {
                list_0.Add(new KeyValuePair<uint, uint>(MessageFlag, MessageItem));
            }
            return true;
        }
    }
}

