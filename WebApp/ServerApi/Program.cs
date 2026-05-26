using Core.Model;
using ServerApi.Interfaces;
using ServerApi.Repositories;
using ServerApi.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSingleton<IUser, UserRepositoryMongoDB>();
builder.Services.AddSingleton<IStudio, StudioRepositoryMongoDB>();
builder.Services.AddSingleton<IBooking, BookingRepositoryMongoDB>();
builder.Services.AddSingleton<IInvite, InviteRepositoryMongoDB>();
builder.Services.AddScoped<IEmailService, EmailService>();
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
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
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
