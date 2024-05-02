using LapShop.Ef.Identity;
using LapShop.EF.BL.Implementations;
using LapShop.EF.BL.Interfaces;
using LapShop.EF.DBContext;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

#region ConnectToSQLDataBase
var CS = builder.Configuration.GetConnectionString("CS")
                                ?? throw new InvalidOperationException("No connection string was found");

builder.Services.AddDbContext<LapShopContext>(options =>
    options.UseSqlServer(CS));
#endregion

builder.Services.AddControllersWithViews();

#region Dependency Injections
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IItemTypesService, ItemTypesService>();
builder.Services.AddScoped<IOsService, OsService>();
builder.Services.AddScoped<IItemImageService, ItemImageServic>();
builder.Services.AddScoped<ISalesInvoiceItemsService, SalesInvoiceItemsService>();
builder.Services.AddScoped<ISalesInvoiceService, SalesInvoiceService>();
builder.Services.AddScoped<ISliderService, SliderService>();
#endregion

#region IdentityOptions
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<LapShopContext>();
#endregion

#region Sesstion and cookies
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/User/AccessDenied";
    options.Cookie.Name = "Cookie";
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(7);
    options.LoginPath = "/User/Login";
    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
    options.SlidingExpiration = true;
});
#endregion

#region Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Lap Shop Api",
        Description = "api to access items and items with related categories",
        //TermsOfService = new Uri("https://itlegend.net/"),
        Contact = new OpenApiContact
        {
            Email = "mostafasayedlamlom@gmail.com",
            Name = "Mostafa Lamlom",
            //Url = new Uri("https://itlegend.net/")
        },
        //License = new OpenApiLicense
        //{
        //    Name = "It Legend Licence",
        //    Url = new Uri("https://itlegend.net/")
        //}

    });

    var xmlComments = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var fullPath = Path.Combine(AppContext.BaseDirectory, xmlComments);
    options.IncludeXmlComments(fullPath);
});
#endregion

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();




app.UseEndpoints(endpoints =>
{

    endpoints.MapControllerRoute(
       name: "admin",
       pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");


});


#region Swagger UI
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});
#endregion


app.Run();

