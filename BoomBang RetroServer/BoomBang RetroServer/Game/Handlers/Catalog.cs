using BoomBang_RetroServer.Game.Items;
using BoomBang_RetroServer.Sessions;
using BoomBang_RetroServer.Sockets.Messages;
using BoomBang_RetroServer.Game.Users;
using BoomBang_RetroServer.Configuration;
using BoomBang_RetroServer.Database;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoomBang_RetroServer.Game.Handlers
{
    public partial class Catalog : Handler
    {
        public void Handler_189_133()
        {
            ServerMessage Message1 = new ServerMessage(new byte[] { 189, 133 });
            Message1.AppendParameter(CatalogManager.total_load_items);
            foreach(KeyValuePair<int, CatalogGroup> Item in CatalogManager.Items)
            {
                Message1.AppendParameter(Item.Value.CatalogItem.id);
                Message1.AppendParameter(Item.Value.CatalogItem.name);
                Message1.AppendParameter(Item.Value.CatalogItem.gold);
                Message1.AppendParameter(Item.Value.CatalogItem.silver);
                Message1.AppendParameter(Item.Value.CatalogItem.categories);
                Message1.AppendParameter(Item.Value.CatalogItem.colors);
                Message1.AppendParameter(Item.Value.CatalogItem.rgb);
                Message1.AppendParameter(Item.Value.CatalogItem.size_m);
                Message1.AppendParameter(Item.Value.CatalogItem.size_l);
                Message1.AppendParameter(Item.Value.CatalogItem.size_s);
                Message1.AppendParameter(Item.Value.CatalogItem.something_1);
                Message1.AppendParameter(Item.Value.CatalogItem.something_2);
                Message1.AppendParameter(Item.Value.CatalogItem.something_3);
                Message1.AppendParameter(Item.Value.CatalogItem.something_4);
                Message1.AppendParameter(Item.Value.CatalogItem.something_5);
                Message1.AppendParameter(Item.Value.CatalogItem.something_6);
                Message1.AppendParameter(Item.Value.CatalogItem.something_10);
                Message1.AppendParameter(Item.Value.CatalogItem.vip);
                Message1.AppendParameter(Item.Value.CatalogItem.something_11);
                Message1.AppendParameter(Item.Value.CatalogItem.activated);
                Message1.AppendParameter(Item.Value.CatalogItem.something_12);
                Message1.AppendParameter(Item.Value.CatalogItem.something_13);
                Message1.AppendParameter(Item.Value.CatalogItem.something_14);
                Message1.AppendParameter(Item.Value.CatalogItem.something_15);
                Message1.AppendParameter(Item.Value.CatalogItem.something_16);
                Message1.AppendParameter(Item.Value.CatalogItem.something_17);
            }
            SendMessage(Message1);
        }
    }
}
