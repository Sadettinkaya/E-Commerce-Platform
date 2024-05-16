using eticaret.Data.Abstract;
using eticaret.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace eticaret.Web.Components;

public class CategoriesListViewComponent:ViewComponent
{
    private readonly IeticaretRepository _eticaretRepository;
    public CategoriesListViewComponent(IeticaretRepository eticaretRepository)
    {
        _eticaretRepository=eticaretRepository;
    }
    public IViewComponentResult Invoke()
    {
        return View(_eticaretRepository
        .Categories.Select(c=> new CategoryViewModel{
            Id=c.Id,
            Name=c.Name,
            Url=c.Url
        }).ToList());
        
   
    }
}