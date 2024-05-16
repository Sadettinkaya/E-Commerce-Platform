using eticaret.Data.Concrete;
namespace eticaret.Data.Abstract;

public interface IOrderRepository
{
    IQueryable<Order>Orders {get;}
    void SaveOrder(Order order);
}