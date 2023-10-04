using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
var builder = WebApplication.CreateBuilder(args);

if(!builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<MvcModel3DDataContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("MvcModel3DDataContext") ?? throw new InvalidOperationException("Connection string 'MvcModel3DDataContext' not found.")));
}
else
{
    builder.Services.AddDbContext<MvcWordAssetsContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("MvcWordAssetsContext") ?? throw new InvalidOperationException("Connection string 'MvcWordAssetsContext' not found.")));
}

ConfigurationManager.Initialize(builder.Configuration);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

Console.WriteLine("Environment  is development" + app.Environment.IsDevelopment());

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    
}


app.UseHttpsRedirection();
app.UseStaticFiles();

Console.WriteLine($"Unity data build absolute path: {ConfigurationManager.Instance!.GetUnityDataBuildAbsolutePath()}");
Console.WriteLine($"Unity data build absolute path: {ConfigurationManager.Instance!.GetUnityDataBuildRelativePath()}");



app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        "D:/UET/FINAL THESIS/src/UnityProject/AssetBundleBuilder/Assets/MainContainer/Resources/DataBuild"),
    RequestPath = "/DataBuild"
});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine("D:/UET/FINAL THESIS/src/BEProject/ARENKidProjectBackend", "Temp")),
    RequestPath = "/Temp"
});

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
