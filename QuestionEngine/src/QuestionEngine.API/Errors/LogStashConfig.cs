using System.Configuration;

namespace Keywords.API.Errors
{
    public class LogStashConfig : ConfigurationSection
    {
        private static readonly LogStashConfig _configuration = ConfigurationManager.GetSection("LogStashConfig") as LogStashConfig;

        public static LogStashConfig GetConfig()
        {
            return _configuration;
        }

        [ConfigurationProperty("ipaddress", DefaultValue = "127.0.0.1", IsRequired = false)]
        public string IPAdress
        {
            get
            {
                return (string)this["ipaddress"];
            }
            set { this["ipaddress"] = value; }
        }

        [ConfigurationProperty("port", DefaultValue = 112, IsRequired = false)]
        public int Port
        {
            get
            {
                return (int)this["port"];
            }
            set { this["port"] = value; }
        }

        [ConfigurationProperty("enabled", DefaultValue = false, IsRequired = false)]
        public bool Enabled
        {
            get
            {
                return (bool)this["enabled"];
            }
            set { this["enabled"] = value; }
        }
    }
}