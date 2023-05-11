using ObjectResponse_POC.Filter;
using ObjectResponse_POC.Model;
using ObjectResponse_POC.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Add the middleware with options to validate the object
builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<ValueObjectTFilter>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//dependence injection
builder.Services.AddScoped<HomeService>();
builder.Services.AddScoped<Home>();

var app = builder.Build();

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
