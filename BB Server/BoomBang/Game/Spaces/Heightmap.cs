using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snowlight.Specialized;
using System.Text.RegularExpressions;

namespace Snowlight.Game.Spaces
{
    public class Heightmap
    {
        internal int int_0;
        internal int int_1;
        /* private scope */
        TileState[,] tileState_0;
        /* private scope */
        Vector2[] vector2_0;
        /* private scope */
        Vector2[] vector2_1;

        public Heightmap(string HeightmapData)
        {
            string[] strArray = Regex.Split(HeightmapData, "\r\n");
            this.int_0 = strArray[0].Length;
            this.int_1 = strArray.Length;
            this.tileState_0 = new TileState[this.int_0, this.int_1];
            for (int i = 0; i < this.int_1; i++)
            {
                for (int j = 0; j < this.int_0; j++)
                {
                    string str = strArray[i][j].ToString();
                    this.tileState_0[j, i] = (str == "1") ? TileState.Blocked : TileState.Open;
                }
            }
            this.GetOpenTiles();
            this.GetClosedTiles();
        }

        public void GetClosedTiles()
        {
            int num = 0;
            for (int i = 0; i < this.int_1; i++)
            {
                for (int k = 0; k < this.int_0; k++)
                {
                    if (this.tileState_0[k, i] == TileState.Blocked)
                    {
                        num++;
                    }
                }
            }
            this.vector2_1 = new Vector2[num];
            num = 0;
            for (int j = 0; j < this.int_1; j++)
            {
                for (int m = 0; m < this.int_0; m++)
                {
                    if (this.tileState_0[m, j] == TileState.Blocked)
                    {
                        this.vector2_1[num++] = new Vector2(m, j);
                    }
                }
            }
        }

        public void GetOpenTiles()
        {
            int num = 0;
            for (int i = 0; i < this.int_1; i++)
            {
                for (int k = 0; k < this.int_0; k++)
                {
                    if (this.tileState_0[k, i] == TileState.Open)
                    {
                        num++;
                    }
                }
            }
            this.vector2_0 = new Vector2[num];
            num = 0;
            for (int j = 0; j < this.int_1; j++)
            {
                for (int m = 0; m < this.int_0; m++)
                {
                    if (this.tileState_0[m, j] == TileState.Open)
                    {
                        this.vector2_0[num++] = new Vector2(m, j);
                    }
                }
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < this.int_1; i++)
            {
                for (int j = 0; j < this.int_0; j++)
                {
                    builder.Append((this.tileState_0[j, i] == TileState.Blocked) ? "1" : "0");
                }
                builder.Append(Convert.ToChar(13));
            }
            return builder.ToString();
        }

        public Vector2[] ClosedTiles
        {
            get
            {
                return this.vector2_1;
            }
            set
            {
                this.vector2_1 = value;
            }
        }

        public Vector2[] OpenTiles
        {
            get
            {
                return this.vector2_0;
            }
            set
            {
                this.vector2_0 = value;
            }
        }

        public int SizeX
        {
            get
            {
                return this.int_0;
            }
        }

        public int SizeY
        {
            get
            {
                return this.int_1;
            }
        }

        public TileState[,] TileStates
        {
            get
            {
                return this.tileState_0;
            }
        }
    }
}
