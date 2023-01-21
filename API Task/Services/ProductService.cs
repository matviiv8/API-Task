using API_Task.Models;

namespace API_Task.Services
{
    public class ProductService : IProductService
    {
        private readonly ApiDbContext _context;

        public ProductService(ApiDbContext context)
        {
            _context = context;
        }

        public void Delete(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        public List<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        public Product GetById(int id)
        {
            return _context.Products.Find(id);
        }

        public Product Copy(Product oldProduct)
        {
            return new Product
            {
                Id = oldProduct.Id,
                Name = oldProduct.Name,
                Price = oldProduct.Price,
                Quantity = oldProduct.Quantity,
                Category = oldProduct.Category,
            };
        }

        public void Update(Product oldProduct, Product newProduct)
        {
            oldProduct.Name = newProduct.Name;
            oldProduct.Price = newProduct.Price;
            oldProduct.Quantity = newProduct.Quantity;
            oldProduct.Category = newProduct.Category;

            _context.Products.Update(oldProduct);
            _context.SaveChanges();
        }

        public void Add(Product product)
        {
            _context.Add(product);
            _context.SaveChanges();
        }

        public void UpdateQuantity(int quantity, Product product)
        {
            product.Quantity = quantity;
            _context.Update(product);
            _context.SaveChanges();
        }
    }
}
