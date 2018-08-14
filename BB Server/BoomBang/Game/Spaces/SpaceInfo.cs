using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snowlight.Game.Characters;
using Snowlight.Storage;

namespace Snowlight.Game.Spaces
{
    public class SpaceInfo
    {
        /* private scope */
        bool bool_0;
        /* private scope */
        bool bool_1;
        /* private scope */
        double double_0;
        /* private scope */
        List<string> list_0;
        /* private scope */
        List<string> list_1;
        /* private scope */
        List<uint> list_2;
        /* private scope */
        object object_0;
        /* private scope */
        SpaceAccessType spaceAccessType_0;
        /* private scope */
        Game.Spaces.SpaceType spaceType_0;
        /* private scope */
        string string_0;
        /* private scope */
        string string_1;
        /* private scope */
        string string_2;
        /* private scope */
        string string_3;
        /* private scope */
        uint uint_0;
        /* private scope */
        uint uint_1;
        /* private scope */
        uint uint_2;
        /* private scope */
        uint uint_3;
        internal uint uint_4;

        public SpaceInfo(uint uint_5, List<uint> SubIds, uint ParentId, string Name, string Description, uint OwnerId, uint SpaceId, Game.Spaces.SpaceType SpaceType, SpaceAccessType AccessType, string Password, bool AllowUppercuts, bool AllowCoconuts, List<string> WhiteList, List<string> BlackList, uint MaxUsers, string ModelName)
        {
            this.uint_0 = uint_5;
            this.list_2 = SubIds;
            this.uint_1 = ParentId;
            this.string_0 = Name;
            this.string_1 = Description;
            this.uint_4 = OwnerId;
            this.uint_2 = SpaceId;
            this.spaceType_0 = SpaceType;
            this.spaceAccessType_0 = AccessType;
            this.string_3 = Password;
            this.bool_0 = AllowCoconuts;
            this.bool_1 = AllowUppercuts;
            this.list_0 = WhiteList;
            this.list_1 = BlackList;
            this.double_0 = UnixTimestamp.GetCurrent();
            this.uint_3 = MaxUsers;
            this.string_2 = ModelName;
            this.object_0 = new object();
        }

        public void EditSpace(string Name, string Description, SpaceAccessType AccessType, string Password, bool AllowUppercuts, bool AllowCoconuts, List<string> WhiteList, List<string> BlackList)
        {
            lock (this.object_0)
            {
                this.string_0 = Name;
                this.string_1 = Description;
                this.spaceAccessType_0 = AccessType;
                this.string_3 = Password;
                this.bool_1 = AllowUppercuts;
                this.bool_0 = AllowCoconuts;
                this.list_0 = WhiteList;
                this.list_1 = BlackList;
                using (SqlDatabaseClient client = SqlDatabaseManager.GetClient())
                {
                    StringBuilder builder = new StringBuilder();
                    StringBuilder builder2 = new StringBuilder();
                    foreach (string str in WhiteList)
                    {
                        if (builder.Length > 0)
                        {
                            builder.Append(",");
                        }
                        builder.Append(str);
                    }
                    foreach (string str2 in BlackList)
                    {
                        if (builder2.Length > 0)
                        {
                            builder2.Append(",");
                        }
                        builder.Append(str2);
                    }
                    string str3 = "open";
                    if (this.spaceAccessType_0 == SpaceAccessType.PasswordProtected)
                    {
                        str3 = "password";
                    }
                    else if (this.spaceAccessType_0 == SpaceAccessType.Locked)
                    {
                        str3 = "lock";
                    }
                    client.SetParameter("id", this.uint_0);
                    client.SetParameter("name", this.string_0);
                    client.SetParameter("description", this.string_1);
                    client.SetParameter("accesstype", str3);
                    client.SetParameter("password", this.string_3);
                    client.SetParameter("whitelist", builder);
                    client.SetParameter("blacklist", builder2);
                    client.SetParameter("allowuppercuts", this.bool_1 ? "1" : "0");
                    client.SetParameter("allowcoconuts", this.bool_0 ? "1" : "0");
                    client.ExecuteNonQuery("UPDATE escenarios SET nombre = @name, descripcion = @description, acceso = @accesstype, password = @password, lista_verde = @whitelist, lista_negra = @blacklist, permitir_upper = @allowuppercuts, permitir_coco = @allowcoconuts WHERE id = @id LIMIT 1");
                }
            }
        }

        public SpaceModel TryGetModel()
        {
            lock (this.object_0)
            {
                return SpaceManager.GetModel(this.string_2);
            }
        }

        public SpaceAccessType AccessType
        {
            get
            {
                return this.spaceAccessType_0;
            }
            set
            {
                this.spaceAccessType_0 = value;
            }
        }

        public bool AllowCoconuts
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

        public bool AllowUppercuts
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

        public List<string> BlackList
        {
            get
            {
                return this.list_1;
            }
            set
            {
                this.list_1 = value;
            }
        }

        public double CacheAge
        {
            get
            {
                return (UnixTimestamp.GetCurrent() - this.double_0);
            }
        }

        public int CurrentUsers
        {
            get
            {
                SpaceInstance instanceBySpaceId = SpaceManager.GetInstanceBySpaceId(this.uint_0);
                if (instanceBySpaceId != null)
                {
                    return instanceBySpaceId.CachedNavigatorUserCount;
                }
                return 0;
            }
        }

        public string Description
        {
            get
            {
                return this.string_1;
            }
            set
            {
                this.string_1 = value;
            }
        }

        public uint MaxUsers
        {
            get
            {
                return this.uint_3;
            }
        }

        public string Name
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }

        public uint OwnerId
        {
            get
            {
                return this.uint_4;
            }
        }

        public string OwnerName
        {
            get
            {
                if (this.uint_4 != 0)
                {
                    return CharacterResolverCache.GetNameFromUid(this.uint_4);
                }
                return string.Empty;
            }
        }

        public uint ParentId
        {
            get
            {
                return this.uint_1;
            }
        }

        public string Password
        {
            get
            {
                return this.string_3;
            }
            set
            {
                this.string_3 = value;
            }
        }

        public uint SpaceId
        {
            get
            {
                return this.uint_2;
            }
        }

        public Game.Spaces.SpaceType SpaceType
        {
            get
            {
                return this.spaceType_0;
            }
        }

        public List<uint> SubIds
        {
            get
            {
                return this.list_2;
            }
        }

        public uint UInt32_0
        {
            get
            {
                return this.uint_0;
            }
        }

        public List<string> WhiteList
        {
            get
            {
                return this.list_0;
            }
            set
            {
                this.list_0 = value;
            }
        }
    }
}
