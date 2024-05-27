
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using cafeRecAPI.Data;
using cafeRecAPI.Service;

namespace cafeRecAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<CafeDBContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("CafeDB")));
        builder.Services.AddScoped<ICafeRepo, CafeRepo>();
        builder.Services.AddScoped<ICafeService, CafeService>();
        var app = builder.Build();
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        using (var scope = app.Services.CreateScope())
        {
             var context = scope.ServiceProvider.GetRequiredService<CafeDBContext>();
             context.Database.EnsureCreated();
        }
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
