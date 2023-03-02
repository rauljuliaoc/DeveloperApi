using DeveloperApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DeveloperApi.Services;

public class DeveloperService 
{
    private readonly IMongoCollection<Dev> _devCollection;

    public DeveloperService (IOptions<DeveloperDatabaseSettings> developerDatabaseSetting){
        //Initiate the service with the client values

        var mongoClient = new MongoClient(developerDatabaseSetting.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(developerDatabaseSetting.Value.DatabaseName);

        _devCollection = mongoDatabase.GetCollection<Dev>(developerDatabaseSetting.Value.DevsCollectionName);
    }

    public async Task<List<Dev>> GetDevsAsync() =>
        await _devCollection.Find(_=> true).ToListAsync();

    public async Task<Dev?> GetDevAsync(string id) =>
        await _devCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
    
    public async Task CreateAsync(Dev newDev) =>
        await _devCollection.InsertOneAsync(newDev);

    public async Task UpdateOneAsync(string id, Dev updateDev) =>
        await _devCollection.ReplaceOneAsync(x => x.Id == id, updateDev);

    public async Task RemoveOneAsync( string id) => 
        await _devCollection.DeleteOneAsync(x => x.Id == id);

}