using Microsoft.EntityFrameworkCore;

namespace eticaret.Web.Controllers;

using AutoMapper;
using eticaret.Data.Abstract;
using eticaret.Web.Models;
using Microsoft.AspNetCore.Mvc;
public  class HomeController : Controller
{
    public int pageSize=3;
    private readonly IeticaretRepository _eticaretRepository;

    private readonly IMapper _mapper;
    public  HomeController(IeticaretRepository eticaretRepository ,IMapper mapper)
    {
        _eticaretRepository= eticaretRepository;
        _mapper=mapper;
    }

    //localhost:5000/?page=1
    
    public IActionResult Index(string category , int page =1)
    {  

        return View(new ProductListViewModel
        {
            Products=_eticaretRepository.GetProductsByCategory(category,page,pageSize) .Select(product =>_mapper.Map<ProductViewModel>(product)),
            PageInfo=new PageInfo{
                ItemsPerPage=pageSize,
                CurrentPage=page,
                TotelItems=_eticaretRepository.GetProductCount(category)
            }
        });
    }
   
}