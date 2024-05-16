namespace eticaret.Data.Concrete;

public class Order
{
    public int Id{get;set;}
    public DateTime OrderDate{get;set;}
    public string Name{get;set;} =string.Empty;
    public string City{get;set;} =string.Empty;
    public string Phone{get;set;} =string.Empty;
    public string Email{get;set;} =string.Empty;
    public string AddressLine{get;set;} =string.Empty;

    public List<OrderItem> OrderItems {get;set;}=new();
}

public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; }=null!;

    public int ProductId { get; set; }
    public Product Product { get; set; }=null!;

    public double Price { get; set; }
    public int Quantity { get; set; }


}