using API_Task.Models;

namespace API_Task.Services
{
    public interface IProductService
    {
        List<Product> GetAll();

        Product GetById(int id);

        void Delete(Product product);

        Product Copy(Product oldProduct);

        void Update(Product oldProduct, Product newProduct);

        void Add(Product product);

        void UpdateQuantity(int quantity, Product product);
    }
}
