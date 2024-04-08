using Microsoft.EntityFrameworkCore;
using Sicoob.API.Authh.Business;
using Sicoob.API.Authh.Data;
using Sicoob.API.Authh.Helpers;
using Sicoob.API.Authh.Repository;
using Sicoob.API.Authh.Repository.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<LoginBusiness>();
builder.Services.AddScoped<ISenhaHash, senhaHash>();
builder.Services.AddScoped<ICadastroRepository, CadastroRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => options.AddPolicy("FrontEnd", policy =>
{
    policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("FrontEnd");

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
