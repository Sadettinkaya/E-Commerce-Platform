namespace eticaret.Web.Controllers;

using eticaret.Data.Abstract;
using eticaret.Web.Models;
using Microsoft.AspNetCore.Mvc;
public  class HomeController : Controller
{
    public int pageSize=3;
    private IeticaretRepository _eticaretRepository;
    public  HomeController(IeticaretRepository eticaretRepository)
    {
        _eticaretRepository= eticaretRepository;
    }

    //localhost:5000/?page=1
    //githup
    public IActionResult Index(int page =1)
    {
        var products =_eticaretRepository
        .Products
        .Skip((page-1)*pageSize)
        .Select(p =>
         new ProductViewModel{
            Id=p.Id,
            Name=p.Name,
            Description=p.Description,
            Price=p.Price

        }).Take(pageSize);

        return View(new ProductListViewModel
        {
            Products=products,
            PageInfo=new PageInfo{
                ItemsPerPage=pageSize,
                CurrentPage=page,
                TotelItems=_eticaretRepository.Products.Count()
            }
        });
    }
   
}