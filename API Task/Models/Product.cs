using Swashbuckle.AspNetCore.Annotations;

namespace API_Task.Models
{
    public class Product
    {
        [SwaggerSchema(ReadOnly = true)]
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }

        public int Quantity { get; set; }
    }
}
