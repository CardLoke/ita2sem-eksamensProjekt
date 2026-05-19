using ServerApi.Repositories;
using ServerApi.Interfaces;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSingleton<IUser, UserRepositoryMongoDB>();
builder.Services.AddSingleton<IStudio, StudioRepositoryMongoDB>();
builder.Services.AddSingleton<IBooking, BookingRepositoryMongoDB>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("policy",
                      policy =>
                      {
                          policy.AllowAnyOrigin();
                          policy.AllowAnyMethod();
                          policy.AllowAnyHeader();
                      });
});


builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseRouting();
app.UseCors("policy");

app.UseRouting();

app.UseCors("policy");

app.UseHttpsRedirection();

app.UseAuthorization();



app.MapControllers();

app.Run();
