using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snowlight.Specialized;
using Snowlight.Game.Sessions;

namespace Snowlight.Game.Spaces
{
    public class SpaceModel
    {
        /* private scope */
        Game.Spaces.Heightmap heightmap_0;
        /* private scope */
        int int_0;
        /* private scope */
        int int_1;
        /* private scope */
        SpaceModelType spaceModelType_0;
        /* private scope */
        string string_0;
        /* private scope */
        Vector3 vector3_0;

        public SpaceModel(string string_1, SpaceModelType Type, Game.Spaces.Heightmap Heightmap, Vector3 DoorPosition, int CharacterRotation, int MaxUsers)
        {
            this.string_0 = string_1;
            this.spaceModelType_0 = Type;
            this.heightmap_0 = Heightmap;
            this.vector3_0 = DoorPosition;
            this.int_0 = CharacterRotation;
            this.int_1 = MaxUsers;
        }

        public bool IsUsableBySession(Session Session)
        {
            if (this.spaceModelType_0 == SpaceModelType.Area)
            {
                return false;
            }
            return true;
        }

        public int CharacterRotation
        {
            get
            {
                return this.int_0;
            }
        }

        public Vector3 DoorPosition
        {
            get
            {
                return this.vector3_0;
            }
        }

        public Game.Spaces.Heightmap Heightmap
        {
            get
            {
                return this.heightmap_0;
            }
        }

        public int MaxUsers
        {
            get
            {
                return this.int_1;
            }
        }

        public string String_0
        {
            get
            {
                return this.string_0;
            }
        }

        public SpaceModelType Type
        {
            get
            {
                return this.spaceModelType_0;
            }
        }
    }
}
