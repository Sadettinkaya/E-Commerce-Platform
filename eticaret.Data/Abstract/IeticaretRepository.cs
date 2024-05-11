using eticaret.Data.Concrete;

namespace eticaret.Data.Abstract;

public interface IeticaretRepository
{
    IQueryable<Product>Products{get;}
    IQueryable<Category>Categories{get;}
    void CreateProduct(Product entity);
}