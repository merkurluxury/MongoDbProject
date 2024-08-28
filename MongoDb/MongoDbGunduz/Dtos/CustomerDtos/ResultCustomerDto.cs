using MongoDbGunduz.Entities;

namespace MongoDbGunduz.Dtos.CustomerDtos
{
    public class ResultCustomerDto
    {
        public string CustomerId { get; set; }
        public string CustomerFullName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhone { get; set; }
        public List<Order> Orders { get; set; }
    }
}
