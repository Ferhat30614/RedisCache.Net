using Microsoft.EntityFrameworkCore;
using RedisExampleApp.API.Models;
using RedisExampleApp.API.Repositories;
using RedisExampleApp.Cache;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("MyDatabase");//databaseme isim verdim .
   
});

builder.Services.AddScoped<IProductRepository,ProductRepository>();

builder.Services.AddSingleton<RedisService>(sp =>
{
    return new RedisService(url : builder.Configuration["CacheOptions:Url"]);
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

//Bu gptinin tavsiyesi çalışıyor ...
//var options = new DbContextOptionsBuilder<AppDbContext>()
//    .UseInMemoryDatabase("MyDatabase")
//    .Options;

//using var context = new AppDbContext(options);
//context.Database.EnsureCreated();   

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>(); // burda aslında ben AppDbContexden örnek oluşturdum 
    dbContext.Database.EnsureCreated();  //EnsureCreated metodunu burda çağırdımki seddin verileri eklensin dbye 
}



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
