using eticaret.Data.Abstract;
using eticaret.Data.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<eticaretDbContext>(options =>{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:eticaretDbConnection"], b => b.MigrationsAssembly("eticaret.Web"));
} );

builder.Services.AddScoped<IeticaretRepository,EFeticaretRepository>();

var app = builder.Build();
app.UseStaticFiles();
app.MapDefaultControllerRoute();

app.Run();
