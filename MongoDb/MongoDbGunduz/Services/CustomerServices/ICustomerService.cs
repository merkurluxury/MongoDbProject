using MongoDbGunduz.Dtos.CustomerDtos;

namespace MongoDbGunduz.Services.CustomerServices
{
    public interface ICustomerService
    {
        Task<List<ResultCustomerDto>> GetAllCustomerAsync();
        Task CreateCustomerAsync(CreateCustomerDto createUpdateDto);
        Task UpdateCustomerAsync(UpdateCustomerDto updateUpdateDto);
        Task DeleteCustomerAsync(string id);
        Task<GetByIdCustomerDto> GetByIdCustomerAsync(string id);
    }
}
