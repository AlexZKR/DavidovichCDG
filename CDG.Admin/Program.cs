using CDG.Admin;
using CDG.Admin.Configuration;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
SD.MainAPIBase = builder.Configuration["ServiceUrls:MainAPI"];

builder.Services.AddServices();

var app = builder.Build();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(@"E:\BSUIR\Diploma\Zakrevsky Diploma\CDG.Web\wwwroot","img","books")),
    RequestPath = "/Books"
});
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(@"E:\BSUIR\Diploma\Zakrevsky Diploma\CDG.Admin\wwwroot","img")),
    RequestPath = "/img"
});

app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Orders}/{action=Index}/{id?}");
app.MapControllers();

app.Run();
