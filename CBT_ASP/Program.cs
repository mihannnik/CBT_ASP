using CBT.Infrastructure;
using CBT.Infrastructure.Common.Options;
using CBT.Infrastructure.Database;
using CBT.Web.Configuration;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<JWTOptions>(builder.Configuration.GetRequiredSection(JWTOptions.SectionName));
builder.Services.Configure<SQLiteOptions>(builder.Configuration.GetRequiredSection(SQLiteOptions.SectionName));

builder.Services.AddAuthentication(builder.Configuration.GetRequiredSection(JWTOptions.SectionName).Get<JWTOptions>()!);
builder.Services.AddInfrastructureServices();
builder.Services.AddWebServices();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
    await using (SQLiteDbContext context = scope.ServiceProvider.GetRequiredService<SQLiteDbContext>())
        await context.Database.MigrateAsync();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => options.EnableTryItOutByDefault());
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
