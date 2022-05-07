using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.AutoMapperProfile;
using WebApi.Data;
using WebApi.Repository;

var builder = WebApplication.CreateBuilder(args);

#region DB
builder.Services.AddDbContext<WebApiContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
#endregion

#region Services
builder.Services.AddScoped<IFinanceRepository, FinanceRepository>();
#endregion

// Auto Mapper Configurations
var mapperConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddCors(options =>
{
 options.AddPolicy("CorsPolicy",
                builder => builder
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .SetIsOriginAllowed((_) => true)
                    .AllowAnyHeader());
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
