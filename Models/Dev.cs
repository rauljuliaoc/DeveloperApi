using MongoDB;
using MongoDB.Bson.Serialization.Attributes;

namespace DeveloperApi.Models;

[BsonIgnoreExtraElements]
public class Dev {

    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("FullName")]
    public string FullName { get; set; } = null!;

    [BsonElement("FirstName")]
    public string FirstName { get; set; } = null!;

    [BsonElement("LastName")]
    public string LastName { get; set; } = null!;

    [BsonElement("DeveloperType")]
    public string DeveloperType { get; set; } = null!;

    [BsonElement("Age")]
    public int Age { get; set; }

    [BsonElement("SalaryByHours")]
    public decimal SalaryByHours { get; set; }

    [BsonElement("email")]
    public string email { get; set; } = null!;

    [BsonElement("WorkedHours")]
    public decimal WorkedHours { get; set; }

}