using Company.Delivery.Api;
using Company.Delivery.Api.AppStart;
using Company.Delivery.Database;
using Company.Delivery.Domain;
using Company.Delivery.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:5276",
                                              "http://www.contoso.com");
                      });
});

builder.Services.AddDbContext<DeliveryDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnetion"));
});

builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddScoped<IWaybillService, WaybillService>();

builder.Services.AddDeliveryControllers();
builder.Services.AddDeliveryApi();

var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);

app.UseDeliveryApi();
app.MapControllers();

app.Run();
