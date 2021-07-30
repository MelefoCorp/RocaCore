using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Roca.Core
{
    public static partial class Mongo
    {
        private static MongoClient? _client = null;
        private static IMongoDatabase? _database = null;

        public static void Initialize(IConfiguration configuration)
        {
            _client = new(configuration["Mongo:ConnectionString"]);
            _database = _client.GetDatabase(configuration["Mongo:Database"]);

            InitiliazeUsers();
            InitiliazeGuilds();
            InitiliazeMembers();
        }
    }
}
