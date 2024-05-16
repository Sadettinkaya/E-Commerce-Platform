using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace eticaret.Web.Models;


public class OrderModel
{
    public int Id{get;set;}
    public DateTime OrderDate{get;set;}
    public string Name{get;set;} =string.Empty;
    public string City{get;set;} =string.Empty;
    public string Phone{get;set;} =string.Empty;

    [EmailAddress]
    public string Email{get;set;} =string.Empty;
    public string AddressLine{get;set;} =string.Empty;

    [BindNever]
    public Cart? Cart { get; set; }=null!;

    public string? CartName { get; set; }
    public string? CartNumber { get; set; }
    public string? ExpirationMonth { get; set; }
    public string? ExpirationYear { get; set; }
    public string? Cvc { get; set; }

}