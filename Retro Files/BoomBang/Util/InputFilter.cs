using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snowlight.Util
{
    class InputFilter
    {
        public static string FilterString(string Input, bool PermitLineBreaks = false)
        {
            Input = Input.Trim();
            Input = Input.Replace(Convert.ToChar(1), ' ');
            Input = Input.Replace(Convert.ToChar(2), ' ');
            Input = Input.Replace(Convert.ToChar(3), ' ');
            Input = Input.Replace(Convert.ToChar(9), ' ');
            if (!PermitLineBreaks)
            {
                Input = Input.Replace(Convert.ToChar(10), ' ');
                Input = Input.Replace(Convert.ToChar(13), ' ');
            }
            return Input;
        }

        public static string MergeString(string[] Input, int Start)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < Input.Length; i++)
            {
                if (i >= Start)
                {
                    if (i > Start)
                    {
                        builder.Append(" ");
                    }
                    builder.Append(Input[i]);
                }
            }
            return builder.ToString();
        }
    }
}
