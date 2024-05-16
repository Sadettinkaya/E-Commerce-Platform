using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eticaret.Data.Abstract;
using eticaret.Web.Models;
using eticaret.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;


namespace eticaret.Web.Pages
{

    public class CartModel : PageModel
    {
        private IeticaretRepository _repository;

        public CartModel(IeticaretRepository repository,Cart cartService)
        {
           _repository=repository;
           Cart=cartService;
        }

        public Cart? Cart { get; set; }
        public void OnGet()
        {
            
        }

        public IActionResult OnPost ( int Id)
        {
            var product=_repository.Products.FirstOrDefault(i=>i.Id==Id);

            if(product !=null)
            {
                
                Cart?.AddItem(product,1);
              
            }
            return RedirectToPage("/cart");
        } 

        public IActionResult OnPostRemove(int Id)
        {   
           
            Cart?.RemoveItem(Cart.Items.First(p=>p.Product.Id==Id).Product);
            return RedirectToPage("/Cart");
        }

    }
}