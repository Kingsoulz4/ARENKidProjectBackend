using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Identity;
using ProjectBackend.Areas.Identity.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
var builder = WebApplication.CreateBuilder(args);

if (!builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<MvcModel3DDataContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("MvcModel3DDataContext") ?? throw new InvalidOperationException("Connection string 'MvcModel3DDataContext' not found.")));
}
else
{
    builder.Services.AddDbContext<MvcWordAssetsContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("MvcWordAssetsContext") ?? throw new InvalidOperationException("Connection string 'MvcWordAssetsContext' not found.")));


    builder.Services.AddDbContext<ProjectBackendIdentityDbContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("ProjectBackendIdentityDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ProjectBackendIdentityDbContextConnection' not found.")));
}

builder.Services.AddIdentity<ARENKidUser, IdentityRole>()
        .AddEntityFrameworkStores<ProjectBackendIdentityDbContext>()
        .AddDefaultTokenProviders();
// builder.Services.AddDefaultIdentity<ARENKidUser>(options => options.SignIn.RequireConfirmedAccount = true)
//         .AddEntityFrameworkStores<ProjectBackendIdentityDbContext>()
//         .AddDefaultTokenProviders();
// builder.Services.AddDefaultIdentity<ARENKidUser>(
//     options => options.SignIn.RequireConfirmedEmail = false
//     )
//         .AddEntityFrameworkStores<ProjectBackendIdentityDbContext>()
//         .AddDefaultTokenProviders();

//Config IdentityOptions
builder.Services.Configure<IdentityOptions>(options =>
{
    //Config password
    options.Password.RequireDigit = false; // Không bắt phải có số
    options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
    options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
    options.Password.RequireUppercase = false; // Không bắt buộc chữ in
    options.Password.RequiredLength = 4;

    //Config Lockout - user
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;

    //Config user
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

    //Config login method
    // options.SignIn.RequireConfirmedEmail = true;
    // options.SignIn.RequireConfirmedPhoneNumber = true;
});



// builder.Services
//     .AddAuthentication(options =>
//     {
        
//         options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//         options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//         options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//     })
//     .AddJwtBearer(options =>
//     {
//         options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
//         {
//             ValidateIssuer = true,
//             ValidateLifetime = true,
//             ValidateIssuerSigningKey = true,
//             ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
//             ValidAudience = builder.Configuration["JWT:ValidAudience"],
//             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]!))
//         };
//     });

builder.Services
    .AddAuthentication()
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
            ValidAudience = builder.Configuration["JWT:ValidAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]!))
        };
    });

builder.Services.AddMvc();
builder.Services.AddControllers();
builder.Services.AddRazorPages();

builder.Services.ConfigureApplicationCookie(options =>
        {
            options.AccessDeniedPath = "/AccessDenied";
            options.Cookie.Name = "MyApplication_Auth";
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromDays(7);
            options.LoginPath = "/Login";
            options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            options.SlidingExpiration = true;
        });

builder.Services.AddTransient<IEmailSender, MailSenderServices>();


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
    RequestPath = "/DataBuild",
    // OnPrepareResponse = context =>
    // {
    //     context.Context.Response.Headers["Cache-Control"] = "public,max-age=2592000";
    //     context.Context.Response.Headers["Expires"] = DateTime.UtcNow.AddMonths(1).ToString("R");
    // },
    
});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine("D:/UET/FINAL THESIS/src/BEProject/ARENKidProjectBackend", "Temp")),
    RequestPath = "/Temp"
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"


    );

// app.MapControllerRoute(
//     name: "Identity",
//     pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


app.MapRazorPages();

app.Run();
