using System;

namespace Snowlight.Specialized
{
    public class Vector3
    {
        /* private scope */
        int int_0;
        /* private scope */
        int int_1;
        /* private scope */
        int int_2;

        public Vector3()
        {
            this.int_0 = 0;
            this.int_1 = 0;
            this.int_2 = 0;
        }

        public Vector3(int int_3, int int_4, int int_5)
        {
            this.int_0 = int_3;
            this.int_1 = int_4;
            this.int_2 = int_5;
        }

        public static Vector3 FromString(string Input)
        {
            string[] strArray = Input.Split(new char[] { '|' });
            int result = 0;
            int num2 = 0;
            int num3 = 0;
            int.TryParse(strArray[0], out result);
            if (strArray.Length > 1)
            {
                int.TryParse(strArray[1], out num2);
            }
            if (strArray.Length > 2)
            {
                int.TryParse(strArray[2], out num3);
            }
            return new Vector3(result, num2, num3);
        }

        public Vector2 GetVector2()
        {
            return new Vector2(this.int_0, this.int_1);
        }

        public override string ToString()
        {
            return string.Concat(new object[] { this.int_0, "|", this.int_1, "|", this.int_2 });
        }

        public int Int32_0
        {
            get
            {
                return this.int_0;
            }
            set
            {
                this.int_0 = value;
            }
        }

        public int Int32_1
        {
            get
            {
                return this.int_1;
            }
            set
            {
                this.int_1 = value;
            }
        }

        public int Int32_2
        {
            get
            {
                return this.int_2;
            }
            set
            {
                this.int_2 = value;
            }
        }
    }
}