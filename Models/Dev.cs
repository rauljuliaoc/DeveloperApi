using MongoDB;
using MongoDB.Bson.Serialization.Attributes;

namespace DeveloperApi.Models;

public class Dev {

    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("Name")]
    public string FullName { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string DeveloperType { get; set; }

    public int Age { get; set; }

    public decimal SalaryByHours { get; set; }

    public string email { get; set; }

    public decimal WorkedHours { get; set; }

}