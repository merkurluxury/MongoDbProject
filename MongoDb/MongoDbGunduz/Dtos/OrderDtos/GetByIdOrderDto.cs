using MongoDbGunduz.Entities;

namespace MongoDbGunduz.Dtos.OrderDtos
{
    public class GetByIdOrderDto
    {
        public string OrderId { get; set; }
        public int OrderProductStock { get; set; }
        public DateTime OrderDate { get; set; }
        public bool Status { get; set; }

        public string CustomerId { get; set; }
        public Customer Customer { get; set; }

        public string ProductId { get; set; }
        public Product Product { get; set; }
    }
}
