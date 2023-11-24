using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Products.Shared
{
    public abstract class AggregateRoot
    {
        //id mongo domyślnie mapowane nie ruszać
        [BsonIgnoreIfDefault]
        public ObjectId Id { get; set; }
        public int Version { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public int SchemaVersion { get; set; }
        private bool _versionIncremented;

        protected void IncrementVersion()
        {
            if (_versionIncremented)
            {
                return;
            }

            Version++;
            _versionIncremented = true;
        }

        protected void SetCreationDate() => CreationDate = DateTime.UtcNow;
        protected void SetModyficationDate() => ModificationDate = DateTime.UtcNow;
    }
}
