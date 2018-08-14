namespace BoomBang.Communication.ResponseCache
{
    using BoomBang.Communication;
    using System;

    public class ResponseCacheItem
    {
        /* private scope */ ClientMessage clientMessage_0;
        /* private scope */ ServerMessage serverMessage_0;
        /* private scope */ uint uint_0;

        public ResponseCacheItem(uint GroupId, ClientMessage Request, ServerMessage Response)
        {
            this.uint_0 = GroupId;
            this.clientMessage_0 = Request;
            this.serverMessage_0 = Response;
        }

        public uint GroupId
        {
            get
            {
                return this.uint_0;
            }
        }

        public ClientMessage R
        {
            get
            {
                return this.clientMessage_0;
            }
        }

        public ServerMessage Response
        {
            get
            {
                return this.serverMessage_0;
            }
        }
    }
}

