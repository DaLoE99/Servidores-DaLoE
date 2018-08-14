namespace BoomBang.Specialized
{
    using System;

    public class Vector2
    {
        internal int int_0;
        internal int int_1;

        public Vector2()
        {
            this.int_0 = 0;
            this.int_1 = 0;
        }

        public Vector2(int int_2, int int_3)
        {
            this.int_0 = int_2;
            this.int_1 = int_3;
        }

        public static Vector2 FromString(string Input)
        {
            string[] strArray = Input.Split(new char[] { '|' });
            int result = 0;
            int num2 = 0;
            int.TryParse(strArray[0], out result);
            if (strArray.Length > 1)
            {
                int.TryParse(strArray[1], out num2);
            }
            return new Vector2(result, num2);
        }

        public Vector3 GetVector3()
        {
            return new Vector3(this.int_0, this.int_1, 0);
        }

        public override string ToString()
        {
            return (this.int_0 + "|" + this.int_1);
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
    }
}

