using APIQueroOficina.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace APIQueroOficina.Services
{
    public class WorkShopService
    {
        private readonly IMongoCollection<WorkShop> _workShopCollection;
        public WorkShopService(
            IOptions<WorkShopDatabaseSettings> workShopDatabaseSettings)
        {
            var mongoClient = new MongoClient(workShopDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase((workShopDatabaseSettings.Value.DatabaseName));

            _workShopCollection = mongoDatabase.GetCollection<WorkShop>(
                workShopDatabaseSettings.Value.WorkShopCollectionsName);
        }

        public async Task<List<WorkShop>> GetAsync() => // buscar todos os itens
            await _workShopCollection.Find(_ => true).ToListAsync();
        public async Task<WorkShop?> GetAsync(string id) =>
            await _workShopCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        public async Task CreateAsync(WorkShop newWorkShop) =>
            await _workShopCollection.InsertOneAsync(newWorkShop);
        public async Task UpdateAsync(string id, WorkShop updatedWorkShop) =>
            await _workShopCollection.ReplaceOneAsync(x => x.Id == id, updatedWorkShop);
        public async Task RemoveAsync(string id) =>
            await _workShopCollection.DeleteOneAsync(x => x.Id == id);
    }

    }


