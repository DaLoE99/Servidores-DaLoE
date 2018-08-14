namespace BoomBang.Communication.ResponseCache
{
    using BoomBang;
    using BoomBang.Communication;
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public class ResponseCacheController : IDisposable
    {
        /* private scope */ int int_0;
        /* private scope */ List<ResponseCacheItem> list_0;
        /* private scope */ Thread thread_0;
        /* private scope */ uint uint_0;

        public ResponseCacheController(int ItemLifetime, uint MaximalCachedElements)
        {
            this.int_0 = ItemLifetime;
            this.uint_0 = MaximalCachedElements;
            this.list_0 = new List<ResponseCacheItem>();
            this.thread_0 = new Thread(new ThreadStart(this.method_0));
            this.thread_0.Priority = ThreadPriority.BelowNormal;
            this.thread_0.Name = "CacheControllerInstanceThread";
            this.thread_0.Start();
        }

        public void AddIfNeeded(uint GroupId, ClientMessage Request, ServerMessage Response)
        {
            lock (this.list_0)
            {
                foreach (ResponseCacheItem item in this.list_0)
                {
                    if ((item.GroupId == GroupId) && (item.Request.ToString() == Request.ToString()))
                    {
                        goto Label_0086;
                    }
                }
                ResponseCacheItem item2 = new ResponseCacheItem(GroupId, Request, Response);
                this.list_0.Add(item2);
            Label_0086:;
            }
        }

        public void ClearCache()
        {
            lock (this.list_0)
            {
                this.list_0.Clear();
            }
        }

        public void ClearCacheGroup(uint GroupId)
        {
            lock (this.list_0)
            {
                List<ResponseCacheItem> list = new List<ResponseCacheItem>();
                foreach (ResponseCacheItem item in this.list_0)
                {
                    if (item.GroupId == GroupId)
                    {
                        list.Add(item);
                    }
                }
                foreach (ResponseCacheItem item2 in list)
                {
                    this.list_0.Remove(item2);
                }
            }
        }

        public void Dispose()
        {
            if (this.thread_0 != null)
            {
                this.thread_0.Abort();
                this.thread_0 = null;
            }
            if (this.list_0 != null)
            {
                this.list_0.Clear();
                this.thread_0 = null;
            }
        }

        private void method_0()
        {
            try
            {
                while (Program.Alive)
                {
                    lock (this.list_0)
                    {
                        this.list_0.Clear();
                    }
                    Thread.Sleep((int) (this.int_0 * 0x3e8));
                }
            }
            catch (ThreadAbortException)
            {
            }
            catch (ThreadInterruptedException)
            {
            }
        }

        public ServerMessage TryGetResponse(uint GroupId, ClientMessage Request)
        {
            lock (this.list_0)
            {
                if (this.list_0.Count > this.uint_0)
                {
                    this.list_0.RemoveAt(this.list_0.Count - 1);
                }
                foreach (ResponseCacheItem item in this.list_0)
                {
                    if ((item.GroupId == GroupId) && (item.Request.ToString() == Request.ToString()))
                    {
                        return item.Response;
                    }
                }
            }
            return null;
        }
    }
}

