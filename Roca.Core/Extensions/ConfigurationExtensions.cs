using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace Roca.Core.Extensions
{
    public static class ConfigurationExtensions
    {
        public static void CheckConfig(this IConfiguration config)
        {
            if (string.IsNullOrWhiteSpace(config["Mongo:ConnectionString"]) ||
                string.IsNullOrWhiteSpace(config["Mongo:Database"]))
                throw new ConfigurationErrorsException("Mongo section inside your configuration isn't complete, there must be a \"ConnectionString\" string and a \"Database\" string");

            if (string.IsNullOrWhiteSpace(config["RocaBot:Token"]) ||
                string.IsNullOrWhiteSpace(config["RocaBot:ClientId"]) ||
                string.IsNullOrWhiteSpace(config["RocaBot:ClientSecret"]) ||
                string.IsNullOrWhiteSpace(config["RocaBot:Shards"]))
                throw new ConfigurationErrorsException("RocaBot section inside your configuration isn't complete, there must be a \"Token\" string, a \"ClientId\" string, a \"ClientSecret\" string and a \"Shards\" integer");
        }
    }
}
