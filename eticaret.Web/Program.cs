using eticaret.Data.Abstract;
using eticaret.Data.Concrete;
using eticaret.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);

builder.Services.AddDbContext<eticaretDbContext>(options =>{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:eticaretDbConnection"], b => b.MigrationsAssembly("eticaret.Web"));
} );

builder.Services.AddScoped<IeticaretRepository,EFeticaretRepository>();
builder.Services.AddScoped<IOrderRepository,EfOrderRepository>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
builder.Services.AddScoped<Cart>(sc=>SessionCart.GetCart(sc));


var app = builder.Build();
app.UseStaticFiles();
app.UseSession();

app.MapControllerRoute("products_in_category",
"products/{category?}",new{controller="Home",action="Index"});

app.MapControllerRoute("product_details","{name}",new{controller="Home",action="Details"});



app.MapDefaultControllerRoute();
app.MapRazorPages();

app.Run();
