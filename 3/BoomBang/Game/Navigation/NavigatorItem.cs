using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snowlight.Game.Spaces;

namespace Snowlight.Game.Navigation
{
    class NavigatorItem
    {
        /* private scope */
        bool bool_0;
        /* private scope */
        NavigatorCategory navigatorCategory_0;
        /* private scope */
        string string_0;
        /* private scope */
        uint uint_0;
        /* private scope */
        uint uint_1;
        /* private scope */
        uint uint_2;

        public NavigatorItem(uint uint_3, uint ParentId, uint SpaceId, string Name, bool IsCategory, NavigatorCategory Category)
        {
            this.uint_0 = uint_3;
            this.uint_1 = ParentId;
            this.uint_2 = SpaceId;
            this.string_0 = Name;
            this.bool_0 = IsCategory;
            this.navigatorCategory_0 = Category;
        }

        public SpaceInfo GetSpaceInfo()
        {
            return SpaceInfoLoader.GetSpaceInfo(this.uint_2);
        }

        public SpaceInstance TryGetSpaceInstance()
        {
            return SpaceManager.GetInstanceBySpaceId(this.uint_0);
        }

        public NavigatorCategory Category
        {
            get
            {
                return this.navigatorCategory_0;
            }
        }

        public bool IsCategory
        {
            get
            {
                return this.bool_0;
            }
        }

        public string Name
        {
            get
            {
                return this.string_0;
            }
        }

        public uint ParentId
        {
            get
            {
                return this.uint_1;
            }
        }

        public uint SpaceId
        {
            get
            {
                return this.uint_2;
            }
        }

        public uint UInt32_0
        {
            get
            {
                return this.uint_0;
            }
        }
    }
}
