
using eticaret.Data.Abstract;

namespace eticaret.Data.Concrete;

public class EFeticaretRepository:IeticaretRepository
{
    private eticaretDbContext _context;

    public EFeticaretRepository(eticaretDbContext context)
    {
        _context=context;
    }
    public IQueryable<Product>Products =>_context.Products;
    public IQueryable<Category>Categories =>_context.Categories;
    public void CreateProduct(Product entity)
    {
        throw new NotImplementedException();
    }

    
    
}