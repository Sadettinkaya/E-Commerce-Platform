using AutoMapper;
using eticaret.Data.Concrete;

namespace eticaret.Web.Models;


public class MapperProfile:Profile
{
   public MapperProfile()
   {
     CreateMap<Product,ProductViewModel>();
   }
}