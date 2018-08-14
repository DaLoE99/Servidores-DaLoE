using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoomBang_RetroServer.Configuration
{
    class ConfigurationManager
    {
        static Dictionary<string, Configuration> Configurations;
        private static void Initialize()
        {
            Configurations = new Directory<string, Configuration>();
            Configurations.Add("", new Configuration(ConfigurationType.Number, 0));
        }
        private static void SetConfigValues()
        {
            string[] Lines = File.ReadAllLines(ConfigurationPath, Constants.Encoding);
            foreach(string Line in Lines)
            {
                try
                {
                    if(Line.StartsWith("#") || !Line.Contains("="))
                    {
                        continue;
                    }
                    string[] SplittedLine = Line.Split('=');
                    string Key = SplittedLine[0];
                    string Value = string.Empty;
                }
                catch { }
            }
        }
    }
}
