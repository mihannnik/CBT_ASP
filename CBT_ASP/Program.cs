using CBT.Application;
using CBT.Domain.Interfaces;
using CBT.Domain.Options;
using CBT.Infrastructure;
using CBT.Web.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<JWTOptions>(builder.Configuration.GetSection(JWTOptions.SectionName));
builder.Services.Configure<SQLiteOptions>(builder.Configuration.GetSection(SQLiteOptions.SectionName));

builder.Services.AddAuthentication(builder.Configuration.GetSection(JWTOptions.SectionName).Get<JWTOptions>());
builder.Services.AddAplicationServices();
builder.Services.AddInfrastructureServices();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
