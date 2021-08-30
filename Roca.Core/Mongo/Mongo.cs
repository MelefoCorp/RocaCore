using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Roca.Core
{
    public static partial class Mongo
    {
        private static MongoClient? _client;
        private static IMongoDatabase? _database;

        public static void Initialize(IConfiguration configuration)
        {
            _client = new(configuration["Mongo:ConnectionString"]);
            _database = _client.GetDatabase(configuration["Mongo:Database"]);

            InitializeLocalizers();
            InitializeUsers();
            InitializeGuilds();
            InitializeMembers();
        }
    }
}
