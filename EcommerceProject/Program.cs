using EcommerceProject.Data;
using EcommerceProject.Data.Cart;
using EcommerceProject.Data.Services;
using EcommerceProject.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


//DbContext Configuration
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConn")));
//Services Configuration
builder.Services.AddScoped<IActorsService,ActorsService>();
builder.Services.AddScoped<IProducerServices,ProducersService>();
builder.Services.AddScoped<ICinemasService,CinemasService>();
builder.Services.AddScoped<IMoviesService,MoviesService>();
builder.Services.AddScoped<IOrdersService,OrdersService>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(sc => ShoppingCart.GetShoppingCart(sc));

//Authentication and Authorization
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddMemoryCache();


builder.Services.AddSession();
builder.Services.AddAuthentication(options => options.DefaultScheme=CookieAuthenticationDefaults.AuthenticationScheme);


// Add services to the container.
//builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddControllersWithViews();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        AppDbInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
    }

    AppDbInitializer.SeedUsersAndRolesAsync(app).Wait();

}



//AppDbInitializer.Seed(app);
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


