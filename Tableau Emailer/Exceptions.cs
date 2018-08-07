using System;

namespace Behold_Emailer
{
    public class ConfigurationException : Exception
    {
        public ConfigurationException(string message) : base(message)
        {
        }

        public ConfigurationException()
        {
        }
    }
}