using eticaret.Data.Concrete;

namespace eticaret.Data.Abstract;

public interface IeticaretRepository
{
    IQueryable<Product>Products{get;}
    IQueryable<Category>Categories{get;}
    void CreateProduct(Product entity);
    int GetProductCount(string category);
    IEnumerable<Product>GetProductsByCategory(string category,int page,int pageSize);

}