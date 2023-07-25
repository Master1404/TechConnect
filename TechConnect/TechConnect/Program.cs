using System.Configuration;
using Amazon.Extensions.NETCore.Setup;
using Amazon.S3;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TechConnect.Core;
using TechConnect.DAL.Sql;
using TechConnect.Models;
using TechConnect.Models.SpecialEquipment;

var builder = WebApplication.CreateBuilder(args);


Microsoft.Extensions.Configuration.ConfigurationManager configuration = builder.Configuration;

builder.Services.AddDbContext<TechConnectContext>(options =>
    options
        .UseSqlServer(configuration.GetConnectionString("ConnStr")));
// Add services to the container.
builder.Services.AddScoped<IRepository<User>, UserSqlRepository>();
builder.Services.AddScoped<IService<User, int>, UserService>();


//builder.Services.AddAWSService<IAmazonS3>();

builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions("AWS"));
builder.Services.AddAWSService<IAmazonS3>(new AWSOptions
{
    Region = Amazon.RegionEndpoint.EUWest3,
    DefaultClientConfig =
    {
        ServiceURL = "https://eu-west-3.amazonaws.com"
    }
});



builder.Services.AddScoped<IRepository<SpecialVehicleModel>, SpecialVehicleSqlRepository>();
builder.Services.AddScoped<IService<SpecialVehicleModel, int>, AdsService>();

builder.Services.AddControllersWithViews();
                    

var config = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<CreateUserViewModel, User>();
    cfg.CreateMap<SpecialVehicleModel, SpecialVehicleViewModel>();
    cfg.CreateMap<SpecialVehicleViewModel, SpecialVehicleModel>();

});
IMapper mapper = config.CreateMapper();

builder.Services.AddSingleton(mapper);




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

 app.Run();
