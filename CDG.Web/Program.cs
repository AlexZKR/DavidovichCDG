using CDG.DAL;
using CDG.DAL.Data;
using CDG.Web.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DataConnection") ?? throw new InvalidOperationException("Connection string 'DataConnection' not found.");

builder.Services.AddDbContext<Data>(options => options.UseSqlite(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<Data>();


// if (!builder.Environment.IsDevelopment())
// {
//     builder.Configuration["UseSqlite"] = "false";
// }

DbConfiguration.ConfigureServices(builder.Configuration, builder.Services);

//configure db provider

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().
AddEntityFrameworkStores<AppIdentityDbContext>()
.AddDefaultTokenProviders();

//configuring services

builder.Services.AddBLLServices(builder.Configuration);
builder.Services.AddWebServices(builder.Configuration);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Identity/Account/Login";
                options.LogoutPath = "/Identity/Account/Logout";
            });

var app = builder.Build();

app.Logger.LogInformation("App created...");

app.Logger.LogInformation("Seeding Database...");

//DB seeding
using (var scope = app.Services.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;
    try
    {
        var context = scopedProvider.GetRequiredService<AppDbContext>();
        await DbSeeder.SeedDataAsync(context, app.Logger);

        var idContext = scopedProvider.GetRequiredService<AppIdentityDbContext>();
        // await DbSeeder.SeedIdDataAsync(idContext, app.Logger);
    }
    catch (System.Exception)
    {
        throw;
    }
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();