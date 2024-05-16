using eticaret.Data.Abstract;


namespace eticaret.Data.Concrete;
public class EfOrderRepository : IOrderRepository
{


    private eticaretDbContext _context;

    public EfOrderRepository(eticaretDbContext context)
    {
        _context=context;
    }

    public IQueryable<Order> Orders => _context.Orders;

    public void SaveOrder(Order order)
    {
       _context.Orders.Add(order);
       _context.SaveChanges();
    }
}