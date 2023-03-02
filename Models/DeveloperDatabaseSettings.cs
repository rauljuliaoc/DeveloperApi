namespace DeveloperApi.Models;

public class DeveloperDatabaseSettings {

    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string DevsCollectionName { get; set; } = null!;

}