using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Driver;

namespace Roca.Core
{
    public static partial class Mongo
    {
        private static MongoClient? _client;
        private static IMongoDatabase? _database;

        public static void Initialize(IConfiguration configuration)
        {
            ConventionRegistry.Register("IgnoreExtraElements", new ConventionPack
            {
                new IgnoreExtraElementsConvention(true)
            }, _ => true);
            ConventionRegistry.Register("DictionaryRepresentation", new ConventionPack
            {
                new DictionaryRepresentationConvention(DictionaryRepresentation.ArrayOfDocuments)
            }, _ => true);

            _client = new(configuration["Mongo:ConnectionString"]);
            _database = _client.GetDatabase(configuration["Mongo:Database"]);

            InitializeLocalizers();
            InitializeUsers();
            InitializeGuilds();
            InitializeMembers();
        }
    }

    internal class DictionaryRepresentationConvention : ConventionBase, IMemberMapConvention
    {
        private readonly DictionaryRepresentation _representation;

        public DictionaryRepresentationConvention(DictionaryRepresentation representation)
            => _representation = representation;

        public void Apply(BsonMemberMap map)
            => map.SetSerializer(ConfigureSerializer(map.GetSerializer()));

        private IBsonSerializer ConfigureSerializer(IBsonSerializer serializer)
        {
            if (serializer is IDictionaryRepresentationConfigurable dictionary)
                serializer = dictionary.WithDictionaryRepresentation(_representation);

            return serializer is not IChildSerializerConfigurable child
                ? serializer
                : child.WithChildSerializer(ConfigureSerializer(child.ChildSerializer));
        }
    }
}