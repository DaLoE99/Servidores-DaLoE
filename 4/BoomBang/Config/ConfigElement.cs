namespace BoomBang.Config
{
    using System;
    using System.Net;

    public class ConfigElement
    {
        /* private scope */ bool bool_0;
        /* private scope */ ConfigElementType configElementType_0;
        /* private scope */ object object_0;
        /* private scope */ string string_0;

        public ConfigElement(string Key, ConfigElementType Type, object DefaultValue)
        {
            this.string_0 = Key;
            this.configElementType_0 = Type;
            this.CurrentValue = DefaultValue;
            this.bool_0 = false;
        }

        public object CurrentValue
        {
            get
            {
                return this.object_0;
            }
            set
            {
                string s = value.ToString();
                switch (this.configElementType_0)
                {
                    case ConfigElementType.Boolean:
                        this.object_0 = false;
                        if ((s == "1") || (s.ToLower() == "true"))
                        {
                            this.object_0 = true;
                        }
                        break;

                    case ConfigElementType.Integer:
                    {
                        int result = 0;
                        int.TryParse(s, out result);
                        this.object_0 = result;
                        break;
                    }
                    case ConfigElementType.IpAddress:
                    {
                        IPAddress any = IPAddress.Any;
                        IPAddress.TryParse(s, out any);
                        this.object_0 = any;
                        break;
                    }
                    default:
                        this.object_0 = s;
                        break;
                }
                this.bool_0 = true;
            }
        }

        public string Key
        {
            get
            {
                return this.string_0;
            }
        }

        public ConfigElementType Type
        {
            get
            {
                return this.configElementType_0;
            }
        }

        public bool UserConfigured
        {
            get
            {
                return this.bool_0;
            }
        }
    }
}

