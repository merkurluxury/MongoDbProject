using AutoMapper;
using MongoDB.Driver;
using MongoDbGunduz.Dtos.ProductDtos;
using MongoDbGunduz.Entities;
using MongoDbGunduz.Settings;

namespace MongoDbGunduz.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public ProductService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _productCollection = database.GetCollection<Product>(_databaseSettings.ProductCollectionName);
            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }
        public async Task CreateProductAsync(CreateProductDto productDto)
        {
            var value = _mapper.Map<Product>(productDto);
            await _productCollection.InsertOneAsync(value);
        }
        public async Task DeleteProductAsync(string id)
        {
            await _productCollection.DeleteOneAsync(x => x.ProductId == id);
        }
        public async Task<List<ResultProductDto>> GetAllProductAsync()
        {
            var values = await _productCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultProductDto>>(values);
        }
        public async Task<GetByIdProductDto> GetByIdProductAsync(string id)
        {
            var value = await _productCollection.Find<Product>(x => x.ProductId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdProductDto>(value);
        }

        public async Task<List<ResultProductWithCategoryDto>> GetProductWithCategoryAsync()
        {
            var products = await _productCollection.Find(x => true).ToListAsync();

            foreach (var product in products)
            {
                // Kategoriyi bulmak için FirstOrDefaultAsync kullanın
                var category = await _categoryCollection.Find(x => x.CategoryId == product.CategoryId).FirstOrDefaultAsync();
                product.Category = category; // Kategori bulunamadığında null olabilir
            }

            // Mapping işlemini gerçekleştirin
            return _mapper.Map<List<ResultProductWithCategoryDto>>(products);
        }


        public async Task UpdateProductAsync(UpdateProductDto productDto)
        {
            var values = _mapper.Map<Product>(productDto);
            await _productCollection.FindOneAndReplaceAsync(x => x.ProductId == productDto.ProductId, values);
        }
    }
}
