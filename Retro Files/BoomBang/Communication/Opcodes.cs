using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snowlight.Communication
{
    public static class Opcodes
    {
        public const uint PING = 1630;
        public const uint RFACE = 120145;
        public const uint REGISTERCHECKUSER = 120139;
        public const uint REGISTER = 120131;
        public const uint LOGIN = 120121;
        public const uint INITFLOWER = 120149;
        public const uint LAPTOPLOADFRIENDS = 132120;
        public const uint LAPTOPUPDATE = 132122;
        public const uint LAPTOPLOADMESSAGES = 132121;
        public const uint LAPTOPSEARCHBUDDY = 132129;
        public const uint LAPTOPUPDATELAPTOP = 132122;
        public const uint LAPTOPUPDATEBUDDY = 132126;
        public const uint LAPTOPFRIENREQUEST = 132135;
        public const uint LAPTOPFRIENDDECLINE = 132130;
        public const uint LAPTOPFRIENDACCEPT = 132124;
        public const uint LAPTOPFRIENDDELETE = 132125;
        public const uint LAPTOPSENDMESSAGE = 132136;
        public const uint LAPTOPDELETEMESSAGE = 132128;
        public const uint ADVERTISEMENT = 120141;
        public const uint CONTESTINIT = 120134;
        public const uint CATALOGLOADITEMS = 189133;
        public const uint CATALOGLOADCONFIRMATION = 189180;
        public const uint FLOWERPOWER = 120143;
        public const uint NEWSLOAD = 208120;
        public const uint NEWSREAD = 208121;
        public const uint UNKNOW1 = 120147;
        public const uint NAVIGATORITEMSLOAD = 15432;
        public const uint NAVIGATORSUBITEMSLOAD = 15433;
        public const uint SPACESEXITBROADCAST = 128123;
        public const uint SPACESEXITLAND = 128124;
        public const uint USERCHAT = 1860;
        public const uint USERWHISPER = 1360;
        public const uint SPACEITEMADD = 200120;
        public const uint SPACESPOSTUSER = 128122;
        public const uint SPACESLOADUSER = 128121;
        public const uint ACTIONS = 1750;
        public const uint SENDUPPERCUT = 1450;
        public const uint REMOVEUPPERCUT = 1530;
        public const uint WALK = 1820;
        public const uint SPACESENTERSCENE = 128120;
        public const uint SPACESENTERRANDSCENE = 128125;
        public const uint SPACESLOADUSERS = 128121;
        public const uint ISLANDCREATE = 189120;
        public const uint ISLANDCREATEZONE = 189121;
        public const uint ISLANDPRE = 189124;
        public const uint SPACEITEMCATCH = 200121;
        public const uint USERACTIONS = 1340;
        public const uint USERROTATION = 1350;
        public const uint USERHOBBYS = 1560;
        public const uint USERWHISES = 1570;
        public const uint USERVOTES = 1550;
        public const uint USERMOTTO = 1580;
        public const uint USERINTERACTSEND = 137120;
        public const uint USERINTERACTACCEPT = 137122;
        public const uint USERINTERACTCANCEL = 137124;
        public const uint TARGETUSERINTERACTCANCEL = 137123;
        public const uint LATENCYTEST = 1670;
        public const uint COLORUPPERCUT = 1290;
        public const uint COLORCOCONUT = 1310;
        public const uint USERGOLDCREDITSADD = 1620;
        public const uint USERSILVERCREDITSADD = 1660;
        public const uint USERGOLDCREDITSREMOVE = 1610;
        public const uint USERSILVERCREDITSREMOVE = 1650;
        public const uint SPACEITEMREMOVE = 200123;
        public const uint UPDATESTATISTICS = 1460;
        public const uint SILVERTIMELEFT = 1520;
        public const uint UNKNOW2 = 212120;
        
    }
}
