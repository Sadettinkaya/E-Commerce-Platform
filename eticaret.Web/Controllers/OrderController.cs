using eticaret.Data.Abstract;
using eticaret.Data.Concrete;
using eticaret.Web.Models;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Mvc;

public class OrderController:Controller
{
    private Cart cart;
     private IOrderRepository _orderRepository;
    public OrderController(Cart cartService,IOrderRepository orderRepository)
    {
        cart=cartService;
        _orderRepository=orderRepository;

    }
    public IActionResult Checkout()
    {
        return View(new OrderModel(){Cart=cart});
    }
    [HttpPost]
    public IActionResult Checkout(OrderModel model)
    {
        if(cart.Items.Count==0)
        {
            ModelState.AddModelError("","Sepetinizde ürün yok.");
        }
        if (ModelState.IsValid)
        {
            var order=new Order
            {
                Name=model.Name,
                Email=model.Email,
                City=model.City,
                Phone=model.Phone,
                AddressLine=model.AddressLine,
                OrderDate=DateTime.Now,
                OrderItems=cart.Items.Select(i=>new eticaret.Data.Concrete. OrderItem{
                    ProductId=i.Product.Id,
                   Price=(double)i.Product.Price,
                    Quantity=i.Quantity
                }).ToList()

            };
            model.Cart=cart;
            var payment=ProcessPayment(model);

            if(payment.Status=="success")
            {
                return RedirectToPage("/Completed" ,new{OrderId=order.Id});
                
            }




            _orderRepository.SaveOrder(order);
            cart.Clear();
            return RedirectToPage("/Completed" ,new{OrderId=order.Id});
        }
        else
        {
            model.Cart=cart;
            return View(model);
        }


     
    }

    private Payment ProcessPayment(OrderModel model)
    {
    Options options = new Options();
    options.ApiKey = "sandbox-XMyLE42Jjaqfci4EJnpG3ycE3l8FN4JO";
    options.SecretKey = "sandbox-64wZjPgYgl3KwwNpmw4yaCOhxOFFDSFB";
    options.BaseUrl = "https://sandbox-api.iyzipay.com";
            
    CreatePaymentRequest request = new CreatePaymentRequest();
    request.Locale = Locale.TR.ToString();
    request.ConversationId = new Random().Next(111111111,999999999).ToString();
    request.Price = model?.Cart?.CalculateTotal().ToString();
    request.PaidPrice = model?.Cart?.CalculateTotal().ToString();
    request.Currency = Currency.TRY.ToString();
    request.Installment = 1;
    request.BasketId = "B67832";
    request.PaymentChannel = PaymentChannel.WEB.ToString();
    request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

    PaymentCard paymentCard = new PaymentCard();
    paymentCard.CardHolderName = model?.CartName;
    paymentCard.CardNumber = model?.CartNumber;
    paymentCard.ExpireMonth = model?.ExpirationMonth;
    paymentCard.ExpireYear = model?.ExpirationYear;
    paymentCard.Cvc =model?.Cvc;
    paymentCard.RegisterCard = 0;
    request.PaymentCard = paymentCard;

    Buyer buyer = new Buyer();
    buyer.Id = "BY789";
    buyer.Name = "sadettin";
    buyer.Surname = "kaya";
    buyer.GsmNumber = "+905350000000";
    buyer.Email = "email@email.com";
    buyer.IdentityNumber = "74300864791";
    buyer.LastLoginDate = "2015-10-05 12:43:35";
    buyer.RegistrationDate = "2013-04-21 15:12:09";
    buyer.RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
    buyer.Ip = "85.34.78.112";
    buyer.City = "Istanbul";
    buyer.Country = "Turkey";
    buyer.ZipCode = "34732";
    request.Buyer = buyer;

    Address shippingAddress = new Address();
    shippingAddress.ContactName = "Jane Doe";
    shippingAddress.City = "Istanbul";
    shippingAddress.Country = "Turkey";
    shippingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
    shippingAddress.ZipCode = "34742";
    request.ShippingAddress = shippingAddress;

    Address billingAddress = new Address();
    billingAddress.ContactName = "Jane Doe";
    billingAddress.City = "Istanbul";
    billingAddress.Country = "Turkey";
    billingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
    billingAddress.ZipCode = "34742";
    request.BillingAddress = billingAddress;

    List<BasketItem> basketItems = new List<BasketItem>();


    foreach(var item in model?.Cart?.Items??Enumerable.Empty<CartItem>())
    {


    BasketItem firstBasketItem = new BasketItem();
    firstBasketItem.Id = item.Product.Id.ToString();
    firstBasketItem.Name = item.Product.Name;
    firstBasketItem.Category1 = "Telefon";
    firstBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
    firstBasketItem.Price = item.Product.Price.ToString();
    basketItems.Add(firstBasketItem);

    }



 
    request.BasketItems = basketItems;

    Payment payment = Payment.Create(request, options);
    return payment;
    }
}