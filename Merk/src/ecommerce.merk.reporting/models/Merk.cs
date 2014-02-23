using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace ecommerce.merk.reporting.models
{
    [BsonIgnoreExtraElements]
    public class Merk
    {
        public BrandId Id { get; set; }
        public string Name { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class BrandId
    {
        public string OwnerId { get; set; }
        public string Code { get; set; }
    }
}
