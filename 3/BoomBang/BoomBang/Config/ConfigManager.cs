namespace BoomBang.Config
{
    using BoomBang;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;

    public static class ConfigManager
    {
        /* private scope */ static Dictionary<string, ConfigElement> dictionary_0;
        /* private scope */ static string string_0;

        public static object GetValue(string Key)
        {
            if ((dictionary_0 == null) || !dictionary_0.ContainsKey(Key))
            {
                throw new KeyNotFoundException();
            }
            return dictionary_0[Key].CurrentValue;
        }

        public static void Initialize(string ConfigPath)
        {
            string_0 = ConfigPath;
            dictionary_0 = new Dictionary<string, ConfigElement>();
            dictionary_0.Add("output.enablelogfiles", new ConfigElement("output.enablelogfiles", ConfigElementType.Boolean, true));
            dictionary_0.Add("output.verbositylevel", new ConfigElement("output.verbositylevel", ConfigElementType.Integer, -1));
            dictionary_0.Add("mysql.pool.min", new ConfigElement("mysql.pool.min", ConfigElementType.Integer, 5));
            dictionary_0.Add("mysql.pool.max", new ConfigElement("mysql.pool.max", ConfigElementType.Integer, 20));
            dictionary_0.Add("mysql.pool.lifetime", new ConfigElement("mysql.pool.lifetime", ConfigElementType.Integer, 10));
            dictionary_0.Add("mysql.user", new ConfigElement("mysql.user", ConfigElementType.Text, "useddddr2573869"));
            dictionary_0.Add("mysql.pass", new ConfigElement("mysql.pass", ConfigElementType.Text, "pass"));
            dictionary_0.Add("mysql.host", new ConfigElement("mysql.host", ConfigElementType.Text, "localhost"));
            dictionary_0.Add("mysql.port", new ConfigElement("mysql.port", ConfigElementType.Integer, 0xcea));
            dictionary_0.Add("mysql.dbname", new ConfigElement("mysql.port", ConfigElementType.Text, "db2573869-boombang"));
            dictionary_0.Add("net.backlog", new ConfigElement("net.backlog", ConfigElementType.Integer, 50));
            dictionary_0.Add("net.bind.ip", new ConfigElement("net.bind.ip", ConfigElementType.IpAddress, IPAddress.Any));
            dictionary_0.Add("net.bind.port", new ConfigElement("net.bind.port", ConfigElementType.Integer, 0x94d5));
            dictionary_0.Add("pathfinder.mode", new ConfigElement("pathfinder.mode", ConfigElementType.Text, "simple"));
            dictionary_0.Add("cache.catalog.enabled", new ConfigElement("cache.catalog.enabled", ConfigElementType.Boolean, true));
            dictionary_0.Add("cache.catalog.lifetime", new ConfigElement("cache.catalog.lifetime", ConfigElementType.Integer, 120));
            dictionary_0.Add("cache.catalog.maximaldata", new ConfigElement("cache.catalog.maximaldata", ConfigElementType.Integer, 0x2710));
            dictionary_0.Add("cache.navigator.enabled", new ConfigElement("cache.navigator.enabled", ConfigElementType.Boolean, true));
            dictionary_0.Add("cache.navigator.lifetime", new ConfigElement("cache.navigator.lifetime", ConfigElementType.Integer, 10));
            dictionary_0.Add("cache.navigator.maximaldata", new ConfigElement("cache.navigator.maximaldata", ConfigElementType.Integer, 0x2710));
            dictionary_0.Add("activitypoints.enabled", new ConfigElement("activitypoints.enabled", ConfigElementType.Boolean, true));
            dictionary_0.Add("activitypoints.interval", new ConfigElement("activitypoints.interval", ConfigElementType.Integer, 0x708));
            dictionary_0.Add("activitypoints.amount", new ConfigElement("activitypoints.amount", ConfigElementType.Integer, 50));
            if (System.IO.File.Exists(string_0))
            {
                smethod_0();
                foreach (ConfigElement element in dictionary_0.Values)
                {
                    if (!element.UserConfigured)
                    {
                        Output.WriteLine("Configuration value '" + element.Key.ToLower() + "' missing; using default value.", OutputLevel.Warning);
                    }
                }
            }
            else
            {
                Output.WriteLine("Configuration file is missing at " + string_0 + "; using default values.", OutputLevel.Warning);
            }
        }

        private static void smethod_0()
        {
            foreach (string str in System.IO.File.ReadAllLines(string_0, Constants.DefaultEncoding))
            {
                if (!str.StartsWith("#") && str.Contains("="))
                {
                    string[] strArray2 = str.Split(new char[] { '=' });
                    string key = strArray2[0].ToLower();
                    string str3 = string.Empty;
                    for (int i = 1; i < strArray2.Length; i++)
                    {
                        if (i > 1)
                        {
                            str3 = str3 + '=';
                        }
                        str3 = str3 + strArray2[i];
                    }
                    if (dictionary_0.ContainsKey(key))
                    {
                        dictionary_0[key].CurrentValue = str3;
                    }
                }
            }
        }

        public static string ConfigPath
        {
            get
            {
                return string_0;
            }
        }
    }
}

