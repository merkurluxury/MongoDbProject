using AutoMapper;
using MongoDB.Driver;
using MongoDbGunduz.Dtos.CustomerDtos;
using MongoDbGunduz.Dtos.CustomerDtos;
using MongoDbGunduz.Entities;
using MongoDbGunduz.Services.CustomerServices;
using MongoDbGunduz.Settings;

namespace MongoDbEgitim.Services.CustomerServices
{
    public class CustomerService : ICustomerService
    {
        private readonly IMongoCollection<Customer> _customerCollection;
        private readonly IMapper _mapper;

        public CustomerService(IMapper mapper , IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _mapper = mapper;
            _customerCollection = database.GetCollection<Customer>(_databaseSettings.CustomerCollectionName);
        }

        

        public async Task CreateCustomerAsync(CreateCustomerDto createUpdateDto)
        {
            var value = _mapper.Map<Customer>(createUpdateDto);
            await _customerCollection.InsertOneAsync(value);
        }

        public async Task DeleteCustomerAsync(string id)
        {
            await _customerCollection.DeleteOneAsync(x => x.CustomerId == id);
        }

        public async Task<List<ResultCustomerDto>> GetAllCustomerAsync()
        {
            var values = await _customerCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultCustomerDto>>(values);
        }

        public async Task<GetByIdCustomerDto> GetByIdCustomerAsync(string id)
        {
            var value = await _customerCollection.Find<Customer>(x => x.CustomerId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdCustomerDto>(value);
        }

        public async Task UpdateCustomerAsync(UpdateCustomerDto updateCustomerDto)
        {
            var value = _mapper.Map<Customer>(updateCustomerDto);
            await _customerCollection.FindOneAndReplaceAsync(x => x.CustomerId == updateCustomerDto.CustomerId, value);
        }
    }
}
