using System.Collections.Generic;
using dokuku.mongoconfiguration;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using ecommerce.merk.reporting.models;
using ecommerce.merk.parameters;

namespace ecommerce.merk.reporting
{
    public class MerkReporting
    {
        private MongoConfig mongo;
        private const string _collectionName = "BinaCitraBrand";
        private const int _defaultLimit = 10;

        public MerkReporting(MongoConfig mongo)
        {
            this.mongo = mongo;
        }

        public Merk Get(string code, string ownerId)
        {
            IMongoQuery qry = Query.And(Query.EQ("_id.Code", code), Query.EQ("_id.OwnerId", ownerId));
            return Collection.FindOne(qry);
        }

        public List<Merk> GetAll(QueryParam param)
        {
            //IMongoQuery qry = Query.EQ("_id.OwnerId", param.OwnerId);
            //return Collection.Find(qry).OrderBy(x => x.Id.Code).Skip(param.Offset * _defaultLimit).Take(_defaultLimit).ToList();
            return null;
        }

        private MongoCollection<Merk> Collection
        {
            get { return mongo.Database.GetCollection<Merk>(_collectionName); }
        }
    }
}
